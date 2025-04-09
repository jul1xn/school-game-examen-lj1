using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public int[] levelIndexs;
    public int introLvl = 1;
    private bool gameStarted = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        Initialize();
    }

    private void Initialize()
    {
        int gs = PlayerPrefs.GetInt("game_started", 0);
        gameStarted = gs == 1;
        if (!gameStarted)
        {
            PlayerPrefs.SetInt("score", 0);
            foreach(int i in levelIndexs)
            {
                PlayerPrefs.SetInt("part_" + (i + 1), 0);
                Debug.Log("set for " + (i + 1));
            }
        }
    }

    public void OnApplicationQuit()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index > 1)
        {
            // Detected that player is cheating and setting back part collected
            PlayerPrefs.SetInt("part_" + (index + 1), 0);
        }
    }

    public void ContinueSave()
    {
        for (int i = levelIndexs.Length - 1; i >= 0; i--)
        {
            int levelIndex = levelIndexs[i];
            if (PlayerPrefs.GetInt("part_" + levelIndex, 0) == 1)
            {
                SceneLoader.Instance.LoadScene(levelIndex);
                return;
            }
        }

        SceneLoader.Instance.LoadScene(introLvl);
    }

    public void CreateNewSave()
    {
        PlayerPrefs.SetInt("game_started", 1);
        PlayerPrefs.SetInt("score", 0);
        foreach (int i in levelIndexs)
        {
            PlayerPrefs.SetInt("part_" + (i + 1), 0);
            Debug.Log("set for " + (i + 1));
        }
        SceneLoader.Instance.LoadScene(introLvl);
    }

    public void AssignBtn(Button b)
    {
        b.interactable = gameStarted;
    }
}
