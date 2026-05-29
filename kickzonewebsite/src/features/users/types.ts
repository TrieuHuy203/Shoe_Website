export interface UserResponseDto {
  userId: number;
  username: string;
  email: string;
  fullName?: string;
  phone?: string;
  gender?: string;
  isVerified: boolean;
  dateOfBirth?: string;
  avatarUrl?: string;
  isActive?: boolean;
  isDeleted: boolean;
  createdAt?: string;
  updatedAt?: string;
}

export interface CreateUserRequestDto {
  username: string;
  email: string;
  password: string;
  fullName?: string;
  phone?: string;
  gender?: string;
  dateOfBirth?: string;
}

export interface UpdateUserRequestDto {
  fullName?: string;
  phone?: string;
  gender?: string;
  dateOfBirth?: string;
  isActive?: boolean;
}

export interface ChangeUserStatusRequestDto {
  isActive: boolean;
}