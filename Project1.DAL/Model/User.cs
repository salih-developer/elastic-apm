using Project1.DAL.Repos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.DAL.Model
{
    [Table("User")]
    public class User : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        public override string ToString()
        {
            return $"{Email}";
        }
    }
}
