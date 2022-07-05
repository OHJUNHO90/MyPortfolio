using System;

public int solution(long num)
{
    if (num == 1) 
        return 0;

    int answer = 0;
    while (true)
    {
        if (num % 2 == 0) num = num / 2;
        else num = num * 3 + 1;
        answer++;

        if (500 <= answer) return -1;
        else if (num == 1) break;
    }

    return answer;
}