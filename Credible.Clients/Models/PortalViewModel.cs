
using System.Collections.Generic;
using Core.Models;

namespace Credible.Clients.Models
{
    public class PortalViewModel
    {
        public IEnumerable<Portal> Portals { get; set; }
        public int Total { get; set; }
    }

}
