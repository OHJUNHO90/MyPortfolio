
//solution(new int[] { 1, 3, 2, 4, 2 });
public int[] solution(int[] answers)
{
    //[순차적]1번 수포자가 찍는 방식: 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, ...
    //[0, -1, 0, +1, 0, +2 , 0, +3]2번 수포자가 찍는 방식: 2, 1, 2, 3, 2, 4, 2, 5, 2, 1, 2, 3, 2, 4, 2, 5, ...
    //[0, 0, -2, -2, -1, -1 ,+2, +2, +3, +3]3번 수포자가 찍는 방식: 3, 3, 1, 1, 2, 2, 4, 4, 5, 5, 3, 3, 1, 1, 2, 2, 4, 4, 5, 5, ...

    int[] answer = new int[3];
    int[] case1 = new int[] { 1, 2, 3, 4, 5 };
    int[] case2 = new int[] { 2, 1, 2, 3, 2, 4, 2, 5 };
    int[] case3 = new int[] { 3, 3, 1, 1, 2, 2, 4, 4, 5, 5 };

    for (int i = 0; i < answers.Length; i++)
    {
        if (answers[i].Equals(case1[i % 5]))  answer[0]++;
        if (answers[i].Equals(case2[i % 8]))  answer[1]++;
        if (answers[i].Equals(case3[i % 10])) answer[2]++;
    }

    int max = answer.Max();
    List<int> result = new List<int>();

    for (int i = 0; i < answer.Length; i++)
    {
        if (max.Equals(answer[i]))
        {
            result.Add(i + 1);
        }
    }

    return result.ToArray();
}