
//solution(1, 3); 
public int solution(int left, int right)
{
    int answer = 0;
    for (int i = left; i <= right; i++)
    {
        if (IsEvenNumber(i)) answer += i;
        else answer -= i;
    }

    Console.WriteLine(answer);
    return answer;
}

public bool IsEvenNumber(int num)
{
    if (num == 1)
        return false;

    int count = 2;
    for (int i = 2; i <= Math.Sqrt(num); i++)
    {
        int division = num / i;
        if (num % i == 0)
        {
            count++;
            if (num % division == 0 && i != division) count++;
        }
    }

    return count % 2 == 0 ? true : false;
}