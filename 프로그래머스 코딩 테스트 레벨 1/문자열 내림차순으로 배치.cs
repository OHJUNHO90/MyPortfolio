using System.Linq;

//solution("Zbcdefg");
public string solution(string s)
{
    string answer = string.Empty;
    var array = s.ToArray().OrderByDescending(i => i);
    return string.Join("", array);
}