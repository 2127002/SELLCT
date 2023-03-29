using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface�Ƃ��Ď����������ł����A��������ƃC���X�y�N�^�[�ŊǗ��ł��Ȃ�����
//�������ʂ������ۃN���X�ɂ��܂��B
public abstract class Card : MonoBehaviour
{
    [SerializeField] protected CardParameter _parameter = default!;
    [SerializeField] protected MoneyPossessedController _controller = default!;
    [SerializeField] protected Sprite _baseSprite = default!;
    [SerializeField] protected Sprite _number = default!;
    [SerializeField] protected Sprite _chineseCharacters = default!;
    [SerializeField] protected Sprite _hiragana = default!;
    [SerializeField] protected Sprite _katakana = default!;
    [SerializeField] protected Sprite _alphabet = default!;
    [SerializeField] protected HandMediator _handMediator = default!;

    protected readonly List<Sprite> result = new();

    public virtual string CardName => _parameter.GetName();
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
    public virtual bool IsDisposedOfAfterSell => _parameter.IsDisposedOfAfterSell();
    public virtual int Rarity => _parameter.Rarity();
    public virtual bool ContainsPlayerDeck => _handMediator.ContainsCard(this);
    public abstract void Buy();
    public abstract void Sell();
    /// <summary>
    /// �T���t�F�[�Y�ɂ�����U6�{�^���������̌���
    /// </summary>
    public abstract void Passive();
}