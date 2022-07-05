
//solution(new int[] { 4, 7, 12 }, new bool[] { true, false, true })
static int solution(int[] absolutes, bool[] signs)
{
   //absolutes.Select((t, idx) => signs[idx]? t : -t).Sum();

   int answer = 0;
   
   for (int i = 0; i < absolutes.Length; i++)
   {
       if ( !signs[i] )
       {
           absolutes[i] = absolutes[i] * -1;
       }

       answer += absolutes[i];
   }


   return answer;
}