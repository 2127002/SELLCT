using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUIView : MonoBehaviour
{
    //カード表示
    [SerializeField] List<Image> _cardImages = default!;
    [SerializeField] TextMeshProUGUI _cardText = default!;
    [SerializeField] Vector2 _defaultSizeDelta = default!;

    //選択時画像サイズ補正値
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
    /// カードに応じて表示を切り替える
    /// </summary>
    /// <param name="card">表示したいカード</param>
    public void SetCardSprites(Card card)
    {
        //0番目はBaseなため必ず表示される
        _cardImages[0].enabled = true;
        _cardImages[0].sprite = card.CardSprite[0];

        //以降の文字要素は、エレメントの所持状況で表示が切り替わる
        for (int i = 1; i < _cardImages.Count; i++)
        {
            //カードに該当文字が無い場合の対応
            if (card.CardSprite[i] == null)
            {
                _cardImages[i].sprite = null;
                _cardImages[i].enabled = false;
                continue;
            }

            //Spriteをセットし、エレメントの所持状況で表示を切り替える
            //index番号はbase分がズレている。
            _cardImages[i].sprite = card.CardSprite[i];
            _cardImages[i].enabled = StringManager.hasElements[i - 1];
        }

        //テキストで名前を表示するエレメントか判定
        bool isPrintText = card.Id == 30;

        //表示する
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
        //拡大率を指定値に変える
        foreach (var item in _cardImages)
        {
            Vector2 sizeDelta = item.rectTransform.sizeDelta;
            sizeDelta.Scale(correction);
            item.rectTransform.sizeDelta = sizeDelta;
        }
    }

    public void ResetImagesSizeDelta()
    {
        //拡大率を初期値に戻す
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
