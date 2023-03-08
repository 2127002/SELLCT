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

    [SerializeField] HandMediator _handMediator;

    [SerializeField] Selectable _selectable;

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
        //Imageを無効化する
        _selectable.image.enabled = false;

        //クリック時無効にする
        _selectable.interactable = false;

        //EntityTypeに適した処理を呼ぶ。
        if (_entityType == EntityType.Player)
        {
            _card.Sell();
        }
        else if (_entityType == EntityType.Trader)
        {
            _card.Buy();
        }
        else
        {
            throw new System.NotImplementedException();
        }

        //手札から削除し、新たにカードを引く
        _handMediator.RemoveHandCard(_card);
        InsertCard(_handMediator.TakeDeckTopCard());
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
    }
}
