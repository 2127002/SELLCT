using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationNextButtonView : MonoBehaviour
{
    [SerializeField] Image _baseImage = default!;
    [SerializeField] Sprite _baseEnableSprite = default!;
    [SerializeField] Sprite _baseDisableSprite = default!;

    [SerializeField] Image _kanjiImage = default!;
    [SerializeField] Sprite _kanjiEnableSprite = default!;
    [SerializeField] Sprite _kanjiDisableSprite = default!;

    [SerializeField] Image _hiraganaImage = default!;
    [SerializeField] Sprite _hiraganaEnableSprite = default!;
    [SerializeField] Sprite _hiraganaDisableSprite = default!;

    public void Enable()
    {
        _baseImage.sprite = _baseEnableSprite;

        if (StringManager.hasElements[(int)StringManager.Element.E18])
        {
            _kanjiImage.sprite = _kanjiEnableSprite;
        }
        if (StringManager.hasElements[(int)StringManager.Element.E19])
        {
            _hiraganaImage.sprite = _hiraganaEnableSprite;
        }
    }

    public void Disable()
    {
        _baseImage.sprite = _baseDisableSprite;
        _kanjiImage.sprite = _kanjiDisableSprite;
        _hiraganaImage.sprite = _hiraganaDisableSprite;
    }

    public void OnHiraganaEnabled()
    {
        _hiraganaImage.enabled = true;
    }

    public void OnHiraganaDisabled()
    {
        _hiraganaImage.enabled = false;
    }

    public void OnKanjiEnabled()
    {
        _kanjiImage.enabled = true;
    }

    public void OnKanjiDisabled()
    {
        _kanjiImage.enabled = false;
    }
}
