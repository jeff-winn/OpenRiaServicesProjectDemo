using System.Diagnostics;
using System.Linq;
using OpenRiaServices.DomainServices.EntityFramework;
using OpenRiaServices.DomainServices.Hosting;
using OpenRiaServices.DomainServices.Server;
using OpenRiaServicesProjectDemo.Domain;

namespace OpenRiaServicesProjectDemo.Services
{
    [EnableClientAccess]
    public class AppDomainService : DbDomainService<AppDbContext>
    {
        public IQueryable<AppTable> GetAppTables()
        {
            return DbContext.AppTables;
        }

        [Invoke]
        public bool ExecuteCustomMethod(string value)
        {
            return true;
        }

        protected override void OnError(DomainServiceErrorInfo errorInfo)
        {
            if (errorInfo == null || errorInfo.Error == null)
            {
                return;
            }

            Debug.WriteLine(errorInfo.Error);
        }
    }
}