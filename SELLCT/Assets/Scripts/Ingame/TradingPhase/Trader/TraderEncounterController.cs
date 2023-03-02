using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderEncounterController : MonoBehaviour
{
    [SerializeField] TraderController _traderController;
    [SerializeField] TempChara _tempChara;

    private void Awake()
    {
        _traderController.SetTrader(_tempChara);
    }
}
