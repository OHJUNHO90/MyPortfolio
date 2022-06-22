using System;

//solution("abcde")
public string solution(string s)
{
    double startIndex = s.Length % 2.0d;
    return startIndex == 0.0 ? s.Substring((s.Length / 2) - 1, 2) : s.Substring((s.Length / 2), 1);
}