using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class MemberObject
    {
        [Key]
        public int MemberId { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Required]
        public string Email { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(40)]
        [Required]
        public string CompanyName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        [Required]
        public string City { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        [Required]
        public string Country { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        [Required]
        public string Password { get; set; }
    }
}
