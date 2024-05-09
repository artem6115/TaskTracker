using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Notifies.Create
{
    public class CreateNotifyCommand : IRequest<Notify>
    {
        public long UserId { get; set; }
        public string Message { get; set; }
    }
}
