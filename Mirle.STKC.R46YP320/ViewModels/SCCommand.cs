using Mirle.STKC.R46YP320.Model;
using Mirle.Def;

namespace Mirle.STKC.R46YP320.ViewModels
{
    public class SCCommand
    {
        public int CraneId { get; set; }
        public int ForkNumber { get; set; }
        public clsEnum.TaskMode TransferMode { get; set; }
        public string CstId { get; set; }
        public bool EnableBCRRead { get; set; }
        public int Source { get; set; }
        public int Destination { get; set; }
        public int TravelSpeed { get; set; }
        public int LifterSpeed { get; set; }
        public int RotateSpeed { get; set; }
        public int ForkSpeed { get; set; }
        public int CstType { get; set; }
    }
}