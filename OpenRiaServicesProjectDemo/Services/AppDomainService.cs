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

        public void InsertAppTable(AppTable appTable)
        {
            DbContext.AppTables.Add(appTable);
        }

        public void DeleteAppTable(AppTable appTable)
        {
            if (DbContext.Entry(appTable).State != System.Data.Entity.EntityState.Detached)
            {
                DbContext.Entry(appTable).State = System.Data.Entity.EntityState.Deleted;
            }
            else
            {
                DbContext.AppTables.Attach(appTable);
                DbContext.AppTables.Remove(appTable);
            }
        }

        public void UpdateAppTable(AppTable appTable)
        {
            DbContext.AppTables.AttachAsModified(appTable, ChangeSet.GetOriginal(appTable), DbContext);
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