using System;
using System.Linq;

//solution(new string[] { "Jane", "Kim" });
public string solution(string[] seoul)
{
    string answer = string.Empty;
    var target = seoul.Select((data, idx) => new Tuple<string, int>(data, idx)).Where(t => t.Item1.Equals("Kim"));

    foreach (var item in target)
    {
        answer = string.Format("�輭���� {0}�� �ִ�", item.Item2);
        break;
    }

    return answer;
}