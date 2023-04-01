using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExplorationNextButtonHandler : MonoBehaviour,ISelectableHighlight
{
    //このようにDetectorにわざわざ分けているのは、interfaceのメソッドがpublicになるからです。
    //外部から意図しないタイミングで呼ばれることを避けるため回りくどい手を使っています。
    [SerializeField] LeftClickDetector _clickDetector = default!;
    [SerializeField] PointerEnterDetector _enterDetector = default!;
    [SerializeField] PointerExitDetector _exitDetector = default!;
    [SerializeField] SubmitDetector _submitDetector = default!;
    [SerializeField] SelectDetector _selectDetector = default!;
    [SerializeField] Selectable _selectable = default!;

    [SerializeField] bool _isFirstSelectable;

    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] Floor01Condition _floor01 = default!;

    Color _defalutSelectColor = default!;

    private void Awake()
    {
        //購読
        _clickDetector.AddListener(HandleClick);
        _enterDetector.AddListener(HandleEnter);
        _exitDetector.AddListener(HandleExit);
        _submitDetector.AddListener(HandleSubmit);
        _selectDetector.AddListener(HandleSelect);

        //わかりやすくするため仮に選択時の色を赤に変更。今後の変更推奨
        _defalutSelectColor = _selectable.colors.selectedColor;
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

    private void HandleClick()
    {
        OnSubmit();
    }

    private void OnSubmit()
    {
        //フェーズ終了を知らせる
        EventSystem.current.SetSelectedGameObject(null);

        _phaseController.CompleteExplorationPhase();
        _floor01.OnNextButtonPressed();
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
        OnSubmit();
    }

    private void HandleSelect()
    {
        //TODO:SE1の再生
    }

    public void EnableHighlight()
    {
        var selectableColors = _selectable.colors;
        selectableColors.selectedColor = _defalutSelectColor;
        _selectable.colors = selectableColors;
    }

    public void DisableHighlight()
    {
        var selectableColors = _selectable.colors;

        //元の色を保存しておく
        _defalutSelectColor = selectableColors.selectedColor;

        //ハイライトを消す
        //実際はハイライト色を通常色に変えてるだけ
        selectableColors.selectedColor = selectableColors.normalColor;
        _selectable.colors = selectableColors;
    }
}
