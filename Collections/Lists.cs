using System.Collections.Generic;
using System.Linq;

public static class Lists
{
    public static List<T> RepeatedDefault<T>(int count)
    {
        return Repeated(default(T), count);
    }

    public static List<T> Repeated<T>(T value, int count)
    {
        List<T> repeated = new List<T>(count);
        repeated.AddRange(Enumerable.Repeat(value, count));
        return repeated;
    }
}
