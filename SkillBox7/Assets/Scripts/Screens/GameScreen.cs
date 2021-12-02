using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameScreen : MonoBehaviour
{

    // UI Components
    [SerializeField] private Text peasantCountText;
    [SerializeField] private Text warriorCountText;
    [SerializeField] private Text wheatCountText;

    [SerializeField]
    private ScreenRouting routing;
    private IConsoleInput consoleDelegate;
    // Start is called before the first frame update
    void Start()
    {
        routing = FindComponents.FindComponentWithTag<ScreenRouting>("Router");
        consoleDelegate = FindComponents.FindComponentWithTag<ConsoleController>("ConsoleManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
