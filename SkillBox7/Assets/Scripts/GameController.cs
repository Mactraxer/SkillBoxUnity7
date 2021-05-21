using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TickFrom
{
    warriorTrain, warriorEat, peasantTrain, peasantEat, wheatGet, battle
}
public interface ITickable
{
    void Tick(TickFrom from);
}
public class GameController : MonoBehaviour, ITickable
{

    public int warriorCount;
    public int peasantCount;
    public int wheatCount;

    public int warriorEatCount;
    public int peasantEatCount;
    
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
        warriorCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TrainWarrior()
    {

    }

    private void TrainPeasant()
    {

    }

    private void WarriorEat()
    {

    }

    private void PeasantEat()
    {

    }

    private void StartBattle()
    {

    }

    private void WheatGet()
    {

    }
}
