using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program
{
    public static void Main(string[] args)
    {
        var path = @"C:\Users\Lenovo\Desktop\input5.txt";
        var lines = File.ReadAllLines(path);

        List<(long start, long end)> ranges = new();
        //List<long> ids = new();

        bool readingRanges = true;

        foreach (var raw in lines)
        {
            var line = raw.Trim();
            if (string.IsNullOrWhiteSpace(line))
            {
                readingRanges = false;
                continue;
            }

            if (readingRanges)
            {
                var parts = line.Split('-', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2) throw new FormatException($"Bad range line: '{line}'");
                if (!long.TryParse(parts[0], out var a) || !long.TryParse(parts[1], out var b))
                    throw new FormatException($"Bad numbers in range: '{line}'");

                if (b < a) (a, b) = (b, a);
                ranges.Add((a, b));
            }
            

        }
        var merged = MergeRanges(ranges);

        // محاسبه تعداد کل اعداد داخل بازه‌ها
        long total = merged.Sum(r => r.end - r.start + 1);

        Console.WriteLine($"Total numbers inside ranges = {total}");
    }

    // ============= ادغام بازه‌ها =============
    static List<(long start, long end)> MergeRanges(List<(long start, long end)> ranges)
    {
        var sorted = ranges.OrderBy(r => r.start).ThenBy(r => r.end).ToList();
        var merged = new List<(long start, long end)>();

        foreach (var (s, e) in sorted)
        {
            if (merged.Count == 0)
            {
                merged.Add((s, e));
                continue;
            }

            var last = merged[^1];

            if (s <= last.end + 1) // همپوشانی دارد
                merged[^1] = (last.start, Math.Max(last.end, e));
            else
                merged.Add((s, e));
        }

        return merged;
    }
}


