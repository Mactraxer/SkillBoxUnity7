public class StatisticsStorage
{
    private int wheatCount;
    private int peasantCount;
    private int warriorCount;
    private int deathByRaidCount;
    private int deathByStarvasionCount;

    public StatisticsStorage()
    {
        this.wheatCount = 0;
        this.peasantCount = 0;
        this.deathByRaidCount = 0;
        this.deathByStarvasionCount = 0;
    }

    #region Pesist methods
    public void PersistWheat(int count)
    {
        wheatCount += count;
    }

    public void PersistPeasant(int count)
    {
        peasantCount += count;
    }

    public void PersistIncome(int count)
    {

    }

    public void PersistDeathByRaid(int count)
    {
        deathByRaidCount += count;
    }

    public void PersistDeathByStarvation(int count)
    {
        deathByStarvasionCount += count;
    }
    #endregion

    #region Read methods
    public int GetWheatCount()
    {
        return wheatCount;
    }

    public int GetPeasantCount()
    {
        return peasantCount;
    }

    public int GetWarriorCount()
    {
        return warriorCount;
    }

    public int GetDeathByRaidCount()
    {
        return deathByRaidCount;
    }

    public int GetDeathByStarvation()
    {
        return deathByStarvasionCount;
    }
    #endregion
}
