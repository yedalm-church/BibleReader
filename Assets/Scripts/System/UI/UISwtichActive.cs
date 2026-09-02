using UnityEngine;

public class UISwitchActive : MonoBehaviour
{
    public void Active(int InIndex)
    {
        for (int i = 0, count = transform.childCount; i < count; i++)
        {
            var child = transform.GetChild(i);
            child.gameObject.SetActive(i == InIndex);
        }
    }

    public void Active(GameObject InTarget)
    {
        for (int i = 0, count = transform.childCount; i < count; i++)
        {
            var child = transform.GetChild(i);
            child.gameObject.SetActive(child.gameObject == InTarget);
        }
    }

    public void HideAll()
    {
        for (int i = 0, count = transform.childCount; i < count; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}