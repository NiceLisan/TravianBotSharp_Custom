﻿namespace MainCore.Commands.Queries
{
    public class GetVillageName
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public GetVillageName(IDbContextFactory<AppDbContext> contextFactory = null)
        {
            _contextFactory = contextFactory ?? Locator.Current.GetService<IDbContextFactory<AppDbContext>>();
        }

        public string Execute(VillageId villageId)
        {
            using var context = _contextFactory.CreateDbContext();
            var villageName = context.Villages
                .Where(x => x.Id == villageId.Value)
                .Select(x => x.Name)
                .FirstOrDefault();
            return villageName;
        }
    }
}