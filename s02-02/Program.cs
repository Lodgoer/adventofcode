var path = @"C:\Users\Lenovo\Desktop\inputt2.txt";

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

    for (long i = start; i <= end; i++)
    {
        string number = i.ToString();

        var ok = IsRepeatedEfficient(number);
        if (ok)
            sumOfInvalids += i;
    }
}

Console.WriteLine(sumOfInvalids);


bool IsRepeatedEfficient(string s)
{
    int L = s.Length;
    for (int chunk = 1; chunk <= L / 2; chunk++)
    {
        if (L % chunk != 0)
            continue;

        string pattern = s.Substring(0, chunk);
        bool ok = true;
        for (int i = 0; i < L; i += chunk)
        {
            if (!s.Substring(i, chunk).Equals(pattern))
            {
                ok = false;
                break;
            }
        }
        if (ok) return true;
    }
    return false;
}
