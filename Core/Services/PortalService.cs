using System.Collections.Generic;
using System.Linq;
using Core.Data;
using Core.Models;
using Unity.Attributes;

namespace Core.Services
{
    public interface IPortalService
    {
        IEnumerable<Portal> FindAll();
        Portal GetPortal(int id);
    }

    public class PortalService : IPortalService
    {
        [Dependency]
        public IPortalDao PortalDao { get; set; }
        public IEnumerable<Portal> FindAll()
        {
            return PortalDao.FindAll().OrderBy(c=> c.Portal_Nm);
        }

        public Portal GetPortal(int id)
        {
            return PortalDao.FindAll().AsQueryable().FirstOrDefault(x => x.Portal_Id == id);
        }

    }
}