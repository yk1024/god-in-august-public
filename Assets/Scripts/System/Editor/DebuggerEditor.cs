using UnityEditor;
using UnityEngine;

namespace GodInAugust.System
{
/// <summary>
/// デバッガー用のエディター拡張。
/// よく使う、寝る、感謝、お願いのコマンドをエディター上で行えるようにする。
/// </summary>
[CustomEditor(typeof(Debugger))]
public class DebuggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Debugger debugger = (Debugger)target;

        // プレイ中以外は何もしない。
        if (!EditorApplication.isPlaying) return;

        // 寝るボタン
        if (GUILayout.Button("寝る"))
        {
            debugger.Sleep();
        }

        // 感謝ボタン
        if (GUILayout.Button("感謝"))
        {
            debugger.PrayForGratitude();
        }

        // お願いボタン
        if (GUILayout.Button("お願い"))
        {
            debugger.PrayForWish();
        }
    }
}
}
