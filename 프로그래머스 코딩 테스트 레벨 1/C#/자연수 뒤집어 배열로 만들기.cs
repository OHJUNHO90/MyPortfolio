using System;
using System.Collections.Generic;
using System.Linq;


public int[] solution(long n)
{
    IEnumerable<char> array = n.ToString().ToCharArray().Reverse();
    int[] answer = new int[array.Count()];
    int index = 0;
    foreach (var item in array)
    {
        answer[index] = int.Parse(item.ToString());
        index++;
    }

    return answer;
}