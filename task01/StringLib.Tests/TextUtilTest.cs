using StringLib;

namespace StringLib.Tests;

public class TextUtilPangramTest
{
    [Theory]
    [MemberData(nameof(EnglishPangramTestData))]
    public void IsEnglishPangram_ShouldReturnCorrectResult(string input, bool expected)
    {
        // Arrange - подготовка не требуется, так как метод статический

        // Act
        bool result = TextUtil.IsEnglishPangram(input);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<string, bool> EnglishPangramTestData()
    {
        return new TheoryData<string, bool>
        {
            // Основные успешные сценарии (happy path)
            { "The quick brown fox jumps over the lazy dog", true },
            { "Pack my box with five dozen liquor jugs", true },
            { "The five boxing wizards jump quickly", true },
            { "How vexingly quick daft zebras jump", true },
            
            // Панграммы с разным регистром
            { "THE QUICK BROWN FOX JUMPS OVER THE LAZY DOG", true },
            { "The Quick Brown Fox Jumps Over The Lazy Dog", true },
            { "ThE qUiCk BrOwN fOx JuMpS oVeR tHe LaZy DoG", true },
            
            // Панграммы с дополнительными символами
            { "The quick brown fox jumps over the lazy dog!", true },
            { "The quick brown fox jumps over the lazy dog 123", true },
            { "The quick brown fox jumps over the lazy dog.", true },
            
            // Не панграммы (отсутствуют некоторые буквы)
            { "The quick brown fox jumps over the lazy do", false }, // нет g
            { "The quick brown fox jumps over the lazy", false }, // нет d, o, g
            { "Hello world", false }, // много букв отсутствует
            
            // Пограничные случаи
            { "", false }, // пустая строка
            { null!, false }, // null
            { "          ", false }, // только пробелы
            { "!@#$%^&*()", false }, // только спецсимволы
            { "1234567890", false }, // только цифры
            
            // Строка с повторяющимися буквами (но не все буквы алфавита)
            { "aaaaaaaaaaaaaaaaaaaaaaaaaa", false }, // только буква 'a'
            
            // Минимальная панграмма (все буквы по одному разу)
            { "abcdefghijklmnopqrstuvwxyz", true },
            { "ABCDEFGHIJKLMNOPQRSTUVWXYZ", true },
            
            // Смешанные языки (должны игнорировать не-английские буквы)
            { "The quick brown fox jumps over the lazy dog Привет мир", true },
            { "Съешь же ещё этих мягких французских булок, да выпей чаю", false },
            
            // Панграмма с минимальным количеством букв
            { "Cwm fjord bank glyphs vext quiz", true }, // известная короткая панграмма
        };
    }

    [Theory]
    [MemberData(nameof(SpecificLetterMissingTestData))]
    public void IsEnglishPangram_WithSpecificMissingLetter_ShouldReturnFalse(char missingLetter)
    {
        // Arrange
        string allLetters = "abcdefghijklmnopqrstuvwxyz";
        string textWithoutLetter = allLetters.Replace(missingLetter.ToString(), "");

        // Act
        bool result = TextUtil.IsEnglishPangram(textWithoutLetter);

        // Assert
        Assert.False(result);
    }

    public static TheoryData<char> SpecificLetterMissingTestData()
    {
        return new TheoryData<char>
        {
            'a', 'b', 'c', 'x', 'y', 'z' // проверяем разные буквы
        };
    }

    [Fact]
    public void IsEnglishPangram_WithAllLettersUpperCase_ShouldReturnTrue()
    {
        // Arrange
        string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // Act
        bool result = TextUtil.IsEnglishPangram(text);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsEnglishPangram_WithMixedCaseLetters_ShouldReturnTrue()
    {
        // Arrange
        string text = "AbCdEfGhIjKlMnOpQrStUvWxYz";

        // Act
        bool result = TextUtil.IsEnglishPangram(text);

        // Assert
        Assert.True(result);
    }
}