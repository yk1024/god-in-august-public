namespace GodInAugust.System
{
/// <summary>
/// 祈りの成否等を記録するためのクラス
/// </summary>
public class PrayHistory
{
    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="prayType">祈りの種類</param>
    /// <param name="anomalyExists">その日異変が発生していたかどうか</param>
    public PrayHistory(PrayType prayType, bool anomalyExists)
    {
        PrayType = prayType;
        AnomalyExists = anomalyExists;
    }

    /// <summary>
    /// 祈りの種類
    /// </summary>
    public PrayType PrayType { get; }

    /// <summary>
    /// その日異変が発生していたかどうか
    /// </summary>
    public bool AnomalyExists { get; }

    /// <summary>
    /// 祈りが成功だったか判定する
    /// </summary>
    /// <returns>祈りの成否</returns>
    public bool IsSuccessful()
    {
        // 祈りが成功するのは、異変が存在して「お願い」した場合か、異変が存在しなく「感謝」した場合
        return IsDailyLoop() || IsProceed();
    }

    /// <summary>
    /// 同じ日を繰り返すかどうかを判定する。
    /// 異変が存在した日に「お願い」を行なった場合、祈りが成功し、当日をやり直す。
    /// </summary>
    /// <returns>同じ日を繰り返す場合true、それ以外の場合はfalse</returns>
    public bool IsDailyLoop()
    {
        return AnomalyExists && PrayType == PrayType.Wish;
    }

    /// <summary>
    /// 次の日に進むかどうかを判定する。
    /// 異変が存在しない日に「感謝」を行なった場合、祈りが成功し、次の日に進む。
    /// </summary>
    /// <returns>次の日に進む場合true、それ以外の場合はfalse</returns>
    public bool IsProceed()
    {
        return !AnomalyExists && PrayType == PrayType.Gratitude;
    }

    /// <summary>
    /// ループするかどうかを判定する。
    /// 祈りを失敗した場合は異変初日に戻り、異変が存在した日に「お願い」を行なった場合は当日を繰り返す。
    /// </summary>
    /// <returns>ループする場合true、次の日に進む場合はfalse</returns>
    public bool IsLoop()
    {
        // ループするのは次の日に進まない場合
        return !IsProceed();
    }
}
}
