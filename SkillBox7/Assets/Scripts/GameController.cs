using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TickFrom
{
    warriorTrain, warriorEat, peasantTrain, peasantEat, wheatGet, battle
}
public interface ITickable
{
    void Tick(TickFrom from);
}

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

public class GameController : MonoBehaviour, ITickable, IEatable, ITrainable, IRaidable, IIncomeable
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

    public void Tick(TickFrom from)
    {
        switch(from)
        {
            case TickFrom.warriorTrain:
                TrainWarrior();
                break;
            case TickFrom.warriorEat:
                WarriorEat();
                break;
            case TickFrom.peasantTrain:
                PeasantEat();
                break;
            default:
                break;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        warriorCountText.text = $"Кол-во воинов: {warriorCount}";
        peasantCountText.text = $"Кол-во крестьян: {peasantCount}";
        wheatCountText.text = $"Кол-во пшеницы: {wheatCount}";
    }

    private void TrainWarrior()
    {
        warriorCount++;
    }

    private void TrainPeasant()
    {
        
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
        }
    }

    public void Eat()
    {
        wheatCount -= peasantCount * peasantEatCount;
        wheatCount -= warriorCount * warriorEatCount;
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
    }

    public void StartRaid(int count)
    {
        warriorCount -= count;
    }

    public void IncomeFood()
    {
        wheatCount += incomeFoodRate * peasantCount;
    }
}
