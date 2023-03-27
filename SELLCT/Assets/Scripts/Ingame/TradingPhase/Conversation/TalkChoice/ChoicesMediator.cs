using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicesMediator : MonoBehaviour
{
    [SerializeField] ChoicesManager _choiceManager = default!;
    [SerializeField] List<ConversationChoiceHandler> _choiceHandlers = default!;

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