using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringManager
{
    public enum Element
    {
        E17,
        E18,
        E19,
        E20,
        E21,
    }

    /// <summary>
    /// 表示用エレメント（E17～E21）を所持しているか。index番号はE17 = 0, E18 = 1, ... , E21 = 4
    /// </summary>
    public static bool[] hasElements = new bool[5];

    private static List<char> conformString = new();

    public static string ToDisplayString(string s)
    {
        int size = s.Length;

        conformString.Clear();

        for (int i = 0; i < size; i++)
        {
            char c = s[i];

            char add;
            //数字
            if (c is >= '0' and <= '9')
            {
                add = hasElements[0] ? c : ' ';
            }
            //漢字
            else if (c >= 0x4E00 && c <= 0x9FFF)
            {
                add = hasElements[1] ? c : ' ';
            }
            //ひらがな
            else if (c >= 0x3040 && c <= 0x309F)
            {
                add = hasElements[2] ? c : ' ';
            }
            //カタカナ
            else if ((c >= 0x30A1 && c <= 0x30FF) || (c >= 0x31F0 && c <= 0x31FF) || (c >= 0xFF66 && c <= 0xFF9F))
            {
                add = hasElements[3] ? c : ' ';
            }
            //アルファベット
            else if (c >= 65 && c <= 90 || c >= 97 && c <= 122)
            {
                add = hasElements[4] ? c : ' ';
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
}
