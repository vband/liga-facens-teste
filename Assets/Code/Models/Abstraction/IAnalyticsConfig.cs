namespace Code.Models.Abstraction
{
    public interface IAnalyticsConfig
    {
        string LevelFinishedEventName { get; }
        string LevelFailedEventName { get; }
        
        string LevelIndexParameterName { get; }
    }
}