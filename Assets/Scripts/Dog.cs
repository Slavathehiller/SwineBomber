using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dog : Enemy
{
    [SerializeField] private AudioClip _barking;
    [SerializeField] private AudioClip _growl;
    private bool _isHolding;
    private float _holdTime = 4f;

    protected override void Update()
    {
        if (!_isHolding)
            base.Update();
    }

    private void FixedUpdate()
    {
        if (Random.Range(0, 500) == 0 && !_isHolding)
            Say(_barking);
    }

    protected override float CurrentSpeed
    {
        get
        {
            if (IsDirty || _isHolding)
                return 0;

            return 5;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            GetHold();
        }
        else
            base.OnCollisionEnter2D(collision);
    }

    private void GetHold()
    {
        Say(_growl);
        StartCoroutine(Hold());
    }

    private IEnumerator Hold()
    {
        _isHolding = true;
        yield return new WaitForSeconds(_holdTime);
        _isHolding = false;
    }
}
