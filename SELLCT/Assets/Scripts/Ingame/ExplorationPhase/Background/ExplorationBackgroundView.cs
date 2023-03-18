using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationBackgroundView : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Sprite _normalSprite;
    [SerializeField] Sprite _restSprite;

    public void OnWakeUp()
    {
        //���u���ł��BSprite�̎����ƂƂ��ɍ폜���Ă��������B
        _image.color = Color.gray;

        _image.sprite = _normalSprite;
    }

    public void OnRest()
    {
        //���u���ł��BSprite�̎����ƂƂ��ɍ폜���Ă��������B
        _image.color = Color.cyan;

        _image.sprite = _restSprite;
    }
}
