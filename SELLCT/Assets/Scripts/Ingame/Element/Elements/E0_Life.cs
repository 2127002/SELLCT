using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class E0_Life : Card
{
    [SerializeField] End_5 end_5;
    public override int Id => 0;

    public override void Buy()
    {
        //TODO:SE301�̍Đ�
        //TODO:��ʑS�̂𖬓�������A�j���[�V����
        //TODO:�e�L�X�g�{�b�N�X���X�V����
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        GameOverChecker();
    }

    private void GameOverChecker()
    {
        //�������ۂɖ���1�����Ȃ��Ȃ�Q�[���I�[�o�[
        if (_handMediator.ContainsCard(this)) return;

        Debug.LogWarning(StringManager.ToDisplayString("�����Ȃ��Ȃ�܂����I�V�[��3�ɑJ�ڂ��鏈���͖������Ȃ��ߑ��s����܂��B"));
        end_5.End_5Transition();
    }
}