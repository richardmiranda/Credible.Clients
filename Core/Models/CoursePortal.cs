
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    [Table("u_course_portal")]
    public class CoursePortal 
    {
        [Key]
        public int Course_Portal_Id { get; set; }
        public string Course_Portal_Nm { get; set; }

        public int Portal_Id { get; set; }
        public virtual Portal Portal { get; set; }
    }

}