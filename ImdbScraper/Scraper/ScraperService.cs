using HtmlAgilityPack;
using ImdbScraper.Model;
using ImdbScraper.Model.Enum;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using ImdbScraper.Configuration;

namespace ImdbScraper.Scraper;

public class ScraperService
{
    private readonly string _listUrl;
    private readonly Uri _baseUri;
    private readonly HttpClient _httpClient;

    public ScraperService(string listUrl)
    {
        if (string.IsNullOrWhiteSpace(listUrl))
            throw new ArgumentException("IMDb list URL must not be null or empty.", nameof(listUrl));

        _listUrl = listUrl;
        _baseUri = new Uri(listUrl);

        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
        _httpClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.9");
    }

    public async Task RunAsync(string outputFile, string originalFile)
    {
        Console.WriteLine("Downloading IMDb list page...");

        string html;
        try
        {
            html = await _httpClient.GetStringAsync(_listUrl);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load list page: {ex.Message}");
            return;
        }

        var celebSummaries = ExtractCelebsFromJsonLd(html);

        if (celebSummaries.Count == 0)
        {
            Console.WriteLine("No celebrity items found.");
            return;
        }

        Console.WriteLine($"Found {celebSummaries.Count} celebrity items.\n");

        var celebs = new List<Celeb>();
        var semaphore = new SemaphoreSlim(AppConfiguration.MaxParallelism); // Control parallelism
        var tasks = new List<Task>();

        foreach (var (name, profileUrl) in celebSummaries)
        {
            tasks.Add(Task.Run(async () =>
            {
                await semaphore.WaitAsync();
                try
                {
                    var (birthDate, gender, roles, image) = await ParseDetailsAsync(profileUrl);

                    var celeb = new Celeb
                    {
                        Name = name,
                        ProfileUrl = profileUrl,
                        BirthDate = birthDate,
                        Gender = gender,
                        Roles = roles,
                        Image = image
                    };

                    lock (celebs)
                    {
                        celebs.Add(celeb);
                    }

                    Console.WriteLine($"{name} added");
                    //await Task.Delay(Random.Shared.Next(AppConfiguration.MinDelayMs, AppConfiguration.MaxDelayMs));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing {name}: {ex.Message}");
                }
                finally
                {
                    semaphore.Release();
                }
            }));
        }

        await Task.WhenAll(tasks);

        var json = JsonSerializer.Serialize(celebs, new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        });

        // Save the latest scrape
        await File.WriteAllTextAsync(outputFile, json);
        Console.WriteLine($"\nScraping complete. {celebs.Count} celebs saved to {outputFile}");

        // Save original
        if (File.Exists(originalFile))
        {
            await File.WriteAllTextAsync(originalFile, json);
            Console.WriteLine($"Original data updated at {originalFile}");
        }
        else
        {
            File.Copy(outputFile, originalFile);
            Console.WriteLine($"Original data also saved to {originalFile}");
        }

    }

    private static List<(string name, string url)> ExtractCelebsFromJsonLd(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var jsonNode = doc.DocumentNode.SelectSingleNode("//script[@type='application/ld+json']");
        if (jsonNode == null)
        {
            Console.WriteLine("No JSON-LD found.");
            return [];
        }

        var jsonText = jsonNode.InnerText.Trim();
        var root = JsonNode.Parse(jsonText);
        var items = root?["itemListElement"]?.AsArray();

        if (items == null)
        {
            Console.WriteLine("No items in JSON-LD.");
            return [];
        }

        return [.. items
            .Select(entry => (
                name: entry?["item"]?["name"]?.ToString() ?? "Unknown",
                url: entry?["item"]?["url"]?.ToString() ?? ""
            ))
            .Where(t => !string.IsNullOrWhiteSpace(t.url))];
    }

    private async Task<(string? birthDate, string? gender, List<CelebRole> roles, string? image)> ParseDetailsAsync(string profileUrl)
    {
        string html;

        try
        {
            html = await _httpClient.GetStringAsync(profileUrl);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ParseDetailsAsync] Failed to load profile page: {ex.Message}");
            return (null, null, [], null);
        }

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        // Image from og:image
        var imageNode = doc.DocumentNode.SelectSingleNode("//meta[@property='og:image']");
        string? imageUrl = imageNode?.GetAttributeValue("content", null);

        // Parse birth date from div[data-testid="birth-and-death-birthdate"]
        string? birthDate = null;
        var birthDateDiv = doc.DocumentNode.SelectSingleNode("//div[@data-testid='birth-and-death-birthdate']");
        if (birthDateDiv != null)
        {
            // Remove "Born" and trim whitespace
            var dateText = birthDateDiv.InnerText.Replace("Born", "", StringComparison.OrdinalIgnoreCase).Trim();
            // Try to parse to ISO format
            if (DateTime.TryParse(dateText, out var dt))
                birthDate = dt.ToString("dd-MM-yyyy");
            else
                birthDate = dateText;
        }

        // Parse roles
        var roles = new List<CelebRole>();
        var knownForText = doc.DocumentNode.InnerText;
        if (knownForText.Contains("Actor")) roles.Add(CelebRole.Actor);
        if (knownForText.Contains("Actress")) roles.Add(CelebRole.Actor); // Unified
        if (knownForText.Contains("Producer")) roles.Add(CelebRole.Producer);
        if (knownForText.Contains("Director")) roles.Add(CelebRole.Director);

        // Gender
        string gender = "Unknown";
        if (knownForText.Contains("Actress")) gender = "Female";
        else if (knownForText.Contains("Actor")) gender = "Male";

        return (birthDate, gender, roles.Distinct().ToList(), imageUrl);
    }
}