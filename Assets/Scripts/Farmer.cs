using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Farmer : Enemy
{
    public Image RageLevelIndicator;
    [SerializeField] private Sprite _enragedRightSprite;
    [SerializeField] private Sprite _enragedLeftSprite;
    [SerializeField] private Sprite _enragedUpSprite;
    [SerializeField] private Sprite _enragedDownSprite;
    [SerializeField] private Ending _endWindow;
    [SerializeField] private AudioClip _screamClip;
    private IEnumerator _calmCoroutine;
    private readonly float _rageLevelSurplus = 25f;
    private readonly float _rageLevelDecrease = 0.5f;
    private float _rageLevel;
    private bool IsEnraged
    {
        get
        {
            return _rageLevel >= 70;
        }
    }

    private float RageLevel
    {
        get
        {
            return _rageLevel;
        }
        set
        {            
            _rageLevel = Mathf.Clamp(value, 0, 100);
            SetLook(MoveDirection);
            RageLevelIndicator.fillAmount = _rageLevel / 100;
            if (_rageLevel >= 100)
                _endWindow.EndGame(true);
        }
    }

    protected override float CurrentSpeed
    {
        get
        {
            if (IsDirty)
                return 0;

            if (IsEnraged)
                return 5;

            return 3;
        }
    }

    public override void SetLook(Vector3 direction)
    {
        if (IsDirty)
        {
            SetDirty(direction);
        }
        else
        {
            if (IsEnraged)
                SetEnraged(direction);
            else
                SetNormal(direction);
        }
    }

    void SetEnraged(Vector3 direction)
    {
        Say(_screamClip);
        SetLook(direction, _enragedRightSprite, _enragedLeftSprite, _enragedUpSprite, _enragedDownSprite);
    }

    protected override void SetClean()
    {
        if (IsEnraged)
            SetEnraged(MoveDirection);
        else
            SetNormal(MoveDirection);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Bomb>(out _))
        {
            StopCoroutine(MoveCoroutine);
            RageLevel += _rageLevelSurplus;
            StartCoroutine(BeDirty());
            if (_calmCoroutine != null)
                StopCoroutine(_calmCoroutine);
            _calmCoroutine = CalmTimer();
            StartCoroutine(_calmCoroutine);
        }
    }

    private IEnumerator CalmTimer()
    {
        while (RageLevel > 0)
        {
            yield return new WaitForSeconds(1f);
            RageLevel -= _rageLevelDecrease;
        }
    }

    public override void Rotate(Vector3 direction)
    {
        if (IsEnraged)
            SetLook(direction, _enragedRightSprite, _enragedLeftSprite, _enragedUpSprite, _enragedDownSprite);
        else
            base.Rotate(direction);
    }
}
