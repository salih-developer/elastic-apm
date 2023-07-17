using System.ComponentModel.DataAnnotations;

namespace Project1.DAL.Repos
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
    }
}
