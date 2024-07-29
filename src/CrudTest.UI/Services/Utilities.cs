using System.Globalization;
using System.Text;

namespace CrudTest.UI.Services;

public static class Utilities
{
    public static string GetCorrectExceptionMessage(this string exceptionMsg)
    {
        if (string.IsNullOrEmpty(exceptionMsg))
            return string.Empty;

        exceptionMsg = exceptionMsg.Replace("Exception was thrown by handler.", "");
        Console.WriteLine(exceptionMsg);

        return FilterString(exceptionMsg, true, true, false, new[] { ' ', '.', '!', ',' });
    }

    private static string FilterString(string str, bool keepNumber, bool keepEnglishAlpha, bool keepPersiaAlpha, char[] special)
    {
        return str.Aggregate(new StringBuilder(), (sb, c) =>
            (keepNumber && char.IsDigit(c))
            || (keepEnglishAlpha && ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z')))
            || (keepPersiaAlpha && char.GetUnicodeCategory(c) == UnicodeCategory.OtherLetter)
            || (special != null && special.Contains(c))
                ? sb.Append(c)
                : sb.Append(string.Empty)).ToString();
    }

}