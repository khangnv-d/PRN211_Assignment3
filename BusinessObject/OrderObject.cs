using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class OrderObject
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("MemberObject")]
        public int MemberId { get; set; }
        public MemberObject MemberObject { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime OrderDate { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime RequiredDate { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime ShippedDate { get; set; }

        [Column(TypeName = "money")]
        public decimal Freight { get; set; }
    }
}
