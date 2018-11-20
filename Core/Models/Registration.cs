using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    [Table("u_registration")]
    public class Registration
    {
        [Key]
        public int Registration_Id { get; set; }
        public DateTime Registration_Dttm { get; set; }

        public int Course_Portal_Id { get; set; }
        public int User_Id { get; set; }
        public virtual User User { get; set; }
        public virtual CoursePortal CoursePortal { get; set; }
    }

}