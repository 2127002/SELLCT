using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringManager
{
    public enum Element
    {
        // 外部でintにキャストして使っているので、整数を設定しておきましょう
        E17 = 0,
        E18 = 1,
        E19 = 2,
        E20 = 3,
        E21 = 4,

        COUNT = 5
    }

    /// <summary>
    /// 表示用エレメント（E17～E21）を所持しているか。index番号はE17 = 0, E18 = 1, ... , E21 = 4
    /// </summary>
    public static bool[] hasElements = new bool[(int)Element.COUNT];

    private static readonly List<char> conformString = new();

    /// <summary>
    /// エレメントの効果で変更される表示文字列に変更します。O(N)程度かかります。
    /// </summary>
    /// <param name="s">変更したい文字列</param>
    /// <returns>変更後の文字列</returns>
    public static string ToDisplayString(this string s)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;

        int size = s.Length;

        conformString.Clear();

        for (int i = 0; i < size; i++)
        {
            char c = s[i];
            char add;

            //数字
            if (c is >= '0' and <= '9')
            {
                add = hasElements[(int)Element.E17] ? c : ' ';
            }
            //漢字
            else if (c >= 0x4E00 && c <= 0x9FFF)
            {
                add = hasElements[(int)Element.E18] ? c : ' ';
            }
            //ひらがな
            else if (c >= 0x3040 && c <= 0x309F)
            {
                add = hasElements[(int)Element.E19] ? c : ' ';
            }
            //カタカナ
            else if ((c >= 0x30A1 && c <= 0x30FF) || (c >= 0x31F0 && c <= 0x31FF) || (c >= 0xFF66 && c <= 0xFF9F))
            {
                add = hasElements[(int)Element.E20] ? c : ' ';
            }
            //アルファベット
            else if (c >= 65 && c <= 90 || c >= 97 && c <= 122)
            {
                add = hasElements[(int)Element.E21] ? c : ' ';
            }
            //その他
            else
            {
                add = c;
            }

            conformString.Add(add);
        }

        return new(conformString.ToArray());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public static string ToInBracketsText(this string s)
    {
        string str = "";
        int firstIndex = s.IndexOf('《');
        int lastIndex = s.IndexOf('》');
        if (firstIndex == -1 || lastIndex == -1) throw new System.NotImplementedException("《》のいずれか、あるいは両方が含まれていません。\n"+s);
        
        str += s.Substring(firstIndex + 1, lastIndex - firstIndex - 1);

        return str;
    }
}
