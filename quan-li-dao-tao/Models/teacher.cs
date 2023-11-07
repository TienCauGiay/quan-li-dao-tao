using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace quan_li_dao_tao.Models
{
    [Table("teacher")]
    public partial class teacher
    {
        [Key]
        [Required(ErrorMessage = "Mã giảng viên không được để trống")]
        [StringLength(20)]
        public string teacher_id { get; set; }

        [Required(ErrorMessage = "Mã khoa không được để trống")]
        [StringLength(20)]
        public string faculty_id { get; set; }

        [StringLength(100)]
        public string teacher_name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? birthday { get; set; }

        [StringLength(20)]
        public string gender { get; set; }

        [StringLength(200)]
        public string address { get; set; }

        [StringLength(20)]
        public string phone_number { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(100)]
        public string image { get; set; }

        public virtual faculty FACULTY { get; set; }
    }
}