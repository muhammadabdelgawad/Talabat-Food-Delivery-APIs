using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Talabat.Infrastructure.Persistence._Data.Interceptors
{
    internal class CustomSaveChangesInterceptor : SaveChangesInterceptor
    {
        //private readonly ILoggedInUserService _loggedInUserService ;
        //public CustomSaveChangesInterceptor(ILoggedInUserService loggedInUserService)
        //{
        //    _loggedInUserService = loggedInUserService;
        //}

        //public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        //{
        //    UpdateEntities(eventData.Context);
        //    return base.SavingChanges(eventData, result);
        //}

    }
}
