using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace quan_li_dao_tao.Models
{
    [Table("subject")]
    public partial class subject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public subject()
        {
            SCOREs = new HashSet<score>();
        }

        [Key]
        [StringLength(20)]
        public string subject_id { get; set; }

        [StringLength(20)]
        public string semester_id { get; set; }

        [StringLength(100)]
        public string subject_name { get; set; }

        public int? number_tc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<score> SCOREs { get; set; }

        public virtual semester SEMESTER { get; set; }
    }
}