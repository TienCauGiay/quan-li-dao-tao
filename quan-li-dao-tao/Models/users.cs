using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace quan_li_dao_tao.Models
{
    [Table("users")]
    public partial class users
    {
        [Key]
        [StringLength(100)]
        public string user_name { get; set; }

        [Required]
        [StringLength(100)]
        public string pass_word { get; set; }

        [Required]
        [StringLength(50)]
        public string role_id { get; set; }

        public int? status { get; set; }

        public virtual role Role { get; set; }
    }
}