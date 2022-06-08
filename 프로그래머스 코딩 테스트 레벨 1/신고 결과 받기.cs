

//solution( new string[] { "muzi", "frodo", "apeach", "neo" },
//		    new string[] { "muzi frodo", "apeach frodo", "frodo neo", "muzi neo", "apeach muzi" },
// 		    2 );

static int[] solution(string[] id_list, string[] report, int k)
{
   int[] answer = new int[id_list.Length];
   report = report.Distinct().ToArray();

   /*아이디별 신고당한 횟수*/
   Dictionary<string, int> reportedId = new Dictionary<string, int>();
   Dictionary<string, List<string>> reportIdListByEachUser = new Dictionary<string, List<string>>();

   for (int i = 0; i < report.Length; i++)
   {
       string[] split = report[i].Split();

       if (reportedId.ContainsKey(split[1]) == false)
       {
           reportedId.Add(split[1], 1);
       }
       else
       {
           reportedId[split[1]]++;
       }

       if (reportIdListByEachUser.ContainsKey(split[0]) == false)
       {
           List<string> list = new List<string>();
           list.Add(split[1]);
           reportIdListByEachUser.Add(split[0], list);
       }
       else
       {
           reportIdListByEachUser[split[0]].Add(split[1]);
       }
   }


   for (int i = 0; i < id_list.Length; i++)
   {
       string reportId = id_list[i];

       if (reportIdListByEachUser.ContainsKey(reportId))
       {
           for (int j = 0; j < reportIdListByEachUser[reportId].Count; j++)
           {
               if (reportedId.ContainsKey(reportIdListByEachUser[reportId][j]))
               {
                   if (k <= reportedId[reportIdListByEachUser[reportId][j]])
                   {
                       answer[i]++;
                   }
               }

           }
       }
   }

   return answer;
}