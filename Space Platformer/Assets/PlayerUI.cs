using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public int lives;
    public int score;
    public TMP_Text scoreText;
    public Image life1;
    public Image life2;
    public Image life3;
    public Image life4;

    private void Start()
    {
        lives = 3;
        UpdateHeartImages();
    }

    public void TakeAwayLife()
    {
        lives--;
        UpdateHeartImages();
    }

    public void IncreaseLife()
    {
        lives++;
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
}
