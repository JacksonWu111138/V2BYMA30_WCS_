using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Threading.Tasks;
using Mirle.Grid.U2NMMA30;
using System.Collections.Generic;
using Mirle.Def;
using Mirle.Def.U2NMMA30;
using Mirle.Structure.Info;
using Mirle.DB.Object;
using Mirle.DataBase;
using Mirle.WebAPI.Event.U2NMMA30;
using Unity;
using Mirle.Logger;
using Mirle.WebAPI.U2NMMA30.View;
using Mirle.ASRS.Close.Program;
using System.Threading;
using Mirle.MapController;
using Mirle.Structure;
using Mirle.WebAPI.U2NMMA30.ReportInfo;
using Mirle.ASRS.DBCommand;
using Mirle.Middle;
using Mirle.EccsSignal;

namespace Mirle.ASRS.WCS.View
{
    public partial class MainForm : Form
    {
        private DB.ClearCmd.Proc.clsHost clearCmd;
        private WebApiHost _webApiHost;
        private UnityContainer _unityContainer;
        private clsGetCVLocation CVLocation;
        private static System.Timers.Timer timRead = new System.Timers.Timer();
        private MapHost router;
        public static DeviceInfo[] PCBA = new DeviceInfo[2];
        public static DeviceInfo[] Box = new DeviceInfo[3];
        private ASRSProcess[] AsrsCommand = new ASRSProcess[5];
        private SignalHost[] CraneSignals = new SignalHost[5];
        private MidHost middle = new MidHost();
        public MainForm()
        {
            InitializeComponent();

            timRead.Elapsed += new System.Timers.ElapsedEventHandler(timRead_Elapsed);
            timRead.Enabled = false; timRead.Interval = 500;
        }

