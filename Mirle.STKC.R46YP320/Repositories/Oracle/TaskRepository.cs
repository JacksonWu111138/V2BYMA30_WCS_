using Dapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using System.Collections.Generic;
using Mirle.Def;
using System;
using Mirle.Stocker.R46YP320;

namespace Mirle.STKC.R46YP320.Repositories.Oracle
{
    public class TaskRepository : ITaskRepository
    {
        private readonly OracleDbService _dbService;

        public TaskRepository(OracleDbService dbService)
        {
            _dbService = dbService;
        }

        public bool IsConnected => _dbService.IsConnected;

        public void Insert(Structure.TaskDTO dto)
        {
            var sql = @"insert into Task(StockerID,CommandID,TaskNo,CraneNo,ForkNumber,CSTID,TRANSFERSTATE,CMDState,
TransferMode,Source,SourceBay,Destination,DestinationBay,
TravelAxisSpeed,LifterAxisSpeed,RotateAxisSpeed,ForkAxisSpeed,
UserID,BCRReadFlag,Priority,QueueDT,NextDest)
values(:StockerID, :CommandID, :TaskNo, :CraneNo, :ForkNumber, :CSTID, :TransferState, :CMDState,
:TransferMode, :Source, :SourceBay, :Destination, :DestinationBay,
:TravelAxisSpeed, :LifterAxisSpeed, :RotateAxisSpeed, :ForkAxisSpeed,
:UserID, :BCRReadFlag, :Priority, :QueueDT, :NextDest)";
            using (var con = _dbService.GetDbConnection())
            {
                var affectedRows = con.Execute(sql, dto);
            }
        }

        public void UpdateByTaskNo(Structure.TaskDTO dto)
        {
            var sql = @"update Task set TransferState=:TransferState
                                       ,CompleteCode=:CompleteCode
                                       ,CMDState=:CMDState
                                       ,AtSourceDT=:AtSourceDT
                                       ,AtDestinationDT=:AtDestinationDT
                                       ,BCRReadDT=:BCRReadDT
                                       ,BCRReplyCSTID=:BCRReplyCSTID
                                       ,BCRReadStatus=:BCRReadStatus
                                       ,InitialDT=:InitialDT
                                       ,WaitingDT=:WaitingDT
                                       ,ActiveDT=:ActiveDT
                                       ,FinishDT=:FinishDT
                                       ,FinishLocation=:FinishLocation
                                       ,C1StartDT=:C1StartDT
                                       ,CSTOnDT=:CSTOnDT
                                       ,CSTTakeOffDT=:CSTTakeOffDT
                                       ,C2StartDT=:C2StartDT
                                       ,T1=:T1,T2=:T2,T3=:T3,T4=:T4
                                       ,F1StartDT=:F1StartDT
                                       ,F2StartDT=:F2StartDT
                                       ,RenewFlag='Y' where TaskNo=:TaskNo";

            using (var con = _dbService.GetDbConnection())
            {
                var affectedRows = con.Execute(sql, dto);
            }
        }

        public bool FunWriCommand_Proc(Structure.TaskDTO dto, Crane crane, CraneCmdInfo craneCmdInfo)
        {
            var sql = @"update Task set TransferState=:TransferState
                                       ,CompleteCode=:CompleteCode
                                       ,CMDState=:CMDState
                                       ,AtSourceDT=:AtSourceDT
                                       ,AtDestinationDT=:AtDestinationDT
                                       ,BCRReadDT=:BCRReadDT
                                       ,BCRReplyCSTID=:BCRReplyCSTID
                                       ,BCRReadStatus=:BCRReadStatus
                                       ,InitialDT=:InitialDT
                                       ,WaitingDT=:WaitingDT
                                       ,ActiveDT=:ActiveDT
                                       ,FinishDT=:FinishDT
                                       ,FinishLocation=:FinishLocation
                                       ,C1StartDT=:C1StartDT
                                       ,CSTOnDT=:CSTOnDT
                                       ,CSTTakeOffDT=:CSTTakeOffDT
                                       ,C2StartDT=:C2StartDT
                                       ,T1=:T1,T2=:T2,T3=:T3,T4=:T4
                                       ,F1StartDT=:F1StartDT
                                       ,F2StartDT=:F2StartDT
                                       ,RenewFlag='Y' where TaskNo=:TaskNo";

            using (var con = _dbService.GetDbConnection())
            {
                using (var tran = con.BeginTransaction())
                {
                    if (con.Execute(sql, dto, tran) <= 0)
                    {
                        tran.Rollback();
                        return false;
                    }

                    if (crane.WriteNewCommandAsync(craneCmdInfo).Result == false)
                    {
                        tran.Rollback();
                        return false;
                    }

                    tran.Commit();
                    return true;
                }
            }
        }

