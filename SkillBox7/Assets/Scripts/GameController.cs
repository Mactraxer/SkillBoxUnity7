using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Text peasantCountText;
    [SerializeField] Text warriorCountText;
    [SerializeField] Text wheatCountText;
    [SerializeField] Text consoleText;

    // Start is called before the first frame update
    void Start()
    {
        consoleText.text = "";
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
            PrintTextToConsole($"Из-за нехватки еды у вас умер один воин");
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
            PrintTextToConsole($"Из-за нехватки еды у вас умер один крестьянин");
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
                PrintTextToConsole($"Вы успешно наняли крестьянина");
                break;
            case UnitType.warrior:
                warriorCount++;
                PrintTextToConsole($"Вы успешно наняли воина");
                break;
        }
    }

    private void PrintTextToConsole(string text)
    {
        consoleText.text += text + "\n";
    }

    public void StartRaid(int count)
    {
        warriorCount -= count;
        PrintTextToConsole($"На вашу деревню напали. Вы потеряли {count} воинов");
    }

    public void IncomeFood()
    {
        int foodIncome = incomeFoodRate * peasantCount;
        wheatCount += foodIncome;
        PrintTextToConsole($"Ваш доход зерна составил {foodIncome}");
    }
}
