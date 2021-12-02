using UnityEngine;
using UnityEngine.UI;
using System;

#region Interfaces
public interface IEatable
{
    void Eat();
}

public interface ITrainable
{
    bool TrainIsAvailable(UnitType unit);
    void StartTrain(UnitType unit);
    void FinishedTrain(UnitType unit);
}

public interface IRaidable 
{
    void StartRaid(int count);
    void RaidWavesEnd();
}

public interface IIncomeable
{
    void IncomeFood();
}

public interface IGameEvents
{
    void ChangeWheatCount(int newCount);
    void ChangeWarriorCount(int newCount);
    void ChangePeasantCount(int newCount);
    void UnitsDeath(UnitType unit, DeathReason reason, int count);
    void GameWin();
    void GameLose();
    void NotEnoughPriceToTrainUnit(UnitType unit);
    void SuccessTrainUnit(UnitType unit);
}

#endregion

public class GameController : MonoBehaviour, IEatable, ITrainable, IRaidable, IIncomeable
{

    private int warriorCount;
    private int peasantCount;
    private int wheatCount;

    #region Game setting
    private int raidWaveCount = 1;
    private int currentWave = 0;
    private int maxWaveCount = 10;
    

    public int warriorEatCount;
    public int peasantEatCount;

    public int warriorTrainPrice;
    public int countWarriorDeathByStarvation;
    public int peasantTrainPrice;
    public int countPeasantDeathByStarvation;

    public int incomeFoodRate;
    #endregion


    // Delegates
    
    private IRaidService raidService;
    private IStatisticsWrite statistics;
    private IGameEvents gameEvents;
    

    // Start is called before the first frame update
    void Start()
    {
        SetupGame();
    }

    #region Private methods
    private void SetWarriorCount(int newCount)
    {
        warriorCount = newCount;
        gameEvents.ChangeWarriorCount(newCount);
    }

    private void SetPeasantCount(int newCount)
    {
        peasantCount = newCount;
        gameEvents.ChangePeasantCount(newCount);
    }

    private void SetWheatCount(int newCount)
    {
        wheatCount = newCount;
        gameEvents.ChangeWheatCount(newCount);
    }

    private void WarriorEat()
    {
        if (wheatCount >= warriorEatCount * warriorCount)
        {
            int warriorEat = warriorCount * warriorEatCount;
            SetWheatCount(wheatCount - warriorEat);
        }
        else
        {
            SetWarriorCount(warriorCount - countWarriorDeathByStarvation);
            gameEvents.UnitsDeath(UnitType.warrior, DeathReason.starvation, countWarriorDeathByStarvation);
        }
    }

    private void PeasantEat()
    {
        if (wheatCount >= peasantEatCount * peasantCount)
        {
            int peasantEat = peasantEatCount * peasantCount;
            SetWheatCount(wheatCount - peasantEat);
        }
        else
        {
            SetPeasantCount(peasantCount - countPeasantDeathByStarvation);
            gameEvents.UnitsDeath(UnitType.peasant, DeathReason.starvation, countPeasantDeathByStarvation);
        }
    }

    public void SetupGame()
    {
        
        raidService = FindComponents.FindComponentWithTag<RaidService>("RaidService");
        gameEvents = FindComponents.FindComponentWithTag<IGameEvents>("GameScreen");
        statistics = StatisticsController.shared;

        countPeasantDeathByStarvation = 1;
        countWarriorDeathByStarvation = 1;

        wheatCount = 100;
    }

    private void StartGame()
    {
        raidService.SetupRaidService(1, 10);
    }

    private void GameWin()
    {
        gameEvents.GameWin();
    }

    private void GameLose()
    {
        gameEvents.GameLose();
    }

    #endregion

    #region IEatable
    public void Eat()
    {
        WarriorEat();
        PeasantEat();
    }
    #endregion

    #region ITrainable
    public bool TrainIsAvailable(UnitType unit)
    {
        switch (unit) 
        {
            case UnitType.peasant:
                if (wheatCount >= peasantTrainPrice)
                {
                    return true;
                }
                else
                {
                    gameEvents.NotEnoughPriceToTrainUnit(UnitType.peasant);
                    return false;
                }
            case UnitType.warrior:
                if (wheatCount >= warriorTrainPrice)
                {
                    return true;
                }
                else
                {
                    gameEvents.NotEnoughPriceToTrainUnit(UnitType.warrior);
                    return false;
                }
            default:
                return false;
        }

    }

    public void StartTrain(UnitType unit)
    {
        switch (unit)
        {
            case UnitType.peasant:
                SetWheatCount(wheatCount - peasantTrainPrice);
                break;
            case UnitType.warrior:
                SetWheatCount(wheatCount - warriorTrainPrice);
                break;
        }

    }

    public void FinishedTrain(UnitType unit)
    {
        switch (unit)
        {
            case UnitType.peasant:
                SetPeasantCount(peasantCount + 1);
                break;
            case UnitType.warrior:
                SetWarriorCount(warriorCount + 1);
                break;
        }
        gameEvents.SuccessTrainUnit(unit);
        statistics.trainedUnit(unit, 1);
    }

    #endregion

    #region IRaidable
    public void StartRaid(int count)
    {
        SetWarriorCount(warriorCount - count);
        statistics.deadByRaid(count);
        gameEvents.UnitsDeath(UnitType.warrior, DeathReason.raid, count);
        if (warriorCount < 1)
        {
            GameLose();
        }
    }

    public void RaidWavesEnd()
    {
        GameWin();
    }
    #endregion

    #region IIncomeable
    public void IncomeFood()
    {
        int foodIncome = incomeFoodRate * peasantCount;
        SetWheatCount(wheatCount + foodIncome);
        statistics.addedWheat(foodIncome);
        gameEvents.ChangeWheatCount(foodIncome);
    }
    #endregion

}
