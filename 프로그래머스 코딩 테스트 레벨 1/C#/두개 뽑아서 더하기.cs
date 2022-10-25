using System.Collections.Generic;
using System.Linq;

//solution(new int[] { 5, 0, 2, 7 });
public int[] solution(int[] numbers)
{
    int[] answer = new int[] { };
    List<int> array = new List<int>();
    int currentIndex = 0;

    for (int i = currentIndex + 1; i < numbers.Length; i++)
    {
        int temp = numbers[currentIndex] + numbers[i];
        array.Add(temp);

        if (i + 1 == numbers.Length)
        {
            currentIndex++;
            i = currentIndex;
            continue;
        }
    }

    answer = array.OrderBy(t => t).Distinct().ToArray();
    return answer;
}