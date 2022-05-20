using Mirle.STKC.R46YP320.Model;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Repositories
{
    public interface IAlarmDataRepository
    {
        void Insert(AlarmDataDTO dto);

        void InsertWarning(AlarmDataDTO dto);

        void UpdateClearedAlarmByEqIdAndAlarmCode(string stockerId, string eqId, string alarmCode);

        IEnumerable<AlarmDataDTO> GetAllCurrentAlarm(string stockerId);

        IEnumerable<AlarmDataDTO> GetCurrentAlarmByEqId(string stockerId, string eqId);

        void UpdateClearedAlarm(string stockerId);

        void UpdateClearedAlarmByEqId(string stockerId, string eqId);

        void UpdatePLCDoorClosedDT(string stockerId);

        void UpdatePLCDoorOpenDT(string stockerId);

        void UpdatePLCResetAlarmDT(string stockerId, string eqId);

        void UpdatePLCResetAlarmDT(string stockerId, string eqId, string alarmCode);
    }
}