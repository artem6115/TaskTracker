using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Interfaces
{
    public interface INotifyRepostitory
    {
        public Task<List<Notify>> GetAllNotifies();
        public Task<Notify> CreatelNotify(Notify notify);
        public Task ReadAllNotifies(DateTime time);
        public Task DeleteAllNotifies(DateTime time);
        public Task DeleteNotify(long Id);

    }
}
