using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mirle.Def;
using Mirle.WebAPI.Test.Controllers.ApiList;

namespace Mirle.WebAPI.Test.Controllers
{
    public partial class ControllersAPITest : Form
    {
        public static WebApiConfig _AgvApi_Config = new WebApiConfig();
        public static WebApiConfig _TowerApi_Config = new WebApiConfig();
        public static WebApiConfig _BoxApi_Config = new WebApiConfig();
        public static WebApiConfig _PcbaApi_Config = new WebApiConfig();
        public static WebApiConfig _SmtcApi_Config = new WebApiConfig();
        public static WebApiConfig _OsmtcApi_Config = new WebApiConfig();
        public static WebApiConfig _E04Api_Config = new WebApiConfig();
        public static WebApiConfig _E05Api_Config = new WebApiConfig();

        public ControllersAPITest()
        {
            InitializeComponent();
        }
        public ControllersAPITest(WebApiConfig AgvApi_config, WebApiConfig TowerApi_config, WebApiConfig BoxApi_config, 
            WebApiConfig PcbaApi_config, WebApiConfig SmtcApi_config, WebApiConfig OsmtcApi_config, WebApiConfig E04Api_config, WebApiConfig E05Api_config)
        {
            _AgvApi_Config = AgvApi_config;
            _TowerApi_Config = TowerApi_config;
            _BoxApi_Config = BoxApi_config;
            _PcbaApi_Config = PcbaApi_config;
            _SmtcApi_Config = SmtcApi_config;
            _OsmtcApi_Config = OsmtcApi_config;
            _E04Api_Config = E04Api_config;
            _E05Api_Config = E05Api_config;
            InitializeComponent();
        }

        private void CtrlBlockStatusQuery_Click(object sender, EventArgs e)
        {
            CtrlBlockStatusQuery form = new CtrlBlockStatusQuery(_TowerApi_Config);
            form.Show();
        }

        private void CtrlBufferRollInfo_Click(object sender, EventArgs e)
        {
            CtrlBufferRollInfo form = new CtrlBufferRollInfo(_TowerApi_Config, _SmtcApi_Config, _E04Api_Config,
                _E05Api_Config, _BoxApi_Config, _PcbaApi_Config, _OsmtcApi_Config);
            form.Show();
        }

        private void CtrlBufferStatusQuery_Click(object sender, EventArgs e)
        {
            CtrlBufferStatusQuery form = new CtrlBufferStatusQuery(_TowerApi_Config, _SmtcApi_Config, _E04Api_Config,
                _E05Api_Config, _BoxApi_Config, _PcbaApi_Config, _OsmtcApi_Config);
            form.Show();
        }

        private void CtrlControlStatusQuery_Click(object sender, EventArgs e)
        {
            CtrlControlStatusQuery form = new CtrlControlStatusQuery(_TowerApi_Config);
            form.Show();
        }

        private void CtrlCVReceiveNewBinCmd_Click(object sender, EventArgs e)
        {
            CtrlCVReceiveNewBinCmd form = new CtrlCVReceiveNewBinCmd(_TowerApi_Config, _SmtcApi_Config, _E04Api_Config,
                _E05Api_Config, _BoxApi_Config, _PcbaApi_Config, _OsmtcApi_Config);
            form.Show();
        }
        private void CtrlHealthCheck_Click(object sender, EventArgs e)
        {
            CtrlHealthCheck form = new CtrlHealthCheck(_E04Api_Config, _E05Api_Config, _BoxApi_Config);
            form.Show();
        }

        private void CtrlLotRetrieveTransfer_Click(object sender, EventArgs e)
        {
            CtrlLotRetrieveTransfer form = new CtrlLotRetrieveTransfer(_TowerApi_Config);
            form.Show();
        }

        private void button_LotTransferCancel_Click(object sender, EventArgs e)
        {
            CtrlLotTransferCancel form = new CtrlLotTransferCancel(_TowerApi_Config);
            form.Show();
        }

        private void CtrlNewCarrierToStage_Click(object sender, EventArgs e)
        {
            CtrlNewCarrierToStage form = new CtrlNewCarrierToStage(_TowerApi_Config);
            form.Show();
        }

        private void CtrlPutawayTransferInfo_Click(object sender, EventArgs e)
        {
            CtrlPutawayTransferInfo form = new CtrlPutawayTransferInfo(_TowerApi_Config);
            form.Show();
        }

        private void CtrlMoveTask_Click(object sender, EventArgs e)
        {
            CtrlMoveTask form = new CtrlMoveTask(_AgvApi_Config);
            form.Show();
        }

        private void CtrlRackRequest_Click(object sender, EventArgs e)
        {
            CtrlRackRequest form = new CtrlRackRequest(_AgvApi_Config);
            form.Show();
        }

        private void CtrlRackTurn_Click(object sender, EventArgs e)
        {
            CtrlRackTurn form = new CtrlRackTurn(_AgvApi_Config);
            form.Show();
        }

        private void CtrlRemoteLocalRequest_Click(object sender, EventArgs e)
        {
            CtrlRemoteLocalRequest form = new CtrlRemoteLocalRequest(_TowerApi_Config);
            form.Show();
        }

        private void CtrlRetrieveTransferInfo_Click(object sender, EventArgs e)
        {
            CtrlRetrieveTransferInfo form = new CtrlRetrieveTransferInfo(_TowerApi_Config);
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void CtrlTaskCancel_Click(object sender, EventArgs e)
        {
            CtrlTaskCancel form = new CtrlTaskCancel(_AgvApi_Config);
            form.Show();
        }

        private void CtrlTransferCommandRequest_Click(object sender, EventArgs e)
        {
            CtrlTransferCommandRequest form = new CtrlTransferCommandRequest(_TowerApi_Config, _E04Api_Config, _E05Api_Config);
            form.Show();
        }
    }
}
