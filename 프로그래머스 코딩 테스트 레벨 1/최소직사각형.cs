using System;
using System.Collections.Generic;
using System.Linq;

//Console.WriteLine(solution(new int[,] { { 14, 4 }, { 19, 6 }, { 6, 16 }, { 18, 7 }, { 7, 11 } }));
public int solution(int[,] sizes)
{
    List<int[]> list = new List<int[]>();
    for (int i = 0; i < sizes.GetLength(0); i++)
    {
        if (sizes[i, 0] < sizes[i, 1])
        {
            if (sizes[i, 0] < sizes[i, 1])
            {
                int big = sizes[i, 1];
                int small = sizes[i, 0];
                sizes[i, 0] = big;
                sizes[i, 1] = small;
            }
        }

        list.Add(new int[] { sizes[i, 0], sizes[i, 1] });
    }

    return list.Max(t => t[0]) * list.Max(t => t[1]);
}