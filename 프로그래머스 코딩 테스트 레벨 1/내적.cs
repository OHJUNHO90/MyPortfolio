
//solution(new int[] { 1, 2, 3, 4 }, new int[] { -3, -1, 0, 2 });
static int solution(int[] a, int[] b)
{
   int answer = 0;
   answer =  a.Select((t, idx) => t * b[idx]).Sum();
   return answer;
}