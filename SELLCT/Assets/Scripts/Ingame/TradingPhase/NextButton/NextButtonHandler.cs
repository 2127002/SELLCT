using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NextButtonHandler : MonoBehaviour
{
    //このようにDetectorにわざわざ分けているのは、interfaceのメソッドがpublicになるからです。
    //外部から意図しないタイミングで呼ばれることを避けるため回りくどい手を使っています。
    [SerializeField] LeftClickDetector _clickDetector;
    [SerializeField] PointerEnterDetector _enterDetector;
    [SerializeField] PointerExitDetector _exitDetector;
    [SerializeField] SubmitDetector _submitDetector;
    [SerializeField] SelectDetector _selectDetector;
    [SerializeField] Selectable _selectable;

    [SerializeField] TradingPhaseCompletionHandler _phaseCompletionHandler;

    [SerializeField] bool _isFirstSelectable;

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

    private void HandleClick()
    {
        //フェーズ終了を知らせる
        _phaseCompletionHandler.OnComplete();
    }

    private void HandleEnter()
    {
        //TODO：今後ここに具体的なカーソルをかざした際の処理を追加する
        _selectable.Select();
    }

    private void HandleExit()
    {
        //TODO：今後ここに具体的なカーソルを外した際の処理を追加する
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void HandleSubmit()
    {
        //同一処理のため以下の処理を呼ぶだけにします。クリック時の仕様と差異が発生したら修正してください。
        HandleClick();
    }

    private void HandleSelect()
    {
        //TODO:SE1の再生
    }
}
