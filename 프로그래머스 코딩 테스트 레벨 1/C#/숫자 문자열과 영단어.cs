

//solution("one4seveneight");
static int solution(string s)
{
   string[] array = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

   int answer = 0;

   char[] toCharArray = s.ToCharArray();

   string str = string.Empty;
   string temp = string.Empty;
   foreach (var ch in toCharArray)
   {
       if (ch < 90)
       {
           str += ch;
       }
       else
       {
           temp += ch;
           int index = Array.IndexOf(array, temp);
           if (0 <= index)
           {
               str += index;
               temp = string.Empty;
           }
       }
   }

   answer = Convert.ToInt32(str);

   return answer;
}