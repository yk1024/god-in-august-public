using UnityEngine;
using System;

namespace GodInAugust.System
{
/// <summary>
/// 同時に1つしか存在しないコンポーネントの抽象クラス。
/// AwakeでInstanceプロパティに設定され、OnDestroyで破棄される。
/// 重複して存在すると、例外を投げる。
/// </summary>
/// <typeparam name="TSelf">Instanceの型、自クラスを指定</typeparam>
public abstract class SingletonBehaviour<TSelf> : MonoBehaviour where TSelf : SingletonBehaviour<TSelf>
{
    /// <summary>
    /// 存在しているインスタンス
    /// </summary>
    public static TSelf Instance { get; private set; }

    protected virtual void Awake()
    {
        // インスタンスが既に存在する場合は、重複してしまうので、例外を投げる
        if (Instance != null) throw new InvalidOperationException("Instance already exists.");

        // キャストしてInstanceに保持する。
        Instance = (TSelf)this;
    }

    protected virtual void OnDestroy()
    {
        // コンポーネントが破棄された時点で、Instanceも破棄する。
        Instance = null;
    }
}
}
