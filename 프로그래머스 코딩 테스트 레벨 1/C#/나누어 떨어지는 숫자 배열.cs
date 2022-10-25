using System;
using System.Collections.Generic;
using System.Linq;

//solution(new int[] { 2, 36, 1, 3 }, 1)
public int[] solution(int[] arr, int divisor)
{
    var linq = from element in arr
               where (element % divisor) == 0
               select element;

    List<int> answer = linq.ToList();
    if (answer.Count == 0)
        answer.Add(-1);

    return answer.OrderBy(e => e).ToArray();
}