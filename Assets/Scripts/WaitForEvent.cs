using UnityEngine;
using UnityEngine.Events;

public class WaitForEvent : CustomYieldInstruction
{
    private bool triggered = false;
    public override bool keepWaiting => !triggered;

    private readonly UnityEvent unityEvent;

    public WaitForEvent(UnityEvent unityEvent)
    {
        this.unityEvent = unityEvent;

        unityEvent.AddListener(InvokeTrigger);
    }

    private void InvokeTrigger()
    {
        triggered = true;
        unityEvent.RemoveListener(InvokeTrigger);
    }
}
