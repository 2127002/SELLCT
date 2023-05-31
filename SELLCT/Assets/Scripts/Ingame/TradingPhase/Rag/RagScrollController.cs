using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RagScrollController : MonoBehaviour
{
    [SerializeField] List<Sprite> _upRagRollSprites = default!;
    [SerializeField] List<Sprite> _downRagRollSprites = default!;
    [SerializeField] Image _ragRollImage = default!;

    public async UniTask StartAnimation(CardScrollController.Direction direction, float duration)
    {
        var ragRollSprites = direction switch
        {
            CardScrollController.Direction.Up => _upRagRollSprites,
            CardScrollController.Direction.Down => _downRagRollSprites,
            _ => throw new System.NotImplementedException(),
        };

        int count = ragRollSprites.Count;

        for (int i = 0; i < count; i++)
        {
            await UniTask.Delay((int)(1000f * duration / count));

            var sprite = ragRollSprites[i];

            _ragRollImage.sprite = sprite;
        }
    }
}
