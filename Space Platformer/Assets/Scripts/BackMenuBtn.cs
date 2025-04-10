using UnityEngine;

public class BackMenuBtn : MonoBehaviour
{
    public void ClickAction()
    {
        SceneLoader.Instance.LoadScene(0);
    }
}
