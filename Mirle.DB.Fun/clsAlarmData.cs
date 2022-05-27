using Mirle.DataBase;
using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.DB.Fun
{
    public class clsAlarmData
    {
        public bool SubSqlAlarm_Occur(int StockerID, string AlarmId, DataBase.DB db)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                string strSql = $"select * from {Parameter.clsAlarmData.TableName} where " +
                    $"{Parameter.clsAlarmData.Column.AlarmCode} = '{AlarmId}' ";
                strSql += $" and {Parameter.clsAlarmData.Column.AlarmSts} = '{Parameter.clsAlarmData.Status.Occur}' and " +
                    $"{Parameter.clsAlarmData.Column.DeviceID} = '{StockerID}' ";

                string strEM = "";
                int iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                if (iRet == DBResult.Success) return true;
                else if (iRet == DBResult.NoDataSelect)
                {
                    string NowDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    strSql = $"insert into {Parameter.clsAlarmData.TableName}({Parameter.clsAlarmData.Column.DeviceID}," +
                        $"{Parameter.clsAlarmData.Column.AlarmCode},{Parameter.clsAlarmData.Column.AlarmSts}," +
                        $"{Parameter.clsAlarmData.Column.Start_Date}) values(";
                    strSql += $"'{StockerID}', '{AlarmId}', '{Parameter.clsAlarmData.Status.Occur}', '{NowDate}')";
                    if (db.ExecuteSQL(strSql, ref strEM) == DBResult.Success) return true;
                    else
                    {
                        clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                        return false;
                    }
                }
                else
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
            finally
            {
                dtTmp.Dispose();
            }
        }

        public bool SubSqlAlarm_Solved(int StockerID, string AlarmId, DataBase.DB db)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                string strSql = $"select * from {Parameter.clsAlarmData.TableName} where " +
                    $"{Parameter.clsAlarmData.Column.AlarmCode} = '{AlarmId}' and {Parameter.clsAlarmData.Column.AlarmSts} = ";
                strSql += $"'{Parameter.clsAlarmData.Status.Occur}' and {Parameter.clsAlarmData.Column.DeviceID} = '{StockerID}' ";

                string strEM = "";
                int iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                if (iRet == DBResult.Success)
                {
                    string aAlarmTime = Convert.ToString(dtTmp.Rows[0][Parameter.clsAlarmData.Column.Start_Date]);

                    string sDate1 = aAlarmTime;
                    string sClsTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    TimeSpan ts1 = new TimeSpan(DateTime.ParseExact(sDate1,
                                           "yyyy-MM-dd HH:mm:ss",
                                           System.Globalization.CultureInfo.InvariantCulture
                                           ).Ticks);
                    TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
                    TimeSpan ts = ts1.Subtract(ts2).Duration();
                    double iTotalSecs = 0;
                    iTotalSecs = ts.TotalSeconds;

                    strSql = $"update {Parameter.clsAlarmData.TableName} set {Parameter.clsAlarmData.Column.AlarmSts}=" +
                        $"'{Parameter.clsAlarmData.Status.Clear}',{Parameter.clsAlarmData.Column.Clear_Date}='{sClsTime}'," +
                    $"{Parameter.clsAlarmData.Column.Total_Secs}= {iTotalSecs} where " +
                    $"{Parameter.clsAlarmData.Column.AlarmCode} = '{AlarmId}' and {Parameter.clsAlarmData.Column.AlarmSts} = " +
                    $"'{Parameter.clsAlarmData.Status.Occur}' and {Parameter.clsAlarmData.Column.DeviceID} = '{StockerID}' ";
                    if (db.ExecuteSQL(strSql, ref strEM) == DBResult.Success) return true;
                    else
                    {
                        clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                        return false;
                    }
                }
                else if (iRet == DBResult.NoDataSelect) return true;
                else
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
            finally
            {
                dtTmp.Dispose();
            }
        }
    }
}
