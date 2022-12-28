using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mirle.DB.Object;
using Mirle.Def;
using Mirle.Def.U2NMMA30;
using Mirle.Structure;
using Mirle.WebAPI.Test.Controllers.ApiList;
using Mirle.WebAPI.V2BYMA30.ReportInfo;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Mirle.WebAPI.Test.Controllers
{
    public partial class ControllersAPITest : Form
    {
        public ControllersAPITest()
        {
            InitializeComponent();
        }

        private void CtrlBlockStatusQuery_Click(object sender, EventArgs e)
        {
            CtrlBlockStatusQuery form = new CtrlBlockStatusQuery();
            form.Show();
        }

        private void CtrlBufferRollInfo_Click(object sender, EventArgs e)
        {
            CtrlBufferRollInfo form = new CtrlBufferRollInfo();
            form.Show();
        }

        private void CtrlBufferStatusQuery_Click(object sender, EventArgs e)
        {
            CtrlBufferStatusQuery form = new CtrlBufferStatusQuery();
            form.Show();
        }

        private void CtrlControlStatusQuery_Click(object sender, EventArgs e)
        {
            CtrlControlStatusQuery form = new CtrlControlStatusQuery();
            form.Show();
        }

        private void CtrlCVReceiveNewBinCmd_Click(object sender, EventArgs e)
        {
            CtrlCVReceiveNewBinCmd form = new CtrlCVReceiveNewBinCmd();
            form.Show();
        }
        private void CtrlHealthCheck_Click(object sender, EventArgs e)
        {
            CtrlHealthCheck form = new CtrlHealthCheck();
            form.Show();
        }

        private void CtrlLotRetrieveTransfer_Click(object sender, EventArgs e)
        {
            CtrlLotRetrieveTransfer form = new CtrlLotRetrieveTransfer();
            form.Show();
        }

        private void button_LotTransferCancel_Click(object sender, EventArgs e)
        {
            CtrlLotTransferCancel form = new CtrlLotTransferCancel();
            form.Show();
        }

        private void CtrlNewCarrierToStage_Click(object sender, EventArgs e)
        {
            CtrlNewCarrierToStage form = new CtrlNewCarrierToStage();
            form.Show();
        }

        private void CtrlPutawayTransferInfo_Click(object sender, EventArgs e)
        {
            CtrlPutawayTransferInfo form = new CtrlPutawayTransferInfo();
            form.Show();
        }

        private void CtrlMoveTask_Click(object sender, EventArgs e)
        {
            CtrlMoveTask form = new CtrlMoveTask();
            form.Show();
        }

        private void CtrlRackRequest_Click(object sender, EventArgs e)
        {
            CtrlRackRequest form = new CtrlRackRequest();
            form.Show();
        }

        private void CtrlRackTurn_Click(object sender, EventArgs e)
        {
            CtrlRackTurn form = new CtrlRackTurn();
            form.Show();
        }

        private void CtrlRemoteLocalRequest_Click(object sender, EventArgs e)
        {
            CtrlRemoteLocalRequest form = new CtrlRemoteLocalRequest();
            form.Show();
        }

        private void CtrlRetrieveTransferInfo_Click(object sender, EventArgs e)
        {
            CtrlRetrieveTransferInfo form = new CtrlRetrieveTransferInfo();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void CtrlTaskCancel_Click(object sender, EventArgs e)
        {
            CtrlTaskCancel form = new CtrlTaskCancel();
            form.Show();
        }

        private void CtrlTransferCommandRequest_Click(object sender, EventArgs e)
        {
            CtrlTransferCommandRequest form = new CtrlTransferCommandRequest();
            form.Show();
        }

        private void CtrlBufferInitial_Click(object sender, EventArgs e)
        {
            CtrlBufferInitialInfo form = new CtrlBufferInitialInfo();
            form.Show();
        }

        private void CtrlEmptyBinLoadDone_Click(object sender, EventArgs e)
        {
            CtrlEmptyBinLoadDone form = new CtrlEmptyBinLoadDone();
            form.Show();
        }

        private void CtrlBoxAGVStockIn_Click(object sender, EventArgs e)
        {
            ConveyorInfo[] BoxAgvList = new ConveyorInfo[3];
            BoxAgvList[0] = ConveyorDef.AGV.B1_078;
            BoxAgvList[1] = ConveyorDef.AGV.B1_074;
            BoxAgvList[2] = ConveyorDef.AGV.B1_070;
            bool isGetAgvPort = false;

            for(int i = 0; i < BoxAgvList.Length; i ++)
            {
                BufferStatusQueryInfo stsInfo = new BufferStatusQueryInfo
                {
                    bufferId = BoxAgvList[i].BufferName
                };
                BufferStatusReply stsReply = new BufferStatusReply();
                if(clsAPI.GetAPI().GetBufferStatusQuery().FunReport(stsInfo, clsAPI.GetBoxApiConfig().IP, ref stsReply))
                {
                    stsReply.jobId = stsReply.jobId.PadLeft(5, '0');
                    int.TryParse(stsReply.ready, out var ready);
                    if (stsReply.jobId == "00000" && ready == (int)clsEnum.ControllerApi.Ready.OutReady)
                    {
                        //可以送這個port口
                        CVReceiveNewBinCmdInfo ReceiveInfo = new CVReceiveNewBinCmdInfo
                        {
                            jobId = "3000" + BoxAgvList[i].BufferName.Substring(5, 1),
                            bufferId = BoxAgvList[i].BufferName,
                            carrierType = clsConstValue.ControllerApi.CarrierType.Bin
                        };
                        if(!clsAPI.GetAPI().GetCV_ReceiveNewBinCmd().FunReport(ReceiveInfo, clsAPI.GetBoxApiConfig().IP))
                        {
                            continue;
                        }

                        BufferRollInfo RollInfo = new BufferRollInfo
                        {
                            jobId = ReceiveInfo.jobId,
                            bufferId = BoxAgvList[i].BufferName
                        };
                        if(!clsAPI.GetAPI().GetBufferRoll().FunReport(RollInfo, clsAPI.GetBoxApiConfig().IP))
                        {
                            //清值
                            BufferInitialInfo InitialInfo = new BufferInitialInfo
                            {
                                bufferId = BoxAgvList[i].BufferName
                            };

                            if(!clsAPI.GetAPI().GetBufferInitial().FunReport(InitialInfo, clsAPI.GetBoxApiConfig().IP))
                            {
                                //清值失敗後動作未定
                            }
                            continue;
                        }

                        string portLocation = "";
                        switch(i)
                        {
                            case 0:
                                portLocation = "右邊";
                                break;
                            case 1:
                                portLocation = "中間";
                                break;
                            case 2:
                                portLocation = "左邊";
                                break;
                        }

                        MessageBox.Show($"請將靜電箱從{portLocation}AGV站口入庫", "手動入箱式倉", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        isGetAgvPort = true;
                        break;
                    }
                }
            }
            if(!isGetAgvPort)
            {
                MessageBox.Show($"暫時無可空閒之箱式倉AGV口，請稍後再試", "手動入箱式倉", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
