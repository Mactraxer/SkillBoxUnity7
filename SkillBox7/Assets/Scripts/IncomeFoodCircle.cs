using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncomeFoodCircle : MonoBehaviour
{
    [SerializeField] Image changableImage;
    public float timeIncomeFood;
    private float timeBeforeIncomeFood;
    private IIncomeable incomeDelegate;
    // Start is called before the first frame update
    void Start()
    {
        timeBeforeIncomeFood = timeIncomeFood;
        incomeDelegate = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        timeBeforeIncomeFood -= Time.deltaTime;
        if (timeBeforeIncomeFood < 0)
        {
            incomeDelegate.IncomeFood();
            timeBeforeIncomeFood = timeIncomeFood;
        }

        changableImage.fillAmount = timeBeforeIncomeFood / timeIncomeFood;
    }
}
