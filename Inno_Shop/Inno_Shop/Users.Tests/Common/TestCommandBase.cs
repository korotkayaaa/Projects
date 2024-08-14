using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Persistence;

namespace Users.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly Inno_ShopDbContext Context;
        public TestCommandBase()
        {
            Context = Inno_ShopContextFactory.Create();
        }
        public void Dispose()
        {
            Inno_ShopContextFactory.Destroy(Context);
        }
    }
}
