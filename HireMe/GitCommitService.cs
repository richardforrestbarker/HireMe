using System.Diagnostics;
using System.Text;

namespace HireMe;

public class GitCommitService
{
    public async Task<Dictionary<DateTime, int>> GetCommitCountsForDatesAsync(List<DateTime> dates)
    {
        var counts = new Dictionary<DateTime, int>();

        if (dates.Count == 0)
        {
            return counts;
        }

        try
        {
            // Get git log for the date range
            var minDate = dates.Min().ToString("yyyy-MM-dd");
            var maxDate = dates.Max().AddDays(1).ToString("yyyy-MM-dd");

            var output = await RunGitCommandAsync($"log --all --format=%ci --since=\"{minDate}\" --until=\"{maxDate}\"");

            if (string.IsNullOrWhiteSpace(output))
            {
                return counts;
            }

            // Parse the output to count commits per date
            var lines = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (DateTime.TryParse(line.Trim().Split(' ')[0], out var commitDate))
                {
                    var date = commitDate.Date;
                    counts[date] = counts.GetValueOrDefault(date, 0) + 1;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Warning: Could not get existing commit counts: {ex.Message}");
        }

        return counts;
    }

    public async Task CreateCommitsForDateAsync(DateTime date, int count)
    {
        var dateStr = date.ToString("yyyy-MM-dd");

        for (int i = 0; i < count; i++)
        {
            // Create a unique commit message
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var message = $"HireMe contribution for {dateStr} ({i + 1}/{count}) - {timestamp}";

            // Create an empty commit with the specific date
            var commitDate = $"{dateStr}T12:00:00";

            var result = await RunGitCommandAsync(
                $"commit --allow-empty -m \"{message}\" --date=\"{commitDate}\""
            );

            if (result.Contains("error", StringComparison.OrdinalIgnoreCase) ||
                result.Contains("fatal", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception($"Git commit failed: {result}");
            }
        }
    }

    private async Task<string> RunGitCommandAsync(string arguments)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "git",
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = startInfo };

        var outputBuilder = new StringBuilder();
        var errorBuilder = new StringBuilder();

        process.OutputDataReceived += (sender, e) =>
        {
            if (e.Data != null)
            {
                outputBuilder.AppendLine(e.Data);
            }
        };

        process.ErrorDataReceived += (sender, e) =>
        {
            if (e.Data != null)
            {
                errorBuilder.AppendLine(e.Data);
            }
        };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        await process.WaitForExitAsync();

        var error = errorBuilder.ToString().Trim();
        if (!string.IsNullOrEmpty(error) && process.ExitCode != 0)
        {
            throw new Exception($"Git command failed: {error}");
        }

        return outputBuilder.ToString().Trim();
    }
}
