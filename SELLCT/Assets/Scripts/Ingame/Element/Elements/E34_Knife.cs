using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E34_Knife : Card
{
    [SerializeField] PhaseController _phaseController = default!;
    
    [SerializeField] Image _E34CommandImage = default!;

    public override int Id => 34;

    private void Awake()
    {
        _phaseController.OnGameStart.Add(OnGameStart);
    }

    private void OnDestroy()
    {
        _phaseController.OnGameStart.Remove(OnGameStart);
    }

    private void OnGameStart()
    {
        _E34CommandImage.gameObject.SetActive(_handMediator.ContainsCard(this));
    }

    public override void Buy()
    {
        _E34CommandImage.gameObject.SetActive(true);
    }

    public override void OnPressedU6Button()
    {
        //TODO：シーン3に遷移
        Debug.LogWarning(StringManager.ToDisplayString("命がなくなりました！シーン3に遷移する処理は未実装なため続行されます。"));
    }

    public override void Sell()
    {
        if (_handMediator.ContainsCard(this)) return;
        _E34CommandImage.gameObject.SetActive(false);
    }
}
