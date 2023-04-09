using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradersInstance : MonoBehaviour
{
    [Header("�o�ꂳ�������g���[�_�[��I�����Ă��������B\n�E���3�_���[�_����Reset���������ƂŎ����I�ɑS�g���[�_�[���A�^�b�`����܂��B")]
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
