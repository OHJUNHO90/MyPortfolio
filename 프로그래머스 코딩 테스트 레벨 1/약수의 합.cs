using System;

//
public int solution(int n)
{
    int answer = 0;
    int divided = n / 2;

    for (int i = 1; i <= divided; i++)
    {
        if (n % i == 0) answer += i;
    }

    return answer + n;
}