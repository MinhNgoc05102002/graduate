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
        public AccountDTO()
        {
        }

        public AccountDTO(string username, string email, string? avatar, DateTime? createdAt, int? countCredit, int? countClass, int? mark)
        {
            Username = username;
            Email = email;
            Avatar = avatar;
            CreatedAt = createdAt;
            CountCredit = countCredit;
            CountClass = countClass;
            Mark = mark;
        }

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

        // add thêm field để show dashboard 
        public int? CountCredit { get; set; }

        public int? CountClass { get; set; }

        // Điểm để đánh giá Tác giả hàng đầu
        public int? Mark { get; set; }
    }
}
