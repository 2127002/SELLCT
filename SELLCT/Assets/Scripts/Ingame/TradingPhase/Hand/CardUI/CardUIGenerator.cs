using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardUIGenerator : MonoBehaviour
{
    [SerializeField] CardUIInstance _cardUIInstance = default!;
    [SerializeField] Transform _parent = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] CardUIHandler _handler = default!;
    [SerializeField] Hand _hand = default!;
    [SerializeField] CursorController _cursorController = default!;

    [SerializeField] string _handlerName = default!;

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
        int alreadyGenerated = 1;

        //1‚Â‚Í‚·‚Å‚ÉƒTƒ“ƒvƒ‹‚Æ‚µ‚Ä¶¬Ï‚İ
        for (int i = alreadyGenerated; i < _hand.Capacity; i++)
        {
            var newHandler = Instantiate(_handler, _parent);
            newHandler.OnGenerate();

            newHandler.name = _handlerName + (i + alreadyGenerated);

            _cardUIInstance.Add(newHandler);
        }
    }

    public void Generate()
    {
        var newHandler = Instantiate(_handler, _parent);

        newHandler.name = _handlerName + (_cardUIInstance.Handlers.Count + 1);
        newHandler.OnGenerate();

        _cardUIInstance.Add(newHandler);
        _cursorController.AddRectTransform(newHandler.GetComponent<RectTransform>());
    }
}
