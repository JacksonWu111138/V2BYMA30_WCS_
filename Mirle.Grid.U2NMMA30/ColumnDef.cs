using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Mirle.Gird;

namespace Mirle.Grid.U2NMMA30
{
    public class ColumnDef
    {
        public class Teach
        {
            public static readonly ColumnInfo DeviceID = new ColumnInfo { Index = 0, Name = "DeviceID", Width = 60 };
            public static readonly ColumnInfo Loc = new ColumnInfo { Index = 1, Name = "Loc", Width = 68 };
            public static readonly ColumnInfo LocSts = new ColumnInfo { Index = 2, Name = "LocSts", Width = 60 };
            public static readonly ColumnInfo BoxId = new ColumnInfo { Index = 3, Name = "CstID", Width = 100 };

            public static void GridSetLocRange(ref DataGridView oGrid)
            {
                oGrid.ColumnCount = 4;
                oGrid.RowCount = 0;
                clInitSys.SetGridColumnInit(DeviceID, ref oGrid);
                clInitSys.SetGridColumnInit(Loc, ref oGrid);
                clInitSys.SetGridColumnInit(BoxId, ref oGrid);
                clInitSys.SetGridColumnInit(LocSts, ref oGrid);
            }
        }

        public class CMD_MST
        {
            public static readonly ColumnInfo CmdSno = new ColumnInfo { Index = 0, Name = "任務號", Width = 68 };
            public static readonly ColumnInfo JobID = new ColumnInfo { Index = 1, Name = "JobID", Width = 200 };
            public static readonly ColumnInfo BoxId = new ColumnInfo { Index = 2, Name = "CstID", Width = 100 };
            public static readonly ColumnInfo CmdSts = new ColumnInfo { Index = 3, Name = "狀態", Width = 60 };
            public static readonly ColumnInfo PRT = new ColumnInfo { Index = 4, Name = "優先權", Width = 68 };
            public static readonly ColumnInfo CmdMode = new ColumnInfo { Index = 5, Name = "模式", Width = 60 };
            public static readonly ColumnInfo StnNo = new ColumnInfo { Index = 6, Name = "站口", Width = 200 };
            public static readonly ColumnInfo Loc = new ColumnInfo { Index = 7, Name = "儲位", Width = 68 };
            public static readonly ColumnInfo Remark = new ColumnInfo { Index = 8, Name = "說明", Width = 250 };
            public static readonly ColumnInfo CurDeviceID = new ColumnInfo { Index = 9, Name = "當下設備", Width = 80 };
            public static readonly ColumnInfo CurLoc = new ColumnInfo { Index = 10, Name = "當下位置", Width = 80 };
            public static readonly ColumnInfo EquNO = new ColumnInfo { Index = 11, Name = "STK", Width = 60 };
            public static readonly ColumnInfo BatchID = new ColumnInfo { Index = 12, Name = "BatchID", Width = 200 };
            public static readonly ColumnInfo ZoneID = new ColumnInfo { Index = 13, Name = "ZoneID", Width = 80 };
            public static readonly ColumnInfo NewLoc = new ColumnInfo { Index = 14, Name = "新儲位", Width = 68 };
            public static readonly ColumnInfo NeedShelfToShelf = new ColumnInfo { Index = 15, Name = "需要庫對庫", Width = 100 };
            public static readonly ColumnInfo CrtDate = new ColumnInfo { Index = 16, Name = "產生時間", Width = 100 };
            public static readonly ColumnInfo ExpDate = new ColumnInfo { Index = 17, Name = "執行時間", Width = 100 };
            public static readonly ColumnInfo BackupPortId = new ColumnInfo { Index = 18, Name = "BackupPortId", Width = 200 };
            public static readonly ColumnInfo TicketId = new ColumnInfo { Index = 19, Name = "訂單號", Width = 200 };
            public static readonly ColumnInfo ManualStockIn = new ColumnInfo { Index = 20, Name = "手動入庫", Width = 200 };

            public static void GridSetLocRange(ref DataGridView oGrid)
            {
                oGrid.ColumnCount = 21;
                oGrid.RowCount = 0;
                clInitSys.SetGridColumnInit(CmdSno, ref oGrid);
                clInitSys.SetGridColumnInit(JobID, ref oGrid);
                clInitSys.SetGridColumnInit(BoxId, ref oGrid);
                clInitSys.SetGridColumnInit(CmdSts, ref oGrid);
                clInitSys.SetGridColumnInit(PRT, ref oGrid);
                clInitSys.SetGridColumnInit(CmdMode, ref oGrid);
                clInitSys.SetGridColumnInit(StnNo, ref oGrid);
                clInitSys.SetGridColumnInit(Loc, ref oGrid);
                clInitSys.SetGridColumnInit(Remark, ref oGrid);
                clInitSys.SetGridColumnInit(CurDeviceID, ref oGrid);
                clInitSys.SetGridColumnInit(CurLoc, ref oGrid);
                clInitSys.SetGridColumnInit(EquNO, ref oGrid);
                clInitSys.SetGridColumnInit(BatchID, ref oGrid);
                clInitSys.SetGridColumnInit(ZoneID, ref oGrid);
                clInitSys.SetGridColumnInit(NewLoc, ref oGrid);
                clInitSys.SetGridColumnInit(NeedShelfToShelf, ref oGrid);
                clInitSys.SetGridColumnInit(CrtDate, ref oGrid);
                clInitSys.SetGridColumnInit(ExpDate, ref oGrid);
                clInitSys.SetGridColumnInit(BackupPortId, ref oGrid);
                clInitSys.SetGridColumnInit(TicketId, ref oGrid);
                clInitSys.SetGridColumnInit(ManualStockIn, ref oGrid);
            }
        }
    }
}
