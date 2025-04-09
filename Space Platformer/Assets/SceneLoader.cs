using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    public bool isLoadingScene { get; private set; }
    private Animator animator;
    private AudioSource loadSound;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        isLoadingScene = false;
        animator = GetComponentInChildren<Animator>();
        loadSound = GetComponent<AudioSource>();
    }

    public static int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadScene(int index)
    {
        if (isLoadingScene)
        {
            Debug.LogWarning("Already loading another scene!");
            return;
        }

        StartCoroutine(SceneLoad(index));
    }

    private IEnumerator SceneLoad(int index)
    {
        isLoadingScene = true;
        Time.timeScale = 1.0f;

        loadSound.Play();
        animator.SetBool("loading", true);
        yield return new WaitForSeconds(1f);

        SceneManager.LoadSceneAsync(index);

        animator.SetBool("loading", false);
        yield return new WaitForSeconds(1f);

        isLoadingScene = false;
    }
}
