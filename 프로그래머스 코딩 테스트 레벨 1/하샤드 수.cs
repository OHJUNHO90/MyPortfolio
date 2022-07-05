using System.Linq;

public bool solution(int x)
{
    var array = x.ToString().ToArray();
    return x % array.Sum(t => int.Parse(t.ToString())) == 0 ? true : false;
}