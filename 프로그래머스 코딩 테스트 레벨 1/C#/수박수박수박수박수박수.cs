using System.Linq;

//solution(3);
public string solution(int n)
{
    int rest = n % 2;
    string answer = string.Concat(Enumerable.Repeat("����", (n - rest) / 2));
    if (0 < rest) answer += "��";

    return answer;
}