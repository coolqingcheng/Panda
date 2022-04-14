using Microsoft.EntityFrameworkCore;
using Panda.Admin.Models;
using Panda.Entity.DataModels;

namespace Panda.Service.DashBoard;

public interface IDashBoardService
{
    Task<DashBoardStatisticModel> GetStatistic();
}

public class DashBoardService : IDashBoardService
{
    private readonly DbContext _dbContext;

    public DashBoardService(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DashBoardStatisticModel> GetStatistic()
    {
        var model = new DashBoardStatisticModel();

        model.PostCount = await _dbContext.Set<Posts>().CountAsync();

        return model;
    }
}