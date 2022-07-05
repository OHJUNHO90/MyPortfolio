using System;
using System.Linq;

public void solution()
{
    String[] s;
    Console.Clear();
    s = Console.ReadLine().Split(' ');

    int a = Int32.Parse(s[0]);
    int b = Int32.Parse(s[1]);

    for (int i = 0; i < b; i++)
    {
        Console.WriteLine(string.Concat(Enumerable.Repeat("*", a)));
    }
}