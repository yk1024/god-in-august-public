namespace GodInAugust.Level
{
/// <summary>
/// インタラクト可能なコンポーネント用のインターフェース
/// </summary>
public interface IInteractable : ILookAtTarget
{
    /// <summary>
    /// インタラクトした際に実行する処理
    /// </summary>
    public void Interact();
}
}
