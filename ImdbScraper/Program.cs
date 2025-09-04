using ImdbScraper.Configuration;
using ImdbScraper.Scraper;

string imdbListUrl = AppConfiguration.ListUrl;

// Step 1: Get CelebApi/Data directory
var solutionRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\"));
var dataDir = Path.Combine(solutionRoot, AppConfiguration.DataDir);
Directory.CreateDirectory(dataDir); // Ensure directory exists

// Step 2: Define file paths
var outputFile = Path.Combine(dataDir, AppConfiguration.DataFile);
var originalFile = Path.Combine(dataDir, AppConfiguration.OriginalFile);

// Step 3: Run the scraper
var scraper = new ScraperService(imdbListUrl);
await scraper.RunAsync(outputFile, originalFile);