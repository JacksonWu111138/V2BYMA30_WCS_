using Mirle.STKC.R46YP320.Extensions;
using Mirle.LCS.Models;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using Mirle.STKC.R46YP320.ViewModels;
using System;
using System.Globalization;
using System.Windows.Forms;
using Mirle.Def;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class SCCommandView : Form
    {
        private readonly LoggerService _loggerService;
        private readonly TaskCommandService _taskCommandService;

        public SCCommandView(LoggerService loggerService, TaskCommandService taskCommandService)
        {
            _loggerService = loggerService;
            _taskCommandService = taskCommandService;
            InitializeComponent();
        }

        private void SCCommandView_Load(object sender, EventArgs e)
        {
            cbbRMID.Items.Clear();
            cbbRMID.Items.Add("Crane-1");
            cbbRMID.Items.Add("Crane-2");
            cbbRMID.SelectedIndex = 0;

            cbbTransferMode.Items.Clear();
            cbbTransferMode.Items.Add("1-MOVE");
            cbbTransferMode.Items.Add("2-FROM");
            cbbTransferMode.Items.Add("3-TO");
            cbbTransferMode.Items.Add("4-FROM_TO");
            cbbTransferMode.Items.Add("7-SCAN");
            cbbTransferMode.SelectedIndex = 0;

            cbbBCRRead.Items.Clear();
            cbbBCRRead.Items.Add("N-PASS");
            cbbBCRRead.Items.Add("Y-ENABLE");
            cbbBCRRead.SelectedIndex = 0;

            cbbCstType.Items.Clear();
            cbbCstType.Items.Add("1-Small");
            cbbCstType.Items.Add("2-Big");
            cbbCstType.Items.Add("FF-Ignore");
            cbbCstType.SelectedIndex = 1;

            cbbTravelAxisSpeed.Items.Clear();
            cbbTravelAxisSpeed.Items.Add("40-Level2");
            cbbTravelAxisSpeed.Items.Add("60-Level3");
            cbbTravelAxisSpeed.Items.Add("80-Level4");
            cbbTravelAxisSpeed.Items.Add("100-Level5");
            cbbTravelAxisSpeed.SelectedIndex = 0;

            cbbLifterAxisSpeed.Items.Clear();
            cbbLifterAxisSpeed.Items.Add("40-Level2");
            cbbLifterAxisSpeed.Items.Add("60-Level3");
            cbbLifterAxisSpeed.Items.Add("80-Level4");
            cbbLifterAxisSpeed.Items.Add("100-Level5");
            cbbLifterAxisSpeed.SelectedIndex = 0;

            cbbRotateAxisSpeed.Items.Clear();
            cbbRotateAxisSpeed.Items.Add("40-Level2");
            cbbRotateAxisSpeed.Items.Add("60-Level3");
            cbbRotateAxisSpeed.Items.Add("80-Level4");
            cbbRotateAxisSpeed.Items.Add("100-Level5");
            cbbRotateAxisSpeed.SelectedIndex = 0;

            cbbForkAxisSpeed.Items.Clear();
            cbbForkAxisSpeed.Items.Add("40-Level2");
            cbbForkAxisSpeed.Items.Add("60-Level3");
            cbbForkAxisSpeed.Items.Add("80-Level4");
            cbbForkAxisSpeed.Items.Add("100-Level5");
            cbbForkAxisSpeed.SelectedIndex = 0;

            cbbForkNumber.Items.Clear();
            cbbForkNumber.Items.Add("1-Left");
            cbbForkNumber.Items.Add("2-Right");
            cbbForkNumber.SelectedIndex = 0;
        }

        private void butSaveCommand_Click(object sender, EventArgs e)
        {
            int.TryParse(cbbRMID.Text.Split("-"[0])[1], out var craneNo);
            int.TryParse(cbbForkNumber.Text.Split("-"[0])[0], out var forkNo);
            int.TryParse(cbbTransferMode.Text.Split("-"[0])[0], out var transferModeNo);
            int.TryParse(txtSource.Text.Trim(), out var source);
            int.TryParse(txtDestination.Text.Trim(), out var dest);

            int.TryParse(cbbTravelAxisSpeed.Text.Split("-"[0])[0], out var travelSpeed);
            int.TryParse(cbbLifterAxisSpeed.Text.Split("-"[0])[0], out var lifterSpeed);
            int.TryParse(cbbRotateAxisSpeed.Text.Split("-"[0])[0], out var rotateSpeed);
            int.TryParse(cbbForkAxisSpeed.Text.Split("-"[0])[0], out var forkSpeed);

            //int.TryParse(cbbCstType.Text.Split("-"[0])[0], NumberStyles.HexNumber, CultureInfo.InvariantCulture , out var cstType);
            var cstType = 0;

            var newCmd = new SCCommand()
            {
                CraneId = craneNo,
                ForkNumber = forkNo,
                TransferMode = transferModeNo.ToEnum<clsEnum.TaskMode>(),
                CstId = txtCSTID.Text.Trim(),
                EnableBCRRead = cbbBCRRead.Text.Split("-"[0])[0] == "Y",
                Source = source,
                Destination = dest,
                TravelSpeed = travelSpeed,
                LifterSpeed = lifterSpeed,
                RotateSpeed = rotateSpeed,
                ForkSpeed = forkSpeed,
                CstType = cstType,
            };

            var strNO = _taskCommandService.CreateNewTaskCommand(newCmd);

            TraceLogFormat objLog = new TraceLogFormat();
            objLog.Message = "butSaveCommand_Click(TaskNo:" + strNO + ")";// ",intRet=" + intRet.ToString() + ")";
            _loggerService.ShowUI(0, objLog);
        }

        private void chkSetAxisSpeed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSetAxisSpeed.Checked)
            {
                cbbTravelAxisSpeed.Enabled = true;
                cbbLifterAxisSpeed.Enabled = true;
                cbbRotateAxisSpeed.Enabled = true;
                cbbForkAxisSpeed.Enabled = true;
            }
            else
            {
                cbbTravelAxisSpeed.Enabled = true;
                cbbLifterAxisSpeed.SelectedIndex = cbbTravelAxisSpeed.SelectedIndex;
                cbbRotateAxisSpeed.SelectedIndex = cbbTravelAxisSpeed.SelectedIndex;
                cbbForkAxisSpeed.SelectedIndex = cbbTravelAxisSpeed.SelectedIndex;
                cbbLifterAxisSpeed.Enabled = false;
                cbbRotateAxisSpeed.Enabled = false;
                cbbForkAxisSpeed.Enabled = false;
            }
        }

        private void cbbTravelAxisSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkSetAxisSpeed.Checked == false)
            {
                cbbLifterAxisSpeed.SelectedIndex = cbbTravelAxisSpeed.SelectedIndex;
                cbbRotateAxisSpeed.SelectedIndex = cbbTravelAxisSpeed.SelectedIndex;
                cbbForkAxisSpeed.SelectedIndex = cbbTravelAxisSpeed.SelectedIndex;
            }
        }
    }
}
