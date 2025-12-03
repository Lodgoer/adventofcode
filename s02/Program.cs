var path = @"C:\Users\Lenovo\Desktop\input2.txt";

if (!File.Exists(path))
{
    Console.WriteLine($"File Does Not Exist: {path}");
    return;
}

var text = File.ReadAllText(path);

var listOfIds = text.Split(',', StringSplitOptions.RemoveEmptyEntries)
    .Select(x => x.Trim())
    .ToList();

long sumOfInvalids = 0;

foreach (var id in listOfIds)
{
    var parts = id.Split('-');
    long start = long.Parse(parts[0]);
    long end = long.Parse(parts[1]);

    for(long i = start; i <= end; i++)
    {
        string number = i.ToString();

        if (number.Length % 2 == 1)
            continue;

        long half = number.Length / 2;
        string firstPart = number.Substring(0, (int)half);
        string secondPart = number.Substring((int)half, (int)half);

        if (firstPart == secondPart)
            sumOfInvalids += i;
    }
}

Console.WriteLine(sumOfInvalids);