using System;
using System.Collections.Generic;
using Core.Models;

namespace Credible.Clients.Models
{
    public class RegistrationViewModel
    {
        public int Course_Id { get; set; }
        public string Course_Nm { get; set; }
        public IEnumerable<RegistrationModel> Registrations { get; set; }
    }

    public class RegistrationModel
    {
        public int User_Id { get; set; }
        public string First_Nm { get; set; }
        public string Last_Nm { get; set; }
        public DateTime Registration_Dttm { get; set; }

        public string Registration_Dttm_Formatted
        {
            get { return this.Registration_Dttm.ToShortDateString(); }
        }
    }
}