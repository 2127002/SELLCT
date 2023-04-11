using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationNextButtonController : MonoBehaviour
{
    [SerializeField] ExplorationNextButtonView _explorationNextButtonView = default!;
    [SerializeField] Selectable _ExplorationNextButtonSelectable = default!;

    private void Reset()
    {
        _explorationNextButtonView = GetComponent<ExplorationNextButtonView>();
        _ExplorationNextButtonSelectable = GetComponentInChildren<Selectable>();
    }

    public void OnKanjiEnabled()
    {
        _explorationNextButtonView.OnKanjiEnabled();
    }

    public void OnKanjiDisabled()
    {
        _explorationNextButtonView.OnKanjiDisabled();
    }

    public void OnHiraganaEnabled()
    {
        _explorationNextButtonView.OnHiraganaEnabled();
    }

    public void OnHiraganaDisabled()
    {
        _explorationNextButtonView.OnHiraganaDisabled();
    }

    public void Enable()
    {
        _explorationNextButtonView.Enable();
        _ExplorationNextButtonSelectable.interactable = true;
    }

    public void Disable()
    {
        _explorationNextButtonView.Disable();
        _ExplorationNextButtonSelectable.interactable = false;
    }

}
