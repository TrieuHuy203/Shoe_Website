using BCrypt.Net;
using KickZone.Contracts.DTOs.Auth;
using KickZone.DomainEntities.Entities;
using KickZone.Repositories.Interfaces;
using KickZone.Services.Interfaces;

namespace KickZone.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserOtpRepository _otpRepository;
    private readonly IEmailService _emailService;
    private readonly IJwtService _jwtService;
    private readonly IOtpService _otpService;

    public AuthService(
    IUserRepository userRepository,
    IUserOtpRepository otpRepository,
    IEmailService emailService,
    IJwtService jwtService,
    IOtpService otpService
)
{
    _userRepository = userRepository;
    _otpRepository = otpRepository;
    _emailService = emailService;
    _jwtService = jwtService;
    _otpService = otpService;
}

    // ================= REGISTER =================
    public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        // Check email
        var existingEmail = await _userRepository.GetByEmailAsync(request.Email);

        if (existingEmail != null)
        {
            throw new Exception("Email already exists");
        }

        // Check username
        var existingUsername = await _userRepository.GetByUsernameAsync(request.Username);

        if (existingUsername != null)
        {
            throw new Exception("Username already exists");
        }

        // Hash password
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // Create user
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHash,
            FullName = request.FullName,
            IsActive = false, // chưa verify email
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        // Save user
        await _userRepository.CreateAsync(user); // lưu user vào database

        
        await _otpService.SendOtpAsync(user.Email); // phần này gọi phương thức otp đã được xử lý bên otpservice

    //     // ================= CREATE OTP =================
    //     var otpCode = new Random().Next(100000, 999999).ToString(); // tạo mã OTP ngẫu nhiên 6 chữ số

    //     var otp = new UserOTP // tạo đối tượng OTP để lưu vào database
    //     {
    //         UserID = user.UserId,
    //         OTPCode = otpCode,
    //         ExpiredAt = DateTime.UtcNow.AddMinutes(5),
    //         IsUsed = false,
    //         CreatedAt = DateTime.UtcNow
    //     };

    //     // Save OTP
    //     await _otpRepository.CreateAsync(otp);

    //     // ================= SEND EMAIL =================
    //     string subject = "KickZone Email Verification"; // tiêu đề email

    //     string body = $@"
    //         <h2>Welcome to KickZone</h2>
    //         <p>Your OTP code is:</p>
    //         <h1>{otpCode}</h1>
    //         <p>This code will expire in 5 minutes.</p>
    //     ";

    //     await _emailService.SendOtpEmailAsync( // Gửi email OTP
    //         user.Email, // láy email người đăng ký để gửi OTP
    //         otpCode // gửi mã OTP trong email
    //     );

    //     // Response trả về cho fe dữ liệu 
        return new RegisterResponseDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            Message = "Register successfully. Please verify OTP sent to your email."
        };
     }

    // ================= LOGIN =================
    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        // Find user
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
        {
            throw new Exception("Email does not exist");
        }

        // Check password
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(
            request.Password,
            user.PasswordHash
        );

        if (!isPasswordValid)
        {
            throw new Exception("Password is incorrect");
        }

        // Check verify email
        if (!(user.IsActive ?? false))
        {
            throw new Exception("Email is not verified");
        }

        // Generate token
        var token = _jwtService.GenerateToken(user);

        // Response
        return new LoginResponseDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            Token = token
        };
    }
}