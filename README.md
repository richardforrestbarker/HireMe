
This application uses Git to write out the words "Hire Me" to the Git Contributions graph on GitHub by making commits on specific dates. 
The number of commits must take into account any commits that have already been made on those dates to ensure the desired appearance on the contributions graph.

## Features

- Creates commits that spell "HIRE ME" on your GitHub contributions graph
- Uses 15 commits per active cell to ensure bright green coloring
- 3-column spacing between "HIRE" and "ME" for better readability
- Support for targeting a specific year (2000-2100)
- Accounts for existing commits to avoid duplicates

## Usage

### Default Mode (Rolling View)
Run without arguments to create the pattern in the current rolling year view (visible in the last 52 weeks):

```bash
HireMe
```

### Target Specific Year
Specify a year to create the pattern for that specific year:

```bash
HireMe 2024
HireMe 2025
```

### Help
Display usage information:

```bash
HireMe --help
```

## How It Works

Since the GitHub contributions graph is a rolling graph showing only the last year of contributions, the words "slide" across the graph over time in default mode. When targeting a specific year, the pattern is positioned to start a few weeks into that year.

This can execute via a GitHub Action on a schedule, or can be run locally with .NET and Git installed.

Tests are included to verify that the graph will appear as expected. 

## Requirements

- .NET 10.0 or higher
- Git installed and configured
- A Git repository initialized in the current directory

## GitHub Contributions

Here is how Git counts contributions: https://docs.github.com/en/account-and-profile/how-tos/contribution-settings/troubleshooting-missing-contributions
