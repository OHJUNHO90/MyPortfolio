﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Programmers
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        #region 신고 결과 받기

        //solution(new string[] { "muzi", "frodo", "apeach", "neo" },
        //             new string[] { "muzi frodo", "apeach frodo", "frodo neo", "muzi neo", "apeach muzi" },
        //             2);
        //static int[] solution(string[] id_list, string[] report, int k)
        //{
        //    int[] answer = new int[id_list.Length];
        //    report = report.Distinct().ToArray();

        //    /*아이디별 신고당한 횟수*/
        //    Dictionary<string, int> reportedId = new Dictionary<string, int>();
        //    Dictionary<string, List<string>> reportIdListByEachUser = new Dictionary<string, List<string>>();

        //    for (int i = 0; i < report.Length; i++)
        //    {
        //        string[] split = report[i].Split();

        //        if (reportedId.ContainsKey(split[1]) == false)
        //        {
        //            reportedId.Add(split[1], 1);
        //        }
        //        else
        //        {
        //            reportedId[split[1]]++;
        //        }

        //        if (reportIdListByEachUser.ContainsKey(split[0]) == false)
        //        {
        //            List<string> list = new List<string>();
        //            list.Add(split[1]);
        //            reportIdListByEachUser.Add(split[0], list);
        //        }
        //        else
        //        {
        //            reportIdListByEachUser[split[0]].Add(split[1]);
        //        }
        //    }


        //    for (int i = 0; i < id_list.Length; i++)
        //    {
        //        string reportId = id_list[i];

        //        if (reportIdListByEachUser.ContainsKey(reportId))
        //        {
        //            for (int j = 0; j < reportIdListByEachUser[reportId].Count; j++)
        //            {
        //                if (reportedId.ContainsKey(reportIdListByEachUser[reportId][j]))
        //                {
        //                    if (k <= reportedId[reportIdListByEachUser[reportId][j]])
        //                    {
        //                        answer[i]++;
        //                    }
        //                }

        //            }
        //        }
        //    }

        //    return answer;
        //}

        #endregion
        #region 소수 만들기
        //solution(new int[] { 1, 2, 7, 6, 4 });

        //static int[] array = new int[3];
        //static int r = 3;
        //static int answer = 0;

        //static int solution(int[] nums)
        //{
        //    Combination(nums, 0, 0);
        //    return answer;
        //}

        //static void Combination(int[] nums, int depth, int next)
        //{
        //    if (depth == r)
        //    {
        //        if (IsPrime())
        //        {
        //            answer++;
        //        }

        //        return;
        //    }

        //    for (int i = next; i < nums.Length; i++)
        //    {
        //        array[depth] = nums[i];
        //        Combination(nums, depth + 1, i + 1);
        //    }
        //}

        //static bool IsPrime()
        //{
        //    /*소수인지 판단*/
        //    int num = array.ToList().Select(t => t).Sum();

        //    int nr = (int)Math.Sqrt(num);
        //    for (int i = 2; i <= nr; i++)
        //    {
        //        if (num % i == 0)
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}
        #endregion
        #region 내적
        //solution(new int[] { 1, 2, 3, 4 }, new int[] { -3, -1, 0, 2 });
        //static int solution(int[] a, int[] b)
        //{
        //    int answer = 0;
        //    answer =  a.Select((t, idx) => t * b[idx]).Sum();
        //    return answer;
        //}
        #endregion
        #region 음양더하기
        //solution(new int[] { 4, 7, 12 }, new bool[] { true, false, true })
        //static int solution(int[] absolutes, bool[] signs)
        //{
        //    // absolutes.Select((t, idx) => signs[idx]? t : -t).Sum();

        //    int answer = 0;

        //    for (int i = 0; i < absolutes.Length; i++)
        //    {
        //        if ( !signs[i] )
        //        {
        //            absolutes[i] = absolutes[i] * -1;
        //        }

        //        answer += absolutes[i];
        //    }


        //    return answer;
        //}
        #endregion
        #region 없는 숫자 더하기
        //solution(new int[] { 1, 2, 6, 7, 8, 0 })
        //static int solution(int[] numbers)
        //{
        //    int answer = 0;
        //    int[] comparison = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        //    answer = comparison.Except(numbers).Sum();

        //    return answer;
        //}
        #endregion
        #region 숫자 문자열과 영단어
        //solution("one4seveneight");
        //static int solution(string s)
        //{
        //    string[] array = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        //    int answer = 0;

        //    char[] toCharArray = s.ToCharArray();

        //    string str = string.Empty;
        //    string temp = string.Empty;
        //    foreach (var ch in toCharArray)
        //    {
        //        if (ch < 90)
        //        {
        //            str += ch;
        //        }
        //        else
        //        {
        //            temp += ch;
        //            int index = Array.IndexOf(array, temp);
        //            if (0 <= index)
        //            {
        //                str += index;
        //                temp = string.Empty;
        //            }
        //        }
        //    }

        //    answer = Convert.ToInt32(str);

        //    return answer;
        //}


        #endregion
        #region 로또의 최고 순위와 최저 순위
        //Solution(new int[] { 33, 0, 16, 10, 0, 22 }, new int[] { 22, 19, 9, 7, 3, 45 });
        //static void Solution(int[] lottos, int[] win_num)
        //{
        //    int[] answer = new int[2];

        //    var intersection  = lottos.Intersect(win_num);
        //    var indiscernible = from number in lottos
        //                        where number == 0
        //                        select number;

        //    GetMyLank(ref answer[0], intersection.Count() + indiscernible.Count());
        //    GetMyLank(ref answer[1], intersection.Count());

        //}

        //static void GetMyLank(ref int array,  int matchingCount)
        //{
        //    switch (matchingCount)
        //    {
        //        case 6:
        //            array = 1;
        //            break;
        //        case 5:
        //            array = 2;
        //            break;
        //        case 4:
        //            array = 3;
        //            break;
        //        case 3:
        //            array = 4;
        //            break;
        //        case 2:
        //            array = 5;
        //            break;
        //        default:
        //            array = 6;
        //            break;
        //    }
        //}
        #endregion
    }
}
