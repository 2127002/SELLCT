using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interfaceとして実装したいですが、そうするとインスペクターで管理できないため
//同じ効果を持つ抽象クラスにします。
public abstract class Card : MonoBehaviour
{
    [Header("カードのパラメータ一覧です。レベルデザインは主にこの内部の値を変更してください。")]
    [SerializeField] protected CardParameter _parameter = default!;

    [Header("カードイラストです。該当するイラストがない場合はNoneのままにしてください。")]
    [SerializeField] Sprite _baseSprite = default!;
    [SerializeField] Sprite _number = default!;
    [SerializeField] Sprite _chineseCharacters = default!;
    [SerializeField] Sprite _hiragana = default!;
    [SerializeField] Sprite _katakana = default!;
    [SerializeField] Sprite _alphabet = default!;

    [Header("プレイヤーの手札管理するオブジェクトを選択してください。")]
    [SerializeField] protected HandMediator _handMediator = default!;

    readonly List<Sprite> result = new();

    protected virtual void Reset()
    {
        _parameter = GetComponent<CardParameter>();
    }

    /// <summary>
    /// カードID。EEX_NULLは-1、その他はエレメント番号で管理しています。
    /// </summary>
    public abstract int Id { get; }
    /// <summary>
    /// カードの名前
    /// </summary>
    public virtual string CardName => _parameter.GetName();
    /// <summary>
    /// カードのイラストが格納されている。順番は、baseを先頭に残りはエレメント番号順
    /// </summary>
    public virtual IReadOnlyList<Sprite> CardSprite
    {
        get
        {
            //初期化
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
    /// 売却後に消滅するかどうか
    /// </summary>
    public virtual bool IsDisposedOfAfterSell => _parameter.IsDisposedOfAfterSell();
    /// <summary>
    /// カードのレアリティ
    /// </summary>
    public virtual int Rarity => _parameter.Rarity();
    /// <summary>
    /// プレイヤーのデッキに含まれるかどうか
    /// </summary>
    public virtual bool ContainsPlayerDeck => _handMediator.ContainsCard(this);
    /// <summary>
    /// プレイヤーのデッキに含まれる枚数
    /// </summary>
    public virtual int FindAll => _handMediator.FindAll(this);
    /// <summary>
    /// 金額(購入・売却)
    /// </summary>
    public virtual Money Price => _parameter.GetMoney();

    /// <summary>
    /// 購入時処理
    /// </summary>
    public abstract void Buy();
    /// <summary>
    /// 売却時処理
    /// </summary>
    public abstract void Sell();
    /// <summary>
    /// 探索フェーズにおけるU6ボタン押下時の効果
    /// </summary>
    public abstract void OnPressedU6Button();
}
