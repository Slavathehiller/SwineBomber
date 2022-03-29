using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Animator animator;
    AudioSource audioSource;
    public AudioClip burnClip;
    public AudioClip boomClip;
    public AudioClip settingClip;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(settingClip);
        StartCoroutine(StartTicking());        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartTicking()
    {
        audioSource.PlayOneShot(burnClip);
        yield return new WaitForSeconds(2);
        audioSource.Stop();
        Boom();
    }

    void Boom()
    {
        animator.SetTrigger("Boom");
        audioSource.PlayOneShot(boomClip);
        boxCollider.enabled = true;
        Destroy(gameObject, 0.7f);
    }
}
