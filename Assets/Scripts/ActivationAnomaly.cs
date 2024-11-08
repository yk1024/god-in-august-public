using UnityEngine;

public class ActivationAnomaly : MonoBehaviour, IAnomaly
{
    public void OnOccur()
    {
        gameObject.SetActive(true);
    }
}
