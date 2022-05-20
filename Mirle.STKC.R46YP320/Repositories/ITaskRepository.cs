using Mirle.STKC.R46YP320.Model;
using Mirle.Stocker.R46YP320;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Repositories
{
    public interface ITaskRepository
    {
        bool IsConnected { get; }
        bool FunWriCommand_Proc(Structure.TaskDTO dto, Crane crane, CraneCmdInfo craneCmdInfo);
        void Insert(Structure.TaskDTO dto);

        void UpdateByTaskNo(Structure.TaskDTO dto);

        int UpdateByTaskNo_ReturnInt(Structure.TaskDTO dto);

        IEnumerable<Structure.TaskDTO> GetByTaskStateIsQueue(string stockerId);

        IEnumerable<Structure.TaskDTO> GetByCommandIdAndTransferModeIsTo(string commandId);

        IEnumerable<Structure.TaskDTO> GetByTaskNo(string taskNo);

        void RollbackByTaskNo(string taskNo);

        void Delete(Structure.TaskDTO dto);

        void InsertHisTask(HisTaskDTO dto);
        void InsertHistory(string deviceID);
    }
}