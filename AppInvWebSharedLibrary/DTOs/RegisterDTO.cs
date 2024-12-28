using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInvWebSharedLibrary.DTOs
{
    public class RegisterDTO : LoginDTO
    {
        [Required]
        public string? Name { get; set; }

        [Required, Compare(nameof(Password)), DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        public string? Role { get; set; }
    }
}
