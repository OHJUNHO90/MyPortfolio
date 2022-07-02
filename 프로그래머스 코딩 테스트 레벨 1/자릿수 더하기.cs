using System.Linq;

public int solution(int n)
{
    return n.ToString().ToCharArray().Sum(t => int.Parse(t.ToString()));
}