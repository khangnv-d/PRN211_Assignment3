using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Models
{
    public class LoginRequest
    {
		[MaxLength(50)]
		[Required(ErrorMessage = "Your input can not be empty")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[MaxLength(20)]
		[Required(ErrorMessage = "Your input can not be empty")]
		[DataType(DataType.Password)]
		[MinLength(1)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
