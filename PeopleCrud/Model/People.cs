using System.ComponentModel.DataAnnotations;
namespace PeopleCrud.Model
{
    public class People
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Name must be of Atleast 2 Characters long.")]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; } 

        [Range(18, 60, ErrorMessage = "Your Age Must be Between 18 and 60.")]
        public int Age { get; set; }

        public People()
        {
            Id = Guid.NewGuid();
        }

    }
}
