using UnityEngine;

public class FindComponents : MonoBehaviour
{
    static public T FindComponentWithTag<T>(string tagName)
    {
        var foundGameObject = GameObject.FindGameObjectWithTag(tagName);
        if (foundGameObject == null)
        {
            throw new System.NullReferenceException("Not found game object with tag " + tagName);
        }

        var foundComponent = foundGameObject.GetComponent<T>();
        if (foundComponent == null)
        {
            throw new System.NullReferenceException("Not found component with tag" + tagName);
        }

        return foundComponent;
    }
}
