namespace CommunicationLibrary
{
    /// <summary>
    /// Interface of of alarm service
    /// </summary>
    public interface IAlarmService
    {
        AlarmData[] GetLastSelectedAlarmList();
    }
}
