using System.Linq;

public long solution(long n)
{
    string answer = string.Empty;
    var array = n.ToString().ToArray().OrderByDescending(t => t);

    foreach (var item in array)
        answer += item.ToString();

    return long.Parse(answer);
}