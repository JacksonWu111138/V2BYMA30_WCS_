﻿using System;
using System.Collections.Generic;
using Mirle.Def;
using System.Data;
using Mirle.DataBase;

namespace Mirle.DB.WMS.Fun
{
    public class clsLocMst
    {
        /// <summary>
        /// 確認儲位是否是外儲位
        /// </summary>
        /// <param name="sLoc"></param>
        /// <param name="IsOutside"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int CheckLocIsOutside(string sLoc, ref bool IsOutside, ref string sLocDD, ref bool IsEmpty_DD, ref string BoxID_DD, DataBase.DB db)
        {
            try
            {
                int iRet = CheckLocIsOutside(sLoc, ref IsOutside, db);
                if (iRet == DBResult.Success)
                {
                    sLocDD = GetLocDD(sLoc, db);
                    if (string.IsNullOrWhiteSpace(sLocDD))
                        throw new Exception($"找不到{sLoc}的對照儲位！");
                    return CheckLocIsEmpty(sLocDD, ref IsEmpty_DD, ref BoxID_DD, db);
                }

                return iRet;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return DBResult.Exception;
            }
        }

        /// <summary>
        /// 確認儲位是否是外儲位
        /// </summary>
        /// <param name="sLoc"></param>
        /// <param name="IsOutside"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int CheckLocIsOutside(string sLoc, ref bool IsOutside, DataBase.DB db)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                string strEM = "";
                string strSql = $"select IS_INSIDE from {Parameter.clsLocMst.TableName} where {Parameter.clsLocMst.Column.Loc} = '{sLoc}' ";
                int iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                if (iRet == DBResult.Success)
                {
                    IsOutside = Convert.ToString(dtTmp.Rows[0][0]) == "N" ? true : false;
                }
                else
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                }

