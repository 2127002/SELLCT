using Cysharp.Threading.Tasks;
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
    [SerializeField] TextMeshProUGUI _countText = default!;

    [SerializeField] Image _selectedImage = default!;

    bool _enabledHighLight = true;

    public IReadOnlyList<Image> CardImages => _cardImages;

    private void Reset()
    {
        _cardImages = GetComponentsInChildren<Image>().ToList();
        _cardText = GetComponentInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// �J�[�h�ɉ����ĕ\����؂�ւ���
    /// </summary>
    /// <param name="card">�\���������J�[�h</param>
    public void SetCardSprites(Card card, int cardCount)
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
        _cardText.text = card.CardName.ToDisplayString();

        //�J�[�h����1�ȉ��Ȃ���ɕ\�����Ȃ�
        if (cardCount <= 1)
        {
            _countText.enabled = false;
            return;
        }

        _countText.enabled = true;
        _countText.text = "�~" + cardCount.ToString().ToDisplayString();
    }

    public void DisableAllCardUIs()
    {
        foreach (var image in _cardImages)
        {
            image.enabled = false;
        }

        _cardText.enabled = false;

        _countText.enabled = false;
        _selectedImage.enabled = false;
    }

    public void OnSelect()
    {
        if (_enabledHighLight) _selectedImage.enabled = true;

        HighlightImageAnimation();
    }

    private async void HighlightImageAnimation()
    {
        float time = 0;

        //�A�j���[�V�����I���܂ł̕b��
        const float duration = 0.1f;

        var token = this.GetCancellationTokenOnDestroy();

        while (time < duration)
        {
            await UniTask.Yield(token);

            time += Time.deltaTime;
            time = Mathf.Min(time, duration);
            _selectedImage.rectTransform.localScale = Vector3.one * TM.Easing.Management.EasingManager.EaseProgress(TM.Easing.EaseType.OutBack, time, duration, 1f, 1f);
        }
    }

    public void ResetImagesSize()
    {
        if (_enabledHighLight) _selectedImage.enabled = false;
    }

    public void EnableHighlight()
    {
        _enabledHighLight = true;
    }

    public void DisableHighlight()
    {
        _enabledHighLight = false;
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
