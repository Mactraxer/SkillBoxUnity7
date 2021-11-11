using UnityEngine.UI;
using UnityEngine;

public interface IConsoleInput
{
    void DidIncome(int count);
    void DidTrain(UnitType unit);
    void TrainPriceError();
    void DidEat(int eatCount, UnitType unit);
    void DidRaid(int deathCount);
    void DidDeathByStarvation(UnitType unit);
}

public class ConsoleController : MonoBehaviour, IConsoleInput
{

    [SerializeField] private Text incomeConsole;
    [SerializeField] private Text eatConsole;
    [SerializeField] private Text trainingConsole;
    [SerializeField] private Text raidConsole;

    #region IConsoleInput
    void IConsoleInput.TrainPriceError()
    {
        trainingConsole.text += $"Не хватает еды для найма юнитов\n";
    }

    void IConsoleInput.DidDeathByStarvation(UnitType unit)
    {
        switch (unit)
        {
            case UnitType.peasant:
                eatConsole.text += $"Жителям не хватило еды. От голода умер крестьянин\n";
                break;
            case UnitType.warrior:
                eatConsole.text += $"Жителям не хватило еды. От голода умер воин\n";
                break;
        }
    }

    void IConsoleInput.DidEat(int eatCount, UnitType unit)
    {
        eatConsole.text += $"Ваши жители съели {eatConsole} зерна\n";
    }

    void IConsoleInput.DidIncome(int count)
    {
        incomeConsole.text += $"Ваш доход зерна составил {count}\n";
    }

    void IConsoleInput.DidRaid(int deathCount)
    {
        raidConsole.text += $"На вашу деревню напали. Вы потеряли {deathCount} воинов\n";
    }

    void IConsoleInput.DidTrain(UnitType unit)
    {
        switch (unit)
        {
            case UnitType.peasant:
                trainingConsole.text += $"Вы успешно наняли крестьянина\n";
                break;
            case UnitType.warrior:
                trainingConsole.text += $"Вы успешно наняли воина\n";
                break;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        incomeConsole.text = "";
        eatConsole.text = "";
        raidConsole.text = "";
        trainingConsole.text = "";
    }

}
