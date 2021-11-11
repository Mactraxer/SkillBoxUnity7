using UnityEngine;

public class RaidService : MonoBehaviour, ITickable
{

    private int currentWave;
    private int maxWaves;
    private int enemyCountByWave;

    private IRaidable raidDelegate;

    // Start is called before the first frame update
    void Start()
    {
        currentWave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    void SetupRaidService(int enemyCountByWave, int maxWaves)
    {
        this.enemyCountByWave = enemyCountByWave;
        this.maxWaves = maxWaves;
    }

}
