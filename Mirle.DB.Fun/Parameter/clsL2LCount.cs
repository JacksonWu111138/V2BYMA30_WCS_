using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.DB.Fun.Parameter
{
    public class clsL2LCount
    {
        public const string TableName = "L2LCount";
        public class Column
        {
            /// <summary>
            /// 料盒ID
            /// </summary>
            public const string BoxID = "BoxId";
            /// <summary>
            /// 所在裝置ID
            /// </summary>
            public const string EquNo = "EquNo";
            /// <summary>
            /// 該趟狀態
            /// </summary>
            public const string RoundSts = "RoundSts";
            /// <summary>
            /// 累計次數
            /// </summary>
            public const string Count = "L2LTimes";
            /// <summary>
            /// 歷史儲位資料
            /// </summary>
            public const string HisLoc = "HisLoc";
            /// <summary>
            /// 新增時間
            /// </summary>
            public const string Create_Date = "CrtDate";
            /// <summary>
            /// 更新時間
            /// </summary>
            public const string Update_Date = "ExpDate";
            /// <summary>
            /// 目的校正儲位
            /// </summary>
            public const string TeachLoc = "TeachLoc";
        }
    }
}
