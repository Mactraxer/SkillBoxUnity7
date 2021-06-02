using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaidCircle : MonoBehaviour
{
    [SerializeField] Image changableImage;
    public float raidTime;
    private float timeBeforeRaid;
    public int enemyCount;
    public IRaidable raidDelegate;
    // Start is called before the first frame update
    void Start()
    {
        timeBeforeRaid = raidTime;
        raidDelegate = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        timeBeforeRaid -= Time.deltaTime;

        if (timeBeforeRaid < 0)
        {
            timeBeforeRaid = raidTime;
            raidDelegate.StartRaid(enemyCount);
        }

        changableImage.fillAmount = timeBeforeRaid / raidTime;
    }
}
