
//solution( new int[] { 1, 5, 2, 6, 3, 7, 4 }, new int[,] { { 2, 5, 3 }, { 4, 4, 1 }, { 1, 7, 3 } });
static int[] solution(int[] array, int[,] commands)
{
   int[] answer = new int[commands.GetLength(0)];

   int targetIndex = 0;
   int currentIndex = 1;
   int i = 0;
   int j = 0;
   int k = 0;

   foreach (int element in commands)
   {
	   int remainder = currentIndex % 3;

	   if      (remainder == 1) i = element;
	   else if (remainder == 2) j = element;
	   else if (remainder == 0)
	   {
		   k = element;
		   int[] temp = new int[j-i+1];
		   Array.Copy(array, i - 1, temp, 0, (j-i+1));
		   temp = temp.OrderBy(n => n).ToArray();
		   answer[targetIndex] = temp[k - 1];
		   targetIndex++;
	   }

	   currentIndex++;
   }


   return answer;
}