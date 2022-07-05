using System;
using System.Linq;

//solution(1, 7);
public string solution(int a, int b)
{
    string[] months = new string[] { "FRI", "SAT", "SUN", "MON", "TUE", "WED", "THU" };
    int[] thereAre31 = new int[] { 1, 3, 5, 7, 8, 10, 12 };
    int[] thereAre30 = new int[] { 4, 6, 9, 11 };

    int temp = b;
    for (int i = 1; i < a; i++)
    {
        if (thereAre31.Contains(i)) temp += 31;
        else if (thereAre30.Contains(i)) temp += 30;
        else temp += 29;
    }

    int index = temp % 7;
    index = index == 0 ? 6 : index - 1;
    return months[index];
}