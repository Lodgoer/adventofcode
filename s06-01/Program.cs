var path = @"C:\Users\Lenovo\Desktop\input6.txt";

string[] lines = File.ReadAllLines(path);

int width  = lines.Max(l => l.Length);
lines = lines.Select(l => l.PadRight(width)).ToArray();

