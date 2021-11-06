using UnityEngine.UI;
using UnityEngine;

public interface IConsoleInput
{
    void DidIncome(int count);
    void DidTrain(UnitType unit);
    void DidRaid(int deathCount);
}

public class ConsoleController : MonoBehaviour, IConsoleInput
{

    [SerializeField] private Text incomeConsole;
    [SerializeField] private Text trainingConsole;
    [SerializeField] private Text raidConsole;

    void IConsoleInput.DidIncome(int count)
    {
        incomeConsole.text += $"��� ����� ����� �������� {count}\n";
    }

    void IConsoleInput.DidRaid(int deathCount)
    {
        raidConsole.text += $"�� ���� ������� ������. �� �������� {deathCount} ������\n";
    }

    void IConsoleInput.DidTrain(UnitType unit)
    {
        switch (unit)
        {
            case UnitType.peasant:
                trainingConsole.text += $"�� ������� ������ �����������\n";
                break;
            case UnitType.warrior:
                trainingConsole.text += $"�� ������� ������ �����\n";
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        incomeConsole.text = "";
        trainingConsole.text = "";
        raidConsole.text = "";
    }

}
