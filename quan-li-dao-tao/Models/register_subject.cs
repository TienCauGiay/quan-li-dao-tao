using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace quan_li_dao_tao.Models
{
    [Table("register_subject")]
    public partial class register_subject
    {
        [Key]
        [StringLength(20)]
        public string register_subject_id { get; set; }

        [Required]
        [StringLength(20)]
        public string student_id { get; set; }

        [StringLength(100)]
        public string time_learning { get; set; }

        [StringLength(100)]
        public string address_learn { get; set; }

        [StringLength(100)]
        public string subject_name { get; set; }

        [StringLength(100)]
        public string teacher_name { get; set; }

        public int? status { get; set; }

        public virtual student STUDENT { get; set; }
    }
}