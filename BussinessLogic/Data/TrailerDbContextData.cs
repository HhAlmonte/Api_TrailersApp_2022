using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Data
{
    public class TrailerDbContextData
    {
        public static async Task SeedTrailerAsync(TrailerDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Trailers.Any())
                {
                    
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<TrailerDbContextData>();
                logger.LogError(ex.Message);
            }
        }
    }
}
