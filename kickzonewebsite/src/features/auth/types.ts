// Types for Register request and response

export interface RegisterRequestDto {
  username: string;
  email: string;
  password: string;
  confirmPassword: string;
  fullName?: string;
}

export interface RegisterResponseDto {
  userId: number;
  username: string;
  email: string;
  message: string;
}

// Types for Login request and response
export interface LoginRequestDto {
  email: string;
  password: string;
}

export interface LoginResponseDto {
  userId: number;
  username: string;
  email: string;
  token: string;
}

// Types for Verify OTP request and response
export interface VerifyOtpRequestDto {
  email: string;
  otpCode: string;
}


// Types for Resend OTP request and response
export interface ResendOtpRequestDto {
  email: string;
}