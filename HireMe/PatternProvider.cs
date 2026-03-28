namespace HireMe;

public class PatternProvider
{
    // GitHub contribution graph has 7 rows (days of the week: Sun-Sat)
    // Each letter will be 5 columns wide with 1 column spacing between letters
    // 3 column space between words
    // H I R E   M E
    // 5+5+5+5+3+5+5 = 33 columns + 6 letter spaces = 39 columns total

    public bool[,] GetHireMePattern()
    {
        // 7 rows (days) x 39 columns (weeks)
        var pattern = new bool[7, 39];

        // H (columns 0-4)
        DrawH(pattern, 0);

        // I (columns 6-10)
        DrawI(pattern, 6);

        // R (columns 12-16)
        DrawR(pattern, 12);

        // E (columns 18-22)
        DrawE(pattern, 18);

        // 3 column space (columns 23, 24, 25)

        // M (columns 26-30)
        DrawM(pattern, 26);

        // E (columns 32-36)
        DrawE(pattern, 32);

        return pattern;
    }

    private void DrawH(bool[,] pattern, int startCol)
    {
        // H shape: vertical bars on sides, horizontal bar in middle
        for (int row = 0; row < 7; row++)
        {
            pattern[row, startCol] = true;     // Left vertical
            pattern[row, startCol + 4] = true; // Right vertical
            if (row == 3) // Middle horizontal
            {
                pattern[row, startCol + 1] = true;
                pattern[row, startCol + 2] = true;
                pattern[row, startCol + 3] = true;
            }
        }
    }

    private void DrawI(bool[,] pattern, int startCol)
    {
        // I shape: horizontal bars top and bottom, vertical bar in middle
        for (int row = 0; row < 7; row++)
        {
            if (row == 0 || row == 6) // Top and bottom horizontals
            {
                for (int col = 0; col < 5; col++)
                {
                    pattern[row, startCol + col] = true;
                }
            }
            else // Middle vertical
            {
                pattern[row, startCol + 2] = true;
            }
        }
    }

    private void DrawR(bool[,] pattern, int startCol)
    {
        // R shape: vertical bar on left, top horizontal, middle horizontal, diagonal leg
        for (int row = 0; row < 7; row++)
        {
            pattern[row, startCol] = true; // Left vertical

            if (row == 0) // Top horizontal
            {
                pattern[row, startCol + 1] = true;
                pattern[row, startCol + 2] = true;
                pattern[row, startCol + 3] = true;
            }
            else if (row == 1 || row == 2) // Top right corner
            {
                pattern[row, startCol + 4] = true;
            }
            else if (row == 3) // Middle horizontal
            {
                pattern[row, startCol + 1] = true;
                pattern[row, startCol + 2] = true;
                pattern[row, startCol + 3] = true;
            }
            else if (row == 4)
            {
                pattern[row, startCol + 3] = true;
            }
            else if (row == 5)
            {
                pattern[row, startCol + 4] = true;
            }
            else if (row == 6)
            {
                pattern[row, startCol + 4] = true;
            }
        }
    }

    private void DrawE(bool[,] pattern, int startCol)
    {
        // E shape: vertical bar on left, horizontals at top, middle, bottom
        for (int row = 0; row < 7; row++)
        {
            pattern[row, startCol] = true; // Left vertical

            if (row == 0 || row == 3 || row == 6) // Horizontals
            {
                pattern[row, startCol + 1] = true;
                pattern[row, startCol + 2] = true;
                pattern[row, startCol + 3] = true;
                pattern[row, startCol + 4] = true;
            }
        }
    }

    private void DrawM(bool[,] pattern, int startCol)
    {
        // M shape: two vertical bars with diagonal connections at top
        for (int row = 0; row < 7; row++)
        {
            pattern[row, startCol] = true;     // Left vertical
            pattern[row, startCol + 4] = true; // Right vertical

            if (row == 1)
            {
                pattern[row, startCol + 1] = true;
                pattern[row, startCol + 3] = true;
            }
            else if (row == 2)
            {
                pattern[row, startCol + 2] = true;
            }
        }
    }
}