                return iRet;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return DBResult.Exception;
            }
            finally
            {
                dtTmp = null;
            }
        }

        public string GetLocDD(string sLoc, DataBase.DB db)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                string strEM = "";
                string strSql = $"select {Parameter.clsLocMst.Column.LocDD} from {Parameter.clsLocMst.TableName} where " +
                    $"{Parameter.clsLocMst.Column.Loc} = '{sLoc}' ";
                int iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                if (iRet == DBResult.Success)
                {
                    return Convert.ToString(dtTmp.Rows[0][0]);
                }
                else
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return string.Empty;
            }
            finally
            {
                dtTmp = null;
            }
        }

        public int CheckLocIsEmpty(string sLoc, ref bool IsEmpty, DataBase.DB db)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                string strEM = "";
                string strSql = $"select STORAGE_STATUS,from {Parameter.clsLocMst.TableName} where {Parameter.clsLocMst.Column.Loc} = '{sLoc}' ";
                int iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                if (iRet == DBResult.Success)
                {
                    IsEmpty = Convert.ToString(dtTmp.Rows[0]["STORAGE_STATUS"]).Trim().ToUpper() ==
                        clsConstValue.LocSts.Empty;
                }
                else
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                }

                return iRet;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return DBResult.Exception;
            }
            finally
            {
                dtTmp = null;
            }
        }

        public int CheckLocIsEmpty(string sLoc, ref bool IsEmpty, ref string BoxID, DataBase.DB db)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                string strEM = "";
                string strSql = $"select STORAGE_STATUS, {Parameter.clsLocMst.Column.BoxID} from " +
                    $"{Parameter.clsLocMst.TableName} where {Parameter.clsLocMst.Column.Loc} = '{sLoc}' ";
                int iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                if (iRet == DBResult.Success)
                {
                    IsEmpty = Convert.ToString(dtTmp.Rows[0]["STORAGE_STATUS"]).Trim().ToUpper() ==
                        clsConstValue.LocSts.Empty;
                    BoxID = Convert.ToString(dtTmp.Rows[0][Parameter.clsLocMst.Column.BoxID]);
                }
                else
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                }

                return iRet;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return DBResult.Exception;
            }
            finally
            {
                dtTmp = null;
            }
        }

        public int CheckLineByBoxID(string sBoxID, ref int StockerID, ref string sLoc, DataBase.DB db)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                string strSql = $"select * from {Parameter.clsLocMst.TableName} where {Parameter.clsLocMst.Column.BoxID} = '{sBoxID}' ";
                string strEM = "";
                int iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                if (iRet == DBResult.Success)
                {
                    StockerID = Convert.ToInt32(dtTmp.Rows[0][Parameter.clsLocMst.Column.EquNo]);
                    sLoc = Convert.ToString(dtTmp.Rows[0][Parameter.clsLocMst.Column.Loc]);
                }
                else clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");

                return iRet;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return DBResult.Exception;
            }
            finally
            {
                dtTmp = null;
            }
        }

        public string funSearchEmptyLoc(string Equ_No, clsEnum.LocSts_Double locSts, DataBase.DB db)
        {
            string strEM = "";
            DataTable dtTmp = new DataTable();
            try
            {
                string sSQL = $"SELECT TOP 1 {Parameter.clsLocMst.Column.Loc} FROM " +
                    $"{Parameter.clsLocMst.TableName} WHERE STORAGE_STATUS = '" + clsConstValue.LocSts.Empty + "' ";
                sSQL += $" AND OPERATE_STATUS = '{clsConstValue.LocSts.Normal}' and {Parameter.clsLocMst.Column.EquNo} = '" + Equ_No + "' ";

                if (locSts == clsEnum.LocSts_Double.NNNN) //找外側空庫位
                {
                    sSQL += $" AND {Parameter.clsLocMst.Column.Loc} IN (SELECT {Parameter.clsLocMst.Column.LocDD} FROM " +
                        $"{Parameter.clsLocMst.TableName}" +
                        $" WHERE STORAGE_STATUS='{clsConstValue.LocSts.Empty}' AND OPERATE_STATUS = '{clsConstValue.LocSts.Normal}'" +
                        $" AND IS_INSIDE = 'Y') ";
                }
                else if (locSts == clsEnum.LocSts_Double.SNNS)
                {
                    sSQL += $" AND {Parameter.clsLocMst.Column.Loc} IN (SELECT {Parameter.clsLocMst.Column.LocDD} FROM " +
                        $"{Parameter.clsLocMst.TableName}" +
                        $" WHERE STORAGE_STATUS in ('{clsConstValue.LocSts.Full}','{clsConstValue.LocSts.NotFull}')" +
                        $" AND OPERATE_STATUS = '{clsConstValue.LocSts.Normal}' AND IS_INSIDE = 'N') ";
                }
                else if (locSts == clsEnum.LocSts_Double.ENNE)
                {
                    sSQL += $" AND {Parameter.clsLocMst.Column.Loc} IN (SELECT {Parameter.clsLocMst.Column.LocDD} FROM " +
                        $"{Parameter.clsLocMst.TableName}" +
                        $" WHERE STORAGE_STATUS = '{clsConstValue.LocSts.EmptyBox}' AND OPERATE_STATUS = '{clsConstValue.LocSts.Normal}'" +
                        " AND IS_INSIDE = 'N') ";
                }
                else if (locSts == clsEnum.LocSts_Double.XNNX)
                {
                    sSQL += $" AND {Parameter.clsLocMst.Column.Loc} IN (SELECT {Parameter.clsLocMst.Column.LocDD} FROM " +
                        $"{Parameter.clsLocMst.TableName}" +
                        $" WHERE OPERATE_STATUS = '{clsConstValue.LocSts.Block}' AND IS_INSIDE = 'N') ";
                }
                else { } //Single Deep

                sSQL += $" ORDER BY {Parameter.clsLocMst.Column.BAY}, {Parameter.clsLocMst.Column.LEVEL}, {Parameter.clsLocMst.Column.ROW} DESC";

                dtTmp = new DataTable();
                string sNewLoc;
                if (db.GetDataTable(sSQL, ref dtTmp, ref strEM) == DBResult.Success)
                {
                    sNewLoc = dtTmp.Rows[0][Parameter.clsLocMst.Column.Loc].ToString();
                }
                else
                {
                    sNewLoc = "";
                }

                return sNewLoc;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return "";
            }
            finally
            {
                dtTmp = null;
            }
        }

        public string funSearchEmptyLoc_Abnormal(string Equ_No, clsEnum.LocSts_Double locSts, string sSource, DataBase.DB db)
        {
            string sSQL = "";
            string sNewLoc = "";
            string strEM = "";
            DataTable dtTmp = new DataTable();
            try
            {
                sSQL = "SELECT TOP 1 LOC, Equ_RowNo FROM LOC_MST WHERE LocSts = '" + clsEnum.LocSts.N.ToString() + "' ";
                sSQL += " AND Equ_No = " + Equ_No + " ";
                switch (sSource.Substring(0, 2))
                {
                    //case 1:
                    //case 3:
                    //    sSQL += " and Equ_RowNo IN (1,3) ";
                    //    break;
                    //case 2:
                    //case 4:
                    //    sSQL += " and Equ_RowNo IN (2,4) ";
                    //    break;
                    //default:
                    //    return "";
                }

                if (locSts == clsEnum.LocSts_Double.NNNN) //找外側空庫位
                {
                    sSQL += " AND LOC IN (SELECT Loc_DD FROM LOC_MST WHERE LocSts='N' AND Equ_RowNo IN (1,2) ) ";
                }

                if (locSts == clsEnum.LocSts_Double.SNNS)
                {
                    sSQL += " AND LOC IN (SELECT Loc_DD FROM LOC_MST WHERE LocSts='S' AND Equ_RowNo IN (3,4) ) ";
                }

                if (locSts == clsEnum.LocSts_Double.ENNE)
                {
                    sSQL += " AND LOC IN (SELECT Loc_DD FROM LOC_MST WHERE LocSts='E' AND Equ_RowNo IN (3,4) ) ";
                }

                if (locSts == clsEnum.LocSts_Double.XNNX)
                {
                    sSQL += " AND LOC IN (SELECT Loc_DD FROM LOC_MST WHERE LocSts='X' AND Equ_RowNo IN (3,4) ) ";
                }

                sSQL += " ORDER BY BAY_Y,LVL_Z,ROW_X DESC";

                dtTmp = new DataTable();
                if (db.GetDataTable(sSQL, ref dtTmp, ref strEM) == DBResult.Success)
                {
                    sNewLoc = dtTmp.Rows[0]["Loc"].ToString();
                }
                else
                {
                    sNewLoc = "";
                }

                return sNewLoc;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return "";
            }
            finally
            {
                dtTmp = null;
            }
        }
    }
}
