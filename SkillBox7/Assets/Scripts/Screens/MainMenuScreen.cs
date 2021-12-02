using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{

    private ScreenRouting router;

    private void Start()
    {
        SetupRouter();
    }

    private void SetupRouter()
    {
        router = FindComponents.FindComponentWithTag<ScreenRouting>("Router");
    }


    public void StartGameButtonAction()
    {
        router.ShowGameScreen();
    }

    public void RulesButtonAction()
    {
        //TODO
    }

    public void ExitButtonAction()
    {
        Application.Quit();
    }
}
