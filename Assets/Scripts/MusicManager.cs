using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event bgmEvent;

    [SerializeField]
    private AK.Wwise.State defaultState;

    private List<AK.Wwise.State> states = new List<AK.Wwise.State>();

    void Start()
    {
        defaultState.SetValue();
        bgmEvent.Post(gameObject);
    }

    public void AddState(AK.Wwise.State state)
    {
        states.Add(state);
        SetState();
    }

    public void RemoveState(AK.Wwise.State state)
    {
        states.Remove(state);
        SetState();
    }

    private void SetState()
    {
        if (states.Count == 0)
        {
            defaultState.SetValue();
        }
        else
        {
            states[0].SetValue();
        }
    }
}
