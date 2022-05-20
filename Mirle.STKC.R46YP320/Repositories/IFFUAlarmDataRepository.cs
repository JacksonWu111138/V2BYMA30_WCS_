using Mirle.STKC.R46YP320.Model;

namespace Mirle.STKC.R46YP320.Repositories
{
    public interface IFFUAlarmDataRepository
    {
        void Insert(FFUAlarmDataDTO dto);

        void UpdateClearedAlarmByEqIdAndAlarmCode(string eqId, string alarmCode);

        void UpdateAllClearedAlarm();
    }
}