using UnityEngine;
using UnityEngine.UI;


public class ProgressTime : MonoBehaviour
{

    [SerializeField] Image changableImage;
    [SerializeField] float maxTime;

   

    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = maxTime;
       
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = maxTime;

        }

        changableImage.fillAmount = currentTime / maxTime;
    }

}
