using System;
using System.Collections.Generic;
using Mirle.MapController.DB_Proc;
using System.Data;
using System.Linq;
using Mirle.DRCS;
using Mirle.Def;
using Mirle.Def.U2NMMA30;
using Mirle.WriLog;

namespace Mirle.MapController
{
    public class MapHost
    {
        private RouteService routeService = new RouteService();
        private System.Timers.Timer timRead = new System.Timers.Timer();
        private readonly clsHost db;
        public MapHost(clsDbConfig config)
        {
            db = new clsHost(config);
            timRead.Elapsed += new System.Timers.ElapsedEventHandler(timRead_Elapsed);
            timRead.Enabled = true; timRead.Interval = 100;
        }

        private List<Location>[] Nodes = new List<Location>[0];
        public bool Done = false;
        private void timRead_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            timRead.Enabled = false;
            DataTable dtTmp = new DataTable();
            try
            {
                if (db.GetProc().FunMapping_Proc(out Nodes, ref dtTmp))
                {
                    for (int i = 0; i < Nodes.Length; i++)
                    {
                        foreach(var loc1 in Nodes[i])
                        {
                            foreach(var loc2 in Nodes[i])
                            {
                                if(loc1 != loc2)
                                {
                                    switch(loc1.Direction)
                                    {
                                        case clsEnum.IOPortDirection.Both:
                                        case clsEnum.IOPortDirection.InMode:
                                            switch(loc2.Direction)
                                            {
                                                case clsEnum.IOPortDirection.Both:
                                                case clsEnum.IOPortDirection.OutMode:
                                                    routeService.AddPath(loc1, loc2);
                                                    break;
                                            }

                                            break;
                                    }
                                }
                            }
                        }
                    }

                    if (dtTmp != null && dtTmp.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTmp.Rows.Count; i++)
                        {
                            string DeviceID = Convert.ToString(dtTmp.Rows[i][DB_Proc.Parameter.clsRoutdef.Column.DeviceID]);
                            string HostPortID = Convert.ToString(dtTmp.Rows[i][DB_Proc.Parameter.clsRoutdef.Column.HostPortID]);
                            var n1 = GetLocation(DeviceID, HostPortID);
                            if (n1 == null)
                            {
                                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error,
                                    $"Router找不到此Location => <DeviceID>{DeviceID} <HostPortID>{HostPortID}");
                                continue;
                            }

                            DeviceID = Convert.ToString(dtTmp.Rows[i][DB_Proc.Parameter.clsRoutdef.Column.NextDeviceID]);
                            HostPortID = Convert.ToString(dtTmp.Rows[i][DB_Proc.Parameter.clsRoutdef.Column.NextHostPortID]);
                            var n2 = GetLocation(DeviceID, HostPortID);
                            if (n2 == null)
                            {
                                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error,
                                    $"Router找不到此Location => <DeviceID>{DeviceID} <HostPortID>{HostPortID}");
                                continue;
                            }

                            if (n1.DeviceId == n2.DeviceId) continue;
                            else routeService.AddDevicePath(n1, n2);
                        }
                    }

