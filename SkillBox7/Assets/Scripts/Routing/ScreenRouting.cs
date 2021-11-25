using UnityEngine;

public class ScreenRouting : MonoBehaviour
{

    [SerializeField]
    private GameObject mainMenuScreen;//Todo add main menu
    [SerializeField]
    private GameObject gameScreen;
    [SerializeField]
    private GameObject endGameScreen;

    // Start is called before the first frame update
    void Start()
    {
        //ShowGameScreen();
    }

    public void ShowEndGameScreen()
    {
        endGameScreen.SetActive(true);
        //mainMenuScreen.SetActive(true);
        //gameScreen.SetActive(true);
    }

    public void ShowMainMenuScree()
    {
        /*mainMenuScreen.SetActive(true);
        endGameScreen.SetActive(false);
        gameScreen.SetActive(false);*/
        throw new System.ArgumentNullException("Need setup main menu");
    }

    public void ShowGameScreen()
    {
        //gameScreen.SetActive(true);
        //endGameScreen.SetActive(false);
        //mainMenuScreen.SetActive(false);
        throw new System.ArgumentNullException("Need setup game screen");
    }
 
}
