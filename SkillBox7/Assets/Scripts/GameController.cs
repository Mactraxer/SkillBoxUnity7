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

#endregion

public class GameController : MonoBehaviour, IEatable, ITrainable, IRaidable, IIncomeable
{

    private int warriorCount;
    private int peasantCount;
    private int wheatCount;

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

    // UI Components
    [SerializeField] private Text peasantCountText;
    [SerializeField] private Text warriorCountText;
    [SerializeField] private Text wheatCountText;

    // Delegates
    private IConsoleInput consoleDelegate;

    private IRaidService raidService;

    // Start is called before the first frame update
    void Start()
    {
        consoleDelegate = GameObject.FindGameObjectWithTag("ConsoleManager").GetComponent<ConsoleController>();
        raidService = GameObject.FindGameObjectWithTag("RaidService").GetComponent<RaidService>();
        UpdateLabels();

        wheatCount = 100;

        raidService.SetupRaidService(1, 10);
        UpdateLabels();
    }

    #region Private methods
    private void SetWarriorCount(int newCount)
    {
        warriorCount = newCount;
        UpdateLabels();
    }

    private void SetPeasantCount(int newCount)
    {
        peasantCount = newCount;
        UpdateLabels();
    }

    private void SetWheatCount(int newCount)
    {
        wheatCount = newCount;
        UpdateLabels();
    }
    private void UpdateLabels()
    {
        warriorCountText.text = $"{warriorCount}";
        peasantCountText.text = $"{peasantCount}";
        wheatCountText.text = $"{wheatCount}";
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
            consoleDelegate.DidDeathByStarvation(UnitType.warrior);
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
            consoleDelegate.DidDeathByStarvation(UnitType.peasant);
        }
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
                    consoleDelegate.TrainPriceError();
                    return false;
                }
            case UnitType.warrior:
                if (wheatCount >= warriorTrainPrice)
                {
                    return true;
                }
                else
                {
                    consoleDelegate.TrainPriceError();
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
        WriteTrainResultToConsole(unit);
    }

    #endregion

    #region IRaidable
    public void StartRaid(int count)
    {
        SetWarriorCount(warriorCount - count);
        WriteRaidResultToConsole(count);
        if (warriorCount < 1)
        {
            //TODO lose game
            Debug.Log("You lose game");
        }
    }

    public void RaidWavesEnd()
    {
        //TODO win game
        Debug.Log("You win game");
    }
    #endregion

    #region IIncomeable
    public void IncomeFood()
    {
        int foodIncome = incomeFoodRate * peasantCount;
        SetWheatCount(wheatCount + foodIncome);

        WriteIncomeToConsole(foodIncome);
    }
    #endregion

    #region WorkWithConsole
    private bool TryAccessToConsole()
    {
        if (consoleDelegate == null)
        {
            throw new NullReferenceException(); 
        }
        else
        {
            return true;
        }
    }

    private void WriteIncomeToConsole(int count)
    {
        if (TryAccessToConsole())
        {
            consoleDelegate.DidIncome(count);
        }
    }

    private void WriteTrainResultToConsole(UnitType unit)
    {
        if (TryAccessToConsole())
        {
            consoleDelegate.DidTrain(unit);
        }
    }

    private void WriteRaidResultToConsole(int deathCount)
    {
        if (TryAccessToConsole())
        {
            consoleDelegate.DidRaid(deathCount);
        }
    }
    #endregion
}
