using System.Linq;

//solution("aa1234");
public bool solution(string s)
{
    return (s.Length == 4 || s.Length == 6) && int.TryParse(s, out int result);
}