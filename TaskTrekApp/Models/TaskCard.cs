using System.ComponentModel.DataAnnotations;

namespace TaskTrekApp.Models
{
    public partial class TaskCard
    {
        [Key]
        public int TaskId { get; set; }
        public string UserId { get; set; }
        public int? TagId { get; set; }
        public int ColumnId {  get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public Tag? Tag { get; set; }
        public Column Column { get; set; } = null!;
        public User User { get; set; } = null!;

    }
}
