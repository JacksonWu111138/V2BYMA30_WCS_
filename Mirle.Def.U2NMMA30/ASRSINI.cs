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

        [Option(Alias = "Client API")]
        SendAPI_Config Client_API { get; }

        [Option(Alias = "StnNo")]
        StnNoConfig StnNo { get; }

        [Option(Alias = "EquNo")]
        EquNoConfig EquNo { get; }

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

    public interface SendAPI_Config
    {
        string WES { get; }
        string AGV { get; }
        string Tower { get; }
        string Box { get; }
        string PCBA { get; }
        string SMTC { get; }
        string Line { get; }
        string SMT3C { get; }
        string SMT5C { get; }
        string SMT6C { get; }
        string E04 { get; }
        string E05 { get; }
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
        string A1_02 { get; }
        string A1_03 { get; }
        string A1_06 { get; }
        string A1_07 { get; }
        string A1_10 { get; }
        string A1_11 { get; }
        string A2_02 { get; }
        string A2_03 { get; }
        string A2_06 { get; }
        string A2_07 { get; }
        string A2_10 { get; }
        string A2_11 { get; }
        string A2_14 { get; }
        string A2_15 { get; }
        string A2_18 { get; }
        string A2_19 { get; }
        string A3_02 { get; }
        string A3_03 { get; }
        string A3_06 { get; }
        string A3_07 { get; }
        string A3_10 { get; }
        string A3_11 { get; }
        string A3_14 { get; }
        string A3_15 { get; }
        string A3_18 { get; }
        string A3_19 { get; }
        string S1_01 { get; }
        string S1_07 { get; }
        string S1_13 { get; }
        string S1_25 { get; }
        string S1_31 { get; }
        string S1_38 { get; }
        string S1_39 { get; }
        string S1_42 { get; }
        string S1_43 { get; }
        string S1_46 { get; }
        string S1_47 { get; }
        string S1_49 { get; }
        string S1_50 { get; }
        string S2_01 { get; }
        string S2_07 { get; }
        string S2_13 { get; }
        string S2_25 { get; }
        string S2_31 { get; }
        string S2_49 { get; }
        string S3_38 { get; }
        string S3_39 { get; }
        string S3_42 { get; }
        string S3_43 { get; }
        string S3_46 { get; }
        string S3_47 { get; }
        string S5_38 { get; }
        string S5_39 { get;}
        string A4_02 { get; }
        string A4_03 { get; }
        string A4_06 { get; }
        string A4_07 { get; }
        string A4_10 { get; }
        string A4_11 { get; }
        string A4_14 { get; }
        string A4_15 { get; }
        string A4_18 { get; }
        string A4_19 { get; }
        string E1_04 { get; }
        string E2_35 { get; }
        string E2_36 { get; }
        string E2_37 { get; }
        string E2_38 { get; }
        string E2_39 { get; }
        string E2_44 { get; }
        string M1_10 { get; }
        string M1_20 { get; }
        string M1_05 { get; }
        string M1_15 { get; }
        string WES_B800CV { get; }
        string WaterLevel { get; }
    }
}
