namespace Common.Domain.Extensions;

public static class DictionaryExtensions
{
    public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> target,
        IDictionary<TKey, TValue> source)
    {
        foreach (var item in source)
            target[item.Key] = item.Value;
    }
}