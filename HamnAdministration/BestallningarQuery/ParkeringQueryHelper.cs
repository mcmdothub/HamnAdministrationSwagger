using HamnAdministration.Meddelanden.Responses;
using HamnAdministration.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HamnAdministration.BestallningarQuery
{
    public class ParkeringQueryHelper
    {
        private readonly DbContext _dbContext;

        public ParkeringQueryHelper(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ParkeringInformation> Helper(GetAllParkeringInformationQuery _)
        {
            var parkeringsplatser = _dbContext.Set<Models.Parkering>()
                .Include(p => p.Platser)
                .ToList();

            return parkeringsplatser.Select(p =>
            {
                return new ParkeringInformation
                {
                    Namn = p.Namn,
                    IsOpened = p.IsOpened,
                    MaxAntalPlatser = p.Platser.Count,
                    LedigaPlatser =
                        p.IsOpened
                            ? p.Platser.Where(pp => pp.IsFree).Count()
                            : 0
                };
            });
        }

        public ParkeringInformation Helper(GetParkeringInformationQuery query)
        {
            var parkering = _dbContext.Set<Models.Parkering>()
                .Include(p => p.Platser)
                .FirstOrDefault(p => p.Namn == query.ParkeringsNamn);

            if (parkering == null)
            {
                throw new Exception($"Cannot find parking '{query.ParkeringsNamn}'.");
            }

            return new ParkeringInformation
            {
                Namn = parkering.Namn,
                IsOpened = parkering.IsOpened,
                MaxAntalPlatser = parkering.Platser.Count,
                LedigaPlatser =
                    parkering.IsOpened
                        ? parkering.Platser.Where(pp => pp.IsFree).Count()
                        : 0
            };
        }

        public ParkeringPlatsInformation Helper(GetRandomLedigaPlatser _)
        {
            var random = new Random();

            var parkingPlatser = _dbContext.Set<ParkeringsPlats>()
                .Include(p => p.Parkering)
                .Where(p => p.Parkering.IsOpened && p.IsFree)
                .OrderBy(p => random.Next())
                .FirstOrDefault();

            return new ParkeringPlatsInformation
            {
                ParkeringsNamn = parkingPlatser.ParkeringsNamn,
                Siffra = parkingPlatser.Siffra
            };
        }

        public int Helper(GetTotalLedigaPlatserQuery _)
        {
            return _dbContext.Set<ParkeringsPlats>()
                .Where(p => p.Parkering.IsOpened && p.IsFree)
                .Count();
        }
    }
}
