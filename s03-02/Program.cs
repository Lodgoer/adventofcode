using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

var path = @"C:\Users\Lenovo\Desktop\input3.txt";

if (!File.Exists(path))
{
    Console.WriteLine($"File does not exist: {path}");
    return;
}

const int K = 12;
var lines = File.ReadAllLines(path);
var results = new List<long>();
int lineNo = 0;

foreach (var raw in lines)
{
    lineNo++;
    var s = raw.Trim();

    var digitsChars = new List<char>();
    foreach (char c in s)
        if (char.IsDigit(c))
            digitsChars.Add(c);

    if (digitsChars.Count < K)
    {
        Console.WriteLine($"Line {lineNo}: not enough digits (have {digitsChars.Count}), skipping.");
        continue;
    }

    int n = digitsChars.Count;
    int toRemove = n - K;
    var stack = new List<char>();

    foreach (char c in digitsChars)
    {
        while (toRemove > 0 && stack.Count > 0 && stack[^1] < c)
        {
            stack.RemoveAt(stack.Count - 1);
            toRemove--;
        }
        stack.Add(c);
    }

    while (toRemove > 0)
    {
        stack.RemoveAt(stack.Count - 1);
        toRemove--;
    }

    var chosen = new string(stack.Take(K).ToArray());

    if (!long.TryParse(chosen, out long value))
    {
        Console.WriteLine($"Line {lineNo}: parse error for '{chosen}', skipping.");
        continue;
    }

    results.Add(value);
    // optional: Console.WriteLine($"Line {lineNo}: chosen = {chosen} -> {value}");
}

long total = results.Sum();
Console.WriteLine($"Total output joltage = {total}");
