using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���ɂ����邷�ׂĂ̑I�������Ǘ�����N���X
/// </summary>
public class ChoicesManager : MonoBehaviour
{
    readonly List<ITalkChoice> _choices = new();

    public void Add(ITalkChoice choice)
    {
        _choices.Add(choice);
    }

    public void Enable(int id)
    {
        _choices[id].Enable();
    }

    public void Disable(int id)
    {
        _choices[id].Disable();
    }

    public ITalkChoice GetTalkChoice(int id)
    {
        if (_choices.Count <= id || _choices[id] == null) throw new System.NullReferenceException("�o�^����Ă��Ȃ�ID" + id + "���I������܂����B�ݒ���������Ă��������B");

        return _choices[id];
    }
}
