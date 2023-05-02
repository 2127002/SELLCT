using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TM.Easing;
using System;

namespace TM.Easing.Core
{
    /// <summary>
    /// Easingのコアとなる変数などを定義するクラス。
    /// </summary>
    /// <typeparam name="T1">startValue</typeparam>
    /// <typeparam name="T2">getter</typeparam>
    public class EasingCore<T1, T2>
    {
        private EaseType m_ease;
        private Getter<T2> m_getter;
        private Setter<T1> m_setter;
        private T1 m_startValue;
        public T1 StartVal
        {
            get { return m_startValue; }
            set { m_startValue = value; }
        }
        private T1 m_endValue;
        private float m_time;
        private float m_duration;
        private float m_overshootOrAmplitude;
        private float m_period;
        private LoopType m_loopType;
        private int m_loopCount = 1;
        private float m_delayTime;
        delegate void CallBack();
        //private event CallBack StartCallBack;
        //private event CallBack UpdateCallBack;
        //private event CallBack EndCallBack;

        public EasingCore(Getter<T2> getter, Setter<T1> setter, T1 startValue, T1 endValue, float duration)
        {
            m_getter = getter;
            m_setter = setter;
            m_startValue = startValue;
            m_endValue = endValue;
            m_duration = duration;
        }
        public EasingCore(Getter<T2> getter, Setter<T1> setter, T1 startValue, T1 endValue, float duration, float overshootOrAmplitude, float period)
        {
            m_getter = getter;
            SetSetter(setter);
            m_startValue = startValue;
            m_endValue = endValue;
            m_duration = duration;
            m_overshootOrAmplitude = overshootOrAmplitude;
            m_period = period;
        }
        public EasingCore(Getter<T2> getter, Setter<T1> setter, T1 startValue, T1 endValue, float duration, float overshootOrAmplitude)
        {
            m_getter = getter;
            m_setter = setter;
            m_startValue = startValue;
            m_endValue = endValue;
            m_duration = duration;
            m_overshootOrAmplitude = overshootOrAmplitude;
        }


        public void SetEaseType(EaseType easeType) => m_ease = easeType;
        public EaseType GetEase() => m_ease;
        public T1 GetStartValue() => m_startValue;
        public void SetStartValue(T1 startValue) => m_startValue = startValue;
        public T1 GetEndValue() => m_endValue;
        public void SetEndValue(T1 value) => m_endValue = value;
        public float GetDuration() => m_duration;
        public void SetTime(float time) => m_time = time;
        public float GetTime() => m_time;
        public void SetOvershootOrAmplitude(float value) => m_overshootOrAmplitude = value;
        public float GetOvershootOrAmplitude() => m_overshootOrAmplitude;
        public void SetPeriod(float value) => m_period = value;
        public float GetPeriod() => m_period;
        public Getter<T2> GetGetter() => m_getter;
        public Setter<T1> GetSetter() => m_setter;
        public void SetSetter(Setter<T1> setter) => m_setter = setter;
        public LoopType GetLoopType() => m_loopType;
        public void SetLoopType(LoopType loopType) => m_loopType = loopType;
        public int GetLoopCount() => m_loopCount;
        public void SetLoopCount(int value) => m_loopCount = value;
        public float GetDelayTime() => m_delayTime;
        public void SetDelayTime(float value) => m_delayTime = value;

        //public void OnStart( MethodName)
        //{
        //    m_startCallBack += MethodName;
        //}
    }
    public class Getter<T>
    {
        private T m_getter;
        public Getter(T getter) => m_getter = getter;
        public T GetGetter() => m_getter;
        public void SetGetter(T getter) => m_getter = getter;
    }

    public class Setter<T>
    {
        private T m_setter;
        public Setter(T setter) => m_setter = setter;
        public T GetSetter() => m_setter;
        public void SetSetter(T setter) => m_setter = setter;
    }

}
