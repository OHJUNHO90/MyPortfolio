
//solution(78413450)
public class Solution
{
    public int solution(int n)
    {
        int answer = 0;
        byte[] array = new byte[32];
        int index = 0;

        while (true)
        {
            int divided = n / 3;
            int rest = n % 3;

            array[index] = (byte)rest;

            if (divided == 0)
            {
                int pow = 0;
                for (int i = index; 0 <= i; i--)
                {
                    answer += (int)(Math.Pow(3, pow) * array[i]);
                    pow++;
                }
                break;
            }

            n = divided;
            index++;
        }

        return answer;
    }
}