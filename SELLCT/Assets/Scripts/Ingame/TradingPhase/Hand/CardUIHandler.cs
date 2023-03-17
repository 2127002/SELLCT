using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CardUIHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler,ISubmitHandler,ISelectHandler,IDeselectHandler
{
    enum EntityType
    {
        Player,
        Trader,

        [InspectorName("")]
        Invalid,
    }

    //デッキと手札の管理者。Subはメインの逆のmediator。
    [SerializeField] DeckMediator _deckMediator = default!;
    [SerializeField] DeckMediator _subDeckMediator = default!;

    //選択肢
    [SerializeField] Selectable _selectable = default!;

    //カード表示
    [SerializeField] List<Image> _cardImages = default!;

    [SerializeField] TraderController _traderController = default!;

    [SerializeField] EntityType _entityType;

    //選択時画像サイズ補正値
    const float CORRECTION_SIZE = 1.25f;
    static readonly Vector3 correction = new(CORRECTION_SIZE, CORRECTION_SIZE, CORRECTION_SIZE);

    public IReadOnlyList<Image> CardImages => _cardImages;

    private void Awake()
    {
        //わかりやすくするため仮に選択時の色を赤に変更。今後の変更推奨
        var b = _selectable.colors;
        b.selectedColor = Color.red;
        _selectable.colors = b;
    }

    private void OnSubmit()
    {
        Card card = _deckMediator.GetCardAtCardUIHandler(this);

        //そのカードを手札からなくす
        _deckMediator.RemoveHandCard(card);

        //EntityTypeに適した処理を呼ぶ。
        if (_entityType == EntityType.Player)
        {
            //売る処理のみ下記処理を行う。
            OnSell(card);
        }
        else if (_entityType == EntityType.Trader)
        {
            //買う処理のみ下記処理を行う。
            OnBuy(card);
        }
        else throw new NotImplementedException();

        //手札整理
        _deckMediator.UpdeteCardSprites();
    }

    private void OnBuy(Card purchasedCard)
    {
        //※手札補充は行わない
        //商人の手札購入処理
        _traderController.CurrentTrader.OnPlayerBuy();

        //プレイヤー購入山札に追加する
        _subDeckMediator.AddBuyingDeck(purchasedCard);

        //Selectableの位置を切り替える
        SetNextSelectable();

        //カードの購入時効果を発動する
        purchasedCard.Buy();
    }

    private void OnSell(Card soldCard)
    {
        //新たにカードを引く
        _deckMediator.DrawCard();

        //商人の手札売却処理
        _traderController.CurrentTrader.OnPlayerSell(soldCard);

        //売却時即時消滅カード以外なら
        if (!soldCard.IsDisposedOfAfterSell)
        {
            //商人購入山札に追加する
            _subDeckMediator.AddBuyingDeck(soldCard);
        }

        //カードの売却時効果を発動する
        soldCard.Sell();
    }

    private void SetNextSelectable()
    {
        Selectable next = _selectable.FindSelectableOnLeft();

        //??演算子はUnityオブジェクトに対して非推奨らしいのでネストさせます
        if (next == null)
        {
            next = _selectable.FindSelectableOnRight();

            if(next == null)
            {
                next = _selectable.FindSelectableOnUp();

                if (next == null)
                {
                    next = _selectable.FindSelectableOnDown();
                    
                    //ここまでやってnullだったら終わり
                    if (next == null) return;
                }
            }
        }

        EventSystem.current.SetSelectedGameObject(next.gameObject);
    }
    
    /// <summary>
    /// カードに応じて表示を切り替える
    /// </summary>
    /// <param name="card">表示したいカード</param>
    public void SetCardSprites(Card card)
    {
        //一旦すべての表示を消す
        DisableAllImages();

        //カードがnull相当だった場合、早期リターン
        if (card is EEX_null)
        {
            //選択できない状態にする
            _selectable.interactable = false;
            return;
        }

        //選択可能な状態にする
        _selectable.interactable = true;

        //0番目はBaseなため必ず表示される
        _cardImages[0].enabled = true;
        _cardImages[0].sprite = card.CardSprite[0];

        //以降の文字要素は、エレメントの所持状況で表示が切り替わる
        for (int i = 1; i < _cardImages.Count; i++)
        {
            //カードに該当文字が無い場合の対応
            if (card.CardSprite[i] == null) continue;

            //Spriteをセットし、エレメントの所持状況で表示を切り替える
            //index番号はbase分がズレている。
            _cardImages[i].sprite = card.CardSprite[i];
            _cardImages[i].enabled = StringManager.hasElements[i - 1];
        }
    }

    private void DisableAllImages()
    {
        foreach (var image in _cardImages)
        {
            image.enabled = false;
        }
    }

    [Obsolete("UnityのEventを受け取って実行されるメソッドです。Eventを受け取る以外の使用は想定されていません。",true)]
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData == null) throw new NullReferenceException();

        //左クリック以外行わない
        if (eventData.button != PointerEventData.InputButton.Left) return;

        //同一処理のため以下の処理を呼ぶだけにします。
        //Submit時の仕様と差異が発生したら修正してください。
        OnSubmit();
    }

    [Obsolete("UnityのEventを受け取って実行されるメソッドです。Eventを受け取る以外の使用は想定されていません。", true)]
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData == null) throw new NullReferenceException();

        //TODO：今後ここに具体的なカーソルをかざした際の処理を追加する

        _selectable.Select();
    }
    
    [Obsolete("UnityのEventを受け取って実行されるメソッドです。Eventを受け取る以外の使用は想定されていません。", true)]
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData == null) throw new NullReferenceException();

        //TODO：今後ここに具体的なカーソルを外した際の処理を追加する
        EventSystem.current.SetSelectedGameObject(null);
    }

    [Obsolete("UnityのEventを受け取って実行されるメソッドです。Eventを受け取る以外の使用は想定されていません。", true)]
    public void OnSubmit(BaseEventData eventData)
    {
        if (eventData == null) throw new NullReferenceException();

        //同一処理のため以下の処理を呼ぶだけにします。
        //クリック時の仕様と差異が発生したら修正してください。
        OnSubmit();
    }

    [Obsolete("UnityのEventを受け取って実行されるメソッドです。Eventを受け取る以外の使用は想定されていません。", true)]
    public void OnSelect(BaseEventData eventData)
    {
        if (eventData == null) throw new NullReferenceException();

        //TODO:SE1の再生

        //拡大率を指定値に変える
        _selectable.image.rectTransform.localScale = correction;
    }

    [Obsolete("UnityのEventを受け取って実行されるメソッドです。Eventを受け取る以外の使用は想定されていません。", true)]
    public void OnDeselect(BaseEventData eventData)
    {
        if (eventData == null) throw new NullReferenceException();

        //拡大率を初期値に戻す
        _selectable.image.rectTransform.localScale = Vector3.one;
    }
}