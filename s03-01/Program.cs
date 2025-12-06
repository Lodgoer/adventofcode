var path = @"C:\Users\Lenovo\Desktop\input3.txt"; //get path

var lines = File.ReadAllLines(path); //read each line
var ids = new List<int>(); //cretae a list of id from each line(two digit number)
int lineNo = 0; //two digit number of each line

foreach (var line in lines)
{
    lineNo++;

    var digits = new List<int>();
    foreach (char c in line)
    {
        digits.Add(c - '0');
    }

    if (digits.Count < 2) //??
    {
        continue;
    }

    int maxRight = digits[digits.Count - 1];
    int bestTwoDigit = -1;

    for (int i = digits.Count - 2; i >= 0; i--)
    {
        int candidate = digits[i] * 10 + maxRight;
        if (candidate > bestTwoDigit) bestTwoDigit = candidate;
        if (digits[i] > maxRight) maxRight = digits[i];
    }

    ids.Add(bestTwoDigit);
}

int totall = ids.Sum();
Console.WriteLine($"Total output joltage = {totall}");
