using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 複数のクラスで使いたいような、汎用的なメソッドを集めたクラス
/// </summary>
public static class Utilities
{
    /// <summary>
    /// UnityEventが発生するまで待つ
    /// </summary>
    /// <param name="unityEvent">発生を待ちたいUnityEvent</param>
    /// <returns>UnityEventの発生を待つためのIEnumerator</returns>
    public static IEnumerator WaitForEvent(UnityEvent unityEvent)
    {
        bool triggered = false;
        void callback() => triggered = true;
        unityEvent.AddListener(callback);
        yield return new WaitUntil(() => triggered);
        unityEvent.RemoveListener(callback);
    }
}
