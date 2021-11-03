using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class ProductObject
    {
        [Key]
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(40)]
        public string ProductName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string Weight { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

    }
}
