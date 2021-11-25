using UnityEngine;
using UnityEngine.UI;

public class StatisticsScreen : MonoBehaviour
{
    #region UI components
    [SerializeField]
    private Text wheatCountText;
    [SerializeField]
    private Text trainedPeasantText;
    [SerializeField]
    private Text trainedWarriorText;
    [SerializeField]
    private Text deathCountByRaidText;
    [SerializeField]
    private Text deathCountByStarvationText;
    #endregion

    private IStatisticsRead statistics;

    // Start is called before the first frame update

    private void OnEnable()
    {
        statistics = StatisticsController.shared;
        SetupTexts();
    }

    private void SetupTexts()
    {
        Debug.Log(statistics);
        wheatCountText.text = "" + statistics.GetWheatCount();
        trainedPeasantText.text = "" + statistics.GetTrainedUnitCount(UnitType.peasant);
        trainedWarriorText.text = "" + statistics.GetTrainedUnitCount(UnitType.warrior);
        deathCountByRaidText.text = "" + statistics.GetDeadByRaidCount();
        deathCountByStarvationText.text = "" + statistics.GetDeadByStarvation();
    }
}
