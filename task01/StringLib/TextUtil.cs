using System.Text.RegularExpressions;

namespace StringLib;

public static class TextUtil
{
    public static List<string> SplitIntoWords(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return [];
        }

        const string pattern = @"\p{L}+(?:[\-\']\p{L}+)*";
        Regex regex = new(pattern, RegexOptions.Compiled);

        return regex.Matches(text)
            .Select(match => match.Value)
            .ToList();
    }

    public static bool IsEnglishPangram(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return false;
        }

        // Создаем множество для отслеживания найденных букв
        HashSet<char> foundLetters = new HashSet<char>();

        foreach (char c in text)
        {
            // Приводим к нижнему регистру для регистронезависимой проверки
            char lowerChar = char.ToLowerInvariant(c);

            // Проверяем, является ли символ английской буквой
            if (lowerChar >= 'a' && lowerChar <= 'z')
            {
                foundLetters.Add(lowerChar);

                // Если нашли все 26 букв, можно досрочно вернуть true
                if (foundLetters.Count == 26)
                {
                    return true;
                }
            }
        }

        return foundLetters.Count == 26;
    }
}