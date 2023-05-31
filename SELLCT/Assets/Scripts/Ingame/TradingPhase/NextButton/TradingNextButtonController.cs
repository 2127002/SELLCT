using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradingNextButtonController : MonoBehaviour
{
    [SerializeField] TradingNextButtonView _tradingNextButtonView = default!;
    [SerializeField] Selectable _tradingNextButtonSelectable = default!;

    private void Reset()
    {
        _tradingNextButtonView = GetComponent<TradingNextButtonView>();
        _tradingNextButtonSelectable=GetComponentInChildren<Selectable>();
    }

    public void OnKanjiEnabled()
    {
        _tradingNextButtonView.OnKanjiEnabled();
    }

    public void OnKanjiDisabled()
    {
        _tradingNextButtonView.OnKanjiDisabled();
    }

    public void OnHiraganaEnabled()
    {
        _tradingNextButtonView.OnHiraganaEnabled();
    }

    public void OnHiraganaDisabled()
    {
        _tradingNextButtonView.OnHiraganaDisabled();
    }

    public void Enable()
    {
        _tradingNextButtonView.Enable();
        _tradingNextButtonSelectable.interactable = true;
    }

    public void Disable()
    {
        _tradingNextButtonView.Disable();
        _tradingNextButtonSelectable.interactable = false;
    }
}
