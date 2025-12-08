using System;
using System.IO;

class Program
{
    static void Main()
    {
        string path = @"C:\Users\Lenovo\Desktop\input4.txt";
        string[] grid = File.ReadAllLines(path);

        int rows = grid.Length;
        int cols = grid[0].Length;

        char[,] map = new char[rows, cols];

        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                map[r, c] = grid[r][c];

        int CountNeighbors(int r, int c)
        {
            int count = 0;
            for (int dr = -1; dr <= 1; dr++)
                for (int dc = -1; dc <= 1; dc++)
                {
                    if (dr == 0 && dc == 0)
                        continue;
                    int rr = r + dr;
                    int cc = c + dc;

                    if (rr < 0 || rr >= rows || cc < 0 || cc >= cols)
                        continue;

                    if (map[rr, cc] == '@')
                        count++;
                }
            return count;
        }

        int removedTotal = 0;

        while (true)
        {
            bool removedAnyThisRound = false;

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    if (map[r, c] == '@' && CountNeighbors(r, c) < 4)
                    {
                        map[r, c] = '.';   // حذف رول
                        removedTotal++;
                        removedAnyThisRound = true;
                    }

            if (!removedAnyThisRound)
                break; // دیگر قابل حذف نیست → پایان
        }

        Console.WriteLine(removedTotal);
    }
}
