using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Button continueSaveBtn;

    private void Start()
    {
        SaveManager.instance.AssignBtn(continueSaveBtn);
    }
    public void BtnExit()
    {
        Application.Quit();
    }

    public void BtnContinue()
    {
        SaveManager.instance.ContinueSave();
    }

    public void BtnNewSave()
    {
        SaveManager.instance.CreateNewSave();
    }
}
