1. Tạo token khi đăng nhập 
        1. Tạo LoginRequestDto
        2. Tạo LoginResponseDto
        3. Tạo Interface cho JWT Service (*)
                using KickZone.DomainEntities.Entities;

                namespace KickZone.Services.Interfaces;

                public interface IJwtService
                {
                    string GenerateToken(User user);
                }

        4. Tạo JwtService

        (using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KickZone.DomainEntities.Entities;
using KickZone.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KickZone.Services.Implementations;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
})

5. Thêm Login vào IAuthService

6. Code Login trong AuthService

(using BCrypt.Net;
using KickZone.Contracts.DTOs.Auth;
using KickZone.Repositories.Interfaces;
using KickZone.Services.Interfaces;

namespace KickZone.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(
        IUserRepository userRepository,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        // tìm user theo email
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
        {
            throw new Exception("Email does not exist");
        }

        // kiểm tra password
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(
            request.Password,
            user.PasswordHash
        );

        if (!isPasswordValid)
        {
            throw new Exception("Password is incorrect");
        }

        // tạo token
        var token = _jwtService.GenerateToken(user);

        // trả dữ liệu
        return new LoginResponseDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            Token = token
        };
    }
})

7. Thêm API Login vào AuthController




/// cấu trúc phần controller
Controllers/
│
├── Auth/
│   ├── AuthController.cs
│   ├── OtpController.cs
│   ├── ExternalAuthController.cs
│
├── Products/
│   ├── ProductController.cs
│   ├── ProductVariantController.cs
│   ├── ProductImageController.cs
│   ├── BrandController.cs
│   ├── CategoryController.cs
│
├── Cart/
│   └── CartController.cs
│
├── Orders/
│   ├── OrderController.cs
│   ├── CheckoutController.cs
│
├── Payments/
│   ├── PaymentController.cs
│   ├── VNPayController.cs
│
├── Reviews/
│   └── ReviewController.cs
│
├── Wishlist/
│   └── WishlistController.cs
│
├── Users/
│   ├── ProfileController.cs
│   ├── AddressController.cs
│
├── Admin/
│   ├── DashboardController.cs
│   ├── AuditLogController.cs
│   ├── SystemSettingController.cs
│
├── Notifications/
│
├── Files/
│
└── Reports/



1. Notifications/
Đây là module xử lý:
gửi thông báo
email
push notification
realtime notification
Ví dụ trong KickZone:
đặt hàng thành công
OTP verify email
forgot password
voucher mới
đơn hàng đang giao
refund thành công
=> tất cả đều thuộc Notifications.


2. phần lưu ảnh , avatar
Có, nhưng miễn phí sẽ có giới hạn.
Thực tế người ta thường dùng free tier lúc phát triển hoặc làm đồ án.
Các loại storage phổ biến
1. Cloudinary (rất hợp cho đồ án)
Free:
lưu ảnh miễn phí
CDN sẵn
resize ảnh tự động
dễ tích hợp ASP.NET Core




////////////////// cấu trúc project react////////////
src/
├── assets/             # Tài nguyên tĩnh: hình ảnh, fonts, icons toàn cục
├── components/         # Các UI component dùng chung (Button, Input, Modal, Table)
├── features/           # Chứa các module/chức năng riêng biệt (VD: auth, products, profile)
│   ├── auth/           # Module Authentication
│   │   ├── components/ # Component chỉ dùng riêng cho Auth
│   │   ├── services/   # Gọi API (axios/fetch) của Auth
│   │   ├── hooks/      # Hook riêng cho Auth
│   │   └── types.ts    # TypeScript interface/type cho Auth
│   └── products/       # Module Sản phẩm
├── hooks/              # Custom hooks dùng chung cho toàn ứng dụng
├── layouts/            # Khung giao diện (Header, Sidebar, Footer, AuthLayout)
├── pages/              # Các trang chính (Home, Login, NotFound). Ghép từ Layout & Feature
├── routes/             # Cấu hình định tuyến (React Router)
├── services/           # Cấu hình Axios/Fetch toàn cục, quản lý API chung
├── store/              # Quản lý state toàn cục (Redux Toolkit, Zustand, Context)
├── styles/             # Global styles, Tailwind config, CSS variables
├── types/              # TypeScript global types/interfaces (dùng mọi nơi)
├── utils/              # Các hàm tiện ích (format date, validate, helpers)
├── App.tsx             # Component gốc
└── main.tsx            # Điểm khởi chạy của ứng dụng (ReactDOM.createRoot)
