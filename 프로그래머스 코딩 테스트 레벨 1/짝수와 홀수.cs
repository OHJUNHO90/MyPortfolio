using System;

public string solution(int num)
{
    if (num == 0) return "Even";
    return Math.Abs(num) % 2 == 1 ? "Odd" : "Even";
}