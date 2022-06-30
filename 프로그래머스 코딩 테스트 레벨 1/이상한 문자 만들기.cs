using System;


public string solution(string s)
{
    char[] array = s.ToCharArray();
    int index = 0;

    for (int i = 0; i < array.Length; i++)
    {
        if (!array[i].Equals((char)32))
        {
            if (index == 0 || index % 2 == 0)
            {
                if ((char)97 <= array[i] && array[i] <= (char)122)
                    array[i] = (char)(array[i] - 32);
            }
            else
            {
                if ((char)65 <= array[i] && array[i] <= (char)90)
                    array[i] = (char)(array[i] + 32);
            }
        }

        index = array[i].Equals((char)32) ? 0 : index + 1;

    }

    return string.Join("", array);
}