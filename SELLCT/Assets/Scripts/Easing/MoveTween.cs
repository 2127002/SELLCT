using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TM.Easing.Management;
using TM.Easing.Core;

namespace TM.Easing
{
    /// <summary>
    /// Tween�A�j���[�V���������s����N���X
    /// </summary>
    public class MoveTween : MonoBehaviour
    {
        private float _time = 0f;

        /// <summary>
        /// �ړ������{����
        /// �ړ����Ԃ���P�t���[���ňړ����鋗�����v�Z���āA�������ړ�����悤�ɂ��Ă���
        /// </summary>
        /// <param name="obj">�ړ�����Q�[���I�u�W�F�N�g</param>
        /// <param name="endPos">���B�|�V�V����</param>
        /// <param name="duration">���B����܂ł̎���</param>
        /// <returns>�C�e���[�^�[�i�R���[�`���ŕK�v�j</returns>
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
