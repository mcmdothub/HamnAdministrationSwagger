using HamnAdministration.Models;
using HamnAdministration.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HamnAdministration.Bestallningar
{
    public class ParkeringBestallHelper
    {
        private readonly DbContext _dbContext;
        private readonly BestallStoreService _bestallStoreService;
        private readonly AuthenticationService _authenticationService;

        public ParkeringBestallHelper(DbContext dbContext, BestallStoreService bestallStoreService, AuthenticationService authenticationService)
        {
            _dbContext = dbContext;
            _bestallStoreService = bestallStoreService;
            _authenticationService = authenticationService;
        }

        public void Helper(SlutParkeringBestall bestall)
        {
            var parkering = _dbContext.Set<Models.Parkering>()
                .FirstOrDefault(p => p.Namn == bestall.ParkeringsNamn);

            if (parkering == null)
            {
                throw new Exception($"Cannot find parking '{bestall.ParkeringsNamn}'.");
            }
            if (!parkering.IsOpened)
            {
                throw new Exception($"Parking '{bestall.ParkeringsNamn}' is already closed.");
            }

            parkering.IsOpened = false;
            _dbContext.SaveChanges();

            _bestallStoreService.Push(bestall);
        }

        public void Helper(SkapaParkeringBestall bestall)
        {
            var platser = Enumerable.Range(1, bestall.Kapacitet)
                .Select(n =>
                {
                    return new ParkeringsPlats
                    {
                        ParkeringsNamn = bestall.ParkeringsNamn,
                        Siffra = n,
                        IsFree = true
                    };
                })
                .ToList();

            var parkering = new Models.Parkering
            {
                Namn = bestall.ParkeringsNamn,
                IsOpened = true,
                Platser = platser
            };

            _dbContext.Add(parkering);
            _dbContext.SaveChanges();

            _bestallStoreService.Push(bestall);
        }

        public void Helper(LamnaParkeringPlatsBestall bestall)
        {
            var parkering = _dbContext.Set<Models.Parkering>()
                .FirstOrDefault(p => p.Namn == bestall.ParkeringsNamn);

            if (parkering == null)
            {
                throw new Exception($"Cannot find parking '{bestall.ParkeringsNamn}'.");
            }
            if (!parkering.IsOpened)
            {
                throw new Exception($"The parking '{bestall.ParkeringsNamn}' is closed.");
            }

            var parkeringsPlats = _dbContext.Set<ParkeringsPlats>()
                .FirstOrDefault(p => p.ParkeringsNamn == bestall.ParkeringsNamn && p.Siffra == bestall.PlatsNummer);

            if (parkeringsPlats == null)
            {
                throw new Exception($"Cannot find place #{bestall.PlatsNummer} in the parking '{bestall.ParkeringsNamn}'.");
            }
            if (parkeringsPlats.IsFree)
            {
                throw new Exception($"Parking place #{bestall.PlatsNummer} is still free.");
            }

            parkeringsPlats.IsFree = true;
            parkeringsPlats.UserId = null;
            _dbContext.SaveChanges();

            _bestallStoreService.Push(bestall);
        }

        public void Helper(OppenParkeringBestall bestall)
        {
            var parkering = _dbContext.Set<Models.Parkering>()
                .FirstOrDefault(p => p.Namn == bestall.ParkeringsNamn);

            if (parkering == null)
            {
                throw new Exception($"Cannot find parking '{bestall.ParkeringsNamn}'.");
            }
            if (parkering.IsOpened)
            {
                throw new Exception($"Parking '{bestall.ParkeringsNamn}' is already opened.");
            }

            parkering.IsOpened = true;
            _dbContext.SaveChanges();

            _bestallStoreService.Push(bestall);
        }

        public void Helper(TaParkeringsPlatsenBestall bestall)
        {
            var parkering = _dbContext.Set<Models.Parkering>()
                .FirstOrDefault(p => p.Namn == bestall.ParkeringsNamn);

            if (parkering == null)
            {
                throw new Exception($"Cannot find parking '{bestall.ParkeringsNamn}'.");
            }
            if (!parkering.IsOpened)
            {
                throw new Exception($"The parking '{bestall.ParkeringsNamn}' is closed.");
            }

            var parkeringsPlats = _dbContext.Set<ParkeringsPlats>()
                .FirstOrDefault(p => p.ParkeringsNamn == bestall.ParkeringsNamn && p.Siffra == bestall.PlatsNummer);

            if (parkeringsPlats == null)
            {
                throw new Exception($"Cannot find place #{bestall.PlatsNummer} in the parking '{bestall.ParkeringsNamn}'.");
            }
            if (!parkeringsPlats.IsFree)
            {
                throw new Exception($"Parking place #{bestall.PlatsNummer} is already taken.");
            }

            parkeringsPlats.IsFree = false;
            parkeringsPlats.UserId = _authenticationService.GetUserId();
            _dbContext.SaveChanges();

            _bestallStoreService.Push(bestall);
        }
    }
}
