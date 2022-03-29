using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRend;
    public Sprite RightSprite;
    public Sprite LeftSprite;
    public Sprite UpSprite;
    public Sprite DownSprite;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void Rotate(Vector3 direction)
    {
        SetLook(direction);
    }

    public void SetLook(Vector3 direction, Sprite rightSprite, Sprite leftSprite, Sprite upSprite, Sprite downSprite)
    {
        if (direction.x > 0)
        {
            spriteRend.sprite = rightSprite;
        }
        if (direction.x < 0)
        {
            spriteRend.sprite = leftSprite;
        }
        if (direction.y > 0)
        {
            spriteRend.sprite = upSprite;
        }
        if (direction.y < 0)
        {
            spriteRend.sprite = downSprite;
        }
    }

    public virtual void SetLook(Vector3 direction)
    {
        SetLook(direction, RightSprite, LeftSprite, UpSprite, DownSprite);
    }


    public void MoveTo(Vector3 position)
    {
        transform.position = position;
    }

    public void MoveBy(Vector3 offset)
    {
        var pos = transform.position;
        pos += offset;
        transform.position = pos;
    }

}
