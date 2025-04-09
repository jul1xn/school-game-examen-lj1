using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public AudioSource hitAudio;
    public AudioSource upgradeAudio;
    public int lives;
    public int score;
    [Space]
    public GameObject mainUI;
    public GameObject pauseUI;
    public GameObject deathUI;
    public GameObject completeUI;
    [Space]
    public TMP_Text scoreText;
    public Image life1;
    public Image life2;
    public Image life3;
    public Image life4;
    public GameObject partPopup;
    public Image part;

    private void Start()
    {
        score = PlayerPrefs.GetInt("score", 0);
        scoreText.text = score.ToString();
        mainUI.SetActive(true);
        deathUI.SetActive(false);
        completeUI.SetActive(false);
        pauseUI.SetActive(false);
        lives = 3;
        UpdateHeartImages();
        partPopup.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !deathUI.activeSelf)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        mainUI.SetActive(!mainUI.activeSelf);
        pauseUI.SetActive(!pauseUI.activeSelf);

        if (pauseUI.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void TakeAwayLife()
    {
        lives--;
        UpdateHeartImages();
        hitAudio.Play();

        if (lives <= 0)
        {
            KillPlayer();
        }
    }

    public void IncreaseLife()
    {
        lives++;
        upgradeAudio.Play();
        if (lives > 4) { lives = 4; }
        UpdateHeartImages();
    }

    private void UpdateHeartImages()
    {
        life1.enabled = false;
        life2.enabled = false;
        life3.enabled = false;
        life4.enabled = false;

        if (lives > 0) { life1.enabled = true; }
        if (lives > 1) { life2.enabled = true; }
        if (lives > 2) { life3.enabled = true; }
        if (lives > 3) { life4.enabled = true; }
    }

    public void UpdateScore(int increase)
    {
        score += increase;
        scoreText.text = score.ToString();
    }

    public void ShowPartPopup(Sprite sprite)
    {
        StartCoroutine(PartPopup(sprite));
    }

    private IEnumerator PartPopup(Sprite sprite)
    {
        part.sprite = sprite;
        partPopup.SetActive(true);
        yield return new WaitForSeconds(5f);
        partPopup.SetActive(false);
    }

    public void KillPlayer()
    {
        PlayerController.instance.gameObject.SetActive(false);
        mainUI.SetActive(false);
        deathUI.SetActive(true);
        pauseUI.SetActive(false);
    }

    public void CompleteLvl()
    {
        int newMangos = PlayerPrefs.GetInt("score", 0) + score;
        PlayerPrefs.SetInt("score", newMangos);
        completeUI.SetActive(true);
        mainUI.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void BtnRetry()
    {
        SceneLoader.Instance.LoadScene(SceneLoader.GetCurrentSceneIndex());
    }

    public void BtnNextLvl()
    {
        SceneLoader.Instance.LoadScene(SceneLoader.GetCurrentSceneIndex() + 1);
    }
}
