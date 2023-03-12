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
    [SerializeField] LeftClickDetector _clickDetector;
    [SerializeField] PointerEnterDetector _enterDetector;
    [SerializeField] PointerExitDetector _exitDetector;
    [SerializeField] SubmitDetector _submitDetector;
    [SerializeField] SelectDetector _selectDetector;

    [SerializeField] DeckMediator _deckMediator;

    [SerializeField] Selectable _selectable;

    [SerializeField] TraderController _traderController;

    [SerializeField] EntityType _entityType;
    [SerializeField] bool _isFirstSelectable;

    Card _card;
    EventSystem _eventSystem;

    private void Awake()
    {
        _eventSystem = EventSystem.current;

        //購読
        _clickDetector.AddListener(HandleClick);
        _enterDetector.AddListener(HandleEnter);
        _exitDetector.AddListener(HandleExit);
        _submitDetector.AddListener(HandleSubmit);
        _selectDetector.AddListener(HandleSelect);

        SetFirstSelectable();

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
    }

    private void SetFirstSelectable()
    {
        //初期選択のチェックボックスがtrueだったら登録
        if (!_isFirstSelectable) return;

        if (_eventSystem.currentSelectedGameObject != null)
        {
            Debug.LogWarning("すでに別のオブジェクトが選択されています。" + gameObject + "の登録は棄却されました。正しい仕様を確認してください。" + _eventSystem.currentSelectedGameObject);
            return;
        }

        _eventSystem.SetSelectedGameObject(_selectable.gameObject);
    }

    //左クリック時処理
    private void HandleClick()
    {
        //EntityTypeに適した処理を呼ぶ。
        if (_entityType == EntityType.Player)
        {
            _card.Sell();

            //売る処理のみ下記処理を行う。
            OnSell();
        }
        else if (_entityType == EntityType.Trader)
        {
            _card.Buy();

            //買う処理のみ下記処理を行う。
            OnBuy();
        }
        else throw new System.NotImplementedException();

        //手札整理
        _deckMediator.RearrangeCardSlots();
    }

    private void OnBuy()
    {
        //商人の手札購入処理
        _traderController.CurrentTrader.OnPlayerSell(_card);

        SetNextSelectable();

        //手札補充は行わない
        InsertCard(EEX_null.Instance);
    }

    private void OnSell()
    {
        //商人の手札売却処理
        _traderController.CurrentTrader.OnPlayerSell(_card);
        
        //商人山札に追加する
        if (!_card.IsDisposedOfAfterSell)
        {
            _deckMediator.AddBuyingDeck(_card);
        }

        //手札から削除し、新たにカードを引く
        _deckMediator.RemoveHandCard(_card);
        InsertCard(_deckMediator.TakeDeckCard());
    }

    private void SetNextSelectable()
    {
        Selectable next = _selectable.FindSelectableOnLeft();

        //nullだった場合こちらに遷移する
        next ??= _selectable.FindSelectableOnRight();
        next ??= _selectable.FindSelectableOnUp();
        next ??= _selectable.FindSelectableOnDown();

        if (next != null) _eventSystem.SetSelectedGameObject(next.gameObject);
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
    }

    /// <summary>
    /// カードを挿入する
    /// </summary>
    /// <param name="card">挿入するカード</param>
    public void InsertCard(Card card)
    {
        _card = card;

        bool isNormalCard = _card is not EEX_null;

        _selectable.image.enabled = isNormalCard;
        _selectable.interactable = isNormalCard;

        if (isNormalCard) _selectable.image.sprite = card.CardSprite;
    }

    public Card GetCardName()
    {
        return _card;
    }
    public bool NullCheck()
    {
        return _card is EEX_null;
    }
}
