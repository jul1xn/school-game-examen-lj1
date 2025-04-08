using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public int score;
    public TMP_Text scoreText;

    public void UpdateScore(int increase)
    {
        score += increase;
        scoreText.text = score.ToString();
    }
}
