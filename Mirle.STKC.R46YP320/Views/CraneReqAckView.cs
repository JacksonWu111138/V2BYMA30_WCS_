using Mirle.MPLC.DataType;
using Mirle.STKC.R46YP320.Extensions;
using Mirle.Stocker.R46YP320;
using System;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class CraneReqAckView : Form
    {
        private readonly Crane _crane;

        public CraneReqAckView(Crane crane)
        {
            _crane = crane;
            InitializeComponent();
        }

        private void CraneAckReqView_Load(object sender, EventArgs e)
        {
            refreshTimer.Enabled = true;
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (this.Width == 0) return;

            var req = _crane.Signal.RequestSignal;
            var ack = _crane.Signal.Controller.AckSignal;

            lblRM1EmptyRetrieval_Ack_LF.BackColor = ack.EmptyRetrievalAck_LF.ToColor();
            lblRM1DoubleStorage_Ack_LF.BackColor = ack.DoubleStorageAck_LF.ToColor();
            lblRM1EQInterlockErr_Ack_LF.BackColor = ack.EQInlineInterlockErrAck_LF.ToColor();
            lblRM1IOInterlockErr_Ack_LF.BackColor = ack.IOInlineInterlockErrAck_LF.ToColor();
            lblRM1IDReadError_Ack_LF.BackColor = ack.IDReadErrorAck_LF.ToColor();
            lblRM1IDMismatch_Ack_LF.BackColor = ack.IDMismatchAck_LF.ToColor();

            lblRM1EmptyRetrieval_Req_LF.BackColor = req.EmptyRetrievalReq_LF.ToColor();
            lblRM1DoubleStorage_Req_LF.BackColor = req.DoubleStorageReq_LF.ToColor();
            lblRM1EQInterlockErr_Req_LF.BackColor = req.EQInlineInterlockErrReq_LF.ToColor();
            lblRM1IOInterlockErr_Req_LF.BackColor = req.IOInlineInterlockErrReq_LF.ToColor();
            lblRM1IDReadError_Req_LF.BackColor = req.IDReadErrorReq_LF.ToColor();
            lblRM1IDMismatch_Req_LF.BackColor = req.IDMismatchReq_LF.ToColor();

            lblRM1EmptyRetrieval_Ack_RF.BackColor = ack.EmptyRetrievalAck_RF.ToColor();
            lblRM1DoubleStorage_Ack_RF.BackColor = ack.DoubleStorageAck_RF.ToColor();
            lblRM1EQInterlockErr_Ack_RF.BackColor = ack.EQInlineInterlockErrAck_RF.ToColor();
            lblRM1IOInterlockErr_Ack_RF.BackColor = ack.IOInlineInterlockErrAck_RF.ToColor();
            lblRM1IDReadError_Ack_RF.BackColor = ack.IDReadErrorAck_RF.ToColor();
            lblRM1IDMismatch_Ack_RF.BackColor = ack.IDMismatchAck_RF.ToColor();

            lblRM1EmptyRetrieval_Req_RF.BackColor = req.EmptyRetrievalReq_RF.ToColor();
            lblRM1DoubleStorage_Req_RF.BackColor = req.DoubleStorageReq_RF.ToColor();
            lblRM1EQInterlockErr_Req_RF.BackColor = req.EQInlineInterlockErrReq_RF.ToColor();
            lblRM1IOInterlockErr_Req_RF.BackColor = req.IOInlineInterlockErrReq_RF.ToColor();
            lblRM1IDReadError_Req_RF.BackColor = req.IDReadErrorReq_RF.ToColor();
            lblRM1IDMismatch_Req_RF.BackColor = req.IDMismatchReq_RF.ToColor();

            lblRM1ScanComplete_Ack.BackColor = ack.ScanCompleteAck.ToColor();
            lblRM1TransferRequestWrong_Ack_LF.BackColor = ack.TransferRequestWrongAck_LF.ToColor();
            lblRM1TransferRequestWrong_Ack_RF.BackColor = ack.TransferRequestWrongAck_RF.ToColor();

            lblRM1ScanComplete_Req.BackColor = req.ScanCompleteReq.ToColor();
            lblRM1TransferRequestWrong_Req_LF.BackColor = req.TransferRequestWrongReq_LF.ToColor();
            lblRM1TransferRequestWrong_Req_RF.BackColor = req.TransferRequestWrongReq_RF.ToColor();

            //lblRM1CstTypeMismatch_Ack.BackColor = ack.CstTypeMismatchAck.ToColor();
            //lblRM1CstTypeMismatch_Req.BackColor = req.CstTypeMismatchReq.ToColor();
        }

        private void RM1ReqAckSetBit_DoubleClick(object sender, EventArgs e)
        {
            var ack = _crane.Signal.Controller.AckSignal;
            Bit ackSignal = null;

            Label lblSignal = (Label)sender;

            switch (lblSignal.Name)
            {
                case nameof(lblRM1EmptyRetrieval_Ack_LF):
                    ackSignal = ack.EmptyRetrievalAck_LF;
                    break;

                case nameof(lblRM1EmptyRetrieval_Ack_RF):
                    ackSignal = ack.EmptyRetrievalAck_RF;
                    break;

                case nameof(lblRM1DoubleStorage_Ack_LF):
                    ackSignal = ack.DoubleStorageAck_LF;
                    break;

                case nameof(lblRM1DoubleStorage_Ack_RF):
                    ackSignal = ack.DoubleStorageAck_RF;
                    break;

                case nameof(lblRM1ScanComplete_Ack):
                    ackSignal = ack.ScanCompleteAck;
                    break;

                case nameof(lblRM1EQInterlockErr_Ack_LF):
                    ackSignal = ack.EQInlineInterlockErrAck_LF;
                    break;

                case nameof(lblRM1EQInterlockErr_Ack_RF):
                    ackSignal = ack.EQInlineInterlockErrAck_RF;
                    break;

                case nameof(lblRM1IOInterlockErr_Ack_LF):
                    ackSignal = ack.IOInlineInterlockErrAck_LF;
                    break;

                case nameof(lblRM1IOInterlockErr_Ack_RF):
                    ackSignal = ack.IOInlineInterlockErrAck_RF;
                    break;

                case nameof(lblRM1TransferRequestWrong_Ack_LF):
                    ackSignal = ack.TransferRequestWrongAck_LF;
                    break;

                case nameof(lblRM1TransferRequestWrong_Ack_RF):
                    ackSignal = ack.TransferRequestWrongAck_RF;
                    break;

                case nameof(lblRM1IDReadError_Ack_LF):
                    ackSignal = ack.IDReadErrorAck_LF;
                    break;

                case nameof(lblRM1IDReadError_Ack_RF):
                    ackSignal = ack.IDReadErrorAck_RF;
                    break;

                case nameof(lblRM1IDMismatch_Ack_LF):
                    ackSignal = ack.IDMismatchAck_LF;
                    break;

                case nameof(lblRM1IDMismatch_Ack_RF):
                    ackSignal = ack.IDMismatchAck_RF;
                    break;

                default:
                    return;
            }

            try
            {
                if (ackSignal.IsOn())
                {
                    ackSignal.SetOff();
                }
                else
                {
                    ackSignal.SetOn(); ;
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}