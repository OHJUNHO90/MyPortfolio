using System;
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

        #region 정수 제곱근 판별
        //static long solution(long n)
        //{
        //    double val = Math.Sqrt(n);
        //    double rv = val % 1;

        //    if (rv.Equals(0)) {
        //        val++;
        //        return (long)Math.Pow(val, 2);
        //    }
        //    else              return -1;
        //}
        #endregion
        #region 정수 내림차순으로 배치하기
        //static long solution(long n)
        //{
        //    string answer = string.Empty;
        //    var array = n.ToString().ToArray().OrderByDescending(t => t) ;

        //    foreach (var item in array)
        //        answer += item.ToString();

        //    return long.Parse(answer);
        //}
        #endregion
        #region 자연수 뒤집어 배열로 만들기
        //static int[] solution(long n)
        //{
        //    IEnumerable<char> array = n.ToString().ToCharArray().Reverse();
        //    int[] answer = new int[array.Count()];
        //    int index = 0;
        //    foreach (var item in array)
        //    {
        //        answer[index] = int.Parse(item.ToString());
        //        index++;
        //    }

        //    return answer;
        //}
        #endregion
        #region 자릿수 더하기
        //static int solution(int n)
        //{
        //    return n.ToString().ToCharArray().Sum(t => int.Parse(t.ToString()));
        //}
        #endregion
        #region 이상한 문자 만들기
        //static string solution(string s)
        //{
        //    char[] array = s.ToCharArray();
        //    int index = 0;

        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        if (!array[i].Equals((char)32)) 
        //        {
        //            if (index == 0 || index % 2 == 0) {
        //                if ((char)97 <= array[i] && array[i] <= (char)122) 
        //                    array[i] = (char)(array[i] - 32);
        //            }
        //            else {
        //                if ((char)65 <= array[i] && array[i] <= (char)90)
        //                    array[i] = (char)(array[i] + 32);
        //            }
        //        }

        //        index = array[i].Equals((char)32) ? 0 : index + 1;

        //    }

        //    return string.Join("", array);
        //}
        #endregion
        #region 약수의 합
        //static int solution(int n)
        //{
        //    int answer = 0;
        //    int divided = n / 2;

        //    for (int i = 1; i <= divided; i++){
        //        if (n % i == 0) answer += i;
        //    }

        //    return answer + n;
        //}
        #endregion
        #region 시저 암호
        // 공백문자 32
        // 대문자 65 ~ 90
        // 소문자 97 ~ 122
        //static string solution(string s, int n)
        //{
        //    string answer = string.Empty;
        //    char[] array = s.ToCharArray();

        //    foreach (char c in array)
        //    {
        //        if (c.Equals((char)32))
        //        {
        //            answer += (char)32;
        //            continue;
        //        }

        //        char ascii = (char)(c + n);
        //        if (c <= 90 && 90 < ascii) ascii = (char)('A' + (ascii % 90) - 1);
        //        else if (c <= 122 && 122 < ascii) ascii = (char)('a' + (ascii % 122) - 1);
        //        answer += ascii;
        //    }

        //    return answer;
        //}
        #endregion
        #region 문자열을 정수로 바꾸기
        // solution("-1234");
        //static int solution(string s)
        //{
        //    int answer = 0;
        //    int.TryParse(s, out answer);
        //    return answer;
        //}
        #endregion
        #region 수박수박수박수박수박수?
        ////solution(3);
        //static string solution(int n)
        //{
        //    int rest = n % 2;
        //    string answer = string.Concat(Enumerable.Repeat("수박", (n - rest) / 2));
        //    if (0 < rest)   answer += "수";

        //    return answer;
        //}
        #endregion
        #region 소수 찾기
        // solution(5);
        //static int solution(int n)
        //{
        //    bool[] isPrime = new bool[n + 1];
        //    Array.Fill(isPrime, true);
        //    isPrime[0] = false;
        //    isPrime[1] = false;

        //    for (int i = 2; i * i <= n; i++)
        //    {
        //        if (isPrime[i]) 
        //        {
        //            for (int j = i * i; j <= n; j += i)    
        //                isPrime[j] = false;
        //        }
        //    }     

        //    return isPrime.Select(t => t).Where(t => t).Count();
        //}
        #endregion
        #region 서울에서 김서방 찾기
        ////solution(new string[] { "Jane", "Kim" });
        //static string solution(string[] seoul)
        //{
        //    string answer = string.Empty;
        //    var target = seoul.Select((data, idx) => new Tuple<string, int>(data, idx)).Where(t => t.Item1.Equals("Kim"));

        //    foreach (var item in target) {
        //        answer = string.Format("김서방은 {0}에 있다", item.Item2);
        //        break;
        //    }

        //    return answer;
        //}
        #endregion
        #region 문자열 다루기 기본
        //solution("aa1234");
        //static bool solution(string s)
        //{
        //    return (s.Length == 4 || s.Length == 6) && int.TryParse(s, out int result);
        //}
        #endregion
        #region 문자열 내림차순으로 배치
        //solution("Zbcdefg");
        //static string solution(string s)
        //{
        //    string answer = string.Empty;
        //    var array = s.ToArray().OrderByDescending(i => i);
        //    return string.Join("", array);
        //}
        #endregion
        #region 문자열 내 마음대로 정렬하기
        //solution(new string[] { "sun", "bed", "car" }, 1);
        //return strings.OrderBy(t => t).ThenBy(t => t[n]).ToArray();
        //static string[] solution(string[] strings, int n)
        //{
        //    var array = from str in strings
        //                orderby str[n]
        //                group str by str[n] into g
        //                select new { data = g };

        //    int index = 0;
        //    foreach (var item in array)
        //    {
        //        var list = item.data.OrderBy(d => d).ToArray();

        //        for (int i = 0; i < list.Length; i++)
        //        {
        //            strings[index] = list[i];
        //            index++;
        //        }
        //    }

        //    return strings;
        //}
        #endregion
        #region 두 정수 사이의 합
        //등차수열의 합 공식 응용 (int가 담을수있는 범위를 연산중 넘어버림)
        // solution(500, -1000000)
        //static long solution(int a, int b)
        //{
        //    long[] array = new long[] { a, b };
        //    return (a + b) * (Math.Abs(array[0] - array[1]) + 1) / 2;
        //}
        #endregion
        #region 나누어 떨어지는 숫자 배열
        //solution(new int[] { 2, 36, 1, 3 }, 1)
        //static int[] solution(int[] arr, int divisor)
        //{
        //    var linq = from element in arr
        //               where (element % divisor) == 0
        //               select element;

        //    List<int> answer = linq.ToList();
        //    if (answer.Count == 0)
        //        answer.Add(-1);

        //    return answer.OrderBy(e => e).ToArray();
        //}
        #endregion
        #region 가운데 글자 가져오기
        //solution("abcde")
        //static string solution(string s)
        //{
        //    double startIndex = s.Length % 2.0d;
        //    return startIndex == 0.0 ? s.Substring((s.Length/2)-1, 2) : s.Substring((s.Length/2), 1);
        //}
        #endregion
        #region 부족한 금액 계산하기
        //solution(3, 20, 4)
        //static long solution(int price, int money, int count)
        //{
        //    long totalCost = 0;
        //    for (int i = 1; i <= count; i++)
        //        totalCost += price * i;

        //    return totalCost < money ? 0 : totalCost - money;
        //}
        #endregion
        #region 나머지가 1이 되는 수 찾기
        //solution(11); 
        //static int solution(int n)
        //{
        //    for (int i = 2; i < n; i++)
        //    {
        //        if (n % i == 1) return i;
        //    }

        //    return 0;
        //}
        #endregion
        #region 최소직사각형
        //Console.WriteLine(solution(new int[,] { { 14, 4 }, { 19, 6 }, { 6, 16 }, { 18, 7 }, { 7, 11 } }));
        //static int solution(int[,] sizes)
        //{
        //    List<int[]> list = new List<int[]>();
        //    for (int i = 0; i < sizes.GetLength(0); i++) {
        //        if (sizes[i,0] < sizes[i,1]) {
        //            if (sizes[i, 0] < sizes[i, 1]) {
        //                int big   = sizes[i, 1];
        //                int small = sizes[i, 0];
        //                sizes[i, 0] = big;
        //                sizes[i, 1] = small;
        //            }
        //        }

        //        list.Add(new int[] { sizes[i, 0], sizes[i, 1] });
        //    }

        //    return list.Max(t => t[0]) * list.Max(t => t[1]);

        //}
        #endregion
        #region 2016년
        //solution(1, 7)
        //static string solution(int a, int b)
        //{
        //    string[] months = new string[] { "FRI", "SAT", "SUN", "MON", "TUE", "WED", "THU"};
        //    int[] thereAre31 = new int[]   { 1, 3, 5, 7, 8, 10, 12 };
        //    int[] thereAre30 = new int[]   { 4, 6, 9, 11 };

        //    int temp = b;
        //    for (int i = 1; i < a; i++)
        //    {
        //        if (thereAre31.Contains(i))      temp += 31;
        //        else if (thereAre30.Contains(i)) temp += 30;
        //        else                             temp += 29;
        //    }

        //    int index = temp % 7;
        //    index = index == 0 ? 6 : index - 1;
        //    return months[index];
        //}
        #endregion
        #region 두개 뽑아서 더하기
        //solution(new int[] { 5, 0, 2, 7 });
        //static int[] solution(int[] numbers)
        //{
        //    int[] answer = new int[] {};
        //    List<int> array = new List<int>();
        //    int currentIndex = 0;

        //    for (int i = currentIndex+1; i < numbers.Length; i++)
        //    {
        //        int temp = numbers[currentIndex] + numbers[i];
        //        array.Add(temp);

        //        if (i + 1 == numbers.Length)
        //        {
        //            currentIndex++;
        //            i = currentIndex;
        //            continue;
        //        }
        //    }

        //    answer = array.OrderBy(t => t).Distinct().ToArray();
        //    return answer;
        //}
        #endregion
        #region 예산
        //solution(new int[] { 2, 2, 3, 3 }, 10);
        //static int solution(int[] d, int budget)
        //{
        //    int answer = 0;
        //    int amountSpent = 0;


        //    var array = from i in d
        //                orderby i select i;

        //    foreach (int properties in array)
        //    {
        //        if (amountSpent + properties <= budget)
        //        {
        //            amountSpent += properties;
        //            answer++;
        //        }
        //    }

        //    return answer;
        //}
        #endregion
        #region 3진법 뒤집기
        //solution(78413450)
        //static int solution(int n)
        //{
        //    int answer = 0;
        //    byte[] array = new byte[32];
        //    int index = 0;

        //    while (true)
        //    {
        //        int divided = n / 3;
        //        int rest = n % 3;

        //        array[index] = (byte)rest;

        //        if (divided == 0)
        //        {
        //            int pow = 0;
        //            for (int i = index; 0 <= i; i--)
        //            {
        //                answer += (int)(Math.Pow(3, pow) * array[i]);
        //                pow++;
        //            }
        //            break;
        //        }

        //        n = divided;
        //        index++;
        //    }

        //    return answer;
        //}
        #endregion
        #region 약수의 개수와 덧셈
        //solution(1, 3);
        //static int solution(int left, int right)
        //{
        //    int answer = 0;
        //    for (int i = left; i <= right; i++)
        //    {
        //        if (IsEvenNumber(i))    answer += i;
        //        else                    answer -= i;
        //    }

        //    Console.WriteLine(answer);
        //    return answer;
        //}

        //static bool IsEvenNumber(int num)
        //{
        //    if (num == 1)
        //        return false;

        //    int count = 2;
        //    for (int i = 2; i <= Math.Sqrt(num); i++)
        //    {
        //        int division = num / i;
        //        if (num % i == 0)
        //        {
        //            count++;
        //            if (num % division == 0 && i != division) count++;
        //        }
        //    }

        //    return count % 2 == 0 ? true : false;
        //}
        #endregion
        #region 체육복
        //solution(7, new int[] { 4,3,2 }, new int[] { 1,6,5 });
        //static int solution(int n, int[] lost, int[] reserve)
        //{
        //    // n 전체 학생수
        //    // lost 도난당한 학생들 번호
        //    // 여벌의 체육복을 가져온 학생들 번호
        //    // -규칙 여벌의 체육복을 가져온 학생의 번호 +1 또는 -1만 체육복을 빌려줄수 있음
        //    // 여벌의 체육복을 가져온 학생이 체육복을 도난당했을수도 있음, 만약 도난당했다면 체육복을 빌려줄수 없음
        //    int answer = n;
        //    int[] temp = reserve.Except(lost).ToArray();
        //    lost = lost.Except(reserve).OrderBy(t => t).ToArray();
        //    List<int> list = (reserve = temp).ToList();

        //    for (int i = 0; i < lost.Length; i++)
        //    {
        //        if (list.Contains(lost[i] - 1))
        //        {
        //            answer++;
        //            list.Remove(lost[i] - 1);
        //        }
        //        else if (list.Contains(lost[i] + 1))
        //        {
        //            answer++;
        //            list.Remove(lost[i] + 1);
        //        }
        //    }

        //    return answer - lost.Length;
        //}
        #endregion
        #region 모의고사
        // solution(new int[] { 1, 3, 2, 4, 2 });
        //static int[] solution(int[] answers)
        //{
        //    //[순차적]1번 수포자가 찍는 방식: 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, ...
        //    //[0, -1, 0, +1, 0, +2 , 0, +3]2번 수포자가 찍는 방식: 2, 1, 2, 3, 2, 4, 2, 5, 2, 1, 2, 3, 2, 4, 2, 5, ...
        //    //[0, 0, -2, -2, -1, -1 ,+2, +2, +3, +3]3번 수포자가 찍는 방식: 3, 3, 1, 1, 2, 2, 4, 4, 5, 5, 3, 3, 1, 1, 2, 2, 4, 4, 5, 5, ...
        //    int[] answer = new int[3];
        //    int[] case1 = new int[] { 1, 2, 3, 4, 5 };
        //    int[] case2 = new int[] { 2, 1, 2, 3, 2, 4, 2, 5 };
        //    int[] case3 = new int[] { 3, 3, 1, 1, 2, 2, 4, 4, 5, 5 };


        //    for (int i = 0; i < answers.Length; i++)
        //    {
        //        if (answers[i].Equals(case1[i % 5])) answer[0]++;
        //        if (answers[i].Equals(case2[i % 8])) answer[1]++;
        //        if (answers[i].Equals(case3[i % 10])) answer[2]++;
        //    }

        //    int max = answer.Max();
        //    List<int> result = new List<int>();

        //    for (int i = 0; i < answer.Length; i++)
        //    {
        //        if (max.Equals(answer[i]))
        //        {
        //            result.Add(i + 1);
        //        }
        //    }

        //    return result.ToArray();
        //}

        #endregion
        #region K번째수
        //solution( new int[] { 1, 5, 2, 6, 3, 7, 4 },
        //          new int[,] { { 2, 5, 3 }, { 4, 4, 1 }, { 1, 7, 3 } });
        //static int[] solution(int[] array, int[,] commands)
        //{
        //    int[] answer = new int[commands.GetLength(0)];

        //    int targetIndex = 0;
        //    int currentIndex = 1;
        //    int i = 0;
        //    int j = 0;
        //    int k = 0;

        //    foreach (int element in commands)
        //    {
        //        int remainder = currentIndex % 3;

        //        if      (remainder == 1) i = element;
        //        else if (remainder == 2) j = element;
        //        else if (remainder == 0)
        //        {
        //            k = element;
        //            int[] temp = new int[j-i+1];
        //            Array.Copy(array, i - 1, temp, 0, (j-i+1));
        //            temp = temp.OrderBy(n => n).ToArray();
        //            answer[targetIndex] = temp[k - 1];
        //            targetIndex++;
        //        }

        //        currentIndex++;
        //    }


        //    return answer;
        //}
        #endregion
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
