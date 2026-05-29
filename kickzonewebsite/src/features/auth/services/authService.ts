import api from "../../../services/api";
import type {
  RegisterRequestDto,
  RegisterResponseDto,
  LoginRequestDto,
  LoginResponseDto,
  VerifyOtpRequestDto,
} from "../types";

// Service function to call Register API
export const register = async (
  data: RegisterRequestDto,
): Promise<RegisterResponseDto> => {
  const response = await api.post<RegisterResponseDto>("/auth/register", data);

  return response.data;
};

// Service function to call Login API

export const login = async (
  data: LoginRequestDto,
): Promise<LoginResponseDto> => {
  const response = await api.post<LoginResponseDto>(
    "/auth/login",
    data, // Dữ liệu đăng nhập được gửi đến API
  );

  return response.data; // Trả về dữ liệu phản hồi từ API
};

// note
/*
1. export : hàm cho phép các file khác gọi nó 
2. const register : Create a constant function named register
3.  async : hàm bất đồng bộ , cho phép sử dụng await bên trong nó
5. (data: RegisterRequestDto):  name : " RegisterRequestDto " Receive data that has been typed by TypeScript
6. : Promise<RegisterResponseDto> : name: RegisterResponseDto" The return data type has been defined by TypeScript
7.const response : Create a variable named "response",  This variable stores all returned data
8. api.post<RegisterResponseDto>(): "post" is method , "<RegisterResponseDto>" is the type of data expected to be returned from the API
9. "/auth/register" : API endpoint for registration

*/

// Service function to call Verify OTP API
export const verifyOtp = async (data: VerifyOtpRequestDto) => {
  const response = await api.post("/auth/otp/verify", data);

  return response.data;
};

// Service function to call Resend OTP API
export const resendOtp = async () => {
  const email = localStorage.getItem("email");
  if (!email) throw new Error("Không tìm thấy email trong localStorage");
  return await api.post("/auth/otp/resend", {
    email,
  });
};
