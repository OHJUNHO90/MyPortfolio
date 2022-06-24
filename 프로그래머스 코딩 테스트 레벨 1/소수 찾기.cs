using System;
using System.Linq;

/*에라토스테네스의 체를 참고하여 구현*/
/*https://loosie.tistory.com/267*/

// solution(5);
public int solution(int n)
{
    bool[] isPrime = new bool[n + 1];
    Array.Fill(isPrime, true);
    isPrime[0] = false;
    isPrime[1] = false;

    for (int i = 2; i * i <= n; i++)
    {
        if (isPrime[i])
        {
            for (int j = i * i; j <= n; j += i)
                isPrime[j] = false;
        }
    }

    return isPrime.Select(t => t).Where(t => t).Count();
}