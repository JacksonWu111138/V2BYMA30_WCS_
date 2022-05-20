namespace Mirle.STKC.R46YP320.Model
{
    public enum CraneSpeedType
    {
        ByHour = 0,
        ByCmd = 1,
        ByCSTType = 2
    }

    public enum AlarmLevel
    {
        Alarm = 0,
        UnitAlarm = 1,
        Warning = 2,
    }

    public enum AlarmStates
    {
        Set = 0,
        Cleared = 1,
        SetCleared = 2,
    }

    public enum AlarmTypes
    {
        LCS = 1,
        Stocker = 2,
        IOPort = 3,
        FFU = 4,
    }

    public enum BCRReadStatus
    {
        Success = 0,
        Failure = 1,
        Mismatch = 2,
        NoCST = 3,

        None = 9,
    }

    public enum FFUModeType
    {
        Mode1 = 1,
        Mode2 = 2,
        Mode3 = 3,
    }

    public enum FFURemoteType
    {
        Local = 0,
        Remote = 1,
    }

    public enum FinishLocationState
    {
        OnSource,
        OnDestination,
        OnCrane,
    }

    public enum LogState
    {
        Disable = 0,
        DB = 1,
        Txt = 2,
    }

    public enum MileageType
    {
        None = 0,
        Travel = 1,
        Lifter = 2,
        Fork = 3,
        Rotate = 4,
    }

    public enum PortType
    {
        EQ = 1,
        IO = 2,
        Lifter = 3,
    }

    public enum ShelfEnable
    {
        Disable = 'N',
        Enable = 'Y',
    }

    public enum Enable
    {
        Disable = 'N',
        Enable = 'Y',
    }

    public enum ShelfType
    {
        None = 0,
        Shelf = 1,
        Port = 2,
        Crane = 3,
    }

    public enum ShelfState
    {
        EmptyShelf = 'N',
        Stored = 'S',
        StorageInReserved = 'I',
        StorageOutReserved = 'O',
    }

    public enum SNOType
    {
        CommandID = 1,
        TaskNo = 2,
    }

    public enum TaskCmdState
    {
        Initialize = 0,
        STKCQueue = 1,
        CheckCMDFail_LCS = 2,
        WriteCMD2PLC = 3,
        CheckCMDFail_PLC = 4,
        Cycle1Start = 5,
        Active = 6,
        AtSource = 7,
        ScanComplete = 8,
        Fork1Start = 9,
        CSTPresentOnCrane = 10,
        Cycle2Start = 11,
        AtDestination = 12,
        Fork2Start = 13,
        CSTPresentOffCrane = 14,
        Finish = 15,
        AbnormalFinish = 16,
        CreateAltCmdFinish = 17,
        AbortCancel = 18,
        AlternateCmd = 19,
        NeedRollback = 20,
    }

    public enum TransferMode
    {
        MOVE = 1,
        FROM = 2,
        TO = 3,
        FROM_TO = 4,
        SCAN = 7,
    }

    public enum TransferState
    {
        Queue = 0,
        Initialize = 1,
        Transferring = 2,
        Pasued = 3,
        Canceling = 4,
        Aborting = 5,
        Complete = 6,
        UpdateOK_Cancel = 7,
        UpdateOK_Abort = 8,
        UpdateOK_Complete = 9,
        UpdateFail = 10,
        CommandFormatError = 11,
        UpdateOK_AutoTimeout = 12,
    }

    public enum TaskState
    {
        Queue = 0,
        Initialize = 1,
        Transferring = 2,
        Complete = 3,
        UpdateOK = 4,
    }

    public enum UnitType
    {
        RM = 1,
        TRU = 2,
        Vehicle = 3,
        Lifter = 4,
    }

    public enum CarrierState
    {
        None = 0,
        WaitIn = 1,
        Transfering = 2,
        StoreCompleted = 3,
        StoreAlternate = 4,
        WaitOut = 5,
        WaitOutLP = 9,
        Installed = 6,
    }

    public enum PortLocationType
    {
        None = 0,
        EQPort_CassetteInOut = 1,
        AutoPort = 2,
        MGVPort = 3,
        EQPort = 4,
        Crane = 5,
        ViewPort = 6,
        TPort = 7,
        MoveCarCPort = 8,
        UConv = 9,
        RGV = 10,
        SharePort = 11,
    }

    public enum ZoneType
    {
        Shelf = 1,
        Port = 2,
        Other = 3,
        Handoff = 9,
    }

    public enum LocateCraneNo
    {
        Crane1 = 1,
        Crane2 = 2,
        Both = 3,
    }

    public enum AbnormalCSTIDType
    {
        Success = 0,
        Failure = 1,
        Duplicate = 2,
        Mismatch = 3,
        NoCarrier = 4,
        DoubleStorage = 5,
        EmptyRetrieval = 6,
    }

    public enum TransferModeType
    {
        None = 0,
        ShelfToShelf = 11,
        ShelfToPort = 12,
        ShelfToRM = 13,
        PortToShelf = 21,
        PortToPort = 22,
        PortToRM = 23,
        RMToShelf = 31,
        RMToPort = 32,

        Move = 41,
        Scan = 51,
        Escape = 61,
    }

    public enum Direction
    {
        Both = 0,
        OnlyIntput = 1,
        OnlyOutput = 2,
    }

    public enum HandoffDirection
    {
        Both = 0,
        Crane1ToCrane2 = 1,
        Crane2ToCrane1 = 2,
    }
}