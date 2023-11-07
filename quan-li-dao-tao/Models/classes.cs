using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace quan_li_dao_tao.Models
{
    [Table("classes")]
    public partial class classes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public classes()
        {
            STUDENTs = new HashSet<student>();
        }

        [Key]
        [Required(ErrorMessage = "Mã lớp không được để trống")]
        [StringLength(20)]
        public string classes_id { get; set; }

        [StringLength(20)]
        public string faculty_id { get; set; }

        [StringLength(100)]
        public string classes_name { get; set; }

        public virtual faculty FACULTY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<student> STUDENTs { get; set; }
    }
}