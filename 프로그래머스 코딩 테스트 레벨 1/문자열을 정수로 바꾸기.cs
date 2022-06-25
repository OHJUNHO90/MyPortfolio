using System;

//solution("-1234");
public int solution(string s)
{
    int answer = 0;
    int.TryParse(s, out answer);
    return answer;
}