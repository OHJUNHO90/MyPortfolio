using System.Linq;

//################
// [프로그래머스 최고의 답안]
// return strings.OrderBy(t => t).ThenBy(t => t[n]).ToArray();
//################

//solution(new string[] { "sun", "bed", "car" }, 1);
public string[] solution(string[] strings, int n)
{
    var array = from str in strings
                orderby str[n]
                group str by str[n] into g
                select new { data = g };

    int index = 0;
    foreach (var item in array)
    {
        var list = item.data.OrderBy(d => d).ToArray();
        for (int i = 0; i < list.Length; i++)
        {
            strings[index] = list[i];
            index++;
        }
    }
    return strings;
}