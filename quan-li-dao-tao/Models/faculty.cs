using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace quan_li_dao_tao.Models
{
    [Table("faculty")]
    public partial class faculty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public faculty()
        {
            CLASSes = new HashSet<classes>();
            TEACHERs = new HashSet<teacher>();
        }

        [Key]
        [Required(ErrorMessage = "Mã khoa không được để trống")]
        [StringLength(20)]
        public string faculty_id { get; set; }

        [Required(ErrorMessage = "Tên khoa không được để trống")]
        [StringLength(100)]
        public string faculty_name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<classes> CLASSes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<teacher> TEACHERs { get; set; }
    }
}