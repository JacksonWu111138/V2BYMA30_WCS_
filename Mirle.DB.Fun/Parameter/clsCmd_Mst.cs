using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.DB.Fun.Parameter
{
    public class clsCmd_Mst
    {
        public const string TableName = "Cmd_Mst";
        public class Column
        {
            /// <summary>
            /// 命令序號
            /// </summary>
            public const string Cmd_Sno = "Cmd_Sno";
            /// <summary>
            /// 命令狀態
            /// </summary>
            public const string Cmd_Sts = "Cmd_Sts";
            /// <summary>
            /// 命令不正常狀態
            /// </summary>
            public const string Cmd_Abnormal = "Cmd_Abnormal";
            /// <summary>
            /// 優先順位
            /// </summary>
            public const string Prty = "Prty";
            /// <summary>
            /// 站號
            /// </summary>
            public const string Stn_No = "Stn_No";
            /// <summary>
            /// 命令模式
            /// </summary>
            public const string Cmd_Mode = "Cmd_Mode";
            /// <summary>
            /// 作業類別
            /// </summary>
            public const string IO_Type = "IO_Type";
            /// <summary>
            /// 倉儲編號
            /// </summary>
            public const string WH_ID = "WH_ID";
            /// <summary>
            /// 儲位
            /// </summary>
            public const string Loc = "Loc";
            /// <summary>
            /// (TO)新儲位/站號
            /// </summary>
            public const string New_Loc = "New_Loc";
            /// <summary>
            /// 混載數
            /// </summary>
            public const string Mix_Qty = "Mix_Qty";
            /// <summary>
            /// 使用率
            /// </summary>
            public const string Avail = "Avail";
            /// <summary>
            /// 儲存區
            /// </summary>
            public const string Zone = "Zone_ID";
            /// <summary>
            /// BOXID/PALLETID
            /// </summary>
            public const string BoxID = "Loc_ID";
            /// <summary>
            /// 命令建立時間
            /// </summary>
            public const string Create_Date = "Crt_Date";
            /// <summary>
            /// 命令送出時間
            /// </summary>
            public const string Expose_Date = "EXP_Date";
            /// <summary>
            /// 命令完成時間
            /// </summary>
            public const string End_Date = "End_Date";
            /// <summary>
            /// 使用者編號
            /// </summary>
            public const string Trn_User = "Trn_User";
            /// <summary>
            /// 電腦名稱
            /// </summary>
            public const string Host_Name = "Host_Name";
            /// <summary>
            /// 時序
            /// </summary>
            public const string Trace = "Trace";
            /// <summary>
            /// 空棧板數量
            /// </summary>
            public const string Pallet_Count = "Plt_Count";
            /// <summary>
            /// CRANE編號
            /// </summary>
            public const string Equ_No = "Equ_No";
            /// <summary>
            /// 備註
            /// </summary>
            public const string Remark = "Remark";
            /// <summary>
            /// 當下位置
            /// </summary>
            public const string CurLoc = "CurLoc";
            /// <summary>
            /// 當下設備
            /// </summary>
            public const string CurDeviceID = "CurDeviceID";
        }
    }
}
