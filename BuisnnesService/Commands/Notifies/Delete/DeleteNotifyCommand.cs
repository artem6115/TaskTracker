using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Notifies.Delete
{
    public class DeleteNotifyCommand : IRequest
    {
        public long Id { get; set; }
    }
}
