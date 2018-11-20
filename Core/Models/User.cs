
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    [Table("u_user")]
    public class User
    {
        [Key]
        public int User_Id { get; set; }
        public string First_Nm { get; set; }
        public string Last_Nm { get; set; }
    }

}
