using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationBackgroundController : MonoBehaviour
{
    [SerializeField] ExplorationBackgroundView _explorationBackgroundView = default!;

    [Header("U6�̃{�^���摜")]
    [SerializeField] Image _E32CommandImage = default!;
    [SerializeField] Sprite _wakeUpSprite = default!;
    [SerializeField] Sprite _restSprite = default!;

    [SerializeField] TextBoxController _textBoxController = default!;
    [SerializeField] ExplorationNextButtonController _nextButtonController = default!;

    //������p�̉��u���ł��B
    [SerializeField] Floor01Condition _floor01Condition = default!;

    bool _isRest = false;

    private void Reset()
    {
        _explorationBackgroundView = GetComponent<ExplorationBackgroundView>();
        _textBoxController = FindObjectOfType<TextBoxController>();
    }

    private void ConvertWakeUp()
    {
        _E32CommandImage.sprite = _wakeUpSprite;
        _explorationBackgroundView.ConvertWakeUp();
        _isRest = false;

        _textBoxController.UpdateText(null, "�悵�A�T���ĊJ���B").Forget();
    }

    private void ConvertRest()
    {
        _E32CommandImage.sprite = _restSprite;
        _explorationBackgroundView.ConvertRest();
        _isRest = true;

        string s = "�����x�e���邩�B";
        if (_floor01Condition.OnRest())
        {
            s = "...�����A�}���Ή��Ƃ͂����������Ƃ������񂾂ȁB���̐����̓�����H���Ă������B";
        }

        _textBoxController.UpdateText(null, s).Forget();
    }

    public void OnPressedU6Button()
    {
        if (_isRest)
        {
            _nextButtonController.Enable(ExplorationNextButtonController.PatternType.ExplorationU6);
            ConvertWakeUp();
        }
        else
        {
            _nextButtonController.Disable(ExplorationNextButtonController.PatternType.ExplorationU6);
            ConvertRest();
        }
    }

    public void SetActiveE32Command(bool enabled)
    {
        _E32CommandImage.gameObject.SetActive(enabled);
    }
}
