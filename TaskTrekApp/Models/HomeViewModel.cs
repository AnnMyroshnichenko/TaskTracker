using Microsoft.AspNetCore.Identity;

namespace TaskTrekApp.Models
{
    public class HomeViewModel
    {
        public IdentityUser User { get; set; }
        public IEnumerable<TaskCard> Cards { get; set; } = new List<TaskCard>();
        public IEnumerable<Column> Columns { get; set; } = new List<Column>();
        public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
    }
}
