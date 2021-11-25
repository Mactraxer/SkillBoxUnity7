public interface IStatisticsWrite
{
    public void addedWheat(int count);
    public void trainedUnit(UnitType unit, int count);
    public void deadByRaid(int count);
    public void deadByStarvation(int count);
}

public interface IStatisticsRead
{
    public int GetWheatCount();
    public int GetTrainedUnitCount(UnitType unit);
    public int GetDeadByRaidCount();
    public int GetDeadByStarvation();
}

public class StatisticsController: IStatisticsWrite, IStatisticsRead
{
    private StatisticsStorage storage;

    public static StatisticsController shared;

    public StatisticsController()
    {
        storage = new StatisticsStorage();
        shared = this;
    }

    #region IStatisticsWrite
    void IStatisticsWrite.addedWheat(int count)
    {
        storage.PersistWheat(count);
    }

    void IStatisticsWrite.deadByRaid(int count)
    {
        storage.PersistDeathByRaid(count);
    }

    void IStatisticsWrite.deadByStarvation(int count)
    {
        storage.PersistDeathByStarvation(count);
    }

    void IStatisticsWrite.trainedUnit(UnitType unit, int count)
    {
        switch (unit) 
        {
            case UnitType.peasant:
                storage.PersistPeasant(count);
                break;
            case UnitType.warrior:
                storage.PersistWheat(count);
                break;
            default:
                throw new System.ArgumentException("Unknow unit type");
        }

    }
    #endregion

    #region IStatisticsRead
    int IStatisticsRead.GetDeadByRaidCount()
    {
        return storage.GetDeathByRaidCount();
    }

    int IStatisticsRead.GetDeadByStarvation()
    {
        return storage.GetDeathByStarvation();
    }

    int IStatisticsRead.GetTrainedUnitCount(UnitType unit)
    {
        switch(unit)
        {
            case UnitType.peasant:
                return storage.GetPeasantCount();
            case UnitType.warrior:
                return storage.GetWarriorCount();
            default:
                throw new System.ArgumentException("Unknow unit type");
        }
    }

    int IStatisticsRead.GetWheatCount()
    {
        return storage.GetWheatCount();
    }
    #endregion
}
