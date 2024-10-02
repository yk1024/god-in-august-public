public class PrayHistory
{
    public PrayHistory(PrayType prayType, bool anomalyExists)
    {
        PrayType = prayType;
        AnomalyExists = anomalyExists;
    }

    public PrayType PrayType { get; }
    public bool AnomalyExists { get; }

    public bool IsSuccessful()
    {
        return IsDailyLoop() || IsProceed();
    }

    public bool IsDailyLoop()
    {
        return AnomalyExists && PrayType == PrayType.Wish;
    }

    public bool IsProceed()
    {
        return !AnomalyExists && PrayType == PrayType.Gratitude;
    }

    public bool IsLoop()
    {
        return !IsProceed();
    }
}
