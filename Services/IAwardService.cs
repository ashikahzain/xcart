using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;
using xcart.ViewModel;

namespace xcart.Services
{
    public interface IAwardService
    {
        public Task<List<Award>> GetAllAwards();
        public Task<int> AddAward(Award award);
        public Task UpdateAward(Award award);
        public Task DeleteAward(int id);
    }
}
