using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HighlightController : MonoBehaviour
{
    public void Enable()
    {
        //���͕ύX�����̂Ŗ���擾���܂�
        //�y�ł������������܂�ǂ��Ȃ��̂ŁA�d�l���m�肵���i�K�ł��ǂ����Ă�������������܂���B
        var selectableHighlights = FindObjectsByType<Selectable>(FindObjectsInactive.Include, FindObjectsSortMode.None).Select(x => x.GetComponent<ISelectableHighlight>());

        foreach (var item in selectableHighlights)
        {
            item.EnableHighlight();
        }
    }

    public void Disable()
    {
        //���͕ύX�����̂Ŗ���擾���܂�
        //�y�ł������������܂�ǂ��Ȃ��̂ŁA�d�l���m�肵���i�K�ł��ǂ����Ă�������������܂���B
        var selectableHighlights = FindObjectsByType<Selectable>(FindObjectsInactive.Include, FindObjectsSortMode.None).Select(x => x.GetComponent<ISelectableHighlight>());

        foreach (var item in selectableHighlights)
        {
            item.DisableHighlight();
        }
    }
}
