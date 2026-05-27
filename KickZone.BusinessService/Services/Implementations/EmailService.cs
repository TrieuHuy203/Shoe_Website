using KickZone.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace KickZone.Services.Implementations;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendOtpEmailAsync(
        string toEmail,
        string otpCode)
    {
        var emailSettings =
            _configuration.GetSection("EmailSettings");

        var senderEmail = emailSettings["Email"];

        var senderPassword = emailSettings["Password"];

        var host = emailSettings["Host"];

        var port = int.Parse(emailSettings["Port"]!);

        var email = new MimeMessage();

        // Tên hiển thị của người gửi
        email.From.Add(
            new MailboxAddress(
                "KickZone Support",
                senderEmail
            )
        );

        email.To.Add(
            MailboxAddress.Parse(toEmail)
        );

        email.Subject = "KickZone Email Verification";

        email.Body = new TextPart(TextFormat.Html)
        {
            Text = $@"
            <div style='font-family: Arial, sans-serif; padding: 20px;'>

                <h2 style='color: #007bff;'>
                    KickZone Verification
                </h2>

                <p>Hello,</p>

                <p>
                    Thank you for registering an account at 
                    <b>KickZone</b>.
                </p>

                <p>
                    Please use the OTP code below to verify your email:
                </p>

                <div style='
                    background-color: #007bff;
                    color: white;
                    display: inline-block;
                    padding: 15px 25px;
                    font-size: 30px;
                    font-weight: bold;
                    border-radius: 10px;
                    letter-spacing: 5px;
                    margin: 20px 0;
                '>
                    {otpCode}
                </div>

                <p>
                    This code will expire in 
                    <b>5 minutes</b>.
                </p>

                <p>
                    If you did not request this email,
                    please ignore it.
                </p>

                <hr/>

                <p style='font-size: 12px; color: gray;'>
                    © 2026 KickZone. All rights reserved.
                </p>

            </div>
            "
        };

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(host, port, false);

        await smtp.AuthenticateAsync(
            senderEmail,
            senderPassword
        );

        await smtp.SendAsync(email);

        await smtp.DisconnectAsync(true);
    }
}