

//solution(new int[] { 1, 2, 7, 6, 4 });
static int[] array = new int[3];
static int r = 3;
static int answer = 0;

static int solution(int[] nums)
{
   Combination(nums, 0, 0);
   return answer;
}

static void Combination(int[] nums, int depth, int next)
{
   if (depth == r)
   {
	   if (IsPrime())
	   {
		   answer++;
	   }

	   return;
   }

   for (int i = next; i < nums.Length; i++)
   {
	   array[depth] = nums[i];
	   Combination(nums, depth + 1, i + 1);
   }
}

static bool IsPrime()
{
   /*소수인지 판단*/
   int num = array.ToList().Select(t => t).Sum();

   int nr = (int)Math.Sqrt(num);
   for (int i = 2; i <= nr; i++)
   {
	   if (num % i == 0)
	   {
		   return false;
	   }
   }

   return true;
}
