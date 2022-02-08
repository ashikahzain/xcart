﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;
using xcart.ViewModel;

namespace xcart.Services
{
    public class AwardHistoryService:IAwardHistoryService
    {
        XCartDbContext db;

        public AwardHistoryService(XCartDbContext db)
        {
            this.db = db;
        }

        #region Add Award history
        public async Task<long> AddAwardHistory(AwardHistory award)
        {
            if (db != null)
            {

                await db.AwardHistory.AddAsync(award);
                await db.SaveChangesAsync();
                return award.Id;
            }
            return 0;
        }
        #endregion

        #region get all award
        public async Task<List<AwardHistory>> GetAllAwardHistory()
        {
            if (db != null)
            {
                return await db.AwardHistory.ToListAsync();
            }
            return null;
        }
        #endregion




        #region Get Award History of an Employee
        public async Task<List<AwardHistoryViewModel>> GetAwardHistory(int UserId)
        {
            if (db != null)
            {
                //join User and Point
                return await (from user in db.User
                              from award in db.AwardHistory
                              from awardName in db.Award

                              where award.Employee.Id == UserId
                              where award.Presentee.Id == user.Id
                              where awardName.Id == award.Award.Id
                              select new AwardHistoryViewModel
                              {
                                  AwardId = award.Id,
                                  AwardName = awardName.Name,
                                  Date = award.Date,
                                  Presenter = user.Name,
                                  Status = award.Status
                              }).ToListAsync();
            }
            return null;
        }
        #endregion

    }
}
