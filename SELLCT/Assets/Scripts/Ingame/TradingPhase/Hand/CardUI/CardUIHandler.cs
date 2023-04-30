using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Selectableのinteractableを変更するタイミング
/// </summary>
public enum InteractableChange
{
    Element,
    Money,
    CurrentCard,

    Max
}

public class CardUIHandler : MonoBehaviour, IPointerDownHandler,IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, ISubmitHandler, ISelectHandler, IDeselectHandler, ISelectableHighlight
{
    enum EntityType
    {
        Player,
        Trader,

        [InspectorName("")]
        Invalid,
    }

    [SerializeField] CardUIView _cardUIView = default!;

    //デッキと手札の管理者。Subはメインの逆のmediator。
    [SerializeField] DeckMediator _deckMediator = default!;
    [SerializeField] DeckMediator _subDeckMediator = default!;

    //選択肢
    [SerializeField] Selectable _selectable = default!;

    [SerializeField] TraderController _traderController = default!;
    [SerializeField] PhaseController _phaseController = default!;

    [SerializeField] ConversationController _conversationController = default!;

    [SerializeField] MoneyPossessedController _moneyPossessedController = default!;

    [SerializeField] Card _buyOrSell = default!;

    [SerializeField] EntityType _entityType;

    Color _defaultSelectColor = default!;

    //selectableの有効かを保存
    readonly bool[] _interactables = new bool[(int)InteractableChange.Max];

    public IReadOnlyList<Image> CardImages => _cardUIView.CardImages;

    private void Reset()
    {
        _cardUIView = GetComponent<CardUIView>();
        _moneyPossessedController = FindObjectOfType<MoneyPossessedController>();
        _traderController = FindObjectOfType<TraderController>();
        _conversationController = FindObjectOfType<ConversationController>();
        _selectable = GetComponent<Selectable>();
    }

    private void Awake()
    {
        _defaultSelectColor = _selectable.colors.selectedColor;
        _phaseController.OnTradingPhaseStart.Add(OnGenerate);

        if (_entityType == EntityType.Player)
        {
            //プレイヤーはお金に関係なく売却可能
            _interactables[(int)InteractableChange.Money] = true;
        }
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseStart.Remove(OnGenerate);
    }

    private void OnSubmit()
    {
        Card card = _deckMediator.GetCardAtCardUIHandler(this);
        if (card.Id < 0) throw new ArgumentNullException("選択したカードがNullです。");

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
    }

    private void OnBuy(Card purchasedCard)
    {
        try
        {
            _moneyPossessedController.DecreaseMoney(purchasedCard.Price);
        }
        catch (NegativeMoneyException)
        {
            return;
        }

        //そのカードを手札からなくす
        _deckMediator.RemoveHandCard(purchasedCard);

        //※手札補充は行わない
        //商人の手札購入処理
        _traderController.CurrentTrader.OnPlayerBuy();

        //プレイヤー購入山札に追加する
        _subDeckMediator.AddBuyingDeck(purchasedCard);

        //Selectableの位置を切り替える
        SetNextSelectable();

        //カードの購入時効果を発動する
        purchasedCard.Buy();

        //会話する
        _conversationController.OnBuy(purchasedCard).Forget();

        //手札整理
        _deckMediator.UpdateCardSprites();
    }

    private void OnSell(Card soldCard)
    {
        _moneyPossessedController.IncreaseMoney(soldCard.Price);

        //そのカードを手札からなくし、新たにカードを引く
        _deckMediator.RemoveHandCard(soldCard);
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

        //会話する
        _conversationController.OnSell(soldCard).Forget();

        //手札整理
        _deckMediator.UpdateCardSprites();
    }

    private void SetNextSelectable()
    {
        Selectable next = _selectable.FindSelectableOnLeft();

        //??演算子はUnityオブジェクトに対して非推奨らしいのでネストさせます
        if (next == null)
        {
            next = _selectable.FindSelectableOnRight();

            if (next == null)
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
        //一度全部UIを消す
        _cardUIView.DisableAllCardUIs();

        //カードがnull相当だった場合、早期リターン
        if (card.Id < 0)
        {
            //選択できない状態にする
            DisableSelectability(InteractableChange.CurrentCard);

            return;
        }

        //選択可能状態を変更する
        EnabledSelectebility(InteractableChange.CurrentCard);

        //UIを変更する
        _cardUIView.SetCardSprites(card);
    }

    /// <summary>
    /// Selectableを有効にする
    /// </summary>
    public void EnabledSelectebility(InteractableChange timing)
    {
        _interactables[(int)timing] = true;

        //選択可能状態を変更する
        bool allInteractablesIsOk = !_interactables.Contains(false);
        _selectable.interactable = allInteractablesIsOk;

        if (!allInteractablesIsOk) return;
        _cardUIView.OnSelectableEnabled(_selectable.colors.normalColor);
    }

    /// <summary>
    /// Selectableを無効にする
    /// </summary>
    public void DisableSelectability(InteractableChange timing)
    {
        _selectable.interactable = false;
        _interactables[(int)timing] = false;

        _cardUIView.OnSelectableDisabled(_selectable.colors.disabledColor);
    }

    /// <summary>
    /// オブジェクト生成時に行う処理
    /// </summary>
    public void OnGenerate()
    {
        if (_buyOrSell.ContainsPlayerDeck)
        {
            EnabledSelectebility(InteractableChange.Element);
        }
        else
        {
            DisableSelectability(InteractableChange.Element);
        }

        //このオブジェクトが選択中なら実行しない
        if (gameObject == EventSystem.current.currentSelectedGameObject) return;

        _cardUIView.ResetImagesSizeDelta();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData == null) throw new NullReferenceException();
        if (!_selectable.interactable) return;

        //TODO：今後ここに具体的なカーソルをかざした際の処理を追加する
        _selectable.Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData == null) throw new NullReferenceException();
        if (!_selectable.interactable) return;

        //TODO：今後ここに具体的なカーソルを外した際の処理を追加する
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        if (eventData == null) throw new NullReferenceException();
        if (!_selectable.interactable) return;

        //同一処理のため以下の処理を呼ぶだけにします。
        //クリック時の仕様と差異が発生したら修正してください。
        OnSubmit();
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (eventData == null) throw new NullReferenceException();
        if (!_selectable.interactable) return;

        //TODO:SE1の再生

        _cardUIView.SetImagesSizeDelta();

        //テキスト更新
        Card card = _deckMediator.GetCardAtCardUIHandler(this);
        _conversationController.OnSelect(card).Forget();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (eventData == null) throw new NullReferenceException();

        _cardUIView.ResetImagesSizeDelta();
    }

    public void EnableHighlight()
    {
        //もとに戻す
        var selectableColors = _selectable.colors;
        selectableColors.selectedColor = _defaultSelectColor;
        _selectable.colors = selectableColors;
    }

    public void DisableHighlight()
    {
        var selectableColors = _selectable.colors;

        //元の色を保存しておく
        _defaultSelectColor = selectableColors.selectedColor;

        //ハイライトを消す
        //実際はハイライト色を通常色に変えてるだけ
        selectableColors.selectedColor = selectableColors.normalColor;
        _selectable.colors = selectableColors;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData == null) throw new NullReferenceException();


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData == null) throw new NullReferenceException();

        if (!_selectable.interactable) return;

        //左クリック以外行わない
        if (eventData.button != PointerEventData.InputButton.Left) return;

        //同一処理のため以下の処理を呼ぶだけにします。
        //Submit時の仕様と差異が発生したら修正してください。
        OnSubmit();
    }
}