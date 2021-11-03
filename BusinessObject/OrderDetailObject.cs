using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class OrderDetailObject
    {
        [Key]
        public int OrderDetailId { get; set; }

        [ForeignKey("OrderObject")]
        public int OrderId { get; set; }
        public OrderObject OrderObject { get; set; }

        [ForeignKey("ProductObject")]
        public int ProductId { get; set; }
        public ProductObject ProductObject { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public float Discount { get; set; }

    }
}
