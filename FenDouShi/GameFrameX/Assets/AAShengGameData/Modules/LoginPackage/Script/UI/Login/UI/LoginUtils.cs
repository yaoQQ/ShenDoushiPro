using System;
using System.Text;

public class LoginUtils
{
    //获取罗马把数字页字符
    public static string GetStrByServicePage(int page)
    {
       return IntToRoman(page);
    }
    private static readonly (int Value, string Symbol)[] ArabicToRoman = new (int, string)[]
    {
        (1000, "M"), (900, "CM"), (500, "D"), (400, "CD"),
        (100, "C"), (90, "XC"), (50, "L"), (40, "XL"),
        (10, "X"), (9, "IX"), (5, "V"), (4, "IV"), (1, "I")
    };
    private static string IntToRoman(int num)
    {
        if (num < 1 || num > 3999)
            throw new ArgumentOutOfRangeException(nameof(num), "Number must be between 1 and 3999");

        var sb = new StringBuilder();
        foreach (var (value, symbol) in ArabicToRoman)
        {
            while (num >= value)
            {
                sb.Append(symbol);
                num -= value;
            }
        }
        return sb.ToString();
    }
}