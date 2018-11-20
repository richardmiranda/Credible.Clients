
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    [Table("u_portal")]
    public class Portal
    {
        [Key]
        public int Portal_Id { get; set; }
        public string Portal_Nm { get; set; }

    }

}
