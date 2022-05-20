using Mirle.Def;
using Mirle.DataBase;

namespace Mirle.DB.Proc
{
    public class clsGetDB
    {
        public static DataBase.DB GetDB(clsDbConfig _config)
        {
            DataBase.DB db;
            if (_config.DBType == DBTypes.SqlServer)
                db = new SqlServer(_config);
            else if (_config.DBType == DBTypes.SQLite)
                db = new Sqlite(_config);
            else db = new OracleClient(_config);

            return db;
        }

        public static int FunDbOpen(DataBase.DB db)
        {
            string strEM = "";
            return FunDbOpen(db, ref strEM);
        }

        public static int FunDbOpen(DataBase.DB db, ref string strEM)
        {
            int iRet = db.Open(ref strEM);
            clsHost.IsConn = db.IsConnected;
            if(iRet != DBResult.Success)
            {
                clsWriLog.Log.FunWriTraceLog_CV($"資料庫開啟失敗！=> {strEM}");
            }

            return iRet;
        }
    }
}
