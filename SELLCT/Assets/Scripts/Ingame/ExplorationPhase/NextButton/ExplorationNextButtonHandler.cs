using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExplorationNextButtonHandler : MonoBehaviour, ISelectableHighlight, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, ISubmitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] Selectable _selectable = default!;

    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] Floor01Condition _floor01 = default!;

    Color _defaultSelectColor = default!;

    private void Reset()
    {
        _selectable = GetComponent<Selectable>();
        _phaseController = FindObjectOfType<PhaseController>();
        _floor01 = FindObjectOfType<Floor01Condition>();
    }

    private void Awake()
    {
        _defaultSelectColor = _selectable.colors.selectedColor;
    }

    private void OnSubmit()
    {
        //フェーズ終了を知らせる
        EventSystem.current.SetSelectedGameObject(null);

        _phaseController.CompleteExplorationPhase();
        _floor01.OnNextButtonPressed();
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

    public void OnPointerUp(PointerEventData eventData)
    {
        OnSubmit();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
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

    public void OnSubmit(BaseEventData eventData)
    {
        OnSubmit();
    }

    public void OnSelect(BaseEventData eventData)
    {
        //TODO:SE1の再生
    }

    public void OnDeselect(BaseEventData eventData)
    {
    }
}
