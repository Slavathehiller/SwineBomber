using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dog : Enemy
{

    private bool isHold;
    private float holdTime = 4f;
    public AudioClip barking;
    public AudioClip growl;

    // Update is called once per frame
    protected override void Update()
    {
        if (!isHold)
            base.Update();
    }

    private void FixedUpdate()
    {
        if (Random.Range(0, 500) == 0 && !isHold)
            audioSource.PlayOneShot(barking);
    }

    protected override float CurrentSpeed
    {
        get
        {
            if (isDirty || isHold)
                return 0;

            return 5;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetHold();
        }
        else
            base.OnCollisionEnter2D(collision);
    }

    private void GetHold()
    {
        audioSource.PlayOneShot(growl);
        StartCoroutine(Hold());
    }

    IEnumerator Hold()
    {
        isHold = true;
        yield return new WaitForSeconds(holdTime);
        isHold = false;
    }
}
