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

            var generator = new ContributionGraphGenerator();
            await generator.GenerateCommitsAsync();

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
}
