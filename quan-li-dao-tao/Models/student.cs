using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace quan_li_dao_tao.Models
{
    [Table("student")]
    public partial class student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public student()
        {
            REGISTERSUBJECTs = new HashSet<register_subject>();
            SCOREs = new HashSet<score>();
        }

        [Key]
        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [StringLength(20)]
        public string student_id { get; set; }

        [Required(ErrorMessage = "Mã lớp không được để trống")]
        [StringLength(20)]
        public string classes_id { get; set; }

        [StringLength(30)]
        public string student_name { get; set; }

        [StringLength(20)]
        public string gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime? birthday { get; set; }

        [StringLength(100)]
        public string address { get; set; }

        [StringLength(20)]
        public string phone_number { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(100)]
        public string image { get; set; }

        [Column(TypeName = "date")]
        public DateTime? year_admission { get; set; }

        public virtual classes CLASS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<register_subject> REGISTERSUBJECTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<score> SCOREs { get; set; }
    }
}