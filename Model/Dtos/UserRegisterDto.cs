using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(12, MinimumLength = 8,ErrorMessage ="DNI No es Valido")]
        public string Dni { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "Password must have at least 8 characters")]
        public string Password { get; set; }
    }
}
