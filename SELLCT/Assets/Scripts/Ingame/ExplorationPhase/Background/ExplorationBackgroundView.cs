using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationBackgroundView : MonoBehaviour
{
    [Header("背景の画像")]
    [SerializeField] Image _image;
    [SerializeField] Sprite _normalSprite;
    [SerializeField] Sprite _restSprite;

    public void ConvertWakeUp()
    {
        //仮置きです。Spriteの実装とともに削除してください。
        _image.color = Color.gray;

        _image.sprite = _normalSprite;
    }

    public void ConvertRest()
    {
        //仮置きです。Spriteの実装とともに削除してください。
        _image.color = Color.cyan;

        _image.sprite = _restSprite;
    }
}
