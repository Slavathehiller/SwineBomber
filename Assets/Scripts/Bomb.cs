using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private AudioClip _burnClip;
    [SerializeField] private AudioClip _boomClip;
    [SerializeField] private AudioClip _settingClip;
    private BoxCollider2D _boxCollider;
    private Animator _animator;
    private AudioSource _audioSource;
    private readonly float _burnTime = 2f;
    private readonly string _boomTriggerName = "Boom";

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_settingClip);
        StartCoroutine(StartTicking());        
    }

    private IEnumerator StartTicking()
    {
        _audioSource.PlayOneShot(_burnClip);
        yield return new WaitForSeconds(_burnTime);
        _audioSource.Stop();
        Boom();
    }

    private void Boom()
    {
        _animator.SetTrigger(_boomTriggerName);
        _audioSource.PlayOneShot(_boomClip);
        _boxCollider.enabled = true;
        Destroy(gameObject, 0.7f);
    }
}
