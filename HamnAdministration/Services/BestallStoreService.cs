using HamnAdministration.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace HamnAdministration.Services
{
    public class BestallStoreService
    {
        private readonly DbContext _dbContext;
        private readonly AuthenticationService _authenticationService;

        public BestallStoreService(
            DbContext dbContext,
            AuthenticationService authenticationService
        )
        {
            _dbContext = dbContext;
            _authenticationService = authenticationService;
        }

        public void Push(object command)
        {
            _dbContext.Set<Bestall>().Add(
                new Bestall
                {
                    Type = command.GetType().Name,
                    Data = JsonConvert.SerializeObject(command),
                    CreatedAt = DateTime.Now,
                    UserId = _authenticationService.GetUserId()
                }
            );
            _dbContext.SaveChanges();
        }
    }
}
