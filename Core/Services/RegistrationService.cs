using System.Collections.Generic;
using Core.Data;
using Unity.Attributes;
using Core.Models;

namespace Core.Services
{
    public interface IRegistrationService
    {
        IEnumerable<Registration> FindAll();
    }

    public class RegistrationService : IRegistrationService
    {
        [Dependency]
        public IRegistrationDao RegistrationDao { get; set; }
        public IEnumerable<Registration> FindAll()
        {
            return RegistrationDao.FindAll();
        }

    }
}