namespace Common.Domain.Utils;

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

public static partial class StringUtils
{
    public static string ReplaceCurly(string text, Dictionary<string, object>? replacements)
    {
        if (replacements == null || replacements.Count == 0) return text;

        var pattern = UriReplacementRegex();
        var builder = new StringBuilder();
        var i = 0;

        foreach (Match match in pattern.Matches(text))
        {
            builder.Append(text.AsSpan(i, match.Index - i)); // Add the text before the match
            
            var replacementKey = match.Groups[1].Value;
            builder.Append(replacements.TryGetValue(replacementKey, out var replacement)
                ? replacement?.ToString()
                : match.Value); // If no replacement found, append the original match

            i = match.Index + match.Length;
        }

        builder.Append(text.AsSpan(i)); // Add remaining text after the last match

        return builder.ToString();
    }

    [GeneratedRegex(@"\{(.+?)}")]
    private static partial Regex UriReplacementRegex();
}
