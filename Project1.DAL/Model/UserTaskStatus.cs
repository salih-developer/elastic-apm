using Project1.DAL.Repos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.DAL.Model
{
    [Table("UserTaskStatus")]
    public class UserTaskStatus : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
