using UnityEngine;
using TM.Easing.Management;
using TM.Easing.Core;
using System;

// 作ろうと思うもの　4/24更新
//----------------------------------------------
//＜＜＜参考サイト＞＞＞ https://easings.net/ja  
//                       https://qiita.com/broken55/items/df152c061da759ad1471
//想定使用例：Easing.To(1f,transform.position,transform.position+new Vector3(0,0,5)).SetEase(Linear).OnStart(hoge());
//ok・transform.〇〇で使えるようにする
//ok・Easingのパターン全種類実装
//ok?・1座標からtransform(vector3)まで様々なパターンで出来るようにやる。
//ok・引数の内容：（Easing時間(確定)、開始位置の設定(初期設定は現在地)、終了値(確定)）
//・座標 / マテリアル(カラー) / アルファ値 / パスを通る（向きも進行方向を向く）
//・毎フレーム実行されるコールバック、開始時に実行されるコールバック、終了時に実行されるコールバック
//・Sequence（一度に同時実行できるもの）
//ok・繰り返しパターン、回数、
//----------------------------------------------

namespace TM.Easing
{
    static class Easing
    {
        /// <summary>
        /// 指示された時間で次の場所に移動するアニメーション
        /// </summary>
        /// <param name="startTrans"></param>
        /// <param name="endPos">移動後の位置</param>
        /// <param name="duration">移動にかける時間（秒）初期値：2</param>
        /// <returns></returns>
        public static EasingCore<Vector3, Transform> Move(this Transform startTrans, Vector3 endPos, float duration = 2f)
        {
            var core = To(startTrans,startTrans.position,endPos ,duration);
            return core;
        }
        public static EasingCore<Vector2, Transform> Move(this Transform startTrans, Vector2 endPos, float duration = 2f)
        {
            var core = To(startTrans, endPos, duration);
            return core;
        }
        public static EasingCore<Quaternion, Transform> Rotate(this Transform startTrans, Quaternion endPos, float duration = 2f)
        {
            var core = To(startTrans, startTrans.rotation,endPos, duration);
            return core;
        }


        public static EasingCore<float, Transform> MoveX(this Transform startTrans, float endPos, float duration = 2f)
        {
            float a = startTrans.position.x;
            var core = To(startTrans, ref a, endPos, duration);
            return core;
        }
        public static EasingCore<float, Transform> MoveY(this Transform startTrans, float endPos, float duration = 2f)
        {
            var getter = new Getter<Transform>(startTrans);
            var setter = new Setter<float>(startTrans.position.y);
            var core = new EasingCore<float, Transform>(getter, setter, startTrans.position.x, endPos, duration);
            return core;
        }
        public static EasingCore<float, Transform> MoveZ(this Transform startTrans, float endPos, float duration = 2f)
        {
            var getter = new Getter<Transform>(startTrans);
            var setter = new Setter<float>(startTrans.position.z);
            var core = new EasingCore<float, Transform>(getter, setter, startTrans.position.x, endPos, duration);
            return core;
        }


        /// <param name="getter">動かしたいやつ</param>
        public static EasingCore<Vector3, Transform> To(Transform getter,Vector3 setter, Vector3 endPos, float duration = 2f)
        {
            var m_getter = new Getter<Transform>(getter);
            var m_setter = new Setter<Vector3>(setter);
            var core = new EasingCore<Vector3, Transform>(m_getter, m_setter, getter.position, endPos, duration);
            return core;
        }
        public static EasingCore<Vector2, Transform> To(Transform getter, Vector2 endPos, float duration = 2f)
        {
            var m_getter = new Getter<Transform>(getter);
            var m_setter = new Setter<Vector2>(getter.position);
            var core = new EasingCore<Vector2, Transform>(m_getter, m_setter, getter.position, endPos, duration);
            return core;
        }
        public static EasingCore<float, Transform> To(Transform getter,ref float setter, float endPos, float duration = 2f)
        {
            var m_getter = new Getter<Transform>(getter);
            var m_setter = new Setter<float>(setter);
            var core = new EasingCore<float, Transform>(m_getter, m_setter, setter, endPos, duration);
            return core;
        }

