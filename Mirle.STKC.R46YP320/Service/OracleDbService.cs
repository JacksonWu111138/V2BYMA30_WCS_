using Mirle.LCS.Models.Info;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Diagnostics;
using System.Timers;

namespace Mirle.STKC.R46YP320.Service
{
    public class OracleDbService : IDbService
    {
        private readonly Timer _checkConnectionTimer;
        private readonly DatabaseInfo _dbInfo;

        public OracleDbService(DatabaseInfo dbInfo)
        {
            _dbInfo = dbInfo;
            _checkConnectionTimer = new Timer();
            _checkConnectionTimer.Interval = 5000;
            _checkConnectionTimer.Elapsed += _checkConnectionTimer_Elapsed;
            _checkConnectionTimer.Start();
        }

        public bool IsConnected { get; private set; }

        public bool CheckConnection()
        {
            try
            {
                using (var con = new OracleConnection(_dbInfo.GetOracleConnectionString()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM DUAL";
                        var x = cmd.ExecuteScalar();
                        return IsConnected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}\n{ex.StackTrace}");
                return IsConnected = false;
            }
        }

        public IDbConnection GetDbConnection()
        {
            var con = new OracleConnection(_dbInfo.GetOracleConnectionString());
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}\n{ex.StackTrace}");
                throw;
            }
            return con;
        }

        private void _checkConnectionTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _checkConnectionTimer.Stop();
                CheckConnection();
            }
            finally
            {
                _checkConnectionTimer.Start();
            }
        }

        private string GetOracleDate()
        {
            return "substr(to_char(systimestamp,'yyyy-MM-dd HH24:mi:ssxff'),1,25)";
        }
    }
}