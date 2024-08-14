using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Common.Mappings;
using Users.Application.Interfaces;
using Users.Persistence;
using Xunit;

namespace Users.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public Inno_ShopDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = Inno_ShopContextFactory.Create();
            var configProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(typeof(IUserDbContext).Assembly));
            });
            Mapper = configProvider.CreateMapper();
        }

        public void Dispose()
        {
            Inno_ShopContextFactory.Destroy(Context);
        }
    }
    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
