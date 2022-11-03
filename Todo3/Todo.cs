using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Todo3;

namespace Todo3
{
    public enum EnumStatus
    {
        notstarted, ongoing, compleated
    }

    public class Todo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Desc { get; set; }

     //   public User user { get; set; }
        [ForeignKey("userId")]
        public int userId { get; set; }
        public DateTime Created { get; set; }
        public DateTime updated { get; set; }

        public EnumStatus Status { get; set; }
    }
}
