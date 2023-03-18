using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUIGenerator : MonoBehaviour
{
    [SerializeField] CardUIInstance _cardUIInstance = default!;
    [SerializeField] Transform _parent = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] CardUIHandler _handler = default!;
    [SerializeField] Hand _hand = default!;

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
        //1つはすでにサンプルとして生成済み
        for (int i = 1; i < _hand.Capacity; i++)
        {
            var newHandler = Instantiate(_handler, _parent);

            newHandler.name = _handlerName + (i + 1);

            _cardUIInstance.Add(newHandler);
        }
    }

    public void Generate()
    {
        var newHandler = Instantiate(_handler, _parent);

        newHandler.name = _handlerName + (_cardUIInstance.Handlers.Count + 1);

        _cardUIInstance.Add(newHandler);
    }
}
