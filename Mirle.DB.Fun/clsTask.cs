using Mirle.Def;
using Mirle.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.DB.Fun
{
    public class clsTask
    {
        private clsSno sno = new clsSno();
        public TaskDTO GetTaskDTO_ByLocation(CmdMstInfo cmd, Location sLoc_Start, Location sLoc_End, DataBase.DB db)
        {
            TaskDTO dto = new TaskDTO();
            dto.CommandID = cmd.Cmd_Sno;
            dto.TaskNo = sno.FunGetSeqNo(clsEnum.enuSnoType.CMDSUO, db);
            if (string.IsNullOrWhiteSpace(dto.TaskNo))
            {
                dto.TaskNo = "20001";
            }

            dto.TaskState = clsConstValue.CmdSts.strCmd_Initial;


            return dto;
        }
    }
}
