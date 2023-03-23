using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringManager
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
    /// �\���p�G�������g�iE17�`E21�j���������Ă��邩�Bindex�ԍ���E17 = 0, E18 = 1, ... , E21 = 4
    /// </summary>
    public static bool[] hasElements = new bool[5];

    private static readonly List<char> conformString = new();

    /// <summary>
    /// �G�������g�̌��ʂŕύX�����\��������ɕύX���܂��BO(N)���x������܂��B
    /// </summary>
    /// <param name="s">�ύX������������</param>
    /// <returns>�ύX��̕�����</returns>
    public static string ToDisplayString(this string s)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;

        int size = s.Length;

        conformString.Clear();

        for (int i = 0; i < size; i++)
        {
            char c = s[i];
            char add;

            //����
            if (c is >= '0' and <= '9')
            {
                add = hasElements[0] ? c : ' ';
            }
            //����
            else if (c >= 0x4E00 && c <= 0x9FFF)
            {
                add = hasElements[1] ? c : ' ';
            }
            //�Ђ炪��
            else if (c >= 0x3040 && c <= 0x309F)
            {
                add = hasElements[2] ? c : ' ';
            }
            //�J�^�J�i
            else if ((c >= 0x30A1 && c <= 0x30FF) || (c >= 0x31F0 && c <= 0x31FF) || (c >= 0xFF66 && c <= 0xFF9F))
            {
                add = hasElements[3] ? c : ' ';
            }
            //�A���t�@�x�b�g
            else if (c >= 65 && c <= 90 || c >= 97 && c <= 122)
            {
                add = hasElements[4] ? c : ' ';
            }
            //���̑�
            else
            {
                add = c;
            }

            conformString.Add(add);
        }

        return new(conformString.ToArray());
    }
}
