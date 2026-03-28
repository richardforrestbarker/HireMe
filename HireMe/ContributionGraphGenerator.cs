using System.Diagnostics;
using System.Globalization;

namespace HireMe;

public class ContributionGraphGenerator
{
    private readonly PatternProvider _patternProvider;
    private readonly GitCommitService _gitService;

    public ContributionGraphGenerator()
    {
        _patternProvider = new PatternProvider();
        _gitService = new GitCommitService();
    }

    public async Task GenerateCommitsAsync()
    {
        // Get the pattern for "HIRE ME"
        var pattern = _patternProvider.GetHireMePattern();

        // Calculate dates where commits should be made
        var targetDates = CalculateTargetDates(pattern);

        Console.WriteLine($"Pattern requires {targetDates.Count} dates with commits");

        // Get existing commit counts for these dates
        var existingCounts = await _gitService.GetCommitCountsForDatesAsync(targetDates);

        // Create commits to fill in the pattern
        int commitsCreated = 0;
        foreach (var date in targetDates.OrderBy(d => d))
        {
            int existingCount = existingCounts.GetValueOrDefault(date, 0);
            int targetCount = GetTargetCommitCount(date, pattern);
            int commitsNeeded = Math.Max(0, targetCount - existingCount);

            if (commitsNeeded > 0)
            {
                Console.WriteLine($"Creating {commitsNeeded} commit(s) for {date:yyyy-MM-dd}");
                await _gitService.CreateCommitsForDateAsync(date, commitsNeeded);
                commitsCreated += commitsNeeded;
            }
        }

        Console.WriteLine($"\nTotal commits created: {commitsCreated}");
    }

    private List<DateTime> CalculateTargetDates(bool[,] pattern)
    {
        var dates = new List<DateTime>();
        var today = DateTime.UtcNow.Date;

        // Find the most recent Sunday (GitHub week starts on Sunday)
        var currentSunday = today;
        while (currentSunday.DayOfWeek != DayOfWeek.Sunday)
        {
            currentSunday = currentSunday.AddDays(-1);
        }

        // GitHub shows 52 weeks (364 days)
        // We want to position the pattern in a visible area
        // Start from 8 weeks ago so the pattern is clearly visible
        var startSunday = currentSunday.AddDays(-7 * 44); // 44 weeks back from current Sunday

        int rows = pattern.GetLength(0); // 7 days
        int cols = pattern.GetLength(1); // width of pattern

        for (int col = 0; col < cols; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                if (pattern[row, col])
                {
                    // Calculate the date for this cell
                    var date = startSunday.AddDays(col * 7 + row);
                    dates.Add(date);
                }
            }
        }

        return dates;
    }

    private int GetTargetCommitCount(DateTime date, bool[,] pattern)
    {
        // For active cells in the pattern, we want many commits to ensure bright green
        // GitHub shows brightest green at 10+ commits per day
        return 15;
    }
}
