using UnityEngine;

namespace GodInAugust.System
{
/// <summary>
/// 特定の日のみアクティブにしたり、非アクティブにしたいようなオブジェクトに付するコンポーネント
/// </summary>
[AddComponentMenu("God In August/System/Activate On Specific Day")]
public class ActivateOnSpecificDay : MonoBehaviour
{
    [SerializeField, Tooltip("日付のインデックス")]
    private int dateIndex;

    [SerializeField, Tooltip("trueならその日にアクティブにし、falseならその日は非アクティブにする")]
    private bool activationMode;

    /// <summary>
    /// 設定に応じてアクティブにしたり非アクティブにしたりする。
    /// 該当日ではない場合は何もしない。
    /// </summary>
    public void ActivateOrDeactivate()
    {
        if (GameState.State.DateIndex == dateIndex)
        {
            gameObject.SetActive(activationMode);
        }
    }
}
}
