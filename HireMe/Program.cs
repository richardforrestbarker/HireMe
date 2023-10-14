using System.Diagnostics;
using System.Globalization;

namespace HireMe;

internal class Program
{
    static async Task<int> Main(string[] args)
    {
        try
        {
            Console.WriteLine("HireMe - GitHub Contributions Graph Writer");
            Console.WriteLine("==========================================");

            int? targetYear = null;

            // Parse command-line arguments
            if (args.Length > 0)
            {
                if (args[0] == "--help" || args[0] == "-h")
                {
                    PrintHelp();
                    return 0;
                }

                if (int.TryParse(args[0], out int year))
                {
                    if (year >= 2000 && year <= 2100)
                    {
                        targetYear = year;
                    }
                    else
                    {
                        Console.Error.WriteLine("Error: Year must be between 2000 and 2100");
                        return 1;
                    }
                }
                else
                {
                    Console.Error.WriteLine($"Error: Invalid year '{args[0]}'");
                    PrintHelp();
                    return 1;
                }
            }

            var generator = new ContributionGraphGenerator();
            await generator.GenerateCommitsAsync(targetYear);

            Console.WriteLine("\nCommits created successfully!");
            Console.WriteLine("Push to GitHub to see the changes on your contributions graph.");

            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return 1;
        }
    }

    static void PrintHelp()
    {
        Console.WriteLine("\nUsage:");
        Console.WriteLine("  HireMe [year]");
        Console.WriteLine();
        Console.WriteLine("Arguments:");
        Console.WriteLine("  year      Optional. The year to create the 'HIRE ME' pattern (2000-2100).");
        Console.WriteLine("            If not specified, uses the current rolling year view.");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  HireMe           # Create pattern for current rolling view");
        Console.WriteLine("  HireMe 2024      # Create pattern for year 2024");
        Console.WriteLine("  HireMe 2025      # Create pattern for year 2025");
        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("  --help, -h       Show this help message");
    }
}
