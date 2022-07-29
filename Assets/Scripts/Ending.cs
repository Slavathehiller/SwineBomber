using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [SerializeField] private Image _winImage;
    [SerializeField] private Image _loseImage;
    [SerializeField] private AudioClip _winClip;
    [SerializeField] private AudioClip _loseClip;
    private AudioSource _audioSource;
    public void EndGame(bool weWin)
    {
        gameObject.SetActive(true);
        _audioSource = GetComponent<AudioSource>();
        _winImage.enabled = weWin;
        _loseImage.enabled = !weWin;
        if (weWin)
            _audioSource.PlayOneShot(_winClip);
        else
            _audioSource.PlayOneShot(_loseClip);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
