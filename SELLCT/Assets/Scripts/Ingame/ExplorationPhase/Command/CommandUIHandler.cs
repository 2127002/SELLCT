using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandUIHandler : MonoBehaviour, ISelectableHighlight, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, ISubmitHandler, ISelectHandler, IDeselectHandler
{
    //選択肢
    [SerializeField] Selectable _selectable = default!;

    [SerializeField] PhaseController _phaseController = default!;

    [SerializeField] Card _card = default!;

    Color _defaultSelectColor = default!;

    private void Awake()
    {
        _phaseController.OnExplorationPhaseStart += OnPhaseStart;

        _defaultSelectColor = _selectable.colors.selectedColor;
    }

    private void OnPhaseStart()
    {
        if (_card == null)
        {
            Debug.LogWarning("U6エレメントカードがnullです"); return;
        }

        bool containsCard = _card.ContainsPlayerDeck;

        //Grid Layout Groupで管理しているため、ImageのEnableではなくGameObjectからオンオフします
        gameObject.SetActive(containsCard);
    }

    private void OnSubmit()
    {
        //U6用の処理
        _card.OnPressedU6Button();
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

    public void OnDeselect(BaseEventData eventData)
    {
    }

    public void OnSelect(BaseEventData eventData)
    {
    }

    public void OnSubmit(BaseEventData eventData)
    {
        //同一処理のため以下の処理を呼ぶだけにします。クリック時の仕様と差異が発生したら修正してください。
        OnSubmit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //TODO：今後ここに具体的なカーソルをかざした際の処理を追加する
        _selectable.Select();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //同一処理のため以下の処理を呼ぶだけにします。Submit時の仕様と差異が発生したら修正してください。
        OnSubmit();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //TODO：今後ここに具体的なカーソルを外した際の処理を追加する
        EventSystem.current.SetSelectedGameObject(null);
    }
}
