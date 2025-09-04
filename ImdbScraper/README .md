# ImdbScraper

This is a C# console application that scrapes IMDb lists and extracts celebrity data.

## Features
- Extracts name, profile URL, birthdate, gender, and roles
- Uses parallel HTTP requests with configurable throttling
- Saves result to JSON files

## Setup & Run
1. Open in Visual Studio.
2. Configure output paths in `Program.cs` or `appsettings.json`.
3. Run the scraper:
   ```bash
   dotnet run --project ImdbScraper
   ```

## Configuration
 ```App Configuration``` acts as a configuration class and holds key values that have a direct impact upon the velocity of the app.
