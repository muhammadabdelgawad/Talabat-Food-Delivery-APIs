using Microsoft.EntityFrameworkCore.Diagnostics;
using Talabat.Application.Abstraction.Services;

namespace Talabat.Infrastructure.Persistence._Data.Interceptors
{
    internal class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly ILoggedInUserService _loggedInUserService;
        public AuditInterceptor(ILoggedInUserService loggedInUserService)
        {
            _loggedInUserService = loggedInUserService;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        private void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            var entries = context.ChangeTracker.Entries<IBaseAuditableEntity>()
              .Where(entity => entity.State is EntityState.Added or EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = _loggedInUserService.UserId!;
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                }

                entry.Entity.LastModifiedBy = _loggedInUserService.UserId!;
                entry.Entity.LastModifiedOn = DateTime.UtcNow;

            }
        }

        
    
    }
}
