using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KyNiem50NamWeb.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(2)]
        public string Rolecode { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        // Thuộc tính điều hướng cho mối quan hệ 1-N (1 user có thể tạo nhiều bài post)
        public virtual ICollection<Post> Posts { get; set; }
    }
}
