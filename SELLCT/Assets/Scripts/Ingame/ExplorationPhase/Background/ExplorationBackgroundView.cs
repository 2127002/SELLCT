using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationBackgroundView : MonoBehaviour
{
    [Header("�w�i�̉摜")]
    [SerializeField] Image _image;
    [SerializeField] Sprite _normalSprite;
    [SerializeField] Sprite _restSprite;

    public void ConvertWakeUp()
    {
        //���u���ł��BSprite�̎����ƂƂ��ɍ폜���Ă��������B
        _image.color = Color.gray;

        _image.sprite = _normalSprite;
    }

    public void ConvertRest()
    {
        //���u���ł��BSprite�̎����ƂƂ��ɍ폜���Ă��������B
        _image.color = Color.cyan;

        _image.sprite = _restSprite;
    }
}
