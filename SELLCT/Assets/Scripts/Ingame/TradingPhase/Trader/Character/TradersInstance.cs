using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradersInstance : MonoBehaviour
{
    [Header("登場させたいトレーダーを選択してください。\n右上の3点リーダからResetを押すことで自動的に全トレーダーがアタッチされます。")]
    [SerializeField] List<Trader> _traders = new();

    private void Reset()
    {
        _traders.Add(FindObjectOfType<TR1_NormalTrader>());
        _traders.Add(FindObjectOfType<TR2_RuinedNobility>());
        _traders.Add(FindObjectOfType<TR3_MatchGirl>());
        _traders.Add(FindObjectOfType<TR4_Knight>());
        _traders.Add(FindObjectOfType<TR5_CrazyScholar>());
        _traders.Add(FindObjectOfType<TR6_GhostBoy>());
        _traders.Add(FindObjectOfType<TR7_Beast>());
    }

    public IReadOnlyList<Trader> Traders => _traders;
}