        public static EasingCore<Quaternion, Transform> To(Transform getter, Quaternion setter, Quaternion endPos, float duration = 2f)
        {
            var m_getter = new Getter<Transform>(getter);
            var m_setter = new Setter<Quaternion>(setter);
            var core = new EasingCore<Quaternion, Transform>(m_getter, m_setter, setter, endPos, duration);
            return core;
        }

        #region SetEase
        public static EasingCore<Vector2, Vector2> SetEase(this EasingCore<Vector2, Vector2> core, EaseType ease)
        {
            core.SetEaseType(ease);
            return core;
        }
        public static EasingCore<Vector2, Transform> SetEase(this EasingCore<Vector2, Transform> core, EaseType ease)
        {
            core.SetEaseType(ease);
            return core;
        }
        public static EasingCore<Vector3, Vector3> SetEase(this EasingCore<Vector3, Vector3> core, EaseType ease)
        {
            core.SetEaseType(ease);
            return core;
        }
        public static EasingCore<Vector3, Transform> SetEase(this EasingCore<Vector3, Transform> core, EaseType ease)
        {
            core.SetEaseType(ease);
            return core;
        }
        public static EasingCore<float, float> SetEase(this EasingCore<float, float> core, EaseType ease)
        {
            core.SetEaseType(ease);
            return core;
        }
        public static EasingCore<float, Transform> SetEase(this EasingCore<float, Transform> core, EaseType ease)
        {
            core.SetEaseType(ease);
            return core;
        }
        public static EasingCore<Vector2, Vector2> SetEase(this EasingCore<Vector2, Vector2> core, EaseType ease, float amplitude, float period)
        {
            core.SetEaseType(ease);
            core.SetOvershootOrAmplitude(amplitude);
            core.SetPeriod(period);
            return core;
        }
        public static EasingCore<Vector2, Transform> SetEase(this EasingCore<Vector2, Transform> core, EaseType ease, float amplitude, float period)
        {
            core.SetEaseType(ease);
            core.SetOvershootOrAmplitude(amplitude);
            core.SetPeriod(period);
            return core;
        }
        public static EasingCore<Vector3, Vector3> SetEase(this EasingCore<Vector3, Vector3> core, EaseType ease, float amplitude, float period)
        {
            core.SetEaseType(ease);
            core.SetOvershootOrAmplitude(amplitude);
            core.SetPeriod(period);
            return core;
        }
        public static EasingCore<Vector3, Transform> SetEase(this EasingCore<Vector3, Transform> core, EaseType ease, float amplitude, float period)
        {
            core.SetEaseType(ease);
            core.SetOvershootOrAmplitude(amplitude);
            core.SetPeriod(period);
            return core;
        }
        public static EasingCore<float, float> SetEase(this EasingCore<float, float> core, EaseType ease, float amplitude, float period)
        {
            core.SetEaseType(ease);
            core.SetOvershootOrAmplitude(amplitude);
            core.SetPeriod(period);
            return core;
        }
        public static EasingCore<float, Transform> SetEase(this EasingCore<float, Transform> core, EaseType ease, float amplitude, float period)
        {
            core.SetEaseType(ease);
            core.SetOvershootOrAmplitude(amplitude);
            core.SetPeriod(period);
            return core;
        }
        public static EasingCore<Vector2, Vector2> SetEase(this EasingCore<Vector2, Vector2> core, EaseType ease, float overshoot)
        {
            core.SetEaseType(ease);
            core.SetOvershootOrAmplitude(overshoot);
            return core;
        }
        public static EasingCore<Vector2, Transform> SetEase(this EasingCore<Vector2, Transform> core, EaseType ease, float overshoot)
        {
            core.SetEaseType(ease);
            core.SetOvershootOrAmplitude(overshoot);
            return core;
        }
        public static EasingCore<Vector3, Vector3> SetEase(this EasingCore<Vector3, Vector3> core, EaseType ease, float overshoot)
        {
            core.SetEaseType(ease);
            core.SetOvershootOrAmplitude(overshoot);
            return core;
        }
        public static EasingCore<Vector3, Transform> SetEase(this EasingCore<Vector3, Transform> core, EaseType ease, float overshoot)
        {
            core.SetEaseType(ease);
            core.SetOvershootOrAmplitude(overshoot);
            return core;
        }
        public static EasingCore<float, float> SetEase(this EasingCore<float, float> core, EaseType ease, float overshoot)
        {
            core.SetEaseType(ease);
            core.SetOvershootOrAmplitude(overshoot);
            return core;
        }
        public static EasingCore<float, Transform> SetEase(this EasingCore<float, Transform> core, EaseType ease, float overshoot)
        {
            core.SetEaseType(ease);
            core.SetOvershootOrAmplitude(overshoot);
            return core;
        }
        public static EasingCore<Quaternion, Transform> SetEase(this EasingCore<Quaternion, Transform> core, EaseType ease)
        {
            core.SetEaseType(ease);
            return core;
        }
        #endregion

