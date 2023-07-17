using Project1.DAL.Repos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.DAL.Model
{
    [Table("UserTask")]
    public class UserTask:IEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        [Required]
        public int? UserId { get; set; }

        [ForeignKey("UserTaskStatusId")]
        public virtual UserTaskStatus? UserTaskStatus { get; set; }
        [Required]
        public int? UserTaskStatusId { get; set; }
        [Required]
        public string Subject { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public TimeSpan? StartTime { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? EndTime { get; set; }

        public ICollection<UserTaskTrack>? UserTaskTracks { get; set; }

        public override string ToString()
        {
            return $"{Subject} ${UserTaskStatus} ${StartDate} ${StartTime}";
        }
    }
}
