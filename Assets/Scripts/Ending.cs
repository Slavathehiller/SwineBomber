using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public Image WinImage;
    public Image LoseImage;

    private AudioSource audioSource;
    public AudioClip winClip;
    public AudioClip loseClip;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame(bool weWin)
    {
        gameObject.SetActive(true);
        audioSource = GetComponent<AudioSource>();
        WinImage.enabled = weWin;
        LoseImage.enabled = !weWin;
        if (weWin)
            audioSource.PlayOneShot(winClip);
        else
            audioSource.PlayOneShot(loseClip);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }

}
