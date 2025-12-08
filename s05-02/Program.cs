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
        List<long> ids = new();

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
            else
            {
                if (!long.TryParse(line, out var id)) throw new FormatException($"Bad id line: '{line}'");
                ids.Add(id);
            }
        }

        var merged = MergeRanges(ranges);
        long freshCount = 0;

        foreach (var id in ids)
        {
            if (IsFresh(id, merged))
                freshCount++;
        }

        Console.WriteLine($"Fresh Count = {freshCount}");
    }

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

            var last = merged[merged.Count - 1];
            if (s <= last.end + 1) // overlap or adjacent -> merge
            {
                merged[merged.Count - 1] = (last.start, Math.Max(last.end, e));
            }
            else
            {
                merged.Add((s, e));
            }
        }

        return merged;
    }

    static bool IsFresh(long id, List<(long start, long end)> mergedRanges)
    {
        // binary search over merged intervals
        int lo = 0, hi = mergedRanges.Count - 1;
        while (lo <= hi)
        {
            int mid = (lo + hi) >> 1;
            var (s, e) = mergedRanges[mid];
            if (id < s) hi = mid - 1;
            else if (id > e) lo = mid + 1;
            else return true; // id in [s, e]
        }
        return false;
    }
}
