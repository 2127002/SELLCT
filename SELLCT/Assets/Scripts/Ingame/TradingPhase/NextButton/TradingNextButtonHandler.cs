using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TradingNextButtonHandler : MonoBehaviour, ISelectableHighlight, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler,ISubmitHandler,ISelectHandler,IDeselectHandler
{
    [SerializeField] Selectable _selectable = default!;

    [SerializeField] PhaseController _phaseController = default!;

    Color _defaultSelectColor = default!;

    private void Awake()
    {
        _defaultSelectColor = _selectable.colors.selectedColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //TODO：今後ここに具体的なカーソルをかざした際の処理を追加する
        _selectable.Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //TODO：今後ここに具体的なカーソルを外した際の処理を追加する
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Submit();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        Submit();
    }

    private void Submit()
    {
        //フェーズ終了を知らせる
        _phaseController.CompleteTradingPhase();
    }

    public void OnSelect(BaseEventData eventData)
    {
        //TODO:SE1の再生
    }

    public void OnDeselect(BaseEventData eventData)
    {
    }

    public void EnableHighlight()
    {
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
}
