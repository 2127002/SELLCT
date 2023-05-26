using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class E25_HandNumber : Card
{
    [SerializeField] Hand _playerHand = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] CardUIInstance _cardUIInstance = default!;
    [SerializeField] CardUIGenerator _cardUIGenerator = default!;

    public override int Id => 25;

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
        int add = _handMediator.FindAll(this);
        _playerHand.AddHandCapacity(add);

        for (int i = 0; i < add; i++)
        {
            _handMediator.DrawCard();
        }
    }

    public override void Buy()
    {
        base.Buy();

        _playerHand.AddHandCapacity(1);

        //Handler������Ă���J�[�h������
        _cardUIGenerator.Generate();
        _handMediator.DrawCard();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        base.Sell();

        _playerHand.AddHandCapacity(-1);

        //��Ɉ������J�[�h���f�b�L�ɖ߂�
        _handMediator.AddPlayerDeck(_playerHand.Cards[^1]);
        _playerHand.Remove(_playerHand.Cards[^1]);

        //CardUIInstance�̍ŏI�I�u�W�F�N�g������
        GameObject preb = EventSystem.current.currentSelectedGameObject;
        bool isLast = preb == _cardUIInstance.Handlers[^1].gameObject;

        //1������
        _cardUIInstance.RemoveAt(_cardUIInstance.Handlers.Count - 1);

        //�������O��CardUIInstance�̍ŏI�I�u�W�F�N�g�Ȃ�Select����
        if (isLast) EventSystem.current.SetSelectedGameObject(_cardUIInstance.Handlers[^1].gameObject);
    }
}
