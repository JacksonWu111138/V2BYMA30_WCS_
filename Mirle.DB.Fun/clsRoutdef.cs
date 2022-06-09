using Mirle.DataBase;
using Mirle.Def;
using Mirle.MapController;
using Mirle.Structure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.DB.Fun
{
    public class clsRoutdef
    {
        private clsTool tool = new clsTool();
        private clsLocMst LocMst = new clsLocMst();
        private clsCmd_Mst Cmd_Mst = new clsCmd_Mst();
        public Location GetFinialDestination(CmdMstInfo cmd, MapHost Router, DataBase.DB db)
        {
            try
            {
                Location End = null; string sRemark = "";
                string Loc_End = cmd.Cmd_Mode == clsConstValue.CmdMode.L2L ? cmd.New_Loc : cmd.Loc;
                switch (cmd.Cmd_Mode)
                {
                    case clsConstValue.CmdMode.StockIn:
                        End = Router.GetLocation(tool.funGetEquNoByLoc(Loc_End).ToString(), Location.LocationID.Shelf.ToString());
                        break;
                    case clsConstValue.CmdMode.L2L:
                        bool IsTeach = false;
                        int iRet = LocMst.CheckIsTeach(tool.funGetEquNoByLoc(Loc_End).ToString(), Loc_End, ref IsTeach, db);
                        if (iRet == DBResult.Exception)
                        {
                            sRemark = $"Error: 確認是否是校正儲位失敗 => <Loc>{Loc_End}";
                            if (sRemark != cmd.Remark)
                            {
                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                            }

                            return null;
                        }

                        string sLocationID = IsTeach ? Location.LocationID.Teach.ToString() : Location.LocationID.Shelf.ToString();
                        End = Router.GetLocation(tool.funGetEquNoByLoc(Loc_End).ToString(), sLocationID);
                        break;
                    case clsConstValue.CmdMode.S2S:
                    case clsConstValue.CmdMode.StockOut:
                        
                        break;
                }

                return End;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return null;
            }
        }
    }
}
