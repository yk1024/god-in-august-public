using UnityEngine;
using UnityEngine.Events;

namespace GodInAugust.System
{
/// <summary>
/// 複数のコンポーネント間でやり取りするために、イベントを保持しておくためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/System/Event Handler")]
public class EventHandler : MonoBehaviour
{
    /// <summary>
    /// このイベントにリスナーを登録しておくことで、Invokeしたことを検知できる。
    /// </summary>
    public UnityEvent UnityEvent { get; } = new UnityEvent();

    /// <summary>
    /// UnityEventに登録されたコールバックを実行する。
    /// </summary>
    public void InvokeCallbacks()
    {
        UnityEvent.Invoke();
    }
}
}
