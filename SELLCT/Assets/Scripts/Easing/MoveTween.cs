using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TM.Easing.Management;
using TM.Easing.Core;

namespace TM.Easing
{
    /// <summary>
    /// Tweenアニメーションを実行するクラス
    /// </summary>
    public class MoveTween : MonoBehaviour
    {
        private float _time = 0f;

        /// <summary>
        /// 移動を実施する
        /// 移動時間から１フレームで移動する距離を計算して、少しずつ移動するようにしている
        /// </summary>
        /// <param name="obj">移動するゲームオブジェクト</param>
        /// <param name="endPos">到達ポシション</param>
        /// <param name="duration">到達するまでの時間</param>
        /// <returns>イテレーター（コルーチンで必要）</returns>
        public IEnumerator MoveEase(EasingCore<Vector3,Transform> core)
        {
            var objPos = core.GetSetter().GetSetter();
            var firstEndVal = core.GetEndValue();

            yield return new WaitForSeconds(core.GetDelayTime());

            while (core.GetLoopCount() != 0)
            {
                while (_time < core.GetDuration())
                {
                    _time += Time.deltaTime;
                    if (_time >= core.GetDuration()) _time = core.GetDuration();
                    var moveDis = (core.GetEndValue() - objPos);
                    core.GetGetter().GetGetter().transform.position= objPos + (moveDis * EasingManager.EaseProgress(core.GetEase(), _time, core.GetDuration(), 0, 0));
                    yield return null;
                }
                core.SetLoopCount(core.GetLoopCount() - 1);
                switch (core.GetLoopType())
                {
                    case LoopType.Yoyo:
                        objPos = core.GetEndValue();
                        core.SetEndValue(core.GetLoopCount() % 2 != 0 ? firstEndVal : core.GetStartValue());
                        _time = 0;
                        break;
                    case LoopType.Restart:
                        core.GetSetter().SetSetter(core.GetStartValue());
                        _time = 0;
                        break;
                    case LoopType.Increment:
                        _time = 0;
                        break;
                }
            }
            yield break;
        }
        public IEnumerator MoveEase(EasingCore<Vector2,Transform> core)
        {
            var objPos = core.GetSetter().GetSetter();
            var firstEndVal = core.GetEndValue();

            while (core.GetLoopCount() != 0)
            {
                while (_time < core.GetDuration())
                {
                    _time += Time.deltaTime;
                    if (_time >= core.GetDuration()) _time = core.GetDuration();
                    var moveDis = (core.GetEndValue() - objPos);
                    core.GetGetter().GetGetter().transform.position = objPos + (moveDis * EasingManager.EaseProgress(core.GetEase(), _time, core.GetDuration(), 0, 0));
                    yield return null;
                }
                core.SetLoopCount(core.GetLoopCount() - 1);
                switch (core.GetLoopType())
                {
                    case LoopType.Yoyo:
                        objPos = core.GetEndValue();
                        core.SetEndValue(core.GetLoopCount() % 2 != 0 ? firstEndVal : core.GetStartValue());
                        _time = 0;
                        break;
                    case LoopType.Restart:
                        core.GetSetter().SetSetter(core.GetStartValue());
                        _time = 0;
                        break;
                    case LoopType.Increment:
                        _time = 0;
                        break;
                }
            }
            yield break;
        }
        //public IEnumerator MoveEase(EasingCore<Quaternion, Transform> core)
        //{
        //    var objPos = core.GetSetter().GetSetter();
        //    var firstEndVal = core.GetEndValue();

        //    while (core.GetLoopCount() != 0)
        //    {
        //        while (_time < core.GetDuration())
        //        {
        //            _time += Time.deltaTime;
        //            if (_time >= core.GetDuration()) _time = core.GetDuration();
        //            var moveDis = (core.GetEndValue() - objPos);
        //            core.GetGetter().GetGetter().transform.rotation =Quaternion.Euler(objPos + (moveDis * EasingManager.EaseProgress(core.GetEase(), _time, core.GetDuration(), 0, 0)));
        //            yield return null;
        //        }
        //        core.SetLoopCount(core.GetLoopCount() - 1);
        //        switch (core.GetLoopType())
        //        {
        //            case LoopType.Yoyo:
        //                objPos = core.GetEndValue();
        //                core.SetEndValue(core.GetLoopCount() % 2 != 0 ? firstEndVal : core.GetStartValue());
        //                _time = 0;
        //                break;
        //            case LoopType.Restart:
        //                core.GetSetter().SetSetter(core.GetStartValue());
        //                _time = 0;
        //                break;
        //            case LoopType.Increment:
        //                _time = 0;
        //                break;
        //        }
        //    }
        //    yield break;
        //}
        public IEnumerator MoveEase(EasingCore<float ,Transform> core,int xyz)
        {
            var objPos = core.GetSetter().GetSetter();
            var firstEndVal = core.GetEndValue();

            while (core.GetLoopCount() != 0)
            {
                while (_time < core.GetDuration())
                {
                    _time += Time.deltaTime;
                    if (_time >= core.GetDuration()) _time = core.GetDuration();
                    var moveDis = (core.GetEndValue() - objPos);
                    switch (xyz) {
                        case 0:
                            core.GetSetter().SetSetter(objPos + (moveDis * EasingManager.EaseProgress(core.GetEase(), _time, core.GetDuration(), 0, 0)));
                            break;
                    }
                    yield return null;
                }
                core.SetLoopCount(core.GetLoopCount() - 1);
                switch (core.GetLoopType())
                {
                    case LoopType.Yoyo:
                        objPos = core.GetEndValue();
                        core.SetEndValue(core.GetLoopCount() % 2 != 0 ? firstEndVal : core.GetStartValue());
                        _time = 0;
                        break;
                    case LoopType.Restart:
                        core.GetSetter().SetSetter(core.GetStartValue());
                        _time = 0;
                        break;
                    case LoopType.Increment:
                        _time = 0;
                        break;
                }
            }
            yield break;
        }
    }
}
