using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Panda.Admin.Repositorys;
using Panda.Admin.Services.Account;

namespace Panda.Admin;

public static class PandaAdminExtensions
{
    public static void AddAdmin(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped(typeof(AccountRepository<>));
        serviceCollection.AddScoped(typeof(IAccountService<>),
            typeof(AccountService<>));
    }
}