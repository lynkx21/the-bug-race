public static class UIHelper
{
    public const int LETTER_OFFSET = 65;
    public const int NUMBER_OFFSET = 22;

    public static string WriteStringToFont(string input)
    {
        string output = "";
        for (int i = 0; i < input.Length; i++)
        {
            output += GetFont(input[i]);
        }
        return output;
    }

    public static string GetFont(char c)
    {
        if (char.IsLetter(c))
        {
            return $"<sprite=\"font\" index={(int)c - LETTER_OFFSET}>";
        }
        else if (char.IsDigit(c))
        {
            return $"<sprite=\"font\" index={(int)c - NUMBER_OFFSET}>";
        }
        else
        {
            return c.ToString();
        }
    }
}
