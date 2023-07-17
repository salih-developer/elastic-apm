using Project1.DAL.Repos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.DAL.Model
{
    [Table("UserTaskTrack")]
    public class UserTaskTrack : IEntity
    {
        [Key]
        public int Id { get; set; }

        [InverseProperty("UserTaskTracks")]
        public virtual UserTask UserTask { get; set; }
        [ForeignKey("UserTaskId")]
        public int? UserTaskId { get; set; }

        [ForeignKey("UserTaskStatusID")]
        public int? UserTaskStatusID { get; set; }

        public virtual UserTaskStatus UserTaskStatus { get; set; }

        public override string ToString()
        {
            return $"{UserTask.Subject} ${UserTask}";
        }
    }
}
    