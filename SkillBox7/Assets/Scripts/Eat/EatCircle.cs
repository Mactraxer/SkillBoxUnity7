using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EatCircle : MonoBehaviour
{
    [SerializeField] Image changableImage;
    public float eatTime;
    private float eatCurrentTime;
    private IEatable eatDelegate;
    // Start is called before the first frame update
    void Start()
    {
        eatCurrentTime = eatTime;
        eatDelegate = FindComponents.FindComponentWithTag<GameController>("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        eatCurrentTime -= Time.deltaTime;
        if (eatCurrentTime < 0)
        {
            eatDelegate.Eat();
            eatCurrentTime = eatTime;
        }

        changableImage.fillAmount = eatCurrentTime / eatTime;
    }
}

