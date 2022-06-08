
//solution(new int[] { 1, 2, 6, 7, 8, 0 })
static int solution(int[] numbers)
{
   int answer = 0;
   int[] comparison = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
   answer = comparison.Except(numbers).Sum();

   return answer;
}