        #region Event
        private void MainForm_Load(object sender, EventArgs e)
        {
            ChkAppIsAlreadyRunning();
            this.Text = this.Text + "  v " + ProductVersion;
            CraneBuffer_Initial();
            clInitSys.FunLoadIniSys();
            FunInit();
            FunEventInit();
            GridInit();

            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, "WCS程式已開啟");
            timRead.Enabled = true;
            timer1.Enabled = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmCloseProgram objCloseProgram;
            try
            {
                e.Cancel = true;

                objCloseProgram = new frmCloseProgram();

                if (objCloseProgram.ShowDialog() == DialogResult.OK)
                {
                    chkOnline.Checked = false;
                    SpinWait.SpinUntil(() => false, 1000);
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, "WCS程式已關閉！");
                    throw new Exception();
                }
            }
            catch
            {
                Environment.Exit(0);
            }
            finally
            {
                objCloseProgram = null;
            }
        }

        private void MainForm_OnNeedShelfToShelfEvent(object sender, DB.Fun.Events.NeedShelfToShelfArgs e)
        {
            //通知WMS產生庫對庫

        }

        private MainTestForm mainTest;
        private void button1_Click(object sender, EventArgs e)
        {
            if (mainTest == null)
            {
                mainTest = new MainTestForm(clInitSys.WmsApi_Config);
                mainTest.TopMost = true;
                mainTest.FormClosed += new FormClosedEventHandler(funMainTest_FormClosed);
                mainTest.Show();
            }
            else
            {
                mainTest.BringToFront();
            }
        }

        private void funMainTest_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mainTest != null)
                mainTest = null;
        }

        private void chkOnline_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOnline.Checked)
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, "WCS OnLine.");
            else
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, "WCS OffLine.");
        }

        private void chkCycleRun_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCycleRun.Checked)
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, "周邊CycleRun啟動！");
            else
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, "周邊CycleRun關閉！");
        }

        private void chkIgnoreTkt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIgnoreTkt.Checked)
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, "啟動IgnoreTicket功能！");
            else
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, "關閉IgnoreTicket功能！");
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            //Ctrl + L
            if (e.KeyCode == Keys.L && e.Modifiers == Keys.Control)
            {
                Def.clsTool.FunVisbleChange(ref button1);
                Def.clsTool.FunVisbleChange(ref btnTeachMaintain);
                Def.clsTool.FunVisbleChange(ref chkCycleRun);
                Def.clsTool.FunVisbleChange(ref chkIgnoreTkt);
            }
        }
        #endregion Event
        #region Timer
        private void timRead_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            timRead.Enabled = false;
            try
            {
                //SubShowCmdtoGrid(ref Grid1);
                if(clsDB_Proc.DBConn)
                {

                }
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
            }
            finally
            {
                timRead.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            try
            {
                lblDBConn_WCS.BackColor = clsDB_Proc.DBConn ? Color.Blue : Color.Red;
                lblTimer.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
            }
            finally
            {
                timer1.Enabled = true;
            }
        } 
        #endregion Timer

        private void CraneBuffer_Initial()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int f = 1; f <= 2; f++)
                {
                    if (i < 2 && f == 2) continue;

                    FloorInfo floor = new FloorInfo();
                    floor.Group_IN = new List<ConveyorInfo>();
                    floor.Group_OUT = new List<ConveyorInfo>();
                    switch(i)
                    {
                        #region PCBA
                        case 0:
                            floor.Group_IN.Add(ConveyorDef.M1_06);
                            floor.Group_OUT.Add(ConveyorDef.M1_01);

                            PCBA[i] = new DeviceInfo();
                            PCBA[i].Floors = new List<FloorInfo>();
                            PCBA[i].Floors.Add(floor);
                            break;
                        case 1:
                            floor.Group_IN.Add(ConveyorDef.M1_16);
                            floor.Group_OUT.Add(ConveyorDef.M1_11);

                            PCBA[i] = new DeviceInfo();
                            PCBA[i].Floors = new List<FloorInfo>();
                            PCBA[i].Floors.Add(floor);
                            break;
                        #endregion PCBA
                        #region 箱式倉
                        case 2:
                            if (f == 1)
                            {
                                floor.Group_IN.Add(ConveyorDef.B1_007);
                                floor.Group_IN.Add(ConveyorDef.B1_010);
                                floor.Group_OUT.Add(ConveyorDef.B1_001);
                                floor.Group_OUT.Add(ConveyorDef.B1_004);
                            }
                            else
                            {
                                floor.Group_IN.Add(ConveyorDef.B1_087);
                                floor.Group_IN.Add(ConveyorDef.B1_090);
                                floor.Group_OUT.Add(ConveyorDef.B1_081);
                                floor.Group_OUT.Add(ConveyorDef.B1_084);
                            }
                            Box[i - 2] = new DeviceInfo();
                            Box[i - 2].Floors = new List<FloorInfo>();
                            Box[i - 2].Floors.Add(floor);
                            break;
                        case 3:
                            if (f == 1)
                            {
                                floor.Group_IN.Add(ConveyorDef.B1_019);
                                floor.Group_IN.Add(ConveyorDef.B1_022);
                                floor.Group_OUT.Add(ConveyorDef.B1_013);
                                floor.Group_OUT.Add(ConveyorDef.B1_016);
                            }
                            else
                            {
                                floor.Group_IN.Add(ConveyorDef.B1_099);
                                floor.Group_IN.Add(ConveyorDef.B1_102);
                                floor.Group_OUT.Add(ConveyorDef.B1_093);
                                floor.Group_OUT.Add(ConveyorDef.B1_096);
                            }
                            Box[i - 2] = new DeviceInfo();
                            Box[i - 2].Floors = new List<FloorInfo>();
                            Box[i - 2].Floors.Add(floor);
                            break;
                        default:
                            if (f == 1)
                            {
                                floor.Group_IN.Add(ConveyorDef.B1_031);
                                floor.Group_IN.Add(ConveyorDef.B1_034);
                                floor.Group_OUT.Add(ConveyorDef.B1_025);
                                floor.Group_OUT.Add(ConveyorDef.B1_028);
                            }
                            else
                            {
                                floor.Group_IN.Add(ConveyorDef.B1_111);
                                floor.Group_IN.Add(ConveyorDef.B1_114);
                                floor.Group_OUT.Add(ConveyorDef.B1_105);
                                floor.Group_OUT.Add(ConveyorDef.B1_108);
                            }
                            Box[i - 2] = new DeviceInfo();
                            Box[i - 2].Floors = new List<FloorInfo>();
                            Box[i - 2].Floors.Add(floor);
                            break;
                            #endregion 箱式倉
                    }
                }
            }
        }

        private void FunInit()
        {
            var archive = new AutoArchive();
            archive.Start();
            clsDB_Proc.Initial(clInitSys.DbConfig, clInitSys.DbConfig_WMS);
            router = new MapHost(clInitSys.DbConfig);
            CVLocation = new clsGetCVLocation(router);
            ConveyorDef.FunStnListAddInit();
            FunAsrsCmdInit();
            //_unityContainer = new UnityContainer();
            //_unityContainer.RegisterInstance(new WCSController());
            //_webApiHost = new WebApiHost(new Startup(_unityContainer), clInitSys.WcsApi_Config.IP);
            //clearCmd = new DB.ClearCmd.Proc.clsHost();
        }

        private void FunEventInit()
        {
            clsDB_Proc.GetDB_Object().GetProc().GetFun_Routdef().OnNeedShelfToShelfEvent += MainForm_OnNeedShelfToShelfEvent;
        }

        private void FunAsrsCmdInit()
        {
            for (int i = 0; i < AsrsCommand.Length; i++)
            {
                clsPlcConfig plcConfig = new clsPlcConfig();
                plcConfig.CraneType = clsEnum.CmdType.CraneType.Single;
                plcConfig.ForkType = clsEnum.CmdType.ForkType.SingleFork;
                plcConfig.LocType = clsEnum.CmdType.LocType.DoubleDeep;

                if (i < 2)
                {
                    CraneSignals[i] = new SignalHost(clInitSys.DbConfig, PCBA[i].DeviceID);
                    plcConfig.CV_Type = clsEnum.CmdType.CV_Type.Single;
                    AsrsCommand[i] = new ASRSProcess(clInitSys.DbConfig, clInitSys.DbConfig_WMS, plcConfig, PCBA[i], router, middle, CraneSignals[i]);
                }
                else
                {
                    CraneSignals[i] = new SignalHost(clInitSys.DbConfig, Box[i - 2].DeviceID);
                    plcConfig.CV_Type = clsEnum.CmdType.CV_Type.Double;
                    AsrsCommand[i] = new ASRSProcess(clInitSys.DbConfig, clInitSys.DbConfig_WMS, plcConfig, Box[i - 2], router, middle, CraneSignals[i]);
                }
            }
        }

        #region Grid顯示
        private void GridInit()
        {
            Gird.clInitSys.GridSysInit(ref Grid1);
            ColumnDef.CMD_MST.GridSetLocRange(ref Grid1);
        }

        delegate void degShowCmdtoGrid(ref DataGridView oGrid);
        private void SubShowCmdtoGrid(ref DataGridView oGrid)
        {
            degShowCmdtoGrid obj;
            DataTable dtTmp = new DataTable();
            try
            {
                if (InvokeRequired)
                {
                    obj = new degShowCmdtoGrid(SubShowCmdtoGrid);
                    Invoke(obj, oGrid);
                }
                else
                {
                    int iRet = clsDB_Proc.GetDB_Object().GetCmd_Mst().FunGetCmdMst_Grid(ref dtTmp);
                    if (iRet == DBResult.Success)
                    {
                        if (oGrid.Columns.Count == 0)
                            return;

                        int intSelectRowIndex = (oGrid.SelectedRows.Count == 0 ? -1 : oGrid.SelectedRows[0].Index);
                        oGrid.SuspendLayout();
                        if (oGrid.Rows.Count > dtTmp.Rows.Count)
                        {
                            for (int intRow = oGrid.Rows.Count; intRow > dtTmp.Rows.Count; intRow--)
                                oGrid.Rows.Remove(oGrid.Rows[intRow - 1]);
                        }
                        else if (oGrid.Rows.Count < dtTmp.Rows.Count)
                        {
                            for (int intRow = oGrid.Rows.Count; intRow < dtTmp.Rows.Count; intRow++)
                            {
                                oGrid.Rows.Add();
                                oGrid.Rows[intRow].HeaderCell.Value = (intRow + 1).ToString();
                            }
                        }
                        else
                        {
                            for (int intRow = 0; intRow < oGrid.Rows.Count; intRow++)
                                oGrid.Rows[intRow].HeaderCell.Value = (intRow + 1).ToString();
                        }

                        string strField = string.Empty;
                        string strSortField = string.Empty;
                        SortOrder sortOrder = SortOrder.Ascending;
                        object sync1 = new object();
                        object sync2 = new object();

                        if (oGrid.SortedColumn != null)
                        {
                            strSortField = oGrid.SortedColumn.Name;
                            sortOrder = oGrid.SortOrder;
                            dtTmp.DefaultView.Sort = strSortField + (sortOrder == SortOrder.Ascending ? " ASC" : " DESC");
                            dtTmp = dtTmp.DefaultView.ToTable();
                        }

                        for (int intRow = 0; intRow < oGrid.Rows.Count; intRow++)
                        {
                            for (int intCol = 0; intCol < dtTmp.Columns.Count; intCol++)
                            {
                                //dataGridView.Columns[intCol].HeaderCell.SortGlyphDirection = SortOrder.None;
                                strField = oGrid.Columns[intCol].Name;
                                if (oGrid.Columns.Contains(strField))
                                {
                                    if (Convert.ToString(oGrid.Rows[intRow].Cells[intCol].Value) != Convert.ToString(dtTmp.Rows[intRow][strField]))
                                        oGrid.Rows[intRow].Cells[intCol].Value = Convert.ToString(dtTmp.Rows[intRow][strField]);
                                }
                            }
                        }

                        if (intSelectRowIndex >= 0)
                        {
                            if (oGrid.Rows.Count > intSelectRowIndex)
                                oGrid.Rows[intSelectRowIndex].Selected = true;
                            else
                                oGrid.Rows[oGrid.Rows.Count - 1].Selected = true;
                        }
                        else
                            oGrid.ClearSelection();

                        oGrid.ResumeLayout();
                    }
                    else
                    {
                        oGrid.Rows.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
            }
            finally
            {
                dtTmp = null;
            }
        }
        #endregion Grid顯示

        /// <summary>
        /// 檢查程式是否重複開啟
        /// </summary>
        private void ChkAppIsAlreadyRunning()
        {
            try
            {
                string aFormName = System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName;
                string aProcName = System.IO.Path.GetFileNameWithoutExtension(aFormName);
                if (System.Diagnostics.Process.GetProcessesByName(aProcName).Length > 1)
                {
                    MessageBox.Show("程式已開啟", "Communication System", MessageBoxButtons.OK);
                    //Application.Exit();
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                Environment.Exit(0);
            }
        }

        private void ChangeSubForm(Form subForm)
        {
            try
            {
                var children = spcMainView.Panel1.Controls;
                foreach (Control c in children)
                {
                    if (c is Form)
                    {
                        var thisChild = c as Form;
                        //thisChild.Hide();
                        spcMainView.Panel1.Controls.Remove(thisChild);
                        thisChild.Width = 0;
                    }
                }

                if (subForm != null)
                {
                    subForm.TopLevel = false;
                    subForm.Dock = DockStyle.Fill;//適應窗體大小
                    subForm.FormBorderStyle = FormBorderStyle.None;//隱藏右上角的按鈕
                    subForm.Parent = spcMainView.Panel1;
                    spcMainView.Panel1.Controls.Add(subForm);
                    subForm.Show();
                }
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
            }
        }
    }
}
