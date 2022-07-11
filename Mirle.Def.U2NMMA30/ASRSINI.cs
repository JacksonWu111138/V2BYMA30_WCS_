﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Config.Net;

namespace Mirle.Def.U2NMMA30
{
    public interface ASRSINI
    {
        [Option(Alias = "Data Base")]
        DatabaseConfig Database { get; }

        [Option(Alias = "WMS Data Base")]
        DatabaseConfig Database_WMS { get; }

        [Option(Alias = "System Info")]
        SystemConfig System_Info { get; }

        [Option(Alias = "ControllerID")]
        ControllerID_Config ControllerID { get; }

        [Option(Alias = "StnNo")]
        StnNoConfig StnNo { get; }

        [Option(Alias = "EquNo")]
        EquNoConfig EquNo { get; }

        [Option(Alias = "Client API")]
        APIConfig Client_API { get; }

        [Option(Alias = "Server API")]
        APIConfig Server_API { get; }
    }

    public interface DatabaseConfig
    {
        /// <summary>
        /// MSSQL, Oracle_Oledb, DB2, Odbc, Access, OleDb, Oracle_DB.enuDatabaseType.Oracle_OracleClient, SQLite,
        /// </summary>
        string DBMS { get; }

        string DbServer { get; }
        string FODbServer { get; }
        string DataBase { get; }
        string DbUser { get; }
        string DbPswd { get; }

        [Option(DefaultValue = 1433)]
        int DBPort { get; }

        [Option(DefaultValue = 30)]
        int ConnectTimeOut { get; }

        [Option(DefaultValue = 30)]
        int CommandTimeOut { get; }
    }

    public interface SystemConfig
    {
        [Option(DefaultValue = 5)]
        int L2L_MaxCount { get; }
    }

    public interface ControllerID_Config
    {
        string Tower { get; }
        string Box { get; }
        string PCBA { get; }
        /// <summary>
        /// 產線
        /// </summary>
        string SMTC { get; }
        /// <summary>
        /// 線邊倉
        /// </summary>
        string Line { get; }
        /// <summary>
        /// 3F產線
        /// </summary>
        string SMT3C { get; }
        /// <summary>
        /// 5F產線
        /// </summary>
        string SMT5C { get; }
        /// <summary>
        /// 6F產線
        /// </summary>
        string SMT6C { get; }
        /// <summary>
        /// E04 + 1F + 8F
        /// </summary>
        string E04 { get; }
        /// <summary>
        /// E05 + 3F + 5F + 6F + 8F
        /// </summary>
        string E05 { get; }
    }

    public interface EquNoConfig
    {
        string PCBA { get; }
        string Box { get; }
        string AGV { get; }
        string Tower { get; }
    }

    public interface APIConfig
    {
        string IP { get; }
    }

    public interface DeviceConfig
    {
        string StockerID { get; }
        string Speed { get; }
    }

    public interface ForkSetupConfig
    {
        string S1 { get; }
        string S2 { get; }
        string S3 { get; }
        string S4 { get; }
    }

    public interface CV_PlcConfig
    {
        [Option(DefaultValue = 0)]
        int MPLCNo { get; }
        string MPLCIP { get; }

        [Option(DefaultValue = 0)]
        int MPLCPort { get; }

        [Option(DefaultValue = 0)]
        int MPLCTimeout { get; }

        [Option(DefaultValue = 0)]
        int WritePLCRawData { get; }

        [Option(DefaultValue = 0)]
        int UseMCProtocol { get; }

        [Option(DefaultValue = 0)]
        int InMemorySimulator { get; }

        [Option(DefaultValue = 10)]
        int CycleCount_Max { get; }
    }

    public interface StkPortConfig
    {
        [Option(DefaultValue = 1)]
        int Left1 { get; }

        [Option(DefaultValue = 2)]
        int Left2 { get; }

        [Option(DefaultValue = 3)]
        int Right1 { get; }

        [Option(DefaultValue = 4)]
        int Right2 { get; }
    }

    public interface StnNoConfig
    {
        string LO1_02 { get; }
        string LO1_07 { get; }
        string B1_062 { get; }
        string B1_067 { get; }
        string B1_142 { get; }
        string B1_147 { get; }
        string WaterLevel { get; }
    }
}
