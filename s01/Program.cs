using System;
using System.IO;
using System.Collections.Generic;

int start = 50;
long password = 0;
var path = @"C:\Users\Lenovo\Desktop\input.txt";

if (!File.Exists(path))
{
    Console.WriteLine($"فایل پیدا نشد: {path}");
    return;
}

var lines = File.ReadAllLines(path);
var directions = new List<Direction>();

foreach (var line in lines)
{
    if (string.IsNullOrWhiteSpace(line)) continue;
    char turn = line[0];
    int steps = int.Parse(line[1..]);
    directions.Add(new Direction(turn, steps));
}

foreach (var d in directions)
{
    int delta = d.Turn == 'R' ? 1 : -1;

    // پیدا کردن کوچک‌ترین t>0 که start + delta * t ≡ 0 (mod 100)
    int t0;
    if (delta == 1) // R
        t0 = (100 - (start % 100)) % 100;
    else // L
        t0 = start % 100;

    if (t0 == 0) t0 = 100; // اگر t0==0 یعنی موقع شروع روی صفریم؛ اولین عبور بعد از 100 کلیک خواهد بود

    int hits = 0;
    if (t0 <= d.Steps)
    {
        hits = 1 + (d.Steps - t0) / 100;
    }

    password += hits;

    // آپدیت موقعیت نهایی برای دور بعدی
    start = (start + delta * d.Steps) % 100;
    if (start < 0) start += 100;
}

Console.WriteLine(password);

// -------------------------
public class Direction
{
    public char Turn { get; set; }
    public int Steps { get; set; }
    public Direction(char turn, int steps) { Turn = turn; Steps = steps; }
}

