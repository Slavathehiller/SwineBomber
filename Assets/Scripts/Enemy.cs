using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Entity
{

    public Sprite DirtyRightSprite;
    public Sprite DirtyLeftSprite;
    public Sprite DirtyUpSprite;
    public Sprite DirtyDownSprite;

    protected Vector3 moveDirection { get; set; }
    protected bool isMoving { get; set; }
    protected bool isDirty { get; set; }
    protected IEnumerator moveCoroutine { set; get; }

    protected readonly float dirtyTime = 3f;

    protected virtual float CurrentSpeed
    {
        get
        {
            if (isDirty)
                return 0;

            return 5;
        }
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        if (isMoving)
        {
            MoveBy(CurrentSpeed * Time.deltaTime * moveDirection);
        }
        else
        {
            StartMoveRandom();
        }
    }



    protected void SetDirty()
    {
        SetLook(moveDirection, DirtyRightSprite, DirtyLeftSprite, DirtyUpSprite, DirtyDownSprite);
    }

    protected void SetNormal()
    {
        SetLook(moveDirection);
    }

    protected virtual void SetClean()
    {
        SetNormal();
    }

    private Vector3 getRandomDirection()
    {
        var rand = Mathf.RoundToInt(Random.Range(1, 5));
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
        changeMoveDirection(getRandomDirection());
    }

    protected void StartMoveRandom(Vector3 foorbiddenDirection)
    {
        Vector3 nextDirection;
        do
        {
            nextDirection = getRandomDirection();
        }
        while (nextDirection == foorbiddenDirection);
        changeMoveDirection(nextDirection);
    }

    private void changeMoveDirection(Vector3 nextDirection)
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
        if (nextDirection != moveDirection)
        {
            Rotate(nextDirection);
        }

        moveDirection = nextDirection;

        var timeMoving = Random.Range(1, 3);

        moveCoroutine = MoveForSeconds(timeMoving);
        StartCoroutine(moveCoroutine);
    }

    IEnumerator MoveForSeconds(int timeWait)
    {
        isMoving = true;
        yield return new WaitForSeconds(timeWait);
        isMoving = false;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
        if (collision.gameObject.tag == "EdgeOfMap")
        {
            changeMoveDirection(moveDirection * -1);        //Если упираемся в стену, разворачиваемся
        }
        else
        {
            StartMoveRandom(moveDirection);                //Если упираемся во что-то другое, меняем направление на любое кроме текущего
    }
}

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            StopCoroutine(moveCoroutine);
            StartCoroutine(BeDirty());
        }
    }

    protected IEnumerator BeDirty()
    {
        isDirty = true;
        SetDirty();
        yield return new WaitForSeconds(dirtyTime);
        SetClean();
        isDirty = false;
    }


}
