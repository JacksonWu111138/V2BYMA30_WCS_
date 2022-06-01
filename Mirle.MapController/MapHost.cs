using System;
using System.Collections.Generic;
using Mirle.DB.Proc;
using Mirle.Def;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Mirle.DRCS;

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

        private readonly List<Location> Nodes = new List<Location>();
        public bool Done = false;
        private void timRead_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            timRead.Enabled = false;
            DataTable dtTmp = new DataTable();
            try
            {
                if (db.GetProc().FunMapping_Proc(out var list, ref dtTmp))
                {
                    foreach(var loc in list)
                    {
                        var node = new Location(loc.DeviceID, loc.HostPortID, Location.GetLocationTypesByPortType(loc.PortType));
                        Nodes.Add(node);
                    }

                    for (int i = 0; i < dtTmp.Rows.Count; i++)
                    {
                        string DeviceID = Convert.ToString(dtTmp.Rows[i][DB.Fun.Parameter.clsRoutdef.Column.DeviceID]);
                        string HostPortID = Convert.ToString(dtTmp.Rows[i][DB.Fun.Parameter.clsRoutdef.Column.HostPortID]);
                        var n1 = GetLocation(DeviceID, HostPortID);
                        if(n1 == null)
                        {
                            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error,
                                $"Router找不到此Location => <DeviceID>{DeviceID} <HostPortID>{HostPortID}");
                            continue;
                        }

                        DeviceID = Convert.ToString(dtTmp.Rows[i][DB.Fun.Parameter.clsRoutdef.Column.NextDeviceID]);
                        HostPortID = Convert.ToString(dtTmp.Rows[i][DB.Fun.Parameter.clsRoutdef.Column.NextHostPortID]);
                        var n2 = GetLocation(DeviceID, HostPortID);
                        if (n2 == null)
                        {
                            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error,
                                $"Router找不到此Location => <DeviceID>{DeviceID} <HostPortID>{HostPortID}");
                            continue;
                        }

                        if (n1.DeviceId == n2.DeviceId) Location.AddPath_Single(routeService, n1, n2);
                        else routeService.AddDevicePath(n1, n2);
                    }

                    Done = true;
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
            var node = Nodes.Where(r => r.DeviceId == DeviceID && r.LocationId == HostPortID);
            foreach(var s in node)
            {
                return s;
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
    }
}
