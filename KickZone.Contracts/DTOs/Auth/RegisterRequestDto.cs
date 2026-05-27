//  Dto nhận dữ liệu đăng ký người dùng từ client gửi lên 
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KickZone.Contracts.DTOs.Auth
{
     
    public class RegisterRequestDto
    {
        [Required] // Bắt buộc phải có
        [StringLength(100, MinimumLength = 3)] // Độ dài username từ 3 đến 100 ký tự
        public string Username { get; set; } =  null !;

        [Required]
        [EmailAddress] // Kiểm tra định dạng email
        public string Email { get; set; } =  null !;

        [Required]
        [MinLength(6)] // Mật khẩu tối thiểu 6 ký tự
        public string Password { get; set; } =  null !;

        [Required]
        [Compare("Password")] // So sánh với trường Password
        public string ConfirmPassword { get; set; } =  null !;

        public string? FullName { get; set; }

    }
}