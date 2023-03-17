using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandUIHandler : MonoBehaviour
{
    //このようにDetectorにわざわざ分けているのは、interfaceのメソッドがpublicになるからです。
    //外部から意図しないタイミングで呼ばれることを避けるため回りくどい手を使っています。
    [SerializeField] LeftClickDetector _clickDetector = default!;
    [SerializeField] PointerEnterDetector _enterDetector = default!;
    [SerializeField] PointerExitDetector _exitDetector = default!;
    [SerializeField] SubmitDetector _submitDetector = default!;
    [SerializeField] SelectDetector _selectDetector = default!;
    [SerializeField] DeselectDetector _deselectDetector = default!;

    //選択肢
    [SerializeField] Selectable _selectable = default!;

    [SerializeField] PhaseController _phaseController = default!;

    [SerializeField] Card _card = default!;

    private void Awake()
    {
        //購読
        _clickDetector.AddListener(HandleClick);
        _enterDetector.AddListener(HandleEnter);
        _exitDetector.AddListener(HandleExit);
        _submitDetector.AddListener(HandleSubmit);
        _selectDetector.AddListener(HandleSelect);
        _deselectDetector.AddListener(HandleDeselect);

        _phaseController.OnExplorationPhaseStart += OnPhaseStart;

        //わかりやすくするため仮に選択時の色を赤に変更。今後の変更推奨
        var b = _selectable.colors;
        b.selectedColor = Color.red;
        _selectable.colors = b;
    }

    private void OnPhaseStart()
    {
        bool containsCard = _card.ContainsPlayerDeck;

        //Grid Layout Groupで管理しているため、ImageのEnableではなくGameObjectからオンオフします
        gameObject.SetActive(containsCard);
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

    //左クリック時処理
    private void HandleClick()
    {
        //同一処理のため以下の処理を呼ぶだけにします。Submit時の仕様と差異が発生したら修正してください。
        OnSubmit();
    }

    //決定時の処理
    private void HandleSubmit()
    {
        //同一処理のため以下の処理を呼ぶだけにします。クリック時の仕様と差異が発生したら修正してください。
        OnSubmit();
    }

    private void OnSubmit()
    {
        //U6用の処理
        _card.Passive();
    }

    //選択時の処理
    private void HandleSelect()
    {
    }

    //選択から外れた時の処理
    private void HandleDeselect()
    {
    }
}
