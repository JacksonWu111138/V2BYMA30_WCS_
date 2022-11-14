using System;
using Mirle.Def;
using System.Windows.Forms;
using Mirle.Structure;
using Mirle.Structure.Info;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Mirle.DB.Object
{
    public class clsControllerID
    {
        public static string tower_ControllerID;
        public static string box1F_ControllerID;
        public static string box2F_ControllerID;
        public static string pCBA_ControllerID;
        public static string sMTC_ControllerID;
        public static string line_ControllerID;
        public static string sMT3C_ControllerID;
        public static string sMT5C_ControllerID;
        public static string sMT6C_ControllerID;
        public static string e04_ControllerID;
        public static string e05_ControllerID;
        public static void Initial(string Tower_ControllerID, string Box1F_ControllerID, string Box2F_ControllerID, string PCBA_ControllerID, string SMTC_ControllerID,
            string Line_ControllerID, string SMT3C_ControllerID, string SMT5C_ControllerID, string SMT6C_ControllerID, string E04_ControllerID, string E05_ControllerID)
        {
            tower_ControllerID = Tower_ControllerID;
            box1F_ControllerID = Box1F_ControllerID;
            box2F_ControllerID = Box2F_ControllerID;
            pCBA_ControllerID = PCBA_ControllerID;
            sMTC_ControllerID = SMTC_ControllerID;
            line_ControllerID = Line_ControllerID;
            sMT3C_ControllerID = SMT3C_ControllerID;
            sMT5C_ControllerID = SMT5C_ControllerID;
            sMT6C_ControllerID = SMT6C_ControllerID;
            e04_ControllerID = E04_ControllerID;
            e05_ControllerID = E05_ControllerID;
        }

        public static string GetTowerControllerID() => tower_ControllerID;
        public static string GetBox1FControllerID() => box1F_ControllerID;
        public static string GetBox2FControllerID() => box2F_ControllerID;
        public static string GetPCBAControllerID() => pCBA_ControllerID;
        public static string GetSMTCControllerID() => sMTC_ControllerID;
        public static string GetLineControllerID() => line_ControllerID;
        public static string GetSMT3CControllerID() => sMT3C_ControllerID;
        public static string GetSMT5CControllerID() => sMT5C_ControllerID;
        public static string GetSMT6CControllerID() => sMT6C_ControllerID;
        public static string GetE04ControllerID() => e04_ControllerID;
        public static string GetE05ControllerID() => e05_ControllerID;

    }
}
