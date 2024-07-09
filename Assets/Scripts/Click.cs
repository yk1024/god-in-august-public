using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Click : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(("Clicked", gameObject.name));
    }

    public void OnMove(InputValue value)
    {
        Debug.Log("Move");
    }

    public void TEST()
    {
        Debug.Log("TEST");
    }
}
