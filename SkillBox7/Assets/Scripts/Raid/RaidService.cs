using UnityEngine;
using UnityEngine.UI;

public interface IRaidService
{
    public void SetupRaidService(int enemyCountByWave, int maxWaves);
}

public class RaidService : MonoBehaviour, ITickable, IRaidService
{

    private int currentWave;
    private int maxWaves;
    private int enemyCountByWave;

    private IRaidable raidDelegate;
    private ITick raidTick;

    [SerializeField]
    private Text raidInfoText;

    // Start is called before the first frame update
    void Start()
    {
        currentWave = 0;
        raidDelegate = GameObject.FindGameObjectWithTag("GameManager").GetComponent<IRaidable>();
        raidTick = GameObject.FindGameObjectWithTag("RaidTick").GetComponent<ITick>();
        raidTick.SetupTick(10);
        raidInfoText.text = "";
    }

    void ITickable.DidTick()
    {
        raidDelegate.StartRaid(currentWave * enemyCountByWave);
        if (currentWave == maxWaves)
        {
            raidDelegate.RaidWavesEnd();
            return;
        }
        currentWave++;
        UpdateRaidInfoLabel($"Кол-во врагов {enemyCountByWave * currentWave}");
    }

    void IRaidService.SetupRaidService(int enemyCountByWave, int maxWaves)
    {
        this.enemyCountByWave = enemyCountByWave;
        this.maxWaves = maxWaves;
        UpdateRaidInfoLabel($"Кол-во врагов {enemyCountByWave * currentWave}");
    }

    private void UpdateRaidInfoLabel(string text)
    {
        raidInfoText.text = text;
    }

}
