using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    [SerializeField] private GameObject _bomb;
    [SerializeField] private Image _harmIndicator;
    [SerializeField] private Image _holdIndicator;
    [SerializeField] private Image _bombRestoreIndicator;
    [SerializeField] private float _bombRestoreTime = 5;
    [SerializeField] private Ending _endWindow;
    [SerializeField] private AudioClip _screamClip;
    private bool _bombAvailable = true;
    private bool _isHarmed;
    private readonly float _harmTime = 5f;
    private IEnumerator _harmCoroutine;    
    private bool _isHolding;
    private float _holdTime = 4f;

    protected override float CurrentSpeed
    {
        get
        {
            if (_isHolding)
                return 0;
            if (_isHarmed)
                return 4;
            return 9;
        }
    }

    public void LayBomb()
    {
        if (_bombAvailable && !_isHolding)
        {
            var bombInstance = Instantiate(_bomb);
            bombInstance.transform.position = transform.position;
            StartCoroutine(DisableBomb());
        }
    }

    public void GetHarm()
    {
        Say(_screamClip);
        if (_harmCoroutine != null)
            StopCoroutine(_harmCoroutine);
        _harmCoroutine = Harm();
        StartCoroutine(_harmCoroutine);
    }

    private IEnumerator DisableBomb()
    {
        _bombAvailable = false;
        _bombRestoreIndicator.fillAmount = 0;
        for (var i = 0; i < 100; i += 5)
        {
            yield return new WaitForSeconds(_bombRestoreTime/20);
            _bombRestoreIndicator.fillAmount += 0.05f;
        }
        _bombAvailable = true;
    }

    private IEnumerator Harm()
    {
        _isHarmed = true;
        _harmIndicator.fillAmount = 1;
        for (var i = 0; i < 100; i += 5)
        {
            yield return new WaitForSeconds(_harmTime/20);
            _harmIndicator.fillAmount -= 0.05f;
        }
        _isHarmed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Bomb>(out _))
        {
            GetHarm();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Farmer>(out _))
        {
            _endWindow.EndGame(false);
        }
        if (collision.gameObject.TryGetComponent<Dog>(out _))
        {
            GetHold();            
        }
    }

    public void GetHold()
    {
        Say(_screamClip);
        StopAllCoroutines();
        StartCoroutine(Hold());
    }

   private IEnumerator Hold()
    {
        _isHolding = true;
        _holdIndicator.fillAmount = 1;
        for (var i = 0; i < 100; i += 5)
        {
            yield return new WaitForSeconds(_holdTime / 20);
            _holdIndicator.fillAmount -= 0.05f;
        }
        _isHolding = false;
    }
}
