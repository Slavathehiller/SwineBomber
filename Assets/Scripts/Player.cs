using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public GameObject bomb;
    bool bombAvailable = true;
    public float bombRestoreTime = 5;
    public Image bombRestoreIndicator;
    public Image harmIndicator;
    public Image holdIndicator;
    private bool isHarmed;
    readonly float harmTime = 5f;
    IEnumerator harmCoroutine;
    public Ending endWindow;
    bool isHold;
    float holdTime = 4f;
    public AudioClip screamClip;

    public float CurrentSpeed
    {
        get
        {
            if (isHold)
                return 0;
            if (isHarmed)
                return 4;
            return 9;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LayBomb()
    {
        if (bombAvailable && !isHold)
        {
            var bombInstance = Instantiate(bomb);
            bombInstance.transform.position = transform.position;
            StartCoroutine(DisableBomb());
        }
    }

    public void GetHarm()
    {
        audioSource.PlayOneShot(screamClip);
        if (harmCoroutine != null)
            StopCoroutine(harmCoroutine);
        harmCoroutine = Harm();
        StartCoroutine(harmCoroutine);
    }

    IEnumerator DisableBomb()
    {
        bombAvailable = false;
        bombRestoreIndicator.fillAmount = 0;
        for (var i = 0; i < 100; i += 5)
        {
            yield return new WaitForSeconds(bombRestoreTime/20);
            bombRestoreIndicator.fillAmount += 0.05f;
        }
        bombAvailable = true;
    }

    IEnumerator Harm()
    {
        isHarmed = true;
        harmIndicator.fillAmount = 1;
        for (var i = 0; i < 100; i += 5)
        {
            yield return new WaitForSeconds(harmTime/20);
            harmIndicator.fillAmount -= 0.05f;
        }
        isHarmed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            GetHarm();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            endWindow.EndGame(false);
        }
        if (collision.gameObject.tag == "Dog")
        {
            GetHold();            
        }
    }

    public void GetHold()
    {
        audioSource.PlayOneShot(screamClip);
        StartCoroutine(Hold());
    }

   IEnumerator Hold()
    {
        isHold = true;
        holdIndicator.fillAmount = 1;
        for (var i = 0; i < 100; i += 5)
        {
            yield return new WaitForSeconds(holdTime / 20);
            holdIndicator.fillAmount -= 0.05f;
        }
        isHold = false;
    }

}
