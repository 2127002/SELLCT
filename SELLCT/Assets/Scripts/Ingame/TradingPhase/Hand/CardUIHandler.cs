using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CardUIHandler : MonoBehaviour
{
    enum EntityType
    {
        Player,
        Trader,

        [InspectorName("")]
        Invalid,
    }

    //このようにDetectorにわざわざ分けているのは、interfaceのメソッドがpublicになるからです。
    //外部から意図しないタイミングで呼ばれることを避けるため回りくどい手を使っています。
    [SerializeField] LeftClickDetector _clickDetector = default!;
    [SerializeField] PointerEnterDetector _enterDetector = default!;
    [SerializeField] PointerExitDetector _exitDetector=default!;
    [SerializeField] SubmitDetector _submitDetector=default!;
    [SerializeField] SelectDetector _selectDetector = default!;
    [SerializeField] DeselectDetector _deselectDetector = default!;

    //デッキと手札の管理者。Subはメインの逆のmediator。
    [SerializeField] DeckMediator _deckMediator = default!;
    [SerializeField] DeckMediator _subDeckMediator = default!;

    //選択肢
    [SerializeField] Selectable _selectable = default!;

    //カード表示
    [SerializeField] List<Image> _images = default!;

    [SerializeField] PhaseController _phaseController = default!;

    [SerializeField] TraderController _traderController = default!;

    [SerializeField] EntityType _entityType;
    [SerializeField] bool _isFirstSelectable;

    Card _card = default!;

    //選択時画像サイズ補正値
    const float CORRECTION_SIZE = 1.25f;
    static readonly Vector2 correction = new(CORRECTION_SIZE, CORRECTION_SIZE);
    static readonly Vector2 recorrection = new(1f / CORRECTION_SIZE, 1f / CORRECTION_SIZE);

    private void Awake()
    {
        //購読
        _clickDetector.AddListener(HandleClick);
        _enterDetector.AddListener(HandleEnter);
        _exitDetector.AddListener(HandleExit);
        _submitDetector.AddListener(HandleSubmit);
        _selectDetector.AddListener(HandleSelect);
        _deselectDetector.AddListener(HandleDeselect);
        _phaseController.onTradingPhaseStart += SetFirstSelectable;

        //わかりやすくするため仮に選択時の色を赤に変更。今後の変更推奨
        var b = _selectable.colors;
        b.selectedColor = Color.red;
        _selectable.colors = b;
    }

    private void OnDestroy()
    {
        //購読解除
        _clickDetector.RemoveListener(HandleClick);
        _enterDetector.RemoveListener(HandleEnter);
        _exitDetector.RemoveListener(HandleExit);
        _submitDetector.RemoveListener(HandleSubmit);
        _selectDetector.RemoveListener(HandleSelect);
        _deselectDetector.RemoveListener(HandleDeselect);
        _phaseController.onTradingPhaseStart -= SetFirstSelectable;
    }

    private void SetFirstSelectable()
    {
        //初期選択のチェックボックスがtrueだったら登録
        if (!_isFirstSelectable) return;

        if (EventSystem.current.currentSelectedGameObject != null)
        {
            Debug.LogWarning("すでに別のオブジェクトが選択されています。" + gameObject + "の登録は棄却されました。正しい仕様を確認してください。" + EventSystem.current.currentSelectedGameObject);
            return;
        }

        EventSystem.current.SetSelectedGameObject(_selectable.gameObject);

        foreach (var item in _images)
        {
            Vector2 sizeDelta = item.rectTransform.sizeDelta;
            sizeDelta.Scale(correction);
            item.rectTransform.sizeDelta = sizeDelta;
        }
    }

    //左クリック時処理
    private void HandleClick()
    {
        //EntityTypeに適した処理を呼ぶ。
        if (_entityType == EntityType.Player)
        {
            //売る処理のみ下記処理を行う。
            OnSell();
        }
        else if (_entityType == EntityType.Trader)
        {
            //買う処理のみ下記処理を行う。
            OnBuy();
        }
        else throw new System.NotImplementedException();

        //手札整理
        _deckMediator.RearrangeCardSlots();
    }

    private void OnBuy()
    {
        Card purchasedCard = _card;

        //手札補充は行わない
        _deckMediator.RemoveHandCard(purchasedCard);
        InsertCard(EEX_null.Instance);

        //商人の手札購入処理
        _traderController.CurrentTrader.OnPlayerBuy();

        //プレイヤー購入山札に追加する
        _subDeckMediator.AddBuyingDeck(purchasedCard);

        SetNextSelectable();

        purchasedCard.Buy();
    }

    private void OnSell()
    {
        Card selledCard = _card;

        //手札から削除し、新たにカードを引く
        _deckMediator.RemoveHandCard(selledCard);
        InsertCard(_deckMediator.TakeDeckCard());

        //商人の手札売却処理
        _traderController.CurrentTrader.OnPlayerSell(selledCard);

        //商人購入山札に追加する
        if (!selledCard.IsDisposedOfAfterSell)
        {
            _subDeckMediator.AddBuyingDeck(selledCard);
        }

        selledCard.Sell();
    }

    private void SetNextSelectable()
    {
        Selectable next = _selectable.FindSelectableOnLeft();

        //nullだった場合こちらに遷移する
        next ??= _selectable.FindSelectableOnRight();
        next ??= _selectable.FindSelectableOnUp();
        next ??= _selectable.FindSelectableOnDown();

        if (next != null) EventSystem.current.SetSelectedGameObject(next.gameObject);
    }

    //カーソルをかざした際の処理
    private void HandleEnter()
    {
        //TODO：今後ここに具体的なカーソルをかざした際の処理を追加する
        _selectable.Select();
    }

    //カーソルを外した際の処理
    private void HandleExit()
    {
        //TODO：今後ここに具体的なカーソルを外した際の処理を追加する
        EventSystem.current.SetSelectedGameObject(null);
    }

    //決定時の処理
    private void HandleSubmit()
    {
        //同一処理のため以下の処理を呼ぶだけにします。クリック時の仕様と差異が発生したら修正してください。
        HandleClick();
    }

    //選択時の処理
    private void HandleSelect()
    {
        //TODO:SE1の再生
        foreach (var item in _images)
        {
            Vector2 sizeDelta = item.rectTransform.sizeDelta;
            sizeDelta.Scale(correction);
            item.rectTransform.sizeDelta = sizeDelta;
        }
    }

    //選択から外れた時の処理
    private void HandleDeselect()
    {
        foreach (var item in _images)
        {
            Vector2 sizeDelta = item.rectTransform.sizeDelta;
            sizeDelta.Scale(recorrection);
            item.rectTransform.sizeDelta = sizeDelta;
        }
    }

    /// <summary>
    /// カードを挿入する
    /// </summary>
    /// <param name="card">挿入するカード</param>
    public void InsertCard(Card card)
    {
        _card = card;

        //一旦すべての表示を消す
        SetImagesEnabled(false);

        bool isNormalCard = _card is not EEX_null;

        _selectable.interactable = isNormalCard;

        if (isNormalCard)
        {
            SetImagesSprite(card.CardSprite);
        }
    }

    public Card GetCardName()
    {
        return _card;
    }
    public bool NullCheck()
    {
        return _card is EEX_null;
    }

    private void SetImagesEnabled(bool enabled)
    {
        foreach (var image in _images)
        {
            image.enabled = enabled;
        }
    }

    private void SetImagesSprite(IReadOnlyList<Sprite> sprites)
    {
        _images[0].enabled = true;

        for (int i = 0; i < _images.Count; i++)
        {
            if (sprites[i] == null) continue;

            _images[i].sprite = sprites[i];

            //i==0はbaseなため必ず表示
            if (i != 0) _images[i].enabled = StringManager.hasElements[i - 1];
        }
    }
}