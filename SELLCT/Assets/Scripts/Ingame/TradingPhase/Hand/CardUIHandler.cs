using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CardUIHandler : MonoBehaviour
{
    enum EntityType
    {
        Player,
        Trader,

        [InspectorName("")]
        Invalid,
    }

    //���̂悤��Detector�ɂ킴�킴�����Ă���̂́Ainterface�̃��\�b�h��public�ɂȂ邩��ł��B
    //�O������Ӑ}���Ȃ��^�C�~���O�ŌĂ΂�邱�Ƃ�����邽�߉�肭�ǂ�����g���Ă��܂��B
    [SerializeField] LeftClickDetector _clickDetector;
    [SerializeField] PointerEnterDetector _enterDetector;
    [SerializeField] PointerExitDetector _exitDetector;
    [SerializeField] SubmitDetector _submitDetector;
    [SerializeField] SelectDetector _selectDetector;

    [SerializeField] HandMediator _handMediator;

    [SerializeField] Selectable _selectable;

    [SerializeField] EntityType _entityType;
    [SerializeField] bool _isFirstSelectable;

    Card _card;
    EventSystem _eventSystem;

    private void Awake()
    {
        _eventSystem = EventSystem.current;

        //�w��
        _clickDetector.AddListener(HandleClick);
        _enterDetector.AddListener(HandleEnter);
        _exitDetector.AddListener(HandleExit);
        _submitDetector.AddListener(HandleSubmit);
        _selectDetector.AddListener(HandleSelect);

        SetFirstSelectable();

        //�킩��₷�����邽�߉��ɑI�����̐F��ԂɕύX�B����̕ύX����
        var b = _selectable.colors;
        b.selectedColor = Color.red;
        _selectable.colors = b;
    }

    private void OnDestroy()
    {
        //�w�ǉ���
        _clickDetector.RemoveListener(HandleClick);
        _enterDetector.RemoveListener(HandleEnter);
        _exitDetector.RemoveListener(HandleExit);
        _submitDetector.RemoveListener(HandleSubmit);
        _selectDetector.RemoveListener(HandleSelect);
    }

    private void SetFirstSelectable()
    {
        //�����I���̃`�F�b�N�{�b�N�X��true��������o�^
        if (!_isFirstSelectable) return;

        if (_eventSystem.currentSelectedGameObject != null)
        {
            Debug.LogWarning("���łɕʂ̃I�u�W�F�N�g���I������Ă��܂��B" + gameObject + "�̓o�^�͊��p����܂����B�������d�l���m�F���Ă��������B" + _eventSystem.currentSelectedGameObject);
            return;
        }

        _eventSystem.SetSelectedGameObject(_selectable.gameObject);
    }

    //���N���b�N������
    private void HandleClick()
    {
        //Image�𖳌�������
        _selectable.image.enabled = false;

        //�N���b�N�������ɂ���
        _selectable.interactable = false;

        //EntityType�ɓK�����������ĂԁB
        if (_entityType == EntityType.Player)
        {
            _card.Sell();
        }
        else if (_entityType == EntityType.Trader)
        {
            _card.Buy();
        }
        else
        {
            throw new System.NotImplementedException();
        }

        //��D����폜���A�V���ɃJ�[�h������
        _handMediator.RemoveHandCard(_card);
        InsertCard(_handMediator.TakeDeckTopCard());
    }

    //�J�[�\�������������ۂ̏���
    private void HandleEnter()
    {
        //TODO�F���ケ���ɋ�̓I�ȃJ�[�\�������������ۂ̏�����ǉ�����
        _selectable.Select();
    }

    //�J�[�\�����O�����ۂ̏���
    private void HandleExit()
    {
        //TODO�F���ケ���ɋ�̓I�ȃJ�[�\�����O�����ۂ̏�����ǉ�����
        EventSystem.current.SetSelectedGameObject(null);
    }

    //���莞�̏���
    private void HandleSubmit()
    {
        //���ꏈ���̂��߈ȉ��̏������ĂԂ����ɂ��܂��B�N���b�N���̎d�l�ƍ��ق�����������C�����Ă��������B
        HandleClick();
    }

    //�I�����̏���
    private void HandleSelect()
    {
        //TODO:SE1�̍Đ�
    }

    /// <summary>
    /// �J�[�h��}������
    /// </summary>
    /// <param name="card">�}������J�[�h</param>
    public void InsertCard(Card card)
    {
        _card = card;

        bool isNormalCard = _card is not EEX_null;

        _selectable.image.enabled = isNormalCard;
        _selectable.interactable = isNormalCard;
    }
}