                    Done = true;
                    FunInitPCBACV();
                    timRead.Enabled = false;
                }
                else timRead.Enabled = true;
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                timRead.Enabled = true;
            }
            finally
            {
                dtTmp.Dispose();
            }
        }

        public Location GetLocation(string DeviceID, string HostPortID)
        {
            for (int i = 0; i < Nodes.Length; i++)
            {
                if(Nodes[i].Any(r => r.DeviceId == DeviceID && r.LocationId == HostPortID))
                {
                    var node = Nodes[i].Where(r => r.DeviceId == DeviceID && r.LocationId == HostPortID);
                    foreach (var s in node)
                    {
                        return s;
                    }
                }
            }

            return null;
        }

        public bool GetPath(Location Start, Location End, ref Location Now_Start, ref Location Now_End)
        {
            try
            {
                var path = routeService.GetPath(Start, End);
                if (!path.Any())
                {
                    return false;
                }

                int iRow_Path = 0;
                foreach (Location p in path)
                {
                    if (iRow_Path > 1) break;
                    if (iRow_Path == 0)
                    {
                        Now_Start = p;
                    }
                    else
                    {
                        Now_End = p;
                    }

                    iRow_Path++;
                }

                return true;
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                return false;
            }
        }

        public bool EnablePath(Location Start, Location End, bool Enable)
        {
            try
            {
                routeService.EnalePath(Start, End, Enable);
                return true;
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                return false;
            }
        }

        public bool EnableNode(Location Node, bool Enable)
        {
            try
            {
                routeService.EnalePath(Node, Enable);
                return true;
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                return false;
            }
        }

        public void FunInitPCBACV()
        {
            Location M1_01 = GetLocation("2", ConveyorDef.PCBA.M1_01.BufferName);
            Location M1_05 = GetLocation(ConveyorDef.DeviceID_AGV + "8", ConveyorDef.AGV.M1_05.BufferName);
            Location M1_06 = GetLocation("2", ConveyorDef.PCBA.M1_06.BufferName);
            Location M1_10 = GetLocation(ConveyorDef.DeviceID_AGV + "8", ConveyorDef.AGV.M1_10.BufferName);
            Location Shelf2 = GetLocation("2", LocationTypes.Shelf.ToString());
            Location LeftFork2 = GetLocation("2", "LeftFork");

            Location M1_11 = GetLocation("1", ConveyorDef.PCBA.M1_11.BufferName);
            Location M1_15 = GetLocation(ConveyorDef.DeviceID_AGV + "8", ConveyorDef.AGV.M1_15.BufferName);
            Location M1_16 = GetLocation("1", ConveyorDef.PCBA.M1_16.BufferName);
            Location M1_20 = GetLocation(ConveyorDef.DeviceID_AGV + "8", ConveyorDef.AGV.M1_20.BufferName);
            Location Shelf1 = GetLocation("1", LocationTypes.Shelf.ToString());
            Location LeftFork1 = GetLocation("1", "LeftFork");

            //禁用PCBA1故障模式路徑
            if (!EnablePath(LeftFork1, M1_16, false))
                clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"初始PCBA_1路徑失敗：禁用路徑{LeftFork1.LocationId}->{M1_16.LocationId}Fail.");
            if (!EnablePath(Shelf1, M1_16, false))
                clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"初始PCBA_1路徑失敗：禁用路徑{Shelf1.LocationId}->{M1_16.LocationId}Fail.");
            if (!EnablePath(M1_16, M1_20, false))
                clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"初始PCBA_1路徑失敗：禁用路徑{M1_16.LocationId}->{M1_20.LocationId}Fail.");
            if (!EnablePath(M1_15, M1_11, false))
                clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"初始PCBA_1路徑失敗：禁用路徑{M1_15.LocationId}->{M1_11.LocationId}Fail.");
            if (!EnablePath(M1_11, Shelf1, false))
                clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"初始PCBA_1路徑失敗：禁用路徑{M1_11.LocationId}->{Shelf1.LocationId}Fail.");

            //禁用PCBA2故障模式路徑
            if (!EnablePath(LeftFork2, M1_06, false))
                clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"初始PCBA_2路徑失敗：禁用路徑{LeftFork2.LocationId}->{M1_06.LocationId}Fail.");
            if (!EnablePath(Shelf2, M1_06, false))
                clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"初始PCBA_2路徑失敗：禁用路徑{Shelf2.LocationId}->{M1_06.LocationId}Fail.");
            if (!EnablePath(M1_06, M1_10, false))
                clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"初始PCBA_2路徑失敗：禁用路徑{M1_06.LocationId}->{M1_10.LocationId}Fail.");
            if (!EnablePath(M1_05, M1_01, false))
                clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"初始PCBA_2路徑失敗：禁用路徑{M1_05.LocationId}->{M1_01.LocationId}Fail.");
            if (!EnablePath(M1_01, Shelf2, false))
                clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"初始PCBA_2路徑失敗：禁用路徑{M1_01.LocationId}->{Shelf2.LocationId}Fail.");

        }
    }
}
