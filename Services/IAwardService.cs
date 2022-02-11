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
        // Get, Add, Update and Delete Awards
        public Task<List<Award>> GetAllAwards();
        public Task<Award> GetAwardById(int id);
        public Task<int> AddAward(Award award);
        public Task UpdateAward(Award award);
        public Task DeleteAward(int id);
    }
}
