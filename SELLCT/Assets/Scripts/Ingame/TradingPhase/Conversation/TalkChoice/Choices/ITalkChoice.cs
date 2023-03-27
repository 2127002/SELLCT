using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITalkChoice
{
    /// <summary>
    /// �I����ID
    /// </summary>
    int Id { get; }

    /// <summary>
    /// �\�������e�L�X�g
    /// </summary>
    string Text { get; }

    /// <summary>
    /// ���̑I�������L������������Ԃ�
    /// </summary>
    bool Enabled { get; }

    /// <summary>
    /// �I�������ꍇ�̏���
    /// </summary>
    void Select();

    /// <summary>
    /// �G�������g�ɂ��L���ɂ��ꂽ�ۂ̏���
    /// </summary>
    void Enable();

    /// <summary>
    /// �G�������g�ɂ�薳���ɂ��ꂽ�ۂ̏���
    /// </summary>
    void Disable();
}
