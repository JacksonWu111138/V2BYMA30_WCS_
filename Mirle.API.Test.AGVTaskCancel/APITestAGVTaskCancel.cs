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
using Mirle.Def.U2NMMA30;
using Mirle.DataBase;
using Mirle.Middle.DB_Proc;
using Mirle.Structure;
using Mirle.WebAPI.V2BYMA30.Function;
using Mirle.WebAPI.V2BYMA30.ReportInfo;

namespace Mirle.WebAPI.Test.AGVTaskCancel
{
    public partial class APITestAGVTaskCancel : Form
    {
        private V2BYMA30.clsHost api;
        private Middle.DB_Proc.clsTool tool;
        private clsDbConfig _config = new clsDbConfig();
        private WebApiConfig AGVApi_config = new WebApiConfig();

        public APITestAGVTaskCancel()
        {
        }

        public APITestAGVTaskCancel(WebApiConfig aGVApi_config)
        {
            InitializeComponent();
            AGVApi_config = aGVApi_config;
        }

        private void ButtonTaskCancel_Click(object sender, EventArgs e)
        {
            if(textBoxjobId.Text == "") 
            {
                MessageBox.Show("空jobId", "Task Cancel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                TaskCancelInfo info = new TaskCancelInfo
                {
                    jobId = textBoxjobId.Text
                };

                //找IP
                if (!api.GetTaskCancel().FunReport(info, AGVApi_config.IP))
                {
                    MessageBox.Show($"取消命令失敗, jobId:{info.jobId}.", "Task Cancel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"取消命令成功, jobId:{info.jobId}.", "Task Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                 
              


                //DataTable dtTmp = new DataTable();
                //using (var db = clsGetDB.GetDB(_config))
                //{
                //    int iRet = clsGetDB.FunDbOpen(db);
                //    if (iRet == DBResult.Success)
                //    {
                //        string strSql = $"select * from {Middle.DB_Proc.Parameter.clsMiddleCmd.TableName} where " +
                //                        $"{Middle.DB_Proc.Parameter.clsMiddleCmd.Column.CommandID} = '{info.jobId}' ";
                //        string strEM = "";
                //        iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                //        if (iRet == DBResult.Success)
                //        {
                //            MiddleCmd cmd = tool.GetMiddleCmd(dtTmp.Rows[0]);
                //            ConveyorInfo conveyor = new ConveyorInfo();
                //            conveyor = ConveyorDef.GetBufferByDevice(cmd.DeviceID);

                //            //發送TaskCancel訊號
                //            if (!api.GetTaskCancel().FunReport(info, conveyor.API.IP))
                //                throw new Exception(strEM);
                //        }
                //        else
                //        {
                //            MessageBox.Show("藉由jobId讀取db錯誤", "Task Cancel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        }
                //    }
                //}
            }
            

        }
    }
}
