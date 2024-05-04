using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Interfaces
{
    public interface IEpicRepository
    {
        public Task<Epic> CreateEpicAsync(Epic epic);
        public Task<Epic> UpdateEpicAsync(Epic epic);
        public Task DeleteEpicAsync(long Id);
        public Task<Epic> GetEpicAsync(long Id);
        public Task<List<Epic>> GetProjectEpicsAsync(long ProjectId);
    }
}
