using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E32_Rest : Card
{
    [SerializeField] PhaseController _phaseController = default!;

    [Header("探索フェーズU6")]
    [SerializeField] Image _E32CommandImage = default!;
    [SerializeField] Sprite _restSprite = default!;
    [SerializeField] Sprite _wakeUpSprite = default!;

    [SerializeField] ExplorationBackgroundView _backgroundView = default!;
    [SerializeField] TextBoxController _textBoxController = default!;
    [SerializeField] Floor01Condition _floor01Condition = default!;

    bool _isRest = false;

    public override int Id => 32;

    private void Awake()
    {
        _phaseController.OnGameStart.Add(OnGameStart);
    }

    private void OnDestroy()
    {
        _phaseController.OnGameStart.Remove(OnGameStart);
    }

    private void OnGameStart()
    {
        _E32CommandImage.gameObject.SetActive(_handMediator.ContainsCard(this));
    }

    public override void Buy()
    {
        _E32CommandImage.gameObject.SetActive(true);
    }

    public override void OnPressedU6Button()
    {
        if (_isRest)
        {
            _E32CommandImage.sprite = _wakeUpSprite;
            _backgroundView.ConvertWakeUp();
            _isRest = false;

            _textBoxController.UpdateText(null, "よし、探索再開だ。").Forget();
        }
        else
        {
            _E32CommandImage.sprite = _restSprite;
            _backgroundView.ConvertRest();
            _isRest = true;


            string s = "少し休憩するか。";
            if (_floor01Condition.OnRest())
            {
                s = "...あぁ、急がば回れとはこういうことだったんだな。この星座の道順を辿っていこう。";
            }

            _textBoxController.UpdateText(null, s).Forget();
        }
    }

    public override void Sell()
    {
        if (_handMediator.ContainsCard(this)) return;
        _E32CommandImage.gameObject.SetActive(false);
    }
}
