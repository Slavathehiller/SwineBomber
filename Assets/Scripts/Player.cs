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
    private bool isHarmed;
    readonly float harmTime = 5f;
    IEnumerator harmCoroutine;
    public Ending endWindow;

    public float CurrentSpeed
    {
        get
        {
            if (isHarmed)
                return 5;
            return 10;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LayBomb()
    {
        if (bombAvailable)
        {
            var bombInstance = Instantiate(bomb);
            bombInstance.transform.position = transform.position;
            StartCoroutine(DisableBomb());
        }
    }

    public void GetHarm()
    {
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
    }
}
