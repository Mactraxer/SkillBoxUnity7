using UnityEngine;

public class Root : MonoBehaviour
{
    private StatisticsController statistics;
    // Start is called before the first frame update
    void Start()
    {
        statistics = new StatisticsController();
    }

}
