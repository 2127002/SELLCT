using UnityEngine;
using TM.Easing.Management;
using TM.Easing.Core;
using System;

// ��낤�Ǝv�����́@4/24�X�V
//----------------------------------------------
//�������Q�l�T�C�g������ https://easings.net/ja  
//                       https://qiita.com/broken55/items/df152c061da759ad1471
//�z��g�p��FEasing.To(1f,transform.position,transform.position+new Vector3(0,0,5)).SetEase(Linear).OnStart(hoge());
//ok�Etransform.�Z�Z�Ŏg����悤�ɂ���
//ok�EEasing�̃p�^�[���S��ގ���
//ok?�E1���W����transform(vector3)�܂ŗl�X�ȃp�^�[���ŏo����悤�ɂ��B
//ok�E�����̓��e�F�iEasing����(�m��)�A�J�n�ʒu�̐ݒ�(�����ݒ�͌��ݒn)�A�I���l(�m��)�j
//�E���W / �}�e���A��(�J���[) / �A���t�@�l / �p�X��ʂ�i�������i�s�����������j
//�E���t���[�����s�����R�[���o�b�N�A�J�n���Ɏ��s�����R�[���o�b�N�A�I�����Ɏ��s�����R�[���o�b�N
//�ESequence�i��x�ɓ������s�ł�����́j
//ok�E�J��Ԃ��p�^�[���A�񐔁A
//----------------------------------------------

namespace TM.Easing
{
    static class Easing
    {
        /// <summary>
        /// �w�����ꂽ���ԂŎ��̏ꏊ�Ɉړ�����A�j���[�V����
        /// </summary>
        /// <param name="startTrans"></param>
        /// <param name="endPos">�ړ���̈ʒu</param>
        /// <param name="duration">�ړ��ɂ����鎞�ԁi�b�j�����l�F2</param>
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


        /// <param name="getter">�������������</param>
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

        //�C�[�W���O�����s����֐��B���\�b�h�`�F�[���̍Ō�ɌĂ�ł�������
        #region Go
        public static EasingCore<Vector2, Transform> Go(this EasingCore<Vector2, Transform> core)
        {
            //??���Z�q��null��������N���鏈���B
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
