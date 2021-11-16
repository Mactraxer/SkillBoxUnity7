using UnityEngine;
using UnityEngine.UI;

public interface ITickable
{
    void DidTick();
}

public interface ITick
{
    void SetupTick(float time);
}

public class RaidCircle : MonoBehaviour, ITick
{
    [SerializeField] Image changableImage;
    
    private float raidTime;
    private float timeBeforeRaid;
    public ITickable tickDelegate;

    // Start is called before the first frame update
    void Start()
    {
        timeBeforeRaid = raidTime;
        tickDelegate = GameObject.FindGameObjectWithTag("RaidService").GetComponent<ITickable>();
    }

    // Update is called once per frame
    void Update()
    {
        timeBeforeRaid -= Time.deltaTime;

        if (timeBeforeRaid < 0)
        {
            timeBeforeRaid = raidTime;
            tickDelegate.DidTick();
        }

        changableImage.fillAmount = timeBeforeRaid / raidTime;
    }

    void ITick.SetupTick(float time)
    {
        raidTime = time;
    }
}
