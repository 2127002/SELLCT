using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewGameButtonHandler : MonoBehaviour, ISubmitHandler
{
    [SerializeField] TitleController _titleController = default!;

    private void Reset()
    {
        _titleController = GetComponentInParent<TitleController>();
    }

    void ISubmitHandler.OnSubmit(BaseEventData eventData)
    {
        _titleController.OnPressedNewGame();
    }
}
