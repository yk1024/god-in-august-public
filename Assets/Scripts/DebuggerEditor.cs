using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Debugger))]
public class DebuggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Debugger debugger = (Debugger)target;

        if (!EditorApplication.isPlaying) return;

        if (GUILayout.Button("寝る"))
        {
            debugger.Sleep();
        }

        if (GUILayout.Button("感謝"))
        {
            debugger.PrayForGratitude();
        }

        if (GUILayout.Button("お願い"))
        {
            debugger.PrayForWish();
        }
    }
}
