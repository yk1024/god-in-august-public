using UnityEngine;

public class PopUpMessage : MonoBehaviour
{
    [SerializeField]
    private GameObject popUpObject;

    private GameObject mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag(Constants.MainCameraTag);
    }

    void Update()
    {
        if (popUpObject.activeInHierarchy)
        {
            popUpObject.transform.forward = popUpObject.transform.position - mainCamera.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            popUpObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            popUpObject.SetActive(false);
        }
    }
}
