using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farmer : Enemy
{
    public Slider RageLevelIndicator;

    public Sprite EnragedRightSprite;
    public Sprite EnragedLeftSprite;
    public Sprite EnragedUpSprite;
    public Sprite EnragedDownSprite;

    public Ending endWindow;

    public AudioClip screamClip;

    private bool isEnraged;

    private IEnumerator calmCoroutine;


    readonly float rageLevelSurplus =25f;
    readonly float rageLevelDecrease = 0.5f;

    private float rageLevel;

    private float RageLevel
    {
        get
        {
            return rageLevel;
        }
        set
        {
            if (rageLevel < value)
                audioSource.PlayOneShot(screamClip);
            rageLevel = value;
            if (isEnraged && rageLevel < 70 && !isDirty)
            {
                SetNormal();
            }
            if (!isEnraged && rageLevel >= 70 && !isDirty)
            {
                SetEnraged();
            }

            isEnraged = rageLevel >= 70;
            RageLevelIndicator.value = rageLevel;
            if (rageLevel >= 100)
                endWindow.EndGame(true);
        }
    }

    protected override float CurrentSpeed
    {
        get
        {
            if (isDirty)
                return 0;

            if (isEnraged)
                return 7;

            return 5;
        }
    }

    void SetEnraged()
    {
        SetLook(moveDirection, EnragedRightSprite, EnragedLeftSprite, EnragedUpSprite, EnragedDownSprite);
    }

    protected override void SetClean()
    {
        if (isEnraged)
            SetEnraged();
        else
            SetNormal();

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            StopCoroutine(moveCoroutine);
            rageLevel += rageLevelSurplus;
            StartCoroutine(BeDirty());
            if (calmCoroutine != null)
                StopCoroutine(calmCoroutine);
            calmCoroutine = CalmTimer();
            StartCoroutine(calmCoroutine);
        }
    }

    private IEnumerator CalmTimer()
    {
        while (RageLevel > 0)
        {
            yield return new WaitForSeconds(1f);
            RageLevel -= rageLevelDecrease;
        }
    }

    public override void Rotate(Vector3 direction)
    {
        if (isEnraged)
            SetLook(direction, EnragedRightSprite, EnragedLeftSprite, EnragedUpSprite, EnragedDownSprite);
        else
            base.Rotate(direction);
    }

}
