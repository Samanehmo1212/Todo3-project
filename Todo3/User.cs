using System.ComponentModel.DataAnnotations;

namespace Todo3
{
    public class User
    {
        [Key]
        public int Id { get; set; }
       
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
}
