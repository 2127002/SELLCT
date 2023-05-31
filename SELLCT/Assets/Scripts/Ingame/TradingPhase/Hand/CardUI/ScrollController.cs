using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardScrollController : MonoBehaviour
{
    public enum Direction
    {
        Up = 0,
        Down = 1,

        [InspectorName("")]
        Invalid,
    }

    [SerializeField] RectTransform _target = default!;
    [SerializeField] float _duration = 0.1f;

    //288+60
    const float CARDHEIGHT = 348f;
    static readonly List<Vector3> offset = new() { new Vector3(0f, -CARDHEIGHT, 0f), new Vector3(0f, CARDHEIGHT, 0f) };

    bool _isAnimation = false;

    public async UniTask StartAnimation(Direction dir)
    {
        if (dir == Direction.Invalid) return;
        
        //既にアニメーション中なら受け付けない
        if (_isAnimation) return;

        float currentTime = 0f;
        Vector3 prebPos = _target.localPosition;

        var cancellationToken = this.GetCancellationTokenOnDestroy();

        while (currentTime < _duration)
        {
            //アニメーション中にする
            _isAnimation = true;

            await UniTask.Yield(cancellationToken);

            float progress = TM.Easing.Management.EasingManager.EaseProgress(TM.Easing.EaseType.InOutSine, currentTime, _duration, 0f, 0f);
            _target.localPosition = prebPos + offset[(int)dir] * progress;

            currentTime += Time.deltaTime;
        }
        _target.localPosition = prebPos + offset[(int)dir];

        //アニメーション中フラグを折る
        _isAnimation = false;
    }
}
