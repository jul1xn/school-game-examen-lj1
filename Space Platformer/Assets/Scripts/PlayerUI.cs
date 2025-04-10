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
    public TMP_Text stopwatchText;
    private float elapsedTime = 0f;
    private bool stopwatchRunning = false;
    [Space]
    public GameObject mainUI;
    public GameObject pauseUI;
    public GameObject deathUI;
    public GameObject completeUI;
    [Space]
    public TMP_Text foundText;
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public Image life1;
    public Image life2;
    public Image life3;
    public Image life4;
    public GameObject partPopup;
    public Image part;

    private void Start()
    {
        stopwatchRunning = true;
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

        if (stopwatchRunning)
        {
            elapsedTime += Time.deltaTime;
            stopwatchText.text = FormattedTime(elapsedTime);
        }
    }

    public static string FormattedTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 100f) % 100);

        return string.Format("{0:D2}:{1:D2};{2:D2}", minutes, seconds, milliseconds);
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
        stopwatchRunning = false;
        PlayerPrefs.SetInt("part_" + (SceneLoader.GetCurrentSceneIndex() + 1), 0);
        PlayerController.instance.gameObject.SetActive(false);
        mainUI.SetActive(false);
        deathUI.SetActive(true);
        pauseUI.SetActive(false);
    }

    bool lvlCompleted = false;

    public void CompleteLvl()
    {
        stopwatchRunning = false;
        int newMangos = PlayerPrefs.GetInt("score", 0) + score;
        PlayerPrefs.SetInt("score", newMangos);
        int found = PlayerPrefs.GetInt("part_" + (SceneLoader.GetCurrentSceneIndex() + 1), 0);
        Debug.Log(found);

        if (found == 0)
        {
            timeText.text = "";
            foundText.text = "You didn't find the spaceship part :(";
        }
        else
        {
            timeText.text = $"Time: {FormattedTime(elapsedTime)}";
            string lvlString = "level_" + SceneLoader.GetCurrentSceneIndex();
            float value = PlayerPrefs.GetFloat(lvlString, float.MaxValue);
            if (value > elapsedTime)
            {
                PlayerPrefs.SetFloat(lvlString, elapsedTime);
                timeText.text += " (New high score!)";
            }

            foundText.text = "You found the spaceship part! :D";
        }

        completeUI.SetActive(true);
        mainUI.SetActive(true);
        Time.timeScale = 0.0f;
        lvlCompleted = true;
    }

    public void BtnRetry()
    {
        if (lvlCompleted)
        {
            int newMangos = PlayerPrefs.GetInt("score", 0) - score;
            PlayerPrefs.SetInt("score", newMangos);
        }
        SceneLoader.Instance.LoadScene(SceneLoader.GetCurrentSceneIndex());
    }

    public void BtnNextLvl()
    {
        SceneLoader.Instance.LoadScene(SceneLoader.GetCurrentSceneIndex() + 1);
    }

    public void BtnMenu()
    {
        SceneLoader.Instance.LoadScene(0);
    }
}
