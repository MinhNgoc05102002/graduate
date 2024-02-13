using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    /// <summary>
    /// Object return lại sau khi login thành công
    /// </summary>
    public class AccountDTO
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Avatar { get; set; }

        public string? Role { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? Status { get; set; }

        public int? HasWarning { get; set; }

        public string? RefreshToken { get; set; }

        public string? Token { get; set; }

        public string? PasswordText { get; set; }

    }
}
