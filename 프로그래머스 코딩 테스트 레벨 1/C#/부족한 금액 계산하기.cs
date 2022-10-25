using System;

//solution(3, 20, 4)
public long solution(int price, int money, int count)
{
    long totalCost = 0;
    for (int i = 1; i <= count; i++)
        totalCost += price * i;

    return totalCost < money ? 0 : totalCost - money;
}