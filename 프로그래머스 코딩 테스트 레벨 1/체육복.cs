
//solution(7, new int[] { 4,3,2 }, new int[] { 1,6,5 });
int solution(int n, int[] lost, int[] reserve)
{
    // n 전체 학생수
    // lost 도난당한 학생들 번호
    // 여벌의 체육복을 가져온 학생들 번호
    // -규칙 여벌의 체육복을 가져온 학생의 번호 +1 또는 -1만 체육복을 빌려줄수 있음
    // 여벌의 체육복을 가져온 학생이 체육복을 도난당했을수도 있음, 만약 도난당했다면 체육복을 빌려줄수 없음
    int answer = n;
    int[] temp = reserve.Except(lost).ToArray();
    lost = lost.Except(reserve).OrderBy(t => t).ToArray();
    List<int> list = (reserve = temp).ToList();

    for (int i = 0; i < lost.Length; i++)
    {
        if (list.Contains(lost[i] - 1))
        {
            answer++;
            list.Remove(lost[i] - 1);
        }
        else if (list.Contains(lost[i] + 1))
        {
            answer++;
            list.Remove(lost[i] + 1);
        }
    }

    return answer - lost.Length;
}