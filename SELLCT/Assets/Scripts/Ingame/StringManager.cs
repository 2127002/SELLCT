using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringManager
{
    public enum Element
    {
        // �O����int�ɃL���X�g���Ďg���Ă���̂ŁA������ݒ肵�Ă����܂��傤
        E17 = 0,
        E18 = 1,
        E19 = 2,
        E20 = 3,
        E21 = 4,

        COUNT = 5
    }

    /// <summary>
    /// �\���p�G�������g�iE17�`E21�j���������Ă��邩�Bindex�ԍ���E17 = 0, E18 = 1, ... , E21 = 4
    /// </summary>
    public static bool[] hasElements = new bool[(int)Element.COUNT];

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
                add = hasElements[(int)Element.E17] ? c : ' ';
            }
            //����
            else if (c >= 0x4E00 && c <= 0x9FFF)
            {
                add = hasElements[(int)Element.E18] ? c : ' ';
            }
            //�Ђ炪��
            else if (c >= 0x3040 && c <= 0x309F)
            {
                add = hasElements[(int)Element.E19] ? c : ' ';
            }
            //�J�^�J�i
            else if ((c >= 0x30A1 && c <= 0x30FF) || (c >= 0x31F0 && c <= 0x31FF) || (c >= 0xFF66 && c <= 0xFF9F))
            {
                add = hasElements[(int)Element.E20] ? c : ' ';
            }
            //�A���t�@�x�b�g
            else if (c >= 65 && c <= 90 || c >= 97 && c <= 122)
            {
                add = hasElements[(int)Element.E21] ? c : ' ';
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public static string ToInBracketsText(this string s)
    {
        string str = "";
        int firstIndex = s.IndexOf('�s');
        int lastIndex = s.IndexOf('�t');
        if (firstIndex == -1 || lastIndex == -1) throw new System.NotImplementedException("�s�t�̂����ꂩ�A���邢�͗������܂܂�Ă��܂���B\n"+s);
        
        str += s.Substring(firstIndex + 1, lastIndex - firstIndex - 1);

        return str;
    }
}
