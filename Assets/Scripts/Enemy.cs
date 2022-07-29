using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private Sprite _dirtyRightSprite;
    [SerializeField] private Sprite _dirtyLeftSprite;
    [SerializeField] private Sprite _dirtyUpSprite;
    [SerializeField] private Sprite _dirtyDownSprite;
    protected Vector3 MoveDirection { get; set; }
    protected bool IsMoving { get; set; }
    protected bool IsDirty { get; set; }
    protected IEnumerator MoveCoroutine { set; get; }

    protected readonly float dirtyTime = 3f;
    protected override float CurrentSpeed
    {
        get
        {
            if (IsDirty)
                return 0;
            return 5;
        }
    }

    public override void SetLook(Vector3 direction)
    {
        if (IsDirty)
            SetDirty(direction);
        else
            SetNormal(direction);
    }

    private bool Stopped()
    {
        return Time.timeScale == 0 || IsDirty;
    }


    protected virtual void Update()
    {
        if (Stopped())
            return;
        if (IsMoving)
        {
            if (!TryMoveTo(MoveDirection))
                MoveDirection *= -1;
        }
        else
        {
            StartMoveRandom();
        }
    }

    protected void SetDirty(Vector3 direction)
    {
        SetLook(direction, _dirtyRightSprite,_dirtyLeftSprite,_dirtyUpSprite,_dirtyDownSprite);
    }

    protected void SetNormal(Vector3 direction)
    {
        base.SetLook(direction);
    }

    protected virtual void SetClean()
    {
        SetNormal(MoveDirection);
    }
 
    private Vector3 GetRandomDirection()
    {
        var rand = Random.Range(1, 5);
        switch (rand)
        {
            case 1:
                return Vector3.left;
            case 2:
                return Vector3.right;
            case 3:
                return Vector3.up;
            case 4:
                return Vector3.down;
            default:
                return Vector3.zero;
        }        
    }

    protected void StartMoveRandom()
    {
        ChangeMoveDirection(GetRandomDirection());
    }

    protected void StartMoveRandom(Vector3 forbiddenDirection)
    {
        Vector3 nextDirection;
        do
        {
            nextDirection = GetRandomDirection();
        }
        while (nextDirection == forbiddenDirection);
        ChangeMoveDirection(nextDirection);
    }

    private void ChangeMoveDirection(Vector3 nextDirection)
    {
        if (MoveCoroutine != null)
            StopCoroutine(MoveCoroutine);
        if (nextDirection != MoveDirection)
        {
            Rotate(nextDirection);
        }

        MoveDirection = nextDirection;

        var timeMoving = Random.Range(1, 3);

        MoveCoroutine = MoveForSeconds(timeMoving);
        StartCoroutine(MoveCoroutine);
    }

    private IEnumerator MoveForSeconds(int timeWait)
    {
        IsMoving = true;
        yield return new WaitForSeconds(timeWait);
        IsMoving = false;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (MoveCoroutine != null)
            StopCoroutine(MoveCoroutine);
        StartMoveRandom(forbiddenDirection: MoveDirection);     //Если упираемся во что-то, меняем направление на любое кроме текущего
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Bomb>(out _))
        {
            StopCoroutine(MoveCoroutine);
            StartCoroutine(BeDirty());
        }
    }

    protected IEnumerator BeDirty()
    {
        IsDirty = true;
        SetDirty(MoveDirection);
        yield return new WaitForSeconds(dirtyTime);
        SetClean();
        IsDirty = false;
    }
}
