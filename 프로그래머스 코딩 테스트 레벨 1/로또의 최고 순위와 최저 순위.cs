
//Solution(new int[] { 33, 0, 16, 10, 0, 22 }, new int[] { 22, 19, 9, 7, 3, 45 });
static void Solution(int[] lottos, int[] win_num)
{
   int[] answer = new int[2];

   var intersection  = lottos.Intersect(win_num);
   var indiscernible = from number in lottos
                       where number == 0
                       select number;

   GetMyLank(ref answer[0], intersection.Count() + indiscernible.Count());
   GetMyLank(ref answer[1], intersection.Count());

}

static void GetMyLank(ref int array,  int matchingCount)
{
   switch (matchingCount)
   {
       case 6:
           array = 1;
           break;
       case 5:
           array = 2;
           break;
       case 4:
           array = 3;
           break;
       case 3:
           array = 4;
           break;
       case 2:
           array = 5;
           break;
       default:
           array = 6;
           break;
   }
}