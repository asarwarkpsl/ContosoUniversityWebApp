using ContosoUniversity.Data.Models.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Student
    {
        public int ID {get;set;}

        [Required]
        [StringLength(50,ErrorMessage = "Last name cannot longer than 50 characters")]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [Required]
        [Column("FirstName")]
        [StringLength(50,ErrorMessage = "First name cannot longer than 50 characters")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName 
        { 
            get 
            {
                return LastName + "," + FirstMidName;
            }
        }
        public User User { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}