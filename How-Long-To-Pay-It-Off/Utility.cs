using System.Text.RegularExpressions;

namespace How_Long_To_Pay_It_Off;

internal static partial class Utility
{
    [GeneratedRegex("^", RegexOptions.Multiline)]
    private static partial Regex Rgx();

    internal static string IndentTable(string table, int indentSize = 4)
    {
        var indent = new string(' ', indentSize);
        var regex = Rgx();
        return regex.Replace(table, indent);
    }
}