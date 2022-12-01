using Mirle.DB.Fun.Parameter;
using Mirle.DB.Object;
using Mirle.Def.U2NMMA30;
using Mirle.Def;
using Mirle.Structure;
using Mirle.Structure.Info;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Mirle.Structure.Info.VIDEnums;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Mirle.CycleRun
{
    public partial class clsCycleRunform : Form
    {
        private DB.Fun.clsTool tool = new DB.Fun.clsTool();
        public clsCycleRunform()
        {
            InitializeComponent();
        }

        private void button_PCBAStart_Click(object sender, EventArgs e)
        {
            try
            {

                DB.Object.clsCycleRun.ChangePCBACycleRun(true);
                CmdMstInfo cmdStock = new CmdMstInfo();
                CmdMstInfo cmdL2L_M801 = new CmdMstInfo();
                CmdMstInfo cmdL2L_M802 = new CmdMstInfo();

                cmdStock.Cmd_Sno = "";
                cmdL2L_M801.Cmd_Sno = "";
                cmdL2L_M802.Cmd_Sno = "";
                
                #region 出入庫cycle命令
                if(textBox_StockInAndOutCarrierId.Text.ToString() != "" && textBox_StockInAndOutStartLocation.Text.ToString() != "")
                {
                    cmdStock.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                    if (string.IsNullOrWhiteSpace(cmdStock.Cmd_Sno))
                    {
                        throw new Exception($"PCBA Cycle Run 取得序號失敗！");
                    }

                    if (string.IsNullOrEmpty(textBox_StockInAndOutCarrierId.Text.ToString()))
                    {
                        throw new Exception($"PCBA Cycle Run 未輸入入出庫之使用 Magazine ID！");
                    }

                    cmdStock.BoxID = textBox_StockInAndOutCarrierId.Text.ToString();
                    cmdStock.Cmd_Mode = clsConstValue.CmdMode.StockOut;
                    cmdStock.CurDeviceID = "";
                    cmdStock.CurLoc = "";
                    cmdStock.End_Date = "";
                    cmdStock.Loc = textBox_StockInAndOutStartLocation.Text.ToString();
                    cmdStock.Equ_No = tool.funGetEquNoByLoc(textBox_StockInAndOutStartLocation.Text.ToString()).ToString();
                    cmdStock.EXP_Date = "";
                    cmdStock.JobID = "CYCLERUN_StockIn&Out";
                    cmdStock.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();
                    cmdStock.New_Loc = "";
                    cmdStock.Prty = "5";
                    cmdStock.Remark = "";

                    if (cmdStock.Equ_No == "1")
                        cmdStock.Stn_No = ConveyorDef.GetBuffer_ByStnNo("M800-2").BufferName;
                    else if (cmdStock.Equ_No == "2")
                        cmdStock.Stn_No = ConveyorDef.GetBuffer_ByStnNo("M800-1").BufferName;

                    if (string.IsNullOrEmpty(cmdStock.Stn_No))
                    {
                        throw new Exception($"PCBA Cycle Run 未找到出庫目的站口！");
                    }

                    cmdStock.Host_Name = "WCS";
                    cmdStock.Zone_ID = "";

                    cmdStock.carrierType = "MAG";
                    cmdStock.lotSize = "";
                }
                
                #endregion 出入庫cycle命令

                #region M801庫對庫cycle命令
                if(textBox_L2LM801.Text.ToString() != "")
                {
                    cmdL2L_M801.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                    if (string.IsNullOrWhiteSpace(cmdL2L_M801.Cmd_Sno))
                    {
                        throw new Exception($"PCBA M801 Cycle Run 取得序號失敗！");
                    }

                    if (string.IsNullOrEmpty(textBox_L2LM801.Text.ToString()) || tool.funGetEquNoByLoc(textBox_L2LM801.Text.ToString()) != 1)
                    {
                        throw new Exception($"PCBA M801 Cycle Run 未輸入正確起點！");
                    }

                    cmdL2L_M801.BoxID = "M801CycleL2L";
                    cmdL2L_M801.Cmd_Mode = clsConstValue.CmdMode.L2L;
                    cmdL2L_M801.CurDeviceID = "";
                    cmdL2L_M801.CurLoc = "";
                    cmdL2L_M801.End_Date = "";
                    cmdL2L_M801.Loc = textBox_L2LM801.Text.ToString();
                    cmdL2L_M801.Equ_No = tool.funGetEquNoByLoc(textBox_L2LM801.Text.ToString()).ToString();
                    cmdL2L_M801.EXP_Date = "";
                    cmdL2L_M801.JobID = "CYCLERUN_M801L2L";
                    cmdL2L_M801.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();

                    cmdL2L_M801.New_Loc = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.L2L, "", textBox_L2LM801.Text.ToString());
                    if (string.IsNullOrEmpty(cmdL2L_M801.New_Loc))
                        throw new Exception("PCBA M801 Cycle Run 未取得正確庫對庫目的地");
                    else if (cmdL2L_M801.New_Loc.Contains("Error"))
                        throw new Exception(cmdL2L_M801.New_Loc);

                    cmdL2L_M801.Prty = "9";
                    cmdL2L_M801.Remark = "";
                    cmdL2L_M801.Stn_No = "";
                    cmdL2L_M801.Host_Name = "WCS";
                    cmdL2L_M801.Zone_ID = "";
                    cmdL2L_M801.carrierType = "MAG";
                    cmdL2L_M801.lotSize = "";
                }
                
                #endregion M801庫對庫cycle命令

                #region M802庫對庫cycle命令
                if(textBox_L2LM802.Text.ToString() != "")
                {
                    cmdL2L_M802.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                    if (string.IsNullOrWhiteSpace(cmdL2L_M802.Cmd_Sno))
                    {
                        throw new Exception($"PCBA M802 Cycle Run 取得序號失敗！");
                    }

                    if (string.IsNullOrEmpty(textBox_L2LM802.Text.ToString()) || tool.funGetEquNoByLoc(textBox_L2LM802.Text.ToString()) != 2)
                    {
                        throw new Exception($"PCBA M802 Cycle Run 未輸入正確起點！");
                    }

                    cmdL2L_M802.BoxID = "M802CycleL2L";
                    cmdL2L_M802.Cmd_Mode = clsConstValue.CmdMode.L2L;
                    cmdL2L_M802.CurDeviceID = "";
                    cmdL2L_M802.CurLoc = "";
                    cmdL2L_M802.End_Date = "";
                    cmdL2L_M802.Loc = textBox_L2LM802.Text.ToString();
                    cmdL2L_M802.Equ_No = tool.funGetEquNoByLoc(textBox_L2LM802.Text.ToString()).ToString();
                    cmdL2L_M802.EXP_Date = "";
                    cmdL2L_M802.JobID = "CYCLERUN_M802L2L";
                    cmdL2L_M802.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();

                    cmdL2L_M802.New_Loc = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.L2L, "", textBox_L2LM802.Text.ToString());
                    if (string.IsNullOrEmpty(cmdL2L_M802.New_Loc))
                        throw new Exception("PCBA M801 Cycle Run 未取得正確庫對庫目的地");
                    else if (cmdL2L_M802.New_Loc.Contains("Error"))
                        throw new Exception(cmdL2L_M802.New_Loc);

                    cmdL2L_M802.Prty = "9";
                    cmdL2L_M802.Remark = "";
                    cmdL2L_M802.Stn_No = "";
                    cmdL2L_M802.Host_Name = "WCS";
                    cmdL2L_M802.Zone_ID = "";
                    cmdL2L_M802.carrierType = "MAG";
                    cmdL2L_M802.lotSize = "";
                }
                
                #endregion M802庫對庫cycle命令

                string sRemark = "";
                if(!clsDB_Proc.GetDB_Object().GetProc().FunPCBACycleRunInitial(cmdStock, cmdL2L_M801, cmdL2L_M802, ref sRemark))
                    throw new Exception(sRemark);
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                MessageBox.Show(ex.Message, "PCBA Cycle Run", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(DB.Object.clsCycleRun.GetPCBAcycleRun())
            {
                string lastShelf = textBox_StockInAndOutStartLocation.Text.ToString();
                string lastColumn = ""; string lastRow = ""; string lastLevel = "";
                lastColumn = lastShelf.Substring(0, 2);
                lastRow = lastShelf.Substring(2, 3);
                lastLevel = lastShelf.Substring(5, 2);
                MessageBox.Show($"PCBA is Cycle running.\t\nColumn = {lastColumn}, Row = {lastRow}, Level = {lastLevel}.", "PCBA Cycle Run Status", MessageBoxButtons.OK);

            }
            else
            {
                MessageBox.Show("PCBA isn't Cycle running.", "PCBA Cycle Run Status", MessageBoxButtons.OK);
            }
        }

        private void button_PCBACycleEnd_Click(object sender, EventArgs e)
        {
            DB.Object.clsCycleRun.ChangePCBACycleRun(false);
        }

        private void label_L2LM801_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string next = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.L2L, "", textBox_testlocation.Text.ToString());
            label_testNextlocation.Text = "Next location = " + next;
        }
    }
}
