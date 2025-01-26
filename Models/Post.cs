using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KyNiem50NamWeb.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(2000)]
        public string Title { get; set; }

        public string ContentPost { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(200)]
        public string? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(200)]
        public string? ModifiedBy { get; set; }

        public string? Image { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        // Các khóa ngoại tham chiếu đến User
        public int CreatedByUserID { get; set; }
        public int? ModifiedByUserID { get; set; }

        // Các thuộc tính điều hướng
        [ForeignKey("CreatedByUserID")]
        public virtual User CreatedByUser { get; set; }
    }
}
