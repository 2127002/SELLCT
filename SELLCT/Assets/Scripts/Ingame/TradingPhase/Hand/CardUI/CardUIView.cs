using Cysharp.Threading.Tasks;
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
    [SerializeField] TextMeshProUGUI _countText = default!;

    [SerializeField] Image _selectedImage = default!;
    [SerializeField] Image _selectedImageKanji = default!;
    [SerializeField] Image _selectedImageHiragana = default!;

    bool _enabledHighLight = true;

    public IReadOnlyList<Image> CardImages => _cardImages;

    private void Reset()
    {
        _cardImages = GetComponentsInChildren<Image>().ToList();
        _cardText = GetComponentInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// カードに応じて表示を切り替える
    /// </summary>
    /// <param name="card">表示したいカード</param>
    public void SetCardSprites(Card card, int cardCount)
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
        bool isPrintText = card is E30_Name;

        //表示する
        _cardText.enabled = isPrintText;
        _cardText.text = card.CardName.ToDisplayString();

        //カード数が1以下なら特に表示しない
        if (cardCount <= 1)
        {
            _countText.enabled = false;
            return;
        }

        _countText.enabled = true;
        _countText.text = "×" + cardCount.ToString().ToDisplayString();
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
        if (!_enabledHighLight) return;

        //有効化
        _selectedImage.enabled = true;
        _selectedImageKanji.enabled = StringManager.hasElements[(int)StringManager.Element.E18];
        _selectedImageHiragana.enabled = StringManager.hasElements[(int)StringManager.Element.E19];

        //アニメーションの再生
        PlayHighlightImageAnimation();
    }

    private async void PlayHighlightImageAnimation()
    {
        float time = 0f;

        //アニメーション終了までの秒数
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
        if (!_enabledHighLight) return;
        
        _selectedImage.enabled = false;
        _selectedImageKanji.enabled = false;
        _selectedImageHiragana.enabled = false;
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
