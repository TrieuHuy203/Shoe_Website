import { useState } from "react";

import { resendOtp, verifyOtp } from "../../features/auth/services/authService";

const VerifyOtpPage = () => {
  // state lưu otp
  const [otpCode, setOtpCode] = useState("");

  // hàm xử lý gửi lại otp
  const handleResendOtp = async () => {
    try {
      await resendOtp();
      alert("Đã gửi lại OTP");
    } catch (error) {
      console.log(error);
      alert("Gửi lại OTP thất bại");
    }
  };
  // lấy email đã lưu
  const email = localStorage.getItem("email");

  // cập nhật input otp
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setOtpCode(e.target.value);
  };

  // submit form
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const result = await verifyOtp({
        email: email || "",
        otpCode: otpCode,
      });

      console.log(result);

      alert("OTP verified successfully");
    } catch (error) {
      console.log(error);

      alert("OTP verification failed");
    }
  };

  return (
    <div>
      <h1>Verify OTP</h1>

      <form onSubmit={handleSubmit}>
        <div>
          <input
            type="text"
            placeholder="Enter OTP"
            value={otpCode}
            onChange={handleChange}
          />
        </div>

        <button type="submit">Verify OTP</button>
        <button type="button" onClick={handleResendOtp}>
          Gửi lại OTP
        </button>
      </form>
    </div>
  );
};

export default VerifyOtpPage;
