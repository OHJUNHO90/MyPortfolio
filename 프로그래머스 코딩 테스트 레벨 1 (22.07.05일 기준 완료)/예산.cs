using System;
using System.Linq;

//solution(new int[] { 2, 2, 3, 3 }, 10);
public int solution(int[] d, int budget)
{
    int answer = 0;
    int amountSpent = 0;

    var array = from i in d
                orderby i
                select i;

    foreach (int properties in array)
    {
        if (amountSpent + properties <= budget)
        {
            amountSpent += properties;
            answer++;
        }
    }

    return answer;
}