using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;
using xcart.ViewModel;

namespace xcart.Services
{
    public interface IAwardHistoryService
    {
        Task<List<AwardHistoryViewModel>> GetAwardHistory(long UserId);

        Task<List<AwardHistory>> GetAllAwardHistory();

        Task<long> AddAwardHistory(AwardHistory award);
    }
}
