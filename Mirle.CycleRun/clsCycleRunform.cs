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

                    if(textBox_L2LM801.Text.ToString().Substring(5,2) == "03" && Convert.ToInt32(textBox_L2LM801.Text.ToString().Substring(2, 3)) > 16)
                    {
                        cmdL2L_M801.Cmd_Mode = clsConstValue.CmdMode.StockOut;
                        cmdL2L_M801.Stn_No = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.L2L, "", textBox_L2LM801.Text.ToString());
                        cmdL2L_M801.Prty = "5";
                        cmdL2L_M801.New_Loc = "";
                    }
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

        private void button_PCBAGetStatus_Click(object sender, EventArgs e)
        {
            if(DB.Object.clsCycleRun.GetPCBAcycleRun())
            {
                /*
                string lastShelf = textBox_StockInAndOutStartLocation.Text.ToString();
                string lastColumn = ""; string lastRow = ""; string lastLevel = "";
                lastColumn = lastShelf.Substring(0, 2);
                lastRow = lastShelf.Substring(2, 3);
                lastLevel = lastShelf.Substring(5, 2);
                */
                MessageBox.Show($"PCBA is Cycle running.", "PCBA Cycle Run Status", MessageBoxButtons.OK);

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

        private void button_CycleGetNextLocation_Click(object sender, EventArgs e)
        {
            string next = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.L2L, "", textBox_testlocation.Text.ToString());
            label_testNextlocation.Text = "Next location = " + next;
        }

        private void button_BoxGetStatus_Click(object sender, EventArgs e)
        {
            if (DB.Object.clsCycleRun.GetBOXcycleRun())
            {
                /*
                string lastShelf = textBox_StockInAndOutStartLocation.Text.ToString();
                string lastColumn = ""; string lastRow = ""; string lastLevel = "";
                lastColumn = lastShelf.Substring(0, 2);
                lastRow = lastShelf.Substring(2, 3);
                lastLevel = lastShelf.Substring(5, 2);
                */
                MessageBox.Show($"B800 is Cycle running.", "B800 Cycle Run Status", MessageBoxButtons.OK);

            }
            else
            {
                MessageBox.Show("B800 isn't Cycle running.", "B800 Cycle Run Status", MessageBoxButtons.OK);
            }
        }

        private void button_BOXStart_Click(object sender, EventArgs e)
        {
            try
            {
                DB.Object.clsCycleRun.ChangeBOXCycleRun(true);
                CmdMstInfo cmdStock_B801_left = new CmdMstInfo();
                CmdMstInfo cmdStock_B801_right = new CmdMstInfo();
                CmdMstInfo cmdStock_B802_left = new CmdMstInfo();
                CmdMstInfo cmdStock_B802_right = new CmdMstInfo();
                CmdMstInfo cmdStock_B803_left = new CmdMstInfo();
                CmdMstInfo cmdStock_B803_right = new CmdMstInfo();
                CmdMstInfo cmdL2L_B801 = new CmdMstInfo();
                CmdMstInfo cmdL2L_B802 = new CmdMstInfo();
                CmdMstInfo cmdL2L_B803 = new CmdMstInfo();

                cmdStock_B801_left.Cmd_Sno = "";
                cmdStock_B801_right.Cmd_Sno = "";
                cmdStock_B802_left.Cmd_Sno = "";
                cmdStock_B802_right.Cmd_Sno = "";
                cmdStock_B803_left.Cmd_Sno = "";
                cmdStock_B803_right.Cmd_Sno = "";
                cmdL2L_B801.Cmd_Sno = "";
                cmdL2L_B802.Cmd_Sno = "";
                cmdL2L_B803.Cmd_Sno = "";

                //位置0 = 去left撿料口；位置1 = 去right撿料口
                string[] B801_carrier = textBox_B801StockInAndOut_carrierId.Text.ToString().Split(',');
                string[] B801_location = textBox_B801StockInAndOut_location.Text.ToString().Split(',');
                string[] B802_carrier = textBox_B802StockInAndOut_carrierId.Text.ToString().Split(',');
                string[] B802_location = textBox_B802StockInAndOut_location.Text.ToString().Split(',');
                string[] B803_carrier = textBox_B803StockInAndOut_carrierId.Text.ToString().Split(',');
                string[] B803_location = textBox_B803StockInAndOut_location.Text.ToString().Split(',');

                #region B801_left出入庫cycle命令
                if (!string.IsNullOrEmpty(B801_carrier[0]) && !string.IsNullOrEmpty(B801_location[0]))
                {
                    cmdStock_B801_left.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                    if (string.IsNullOrWhiteSpace(cmdStock_B801_left.Cmd_Sno))
                    {
                        throw new Exception($"箱式倉 B801Left Cycle Run 取得序號失敗！");
                    }

                    cmdStock_B801_left.BoxID = B801_carrier[0];
                    cmdStock_B801_left.Cmd_Mode = clsConstValue.CmdMode.StockOut;
                    cmdStock_B801_left.CurDeviceID = "";
                    cmdStock_B801_left.CurLoc = "";
                    cmdStock_B801_left.End_Date = "";
                    cmdStock_B801_left.Loc = B801_location[0];
                    cmdStock_B801_left.Equ_No = tool.funGetEquNoByLoc(B801_location[0]).ToString();
                    cmdStock_B801_left.EXP_Date = "";
                    cmdStock_B801_left.JobID = "CYCLERUN_StockIn&Out_B801Left";
                    cmdStock_B801_left.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();
                    cmdStock_B801_left.New_Loc = "";
                    cmdStock_B801_left.Prty = "5";
                    cmdStock_B801_left.Remark = "";

                    cmdStock_B801_left.Stn_No = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.StockIn, "", B801_location[0]);

                    if (string.IsNullOrEmpty(cmdStock_B801_left.Stn_No))
                    {
                        throw new Exception($"箱式倉 B801Left Cycle Run 未找到出庫目的站口！");
                    }

                    cmdStock_B801_left.Host_Name = "WCS";
                    cmdStock_B801_left.Zone_ID = "";

                    cmdStock_B801_left.carrierType = "MAG";
                    cmdStock_B801_left.lotSize = "";
                }
                #endregion B801_left出入庫cycle命令

                #region B801_right出入庫cycle命令
                if (B801_carrier.Length > 1 && B801_location.Length > 1 && !string.IsNullOrEmpty(B801_carrier[1]) && !string.IsNullOrEmpty(B801_location[1]))
                {
                    cmdStock_B801_right.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                    if (string.IsNullOrWhiteSpace(cmdStock_B801_right.Cmd_Sno))
                    {
                        throw new Exception($"箱式倉 B801Right Cycle Run 取得序號失敗！");
                    }

                    cmdStock_B801_right.BoxID = B801_carrier[1];
                    cmdStock_B801_right.Cmd_Mode = clsConstValue.CmdMode.StockOut;
                    cmdStock_B801_right.CurDeviceID = "";
                    cmdStock_B801_right.CurLoc = "";
                    cmdStock_B801_right.End_Date = "";
                    cmdStock_B801_right.Loc = B801_location[1];
                    cmdStock_B801_right.Equ_No = tool.funGetEquNoByLoc(B801_location[1]).ToString();
                    cmdStock_B801_right.EXP_Date = "";
                    cmdStock_B801_right.JobID = "CYCLERUN_StockIn&Out_B801Right";
                    cmdStock_B801_right.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();
                    cmdStock_B801_right.New_Loc = "";
                    cmdStock_B801_right.Prty = "5";
                    cmdStock_B801_right.Remark = "";

                    cmdStock_B801_right.Stn_No = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.StockIn, "", B801_location[1]);

                    if (string.IsNullOrEmpty(cmdStock_B801_right.Stn_No))
                    {
                        throw new Exception($"箱式倉 B801Right Cycle Run 未找到出庫目的站口！");
                    }

                    cmdStock_B801_right.Host_Name = "WCS";
                    cmdStock_B801_right.Zone_ID = "";

                    cmdStock_B801_right.carrierType = "MAG";
                    cmdStock_B801_right.lotSize = "";
                }
                #endregion B801_right出入庫cycle命令

                #region B802_left出入庫cycle命令
                if (!string.IsNullOrEmpty(B802_carrier[0]) && !string.IsNullOrEmpty(B802_location[0]))
                {
                    cmdStock_B802_left.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                    if (string.IsNullOrWhiteSpace(cmdStock_B802_left.Cmd_Sno))
                    {
                        throw new Exception($"箱式倉 B802Left Cycle Run 取得序號失敗！");
                    }

                    cmdStock_B802_left.BoxID = B802_carrier[0];
                    cmdStock_B802_left.Cmd_Mode = clsConstValue.CmdMode.StockOut;
                    cmdStock_B802_left.CurDeviceID = "";
                    cmdStock_B802_left.CurLoc = "";
                    cmdStock_B802_left.End_Date = "";
                    cmdStock_B802_left.Loc = B802_location[0];
                    cmdStock_B802_left.Equ_No = tool.funGetEquNoByLoc(B802_location[0]).ToString();
                    cmdStock_B802_left.EXP_Date = "";
                    cmdStock_B802_left.JobID = "CYCLERUN_StockIn&Out_B802Left";
                    cmdStock_B802_left.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();
                    cmdStock_B802_left.New_Loc = "";
                    cmdStock_B802_left.Prty = "5";
                    cmdStock_B802_left.Remark = "";

                    cmdStock_B802_left.Stn_No = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.StockIn, "", B802_location[0]);

                    if (string.IsNullOrEmpty(cmdStock_B802_left.Stn_No))
                    {
                        throw new Exception($"箱式倉 B802Left Cycle Run 未找到出庫目的站口！");
                    }

                    cmdStock_B802_left.Host_Name = "WCS";
                    cmdStock_B802_left.Zone_ID = "";

                    cmdStock_B802_left.carrierType = "MAG";
                    cmdStock_B802_left.lotSize = "";
                }
                #endregion B802_left出入庫cycle命令

                #region B802_right出入庫cycle命令
                if (B802_carrier.Length > 1 && B802_location.Length > 1 && !string.IsNullOrEmpty(B802_carrier[1]) && !string.IsNullOrEmpty(B802_location[1]))
                {
                    cmdStock_B802_right.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                    if (string.IsNullOrWhiteSpace(cmdStock_B802_right.Cmd_Sno))
                    {
                        throw new Exception($"箱式倉 B802Right Cycle Run 取得序號失敗！");
                    }

                    cmdStock_B802_right.BoxID = B802_carrier[1];
                    cmdStock_B802_right.Cmd_Mode = clsConstValue.CmdMode.StockOut;
                    cmdStock_B802_right.CurDeviceID = "";
                    cmdStock_B802_right.CurLoc = "";
                    cmdStock_B802_right.End_Date = "";
                    cmdStock_B802_right.Loc = B802_location[1];
                    cmdStock_B802_right.Equ_No = tool.funGetEquNoByLoc(B802_location[1]).ToString();
                    cmdStock_B802_right.EXP_Date = "";
                    cmdStock_B802_right.JobID = "CYCLERUN_StockIn&Out_B802Right";
                    cmdStock_B802_right.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();
                    cmdStock_B802_right.New_Loc = "";
                    cmdStock_B802_right.Prty = "5";
                    cmdStock_B802_right.Remark = "";

                    cmdStock_B802_right.Stn_No = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.StockIn, "", B802_location[1]);

                    if (string.IsNullOrEmpty(cmdStock_B802_right.Stn_No))
                    {
                        throw new Exception($"箱式倉 B802Right Cycle Run 未找到出庫目的站口！");
                    }

                    cmdStock_B802_right.Host_Name = "WCS";
                    cmdStock_B802_right.Zone_ID = "";

                    cmdStock_B802_right.carrierType = "MAG";
                    cmdStock_B802_right.lotSize = "";
                }
                #endregion B802_right出入庫cycle命令

                #region B803_left出入庫cycle命令
                if (!string.IsNullOrEmpty(B803_carrier[0]) && !string.IsNullOrEmpty(B803_location[0]))
                {
                    cmdStock_B803_left.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                    if (string.IsNullOrWhiteSpace(cmdStock_B803_left.Cmd_Sno))
                    {
                        throw new Exception($"箱式倉 B803Left Cycle Run 取得序號失敗！");
                    }

                    cmdStock_B803_left.BoxID = B803_carrier[0];
                    cmdStock_B803_left.Cmd_Mode = clsConstValue.CmdMode.StockOut;
                    cmdStock_B803_left.CurDeviceID = "";
                    cmdStock_B803_left.CurLoc = "";
                    cmdStock_B803_left.End_Date = "";
                    cmdStock_B803_left.Loc = B803_location[0];
                    cmdStock_B803_left.Equ_No = tool.funGetEquNoByLoc(B803_location[0]).ToString();
                    cmdStock_B803_left.EXP_Date = "";
                    cmdStock_B803_left.JobID = "CYCLERUN_StockIn&Out_B803Left";
                    cmdStock_B803_left.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();
                    cmdStock_B803_left.New_Loc = "";
                    cmdStock_B803_left.Prty = "5";
                    cmdStock_B803_left.Remark = "";

                    cmdStock_B803_left.Stn_No = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.StockIn, "", B803_location[0]);

                    if (string.IsNullOrEmpty(cmdStock_B803_left.Stn_No))
                    {
                        throw new Exception($"箱式倉 B803Left Cycle Run 未找到出庫目的站口！");
                    }

                    cmdStock_B803_left.Host_Name = "WCS";
                    cmdStock_B803_left.Zone_ID = "";

                    cmdStock_B803_left.carrierType = "MAG";
                    cmdStock_B803_left.lotSize = "";
                }
                #endregion B803_left出入庫cycle命令

                #region B803_right出入庫cycle命令
                if (B803_carrier.Length > 1 && B803_location.Length > 1 && !string.IsNullOrEmpty(B803_carrier[1]) && !string.IsNullOrEmpty(B803_location[1]))
                {
                    cmdStock_B803_right.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                    if (string.IsNullOrWhiteSpace(cmdStock_B803_right.Cmd_Sno))
                    {
                        throw new Exception($"箱式倉 B803Right Cycle Run 取得序號失敗！");
                    }

                    cmdStock_B803_right.BoxID = B803_carrier[1];
                    cmdStock_B803_right.Cmd_Mode = clsConstValue.CmdMode.StockOut;
                    cmdStock_B803_right.CurDeviceID = "";
                    cmdStock_B803_right.CurLoc = "";
                    cmdStock_B803_right.End_Date = "";
                    cmdStock_B803_right.Loc = B803_location[1];
                    cmdStock_B803_right.Equ_No = tool.funGetEquNoByLoc(B803_location[1]).ToString();
                    cmdStock_B803_right.EXP_Date = "";
                    cmdStock_B803_right.JobID = "CYCLERUN_StockIn&Out_B803Right";
                    cmdStock_B803_right.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();
                    cmdStock_B803_right.New_Loc = "";
                    cmdStock_B803_right.Prty = "5";
                    cmdStock_B803_right.Remark = "";

                    cmdStock_B803_right.Stn_No = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.StockIn, "", B803_location[1]);

                    if (string.IsNullOrEmpty(cmdStock_B803_right.Stn_No))
                    {
                        throw new Exception($"箱式倉 B803Right Cycle Run 未找到出庫目的站口！");
                    }

                    cmdStock_B803_right.Host_Name = "WCS";
                    cmdStock_B803_right.Zone_ID = "";

                    cmdStock_B803_right.carrierType = "MAG";
                    cmdStock_B803_right.lotSize = "";
                }
                #endregion B803_right出入庫cycle命令

                #region B801庫對庫cycle命令
                if (textBox_L2LB801.Text.ToString() != "")
                {
                    cmdL2L_B801.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                    if (string.IsNullOrWhiteSpace(cmdL2L_B801.Cmd_Sno))
                    {
                        throw new Exception($"箱式倉 B801 Cycle Run 取得序號失敗！");
                    }

                    if (string.IsNullOrEmpty(textBox_L2LB801.Text.ToString()) || tool.funGetEquNoByLoc(textBox_L2LB801.Text.ToString()) != 3)
                    {
                        throw new Exception($"箱式倉 B801 Cycle Run 未輸入正確起點！");
                    }

                    cmdL2L_B801.BoxID = "B801CycleL2L";
                    cmdL2L_B801.Cmd_Mode = clsConstValue.CmdMode.L2L;
                    cmdL2L_B801.CurDeviceID = "";
                    cmdL2L_B801.CurLoc = "";
                    cmdL2L_B801.End_Date = "";
                    cmdL2L_B801.Loc = textBox_L2LB801.Text.ToString();
                    cmdL2L_B801.Equ_No = tool.funGetEquNoByLoc(textBox_L2LB801.Text.ToString()).ToString();
                    cmdL2L_B801.EXP_Date = "";
                    cmdL2L_B801.JobID = "CYCLERUN_B801L2L";
                    cmdL2L_B801.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();

                    cmdL2L_B801.New_Loc = tool.FunGetB800L2LCycleRunInitialDestiantion(textBox_L2LB801.Text.ToString());
                    if (string.IsNullOrEmpty(cmdL2L_B801.New_Loc))
                        throw new Exception("箱式倉 B801 Cycle Run 未取得正確庫對庫目的地");
                    else if (cmdL2L_B801.New_Loc.Contains("Error"))
                        throw new Exception(cmdL2L_B801.New_Loc);

                    cmdL2L_B801.Prty = "9";
                    cmdL2L_B801.Remark = "";
                    cmdL2L_B801.Stn_No = "";
                    cmdL2L_B801.Host_Name = "WCS";
                    cmdL2L_B801.Zone_ID = "";
                    cmdL2L_B801.carrierType = "MAG";
                    cmdL2L_B801.lotSize = "";
                }

                #endregion B801庫對庫cycle命令

                #region B802庫對庫cycle命令
                if (textBox_L2LB802.Text.ToString() != "")
                {
                    cmdL2L_B802.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                    if (string.IsNullOrWhiteSpace(cmdL2L_B802.Cmd_Sno))
                    {
                        throw new Exception($"箱式倉 B802 Cycle Run 取得序號失敗！");
                    }

                    if (string.IsNullOrEmpty(textBox_L2LB802.Text.ToString()) || tool.funGetEquNoByLoc(textBox_L2LB802.Text.ToString()) != 4)
                    {
                        throw new Exception($"箱式倉 B802 Cycle Run 未輸入正確起點！");
                    }

                    cmdL2L_B802.BoxID = "B802CycleL2L";
                    cmdL2L_B802.Cmd_Mode = clsConstValue.CmdMode.L2L;
                    cmdL2L_B802.CurDeviceID = "";
                    cmdL2L_B802.CurLoc = "";
                    cmdL2L_B802.End_Date = "";
                    cmdL2L_B802.Loc = textBox_L2LB802.Text.ToString();
                    cmdL2L_B802.Equ_No = tool.funGetEquNoByLoc(textBox_L2LB802.Text.ToString()).ToString();
                    cmdL2L_B802.EXP_Date = "";
                    cmdL2L_B802.JobID = "CYCLERUN_B802L2L";
                    cmdL2L_B802.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();

                    cmdL2L_B802.New_Loc = tool.FunGetB800L2LCycleRunInitialDestiantion(textBox_L2LB802.Text.ToString());
                    if (string.IsNullOrEmpty(cmdL2L_B802.New_Loc))
                        throw new Exception("箱式倉 B802 Cycle Run 未取得正確庫對庫目的地");
                    else if (cmdL2L_B802.New_Loc.Contains("Error"))
                        throw new Exception(cmdL2L_B802.New_Loc);

                    cmdL2L_B802.Prty = "9";
                    cmdL2L_B802.Remark = "";
                    cmdL2L_B802.Stn_No = "";
                    cmdL2L_B802.Host_Name = "WCS";
                    cmdL2L_B802.Zone_ID = "";
                    cmdL2L_B802.carrierType = "MAG";
                    cmdL2L_B802.lotSize = "";
                }

                #endregion B802庫對庫cycle命令

                #region B803庫對庫cycle命令
                if (textBox_L2LB803.Text.ToString() != "")
                {
                    cmdL2L_B803.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                    if (string.IsNullOrWhiteSpace(cmdL2L_B803.Cmd_Sno))
                    {
                        throw new Exception($"PCBA B803 Cycle Run 取得序號失敗！");
                    }

                    if (string.IsNullOrEmpty(textBox_L2LB803.Text.ToString()) || tool.funGetEquNoByLoc(textBox_L2LB803.Text.ToString()) != 5)
                    {
                        throw new Exception($"箱式倉 B803 Cycle Run 未輸入正確起點！");
                    }

                    cmdL2L_B803.BoxID = "B803CycleL2L";
                    cmdL2L_B803.Cmd_Mode = clsConstValue.CmdMode.L2L;
                    cmdL2L_B803.CurDeviceID = "";
                    cmdL2L_B803.CurLoc = "";
                    cmdL2L_B803.End_Date = "";
                    cmdL2L_B803.Loc = textBox_L2LB803.Text.ToString();
                    cmdL2L_B803.Equ_No = tool.funGetEquNoByLoc(textBox_L2LB803.Text.ToString()).ToString();
                    cmdL2L_B803.EXP_Date = "";
                    cmdL2L_B803.JobID = "CYCLERUN_B803L2L";
                    cmdL2L_B803.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();

                    cmdL2L_B803.New_Loc = tool.FunGetB800L2LCycleRunInitialDestiantion(textBox_L2LB803.Text.ToString());
                    if (string.IsNullOrEmpty(cmdL2L_B803.New_Loc))
                        throw new Exception("箱式倉 B803 Cycle Run 未取得正確庫對庫目的地");
                    else if (cmdL2L_B803.New_Loc.Contains("Error"))
                        throw new Exception(cmdL2L_B803.New_Loc);

                    cmdL2L_B803.Prty = "9";
                    cmdL2L_B803.Remark = "";
                    cmdL2L_B803.Stn_No = "";
                    cmdL2L_B803.Host_Name = "WCS";
                    cmdL2L_B803.Zone_ID = "";
                    cmdL2L_B803.carrierType = "MAG";
                    cmdL2L_B803.lotSize = "";
                }

                #endregion B803庫對庫cycle命令

                string sRemark = "";
                if (!clsDB_Proc.GetDB_Object().GetProc().FunBoxCycleRunInitial(cmdStock_B801_left, cmdStock_B801_right, cmdStock_B802_left, cmdStock_B802_right,
                    cmdStock_B803_left, cmdStock_B803_right, cmdL2L_B801, cmdL2L_B802, cmdL2L_B803, ref sRemark))
                    throw new Exception(sRemark);
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                MessageBox.Show(ex.Message, "箱式倉 Cycle Run", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button_BOXCycleEnd_Click(object sender, EventArgs e)
        {
            DB.Object.clsCycleRun.ChangeBOXCycleRun(false);
        }
    }
}
