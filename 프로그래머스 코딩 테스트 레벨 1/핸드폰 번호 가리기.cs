using System.Linq;

public string solution(string phone_number)
{
    return string.Concat(Enumerable.Repeat("*", phone_number.Length - 4)) + phone_number.Substring(phone_number.Length - 4, 4);
}