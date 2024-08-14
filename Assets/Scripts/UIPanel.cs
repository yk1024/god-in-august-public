using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    private PlayerInput playerInput;
    private Selectable firstSelectable;
    private bool justEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        firstSelectable = GetComponentInChildren<Selectable>(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (justEnabled)
        {
            playerInput.SwitchCurrentActionMap(Constants.UIActionMap);
            firstSelectable.Select();
            Cursor.lockState = CursorLockMode.None;
            justEnabled = false;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            OnCancel();
        }
    }

    void OnEnable()
    {
        justEnabled = true;
    }

    void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.SwitchCurrentActionMap(Constants.PlayerActionMap);
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnCancel()
    {
        gameObject.SetActive(false);
    }
}
