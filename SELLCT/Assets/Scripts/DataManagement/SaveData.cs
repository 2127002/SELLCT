/// �Z�[�u�f�[�^�̎��̃N���X
/// float�^��Unity�Ǝ��̌^�͎g����
/// int�Adouble�Astring�Abool�����g����Ɗo���Ă����Ă�������
/// ����float���g���Ȃ��͖̂Y�ꂪ���Ȃ̂Œ���
/// �l�X�g�����I�u�W�F�N�g�^���g���邩�͖�����
///
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public bool[] hasCollectedEndings = new bool[64];

    //���L�͏������̗�ł��B
    //public int a = 0;
    //public string b = "string";
    //public double c = 3.14d;
    //public bool d = false;
}