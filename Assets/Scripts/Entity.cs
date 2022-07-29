using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public abstract class Entity : MonoBehaviour
{
    [SerializeField] private Sprite _rightSprite;
    [SerializeField] private Sprite _leftSprite;
    [SerializeField] private Sprite _upSprite;
    [SerializeField] private Sprite _downSprite;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SpriteRenderer _spriteRend;
    [SerializeField] protected Borders _borders;

    protected abstract float CurrentSpeed { get; }

    void Start()
    {
        _spriteRend = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    protected void Say(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public virtual void Rotate(Vector3 direction)
    {
        SetLook(direction);
    }

    public void SetLook(Vector3 direction, Sprite rightSprite, Sprite leftSprite, Sprite upSprite, Sprite downSprite)
    {
        if (direction.x > 0)
        {
            _spriteRend.sprite = rightSprite;
        }
        if (direction.x < 0)
        {
            _spriteRend.sprite = leftSprite;
        }
        if (direction.y > 0)
        {
            _spriteRend.sprite = upSprite;
        }
        if (direction.y < 0)
        {
            _spriteRend.sprite = downSprite;
        }
    }

    public virtual void SetLook(Vector3 direction)
    {
        SetLook(direction, _rightSprite, _leftSprite, _upSprite, _downSprite);
    }

    public bool TryMoveTo(Vector3 direction)
    {
        SetLook(direction);        
        var offset = CurrentSpeed * Time.deltaTime * direction;
        var nextPos = transform.position + offset;
        nextPos.x = Mathf.Clamp(nextPos.x, _borders.Left, _borders.Right);
        nextPos.y = Mathf.Clamp(nextPos.y, _borders.Lower, _borders.Upper);
        if (transform.position != nextPos)
        {
            transform.position = nextPos;
            return true;
        }
        else
            return false;
    }
}