        #region SetLoops
        public static EasingCore<Vector2, Transform> SetLoops(this EasingCore<Vector2,Transform> core,int loopCount,LoopType loopType=LoopType.Yoyo)
        {
            core.SetLoopCount(loopCount);
            core.SetLoopType(loopType);
            return core;
        }
        public static EasingCore<Vector3, Transform> SetLoops(this EasingCore<Vector3, Transform> core,int loopCount,LoopType loopType=LoopType.Yoyo)
        {
            core.SetLoopCount(loopCount);
            core.SetLoopType(loopType);
            return core;
        }
        public static EasingCore<float, Transform> SetLoops(this EasingCore<float, Transform> core,int loopCount,LoopType loopType=LoopType.Yoyo)
        {
            core.SetLoopCount(loopCount);
            core.SetLoopType(loopType);
            return core;
        }
        public static EasingCore<Quaternion, Transform> SetLoops(this EasingCore<Quaternion, Transform> core,int loopCount,LoopType loopType=LoopType.Yoyo)
        {
            core.SetLoopCount(loopCount);
            core.SetLoopType(loopType);
            return core;
        }

        #endregion

        #region SetDelay
        public static EasingCore<Vector2,Transform> SetDelay(this EasingCore<Vector2, Transform> core, float delay)
        {
            core.SetDelayTime(delay);
            return core;
        }
        public static EasingCore<Vector3,Transform> SetDelay(this EasingCore<Vector3, Transform> core, float delay)
        {
            core.SetDelayTime(delay);
            return core;
        }
        public static EasingCore<float,Transform> SetDelay(this EasingCore<float, Transform> core, float delay)
        {
            core.SetDelayTime(delay);
            return core;
        }
        public static EasingCore<Quaternion,Transform> SetDelay(this EasingCore<Quaternion, Transform> core, float delay)
        {
            core.SetDelayTime(delay);
            return core;
        }


        #endregion

        //イージングを実行する関数。メソッドチェーンの最後に呼んでください
        #region Go
        public static EasingCore<Vector2, Transform> Go(this EasingCore<Vector2, Transform> core)
        {
            //??演算子はnullだったら起こる処理。
            MoveTween moveTween = core.GetGetter().GetGetter().GetComponent<MoveTween>()
                ?? core.GetGetter().GetGetter().gameObject.AddComponent<MoveTween>();

            moveTween.StartCoroutine(moveTween.MoveEase(core));
            return core;
        }
        public static EasingCore<Vector3, Transform> Go(this EasingCore<Vector3, Transform> core)
        {
            MoveTween moveTween = core.GetGetter().GetGetter().GetComponent<MoveTween>()
                ?? core.GetGetter().GetGetter().gameObject.AddComponent<MoveTween>();

            moveTween.StartCoroutine(moveTween.MoveEase(core));
            return core;
        }
        public static EasingCore<Quaternion, Transform> Go(this EasingCore<Quaternion, Transform> core)
        {
            MoveTween moveTween = core.GetGetter().GetGetter().GetComponent<MoveTween>()
                ?? core.GetGetter().GetGetter().gameObject.AddComponent<MoveTween>();

            //moveTween.StartCoroutine(moveTween.MoveEase(core));
            return core;
        }
        public static EasingCore<float, Transform> Go(this EasingCore<float, Transform> core)
        {
            //MoveTween moveTween = core.GetGetter().GetGetter().GetComponent<MoveTween>()
            //    ?? core.GetGetter().GetGetter().gameObject.AddComponent<MoveTween>();

            //moveTween.StartCoroutine(moveTween.MoveEase(core.GetEase(), core.GetSetter(), core.GetEndValue(), core.GetDuration()));
            return core;
        }
        #endregion

    }

}
