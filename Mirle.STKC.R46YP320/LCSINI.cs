using Config.Net;

namespace Mirle.STKC.R46YP320
{
    public interface LCSINI
    {
        [Option(Alias = "Data Base")]
        DatabaseConfig Database { get; }

        SystemConfig SystemConfig { get; }

        [Option(Alias = "Log")]
        LogConfig LogConfig { get; }

        //[Option(Alias = "PLC")]
        //PLCConfig PLCConfig { get; }

        //[Option(Alias = "PLCR")]
        //PLCRConfig PLCRConfig { get; }

        [Option(Alias = "STKC")]
        STKCConfig STKCConfig { get; }

        [Option(Alias = "FFU")]
        FFUConfig FFUConfig { get; }
    }

    public interface DatabaseConfig
    {
        /// <summary>
        /// MSSQL, Oracle_Oledb, DB2, Odbc, Access, OleDb, Oracle_DB.enuDatabaseType.Oracle_OracleClient, SQLite,
        /// </summary>
        string DBMS { get; }

        string DbServer { get; }
        string DataSource { get; }
        string DbUser { get; }
        string DbPswd { get; }

        [Option(DefaultValue = "1521")]
        string DBPort { get; }
    }

    public interface SystemConfig
    {
        /// <summary>
        /// 1:STKC, 2:RGV
        /// </summary>
        [Option(DefaultValue = 1)]
        int LCSCMode { get; }

        /// <summary>
        /// 0:Single, 1:DoubleSingle, 2:Dual, 3:TwinFork
        /// </summary>
        [Option(DefaultValue = 2)]
        int ControlMode { get; }

        [Option(DefaultValue = "STK001")]
        string StockerID { get; }

        [Option(Alias = "StockerCraneID-1", DefaultValue = "STK001C01")]
        string StockerCraneID1 { get; }

        [Option(Alias = "StockerCraneID-2", DefaultValue = "STK001C02")]
        string StockerCraneID2 { get; }

        [Option(Alias = "StockerCraneID-3", DefaultValue = "T2STK800CR1R")]
        string StockerCraneID3 { get; }

        [Option(Alias = "StockerCraneID-4", DefaultValue = "T2STK800CR2R")]
        string StockerCraneID4 { get; }

        int EQQty { get; }
        int IOQty { get; }

        //string RMAutoSetRun { get; set; }
        //string IOAutoSetRun { get; set; }

        [Option(DefaultValue = 7)]
        int CSTIDLength { get; }
    }

    public interface LogConfig
    {
        [Option(DefaultValue = 7)]
        int CompressDay { get; }

        [Option(DefaultValue = 90)]
        int DeleteDay { get; }
    }

    //public interface PLCConfig
    //{
    //    [Option(DefaultValue = 1)]
    //    int ActLogicalStationNo { get; }
    //}

    //public interface PLCRConfig
    //{
    //    /// <summary>
    //    /// AddressType,Threads,Start,Length,SMName
    //    /// </summary>
    //    [Option(Alias = "1", DefaultValue = "D,1,5000,5000,WordData,1")]
    //    string MPLCBlock1 { get; }
    //}

    public interface STKCConfig
    {
        [Option(DefaultValue = 1)]
        int StockerQty { get; }

        [Option(DefaultValue = "STK100")]
        string StockerID { get; }

        [Option(DefaultValue = 1)]
        int MPLCNo { get; }

        [Option(DefaultValue = "192.168.10.202")]
        string MPLCIP { get; }

        [Option(DefaultValue = 1282)]
        int MPLCPort { get; }

        [Option(DefaultValue = 5000)]
        int MPLCTimeout { get; }

        [Option(DefaultValue = 0)]
        int UseMCProtocol { get; }

        [Option(DefaultValue = 1)]
        int ControlMode { get; }

        [Option(DefaultValue = 1)]
        int WritePLCRawData { get; }

        [Option(DefaultValue = 0)]
        int InMemorySimulator { get; }

        [Option(DefaultValue = 20)]
        int MaxCstIdLength { get; }
    }

    public interface FFUConfig
    {
        [Option(DefaultValue = 1)]
        int Enable { get; }

        [Option(DefaultValue = "192.168.0.1502")]
        string IPAddress { get; }

        [Option(DefaultValue = 1502)]
        int Port { get; }

        //[Option(DefaultValue = 1)]
        //int EnableCache { get; }

        [Option(DefaultValue = 2000)]
        int Interval { get; }
    }
}
