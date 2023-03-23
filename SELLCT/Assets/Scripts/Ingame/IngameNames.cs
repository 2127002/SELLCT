using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �C���Q�[���̖��O�̃��X�g������t�@�[�X�g�R���N�V�����N���X�ł��B
/// </summary>
public class IngameNames : MonoBehaviour
{
    readonly List<string> _names = new();

    public void Add(string name)
    {
        //���O����Ȃ瓮�삳���Ȃ�
        if (string.IsNullOrEmpty(name)) return;

        _names.Add(name);
    }

    public void AddRange(IEnumerable<string> names)
    {
        //�s���l�̓ǂݎ��̂��߁AAddRange�ł͂Ȃ����O��Add���Ăяo���܂��B
        foreach (var item in names)
        {
            Add(item);
        }
    }

    public IReadOnlyList<string> Names => _names;
}
