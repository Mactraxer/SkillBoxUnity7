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
}

public interface IIncomeable
{
    void IncomeFood();
}

#endregion

public class GameController : MonoBehaviour, IEatable, ITrainable, IRaidable, IIncomeable
{

    public int warriorCount;
    public int peasantCount;
    public int wheatCount;

    public int warriorEatCount;
    public int peasantEatCount;

    public int warriorTrainPrice;
    public int peasantTrainPrice;

    public int incomeFoodRate;

    // UI Components
    [SerializeField] private Text peasantCountText;
    [SerializeField] private Text warriorCountText;
    [SerializeField] private Text wheatCountText;

    // Delegates
    private IConsoleInput consoleDelegate;

    // Start is called before the first frame update
    void Start()
    {
        consoleDelegate = GameObject.FindGameObjectWithTag("ConsoleManager").GetComponent<ConsoleController>();
    }

    // Update is called once per frame
    void Update()
    {
        warriorCountText.text = $"{warriorCount}";
        peasantCountText.text = $"{peasantCount}";
        wheatCountText.text = $"{wheatCount}";
    }

    private void WarriorEat()
    {
        if (wheatCount >= warriorEatCount * warriorCount)
        {
            wheatCount -= warriorCount * warriorEatCount;
        }
        else
        {
            warriorCount--;
            //PrintTextToConsole($"Из-за нехватки еды у вас умер один воин");
        }
        
    }

    private void PeasantEat()
    {
        if (wheatCount >= peasantEatCount * peasantCount)
        {
            wheatCount -= peasantEatCount * peasantCount;
        }
        else
        {
            peasantCount--;
            //PrintTextToConsole($"Из-за нехватки еды у вас умер один крестьянин");
        }
    }

    public void Eat()
    {
        WarriorEat();
        PeasantEat();
    }

    public bool TrainIsAvailable(UnitType unit)
    {
        switch (unit) 
        {
            case UnitType.peasant:
                return wheatCount >= peasantTrainPrice;
            case UnitType.warrior:
                return wheatCount >= warriorTrainPrice;
            default:
                return false;
        }

    }

    public void StartTrain(UnitType unit)
    {
        switch (unit)
        {
            case UnitType.peasant:
                wheatCount -= peasantTrainPrice;
                break;
            case UnitType.warrior:
                wheatCount -= warriorTrainPrice;
                break;
        }
    }

    public void FinishedTrain(UnitType unit)
    {
        switch (unit)
        {
            case UnitType.peasant:
                peasantCount++;
                break;
            case UnitType.warrior:
                warriorCount++;
                break;
        }
        WriteTrainResultToConsole(unit);
    }

    public void StartRaid(int count)
    {
        warriorCount -= count;
        WriteRaidResultToConsole(count);
    }

    public void IncomeFood()
    {
        int foodIncome = incomeFoodRate * peasantCount;
        wheatCount += foodIncome;

        WriteIncomeToConsole(foodIncome);
    }

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
