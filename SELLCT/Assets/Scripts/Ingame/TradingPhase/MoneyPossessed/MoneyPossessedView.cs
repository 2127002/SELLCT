using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyPossessedView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _numberText = default!;
    [SerializeField] TextMeshProUGUI _alphabetText = default!;

    private void Reset()
    {
        _numberText = GameObject.Find("U8_MoneyText_Num").GetComponent<TextMeshProUGUI>();
        _alphabetText = GameObject.Find("U8_MoneyText_ENG").GetComponent<TextMeshProUGUI>();
    }

    public void ChangeMoneyText(Money money)
    {
        _numberText.text = money.CurrentAmount().ToString();
    }

    public void EnableNumber()
    {
        _numberText.enabled = true;
    } 

    public void DisableNumber()
    {
        _numberText.enabled = false;
    }   
    
    public void EnableAlphabet()
    {
        _alphabetText.enabled = true;
    } 

    public void DisableAlphabet()
    {
        _alphabetText.enabled = false;
    }
}
