/// �ݒ�t�@�C���̎��̃N���X
/// ��{�I�ɂ̓Z�[�u�f�[�^�ƈ����͓���
/// Enum�^�͎g���Ȃ��̂ŃV���A���C�Y�������Ɍォ��ϊ����Ă���܂�
using System;
using UnityEngine;

public enum Language
{
    Unknown,
    JP,
    US
}

[Serializable]
public class ConfigData
{
    //��������� ������ȃt���O�Ȃ̂Őݒ�t�@�C���Ɋ܂߂Ȃ�
    //�f�[�^�����݂����A�t�@�C�����쐬(���Z�b�g)���ꂽ�u���ځv�̂ݐ^�ɂȂ�܂�
    [NonSerialized]
    public bool IsReset = false;

    //����ݒ�@���f�V���A���C�Y�o���Ȃ��̂Őݒ�t�@�C���Ɋ܂߂Ȃ�
    [NonSerialized]
    public Language language = Language.JP;

    //����ݒ�̃f�V���A���C�Y�p
    public string languageString = "JP";

    //�Z�[�u�f�[�^�̃t�@�C���p�X
    public string dataFilePath = "default";

    //�}�X�^�[�{�����[��
    public int masterVolume = 10;

    //�T�E���h�G�t�F�N�g�{�����[��
    public int soundEffectVolume = 5;

    //�o�b�N�O���E���h�~���[�W�b�N�{�����[��
    public int backGroundMusicVolume = 5;
}