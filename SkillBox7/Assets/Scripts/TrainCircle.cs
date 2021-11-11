using UnityEngine;
using UnityEngine.UI;

public class TrainCircle : MonoBehaviour
{
    [SerializeField] Image changableImage;
    public UnitType unitType;
    public float traintTime;
    private float currentTrainTime;
    public ITrainable trainDelegate;
    private bool isTraining;
    // Start is called before the first frame update
    void Start()
    {
        isTraining = false;
        currentTrainTime = traintTime;
        trainDelegate = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTraining == false) { return; }
        currentTrainTime -= Time.deltaTime;

        if (currentTrainTime < 0)
        {
            isTraining = false;
            currentTrainTime = traintTime;
            trainDelegate.FinishedTrain(unitType);
        }

        changableImage.fillAmount = currentTrainTime / traintTime;
    }

    public void TrainButtonAction()
    {
        if (trainDelegate.TrainIsAvailable(unitType) && isTraining == false)
        {
            Debug.Log("StartTrain");
            trainDelegate.StartTrain(unitType);
            isTraining = true;
        }
    }
}
