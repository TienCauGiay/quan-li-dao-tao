using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace quan_li_dao_tao.Models
{
    [Table("score")]
    public partial class score
    {
        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string student_id { get; set; }

        [Required(ErrorMessage = "Mã môn học không được để trống")]
        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string subject_id { get; set; }

        public double? score_cc { get; set; }

        public double? score_kt { get; set; }

        public double? score_test { get; set; }

        public double? score_tb { get; set; }

        public virtual student STUDENT { get; set; }

        public virtual subject SUBJECT { get; set; }
    }
}