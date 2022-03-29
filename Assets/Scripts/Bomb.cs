using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(StartTicking());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartTicking()
    {
        yield return new WaitForSeconds(2);
        Boom();
    }

    void Boom()
    {
        animator.SetTrigger("Boom");
        boxCollider.enabled = true;
        Destroy(gameObject, 0.7f);
    }
}
