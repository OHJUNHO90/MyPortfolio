using System;
using System.Collections.Generic;
using System.Linq;

public int[] solution(int[] arr)
{
    if (arr.Length <= 1) return new int[] { -1 };
    List<int> list = new List<int>(arr);
    var target = list.Select((item, idx) => new Tuple<int, int>(item, idx)).Min();
    list.RemoveAt(target.Item2);
    return list.ToArray();
}