using System;

public long solution(long n)
{
    double val = Math.Sqrt(n);
    double rv = val % 1;

    if (rv.Equals(0)){
        val++;
        return (long)Math.Pow(val, 2);
    }
    else return -1;
}