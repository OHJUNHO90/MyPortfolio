using System;
using System.Linq;

//���������� �� ���� ���� (int�� �������ִ� ������ ������ �Ѿ����)
//int temp = 2147483647;
//temp = temp + 1;
//Console.WriteLine(temp);  //// -2147483648
//temp = temp + 2147483647;
//Console.WriteLine(temp);  //// -1
//temp = temp + 2;
//Console.WriteLine(temp);  //// 1

//solution(500, -1000000)
public long solution(int a, int b)
{
    long[] array = new long[] { a, b };
    return (a + b) * (Math.Abs(array[0] - array[1]) + 1) / 2;
}