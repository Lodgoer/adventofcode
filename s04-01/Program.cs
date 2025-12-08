var path = @"C:\Users\Lenovo\Desktop\input4.txt";

string[] grid = File.ReadAllLines(path);

int cols = grid[0].Length;
int rows = grid.Length;

char[,] output = new char[rows, cols];

for (int row = 0; row < rows; row++)
    for(int col = 0; col < cols; col++)
        output[row, col] = grid[row][col];


int CountNeighbors(int row, int col)
{
    int count = 0;
    for(int dr = -1; dr <= 1; dr++)
        for(int dc = -1; dc <= 1; dc++)
        {
            if (dr == 0 && dc == 0)
                continue;
            int newRow = row + dr;
            int newCol = col + dc;
            if (newRow < 0 || newRow >= rows || newCol < 0 || newCol >= cols)
                continue;
            if (output[newRow, newCol] == '@')
                count++;
        }
    return count;
}

int xCount = 0;

for (int r = 0; r < rows; r++)
    for (int c = 0; c < cols; c++)
        if (grid[r][c] == '@' && CountNeighbors(r, c) < 4)
            xCount++;

Console.WriteLine(xCount);