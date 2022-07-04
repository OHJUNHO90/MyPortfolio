using System;

////////////////////////////////////////////////////////////
/* 프로그래머스 참고 답안, */
//public int[] solution(int n, int m)
//{
//    int _gcd = gcd(n, m);
//    int[] answer = new int[] { _gcd, n * m / _gcd };

//    return answer;
//}

//int gcd(int a, int b)
//{
//    return (a % b == 0 ? b : gcd(b, a % b));
//}
////////////////////////////////////////////////////////////

public int[] solution(int n, int m)
{
    int[] answer = new int[2];

    for (int i = n; 1 <= i; i--)
    {
        if (n % i == 0 && m % i == 0)
        {
            answer[0] = i;
            break;
        }
    }

    int index = 1;
    while (true)
    {
        int temp = m * index;
        if ((temp % n) == 0)
        {
            answer[1] = temp;
            break;
        }
        index++;
    }

    return answer;
}