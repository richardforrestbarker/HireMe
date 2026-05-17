This application uses Git to write out the words "Hire Me" to the Git Contributions graph on GitHub by making commits on specific dates. The number of commits must take into account any commits that have already been made on those dates to ensure the desired appearance on the contributions graph. Since the graph is a rolling graph showing only the last year of contributions, the words "slide" across the graph over time, and then repeat. This will execute via a GitHub Action on a schedule, but can also be run locally with Python 3 and Git installed.

Tests are also included to verify that graph will appear as expected.

Here is how Git counts contributions: https://docs.github.com/en/account-and-profile/how-tos/contribution-settings/troubleshooting-missing-contributions
