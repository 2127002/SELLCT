using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationNextButtonController : MonoBehaviour
{
    public enum PatternType
    {
        Element,
        ExplorationU6,

        Max
    }

    [SerializeField] ExplorationNextButtonView _explorationNextButtonView = default!;
    [SerializeField] Selectable _ExplorationNextButtonSelectable = default!;

    bool _elementEnforcement;

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

    public void Enable(PatternType pattern)
    {
        //�G�������g��Enable�����s���ꂽ��G�������g�̋����͂���������
        if (pattern == PatternType.Element) _elementEnforcement = false;
        
        //�����͂���������G�������g�D��
        if (_elementEnforcement) return;

        _explorationNextButtonView.Enable();
        _ExplorationNextButtonSelectable.interactable = true;
    }

    public void Disable(PatternType pattern)
    {
        //�G�������g��Enable�����s���ꂽ��G�������g�̋����͂�����
        if (pattern == PatternType.Element) _elementEnforcement = true;

        _explorationNextButtonView.Disable();
        _ExplorationNextButtonSelectable.interactable = false;
    }

}
