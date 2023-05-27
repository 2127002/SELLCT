using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleSelectableMessage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _u16Text = default!;
    [SerializeField] TextMeshProUGUI _u18Text = default!;

    private void Awake()
    {
        //�\���p�̕����ɕϊ�
        _u16Text.text = _u16Text.text.ToDisplayString();
        _u18Text.text = _u18Text.text.ToDisplayString();
    }
}
