using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBtn : MonoBehaviour
{
    public void ClickAction()
    {
        SceneLoader.Instance.LoadScene(2);
    }

}
