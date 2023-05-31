using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���������Ȃ��I����
public class NothingChoice:ITalkChoice
{
    public int Id => 2;

    public string Text => "���������Ȃ�";

    public bool Enabled => _enabled;

    bool _enabled;

    public NothingChoice(bool enabled)
    {
        _enabled = enabled;
    }

    public void Disable()
    {
        _enabled = false;
    }

    public void Enable()
    {
        _enabled = true;
    }

    public void Select()
    {
        if (!Enabled)
        {
            //�������͉B�����߁A�\�����邱�Ƃ��Ȃ��B
            throw new System.NotImplementedException("���̍��ڂ�������Ԏ��A�N���b�N����鏈���͒�`����Ă��܂���B");
        }

        Debug.Log(".......(���������Ȃ�)");
    }
}
