using CelebApi.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace CelebApi.Repositories;

public class CelebRepository
{
    private readonly string _dataFile;
    private readonly string _originalFile;
    private readonly ReaderWriterLockSlim _lock = new();

    public CelebRepository(IConfiguration configuration)
    {
        var dataFile = configuration["JsonData:DataFilePath"];
        var originalFile = configuration["JsonData:OriginalFilePath"];

        if (string.IsNullOrEmpty(dataFile) || string.IsNullOrEmpty(originalFile))
        {
            Console.WriteLine("[Repository: Init]  Missing file paths in configuration.");
            throw new InvalidOperationException("Missing file paths in configuration.");
        }

        _dataFile = dataFile!;
        _originalFile = originalFile!;
    }

    public List<Celeb> GetAll()
    {
        _lock.EnterReadLock();
        try
        {
            var json = File.ReadAllText(_dataFile);
            return JsonConvert.DeserializeObject<List<Celeb>>(json) ?? new();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"[Repository: GetAll] ❌ File not found: {_dataFile}");
            return new List<Celeb>();
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"[Repository: GetAll] ❌ Access denied: {ex.Message}");
            return new List<Celeb>();
        }
        catch (IOException ex)
        {
            Console.WriteLine($"[Repository: GetAll] ❌ IO error: {ex.Message}");
            return new List<Celeb>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Repository: GetAll] ❌ Unexpected error: {ex.Message}");
            return new List<Celeb>();
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public void SaveAll(List<Celeb> celebs)
    {
        _lock.EnterWriteLock();
        try
        {
            var json = JsonConvert.SerializeObject(celebs, Formatting.Indented);
            File.WriteAllText(_dataFile, json);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"[Repository: SaveAll] ❌ Access denied: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"[Repository: SaveAll] ❌ IO error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Repository: SaveAll] ❌ Unexpected error: {ex.Message}");
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public void Reset()
    {
        _lock.EnterWriteLock();
        try
        {
            File.Copy(_originalFile, _dataFile, overwrite: true);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"[Repository: Reset] ❌ Original file not found: {_originalFile}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"[Repository: Reset] ❌ Access denied: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"[Repository: Reset] ❌ IO error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Repository: Reset] ❌ Unexpected error: {ex.Message}");
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }
}
