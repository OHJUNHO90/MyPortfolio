using System;


// 공백문자 32
// 대문자 65 ~ 90
// 소문자 97 ~ 122

//solution("Z", 25);
public string solution(string s, int n)
{
    string answer = string.Empty;
    char[] array = s.ToCharArray();

    foreach (char c in array)
    {
        if (c.Equals((char)32))
        {
            answer += (char)32;
            continue;
        }

        char ascii = (char)(c + n);
        if (c <= 90 && 90 < ascii) ascii = (char)('A' + (ascii % 90) - 1);
        else if (c <= 122 && 122 < ascii) ascii = (char)('a' + (ascii % 122) - 1);
        answer += ascii;
    }

    return answer;
}