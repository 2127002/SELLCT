using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicesMediator : MonoBehaviour
{
    [SerializeField] ChoicesManager _choiceManager = default!;
    [SerializeField] List<ConversationChoiceHandler> _choiceHandlers = default!;

    private async void Start()
    {
       await UniTask.Yield();
        List<int> choices = new List<int>();
        choices.Add(0);
        choices.Add(1);
        choices.Add(2);
        InsertConversationChoice(choices);
    }

    public void InsertConversationChoice(List<int> IDs)
    {
        for (int i = 0; i < IDs.Count; i++)
        {
            ITalkChoice choice = _choiceManager.GetTalkChoice(IDs[i]);

            _choiceHandlers[i].SetTalkChoice(choice);
        }

        for(int i = IDs.Count; i < _choiceHandlers.Count; i++)
        {
            _choiceHandlers[i].SetTalkChoice(new NullChoice());
        }
    }
}