using System.Data;

namespace Mirle.STKC.R46YP320.Service
{
    public interface IDbService
    {
        bool IsConnected { get; }

        bool CheckConnection();

        IDbConnection GetDbConnection();
    }
}