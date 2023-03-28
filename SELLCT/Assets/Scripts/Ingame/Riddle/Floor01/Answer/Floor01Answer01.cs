using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor01Answer01
{
    E32_Rest _e32;
    E10_Light _e10;
    E7_Move _e7;
    Floor01Condition _condition;

    bool _alreadyRest = false;

    public Floor01Answer01(E32_Rest e32, E10_Light e10, E7_Move e7,Floor01Condition floor01Condition)
    {
        _e32 = e32;
        _e10 = e10;
        _e7 = e7;
        _condition = floor01Condition;
    }

    private bool HasElements()
    {
        return _e32.ContainsPlayerDeck && _e10.FindAll == 1 && _e7.ContainsPlayerDeck;
    }

    public void Go()
    {
        if (!_alreadyRest) return;

        _condition.Solve();
    }

    public bool Rest()
    {
        if (!HasElements()) return false;
        if (_alreadyRest) return false;

        _alreadyRest = true;
        return true;
    }
}
