using Mirle.STKC.R46YP320.Model;

namespace Mirle.STKC.R46YP320.Repositories
{
    public interface ISnoctrlRepository
    {
        SnoctrlDTO GetBySnotype(string snotype);

        void Update(SnoctrlDTO dto);

        void Insert(SnoctrlDTO dto);
    }
}