        public int UpdateByTaskNo_ReturnInt(Structure.TaskDTO dto)
        {
            var sql = @"update Task set TransferState=:TransferState
                                       ,CompleteCode=:CompleteCode
                                       ,CMDState=:CMDState
                                       ,AtSourceDT=:AtSourceDT
                                       ,AtDestinationDT=:AtDestinationDT
                                       ,BCRReadDT=:BCRReadDT
                                       ,BCRReplyCSTID=:BCRReplyCSTID
                                       ,BCRReadStatus=:BCRReadStatus
                                       ,InitialDT=:InitialDT
                                       ,WaitingDT=:WaitingDT
                                       ,ActiveDT=:ActiveDT
                                       ,FinishDT=:FinishDT
                                       ,FinishLocation=:FinishLocation
                                       ,C1StartDT=:C1StartDT
                                       ,CSTOnDT=:CSTOnDT
                                       ,CSTTakeOffDT=:CSTTakeOffDT
                                       ,C2StartDT=:C2StartDT
                                       ,T1=:T1,T2=:T2,T3=:T3,T4=:T4
                                       ,F1StartDT=:F1StartDT
                                       ,F2StartDT=:F2StartDT
                                       ,RenewFlag='Y' where TaskNo=:TaskNo";

            using (var con = _dbService.GetDbConnection())
            {
                return con.Execute(sql, dto);
            }
        }

        public IEnumerable<Structure.TaskDTO> GetByTaskStateIsQueue(string stockerId)
        {
            var sql = "select * from Task where StockerID =:StockerID and TaskState =:TaskState";
            using (var con = _dbService.GetDbConnection())
            {
                return con.Query<Structure.TaskDTO>(sql, new
                {
                    StockerID = stockerId,
                    TaskState = TaskState.Queue
                });
            }
        }

        public IEnumerable<Structure.TaskDTO> GetByCommandIdAndTransferModeIsTo(string commandId)
        {
            var sql = "select * from Task where CommandID=:CommandId and Transfermode =:TransferMode";
            using (var con = _dbService.GetDbConnection())
            {
                return con.Query<Structure.TaskDTO>(sql, new
                {
                    CommandId = commandId,
                    TransferMode = clsEnum.TaskMode.Deposit,
                });
            }
        }

        public IEnumerable<Structure.TaskDTO> GetByTaskNo(string taskNo)
        {
            var sql = "select * from Task where TaskNo=:TaskNo";
            using (var con = _dbService.GetDbConnection())
            {
                return con.Query<Structure.TaskDTO>(sql, new
                {
                    TaskNo = taskNo,
                });
            }
        }

        public void RollbackByTaskNo(string taskNo)
        {
            var sql = @"update Task set TransferState=:TaskState,CMDState=:CMDState,InitialDT='',RenewFlag='Y' where TaskNo=:TaskNo";
            using (var con = _dbService.GetDbConnection())
            {
                var affectedRows = con.Execute(sql, new
                {
                    TaskState = TaskState.Queue,
                    CMDState = TaskCmdState.STKCQueue,
                    TaskNo = taskNo,
                });
            }
        }

        public void Delete(Structure.TaskDTO dto)
        {
            var sql = "delete from Task where TaskNo=:TaskNo";
            using (var con = _dbService.GetDbConnection())
            {
                var affectedRows = con.Execute(sql, dto);
            }
        }

        public void InsertHisTask(HisTaskDTO dto)
        {
            var sql = @"insert into HisTask(HisDT,StockerID,CommandID,TaskNo,CraneNo,ForkNumber,CSTID,TaskState,CompleteCode,CMDState,
TransferMode,TransferModeType,Source,SourceBay,Destination,DestinationBay, AtSourceDT, AtDestinationDT,
TravelAxisSpeed,LifterAxisSpeed,RotateAxisSpeed,ForkAxisSpeed, CmdInfo,
UserID,LotId,EmptyCST,CSTType,BCRReadFlag,BCRReadDT,BCRReplyCSTID,BCRReadStatus,Priority,QueueDT,
InitialDT,WaitingDT,ActiveDT,FinishDT,FinishLocation,C1StartDT,CSTOnDT,CSTTakeOffDT,C2StartDT,T1,T2,T3,T4,
F1StartDT,F2StartDT,RenewFlag,AccStep,UpdateDT,TravelDistance,ReplyCSTID,NextDest)
values(:HisDT, :StockerID, :CommandID, :TaskNo, :CraneNo, :ForkNumber, :CSTID, :TransferState, :CompleteCode, :CMDState,
:TransferMode, :TransferModeType, :Source, :SourceBay, :Destination, :DestinationBay, :AtSourceDT, :AtDestinationDT,
:TravelAxisSpeed, :LifterAxisSpeed, :RotateAxisSpeed, :ForkAxisSpeed, :CmdInfo,
:UserID, :LotId, :EmptyCST, :CSTType, :BCRReadFlag, :BCRReadDT, :BCRReplyCSTID, :BCRReadStatus, :Priority, :QueueDT,
:InitialDT, :WaitingDT, :ActiveDT, :FinishDT, :FinishLocation, :C1StartDT, :CSTOnDT, :CSTTakeOffDT, :C2StartDT, :T1, :T2, :T3, :T4,
:F1StartDT, :F2StartDT, :RenewFlag, :AccStep, :UpdateDT, :TravelDistance, :ReplyCSTID, :NextDest)";
            using (var con = _dbService.GetDbConnection())
            {
                var affectedRows = con.Execute(sql, dto);
            }
        }
        public void InsertHistory(string deviceID)
        {
            string sql1 = $"INSERT INTO HisTask SELECT '{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}', * FROM TASK WHERE DEVICEID='{deviceID}' AND COMMANDID LIKE 'STKC%' AND TaskState=3";
            string sql2 = $"DELETE TASK WHERE DEVICEID='{deviceID}' AND COMMANDID like 'STKC%' AND TaskState=3";
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql1);
                con.Execute(sql2);
            }
        }
    }
}