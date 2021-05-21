using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressTime : MonoBehaviour
{

    [SerializeField] Image changableImage;
    [SerializeField] float maxTime;

    public ITickable eventSender;

    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = maxTime;
        eventSender = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = maxTime;
            eventSender.Tick(TickFrom.warrior);
        }

        changableImage.fillAmount = currentTime / maxTime;
    }

}
