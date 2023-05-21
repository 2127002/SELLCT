using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface�Ƃ��Ď����������ł����A��������ƃC���X�y�N�^�[�ŊǗ��ł��Ȃ�����
//�������ʂ������ۃN���X�ɂ��܂��B
public abstract class Card : MonoBehaviour
{
    [Header("�J�[�h�̃p�����[�^�ꗗ�ł��B���x���f�U�C���͎�ɂ��̓����̒l��ύX���Ă��������B")]
    [SerializeField] protected CardParameter _parameter = default!;

    [Header("�J�[�h�C���X�g�ł��B�Y������C���X�g���Ȃ��ꍇ��None�̂܂܂ɂ��Ă��������B")]
    [SerializeField] Sprite _baseSprite = default!;
    [SerializeField] Sprite _number = default!;
    [SerializeField] Sprite _chineseCharacters = default!;
    [SerializeField] Sprite _hiragana = default!;
    [SerializeField] Sprite _katakana = default!;
    [SerializeField] Sprite _alphabet = default!;

    [Header("�v���C���[�̎�D�Ǘ�����I�u�W�F�N�g��I�����Ă��������B")]
    [SerializeField] protected HandMediator _handMediator = default!;

    readonly List<Sprite> result = new();

    protected virtual void Reset()
    {
        _parameter = GetComponent<CardParameter>();
    }

    /// <summary>
    /// �J�[�hID�BEEX_NULL��-1�A���̑��̓G�������g�ԍ��ŊǗ����Ă��܂��B
    /// </summary>
    public abstract int Id { get; }

    /// <summary>
    /// �J�[�hID���N���X������擾�ł���悤�ɂ���B�upublic static new Id() => ID�ԍ�; �v�ŏ㏑�����ĉ�����
    /// </summary>
    public static int Id() => -1;

    /// <summary>
    /// �J�[�h�̖��O
    /// </summary>
    public virtual string CardName => _parameter.GetName();
    /// <summary>
    /// �J�[�h�̃C���X�g���i�[����Ă���B���Ԃ́Abase��擪�Ɏc��̓G�������g�ԍ���
    /// </summary>
    public virtual IReadOnlyList<Sprite> CardSprite
    {
        get
        {
            //������
            if (result.Count == 0)
            {
                result.Add(_baseSprite);
                result.Add(_number);
                result.Add(_chineseCharacters);
                result.Add(_hiragana);
                result.Add(_katakana);
                result.Add(_alphabet);
            }

            return result;
        }
    }
    /// <summary>
    /// ���p��ɏ��ł��邩�ǂ���
    /// </summary>
    public virtual bool IsDisposedOfAfterSell => _parameter.IsDisposedOfAfterSell();
    /// <summary>
    /// �J�[�h�̃��A���e�B
    /// </summary>
    public virtual int Rarity => _parameter.Rarity();
    /// <summary>
    /// �v���C���[�̃f�b�L�Ɋ܂܂�邩�ǂ���
    /// </summary>
    public virtual bool ContainsPlayerDeck => _handMediator.ContainsCard(this);
    /// <summary>
    /// �v���C���[�̃f�b�L�Ɋ܂܂�閇��
    /// </summary>
    public virtual int FindAll => _handMediator.FindAll(this);
    /// <summary>
    /// ���z(�w���E���p)
    /// </summary>
    public virtual Money Price => _parameter.GetMoney();

    /// <summary>
    /// �w��������
    /// </summary>
    public abstract void Buy();
    /// <summary>
    /// ���p������
    /// </summary>
    public abstract void Sell();
    /// <summary>
    /// �T���t�F�[�Y�ɂ�����U6�{�^���������̌���
    /// </summary>
    public abstract void OnPressedU6Button();
}