using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckGenerator : MonoBehaviour
{
    [SerializeField] CardPool _cardPool = default!;
    [SerializeField] PlayerDeckGenerator _playerDeckGenerator = default!;
    [SerializeField] TraderDeckGenerator _traderDeckGenerator = default!;

    //処理を呼ぶタイミングの購読に使用
    [SerializeField] PhaseController _phaseController = default!;

    private void Awake()
    {
        _phaseController.onGameStart += GenerateDeck;
    }

    private void GenerateDeck()
    {
        _cardPool.Init();

        _playerDeckGenerator.Generate(_cardPool);
        _traderDeckGenerator.Generate(_cardPool);
    }
}
