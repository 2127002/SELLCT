using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUIView : MonoBehaviour
{
    //�J�[�h�\��
    [SerializeField] List<Image> _cardImages = default!;
    [SerializeField] TextMeshProUGUI _cardText = default!;
    [SerializeField] Vector2 _defaultSizeDelta = default!;

    //�I�����摜�T�C�Y�␳�l
    const float CORRECTION_SIZE = 1.25f;
    static readonly Vector2 correction = new(CORRECTION_SIZE, CORRECTION_SIZE);

    public IReadOnlyList<Image> CardImages => _cardImages;

    private void Reset()
    {
        _cardImages = GetComponentsInChildren<Image>().ToList();
        _cardText = GetComponentInChildren<TextMeshProUGUI>();
        _defaultSizeDelta = GetComponent<RectTransform>().sizeDelta;
    }

    /// <summary>
    /// �J�[�h�ɉ����ĕ\����؂�ւ���
    /// </summary>
    /// <param name="card">�\���������J�[�h</param>
    public void SetCardSprites(Card card)
    {
        //0�Ԗڂ�Base�Ȃ��ߕK���\�������
        _cardImages[0].enabled = true;
        _cardImages[0].sprite = card.CardSprite[0];

        //�ȍ~�̕����v�f�́A�G�������g�̏����󋵂ŕ\�����؂�ւ��
        for (int i = 1; i < _cardImages.Count; i++)
        {
            //�J�[�h�ɊY�������������ꍇ�̑Ή�
            if (card.CardSprite[i] == null)
            {
                _cardImages[i].sprite = null;
                _cardImages[i].enabled = false;
                continue;
            }

            //Sprite���Z�b�g���A�G�������g�̏����󋵂ŕ\����؂�ւ���
            //index�ԍ���base�����Y���Ă���B
            _cardImages[i].sprite = card.CardSprite[i];
            _cardImages[i].enabled = StringManager.hasElements[i - 1];
        }

        //�e�L�X�g�Ŗ��O��\������G�������g������
        bool isPrintText = card.Id == 30;

        //�\������
        _cardText.enabled = isPrintText;
        _cardText.text = card.CardName;
    }

    public void DisableAllCardUIs()
    {
        foreach (var image in _cardImages)
        {
            image.enabled = false;
        }

        _cardText.enabled = false;
    }

    public void OnSelect()
    {
        //�g�嗦���w��l�ɕς���
        foreach (var item in _cardImages)
        {
            Vector2 sizeDelta = item.rectTransform.sizeDelta;
            sizeDelta.Scale(correction);
            item.rectTransform.sizeDelta = sizeDelta;
        }
    }

    public void ResetImagesSizeDelta()
    {
        //�g�嗦�������l�ɖ߂�
        foreach (var item in _cardImages)
        {
            item.rectTransform.sizeDelta = _defaultSizeDelta;
        }
    }

    public void OnSelectableEnabled(Color normalColor)
    {
        foreach (var cardImage in _cardImages)
        {
            cardImage.color = normalColor;
        }
    }    
    
    public void OnSelectableDisabled(Color disabledColor)
    {
        foreach (var cardImage in _cardImages)
        {
            cardImage.color = disabledColor;
        }
    }
}
