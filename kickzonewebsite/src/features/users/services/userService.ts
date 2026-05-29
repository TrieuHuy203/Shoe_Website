import api from "../../../services/api";

import type{
  UserResponseDto,
  CreateUserRequestDto,
  UpdateUserRequestDto,
  ChangeUserStatusRequestDto,
} from "../types";

// GET ALL USERS
export const getUsers = async (): Promise<UserResponseDto[]> => {
  const response = await api.get<UserResponseDto[]>(
    "/admin/users"
  );
  return response.data;
};

// GET USER BY ID
export const getUserById = async (
  id: number
): Promise<UserResponseDto> => {
  const response = await api.get<UserResponseDto>(
    `/admin/users/${id}`
  );
  return response.data;
};

// CREATE USER
export const createUser = async (
  data: CreateUserRequestDto
) => {
  const response = await api.post(
    "/admin/users",
    data
  );
  return response.data;
};

// UPDATE USER
export const updateUser = async (
  id: number,
  data: UpdateUserRequestDto
) => {
  const response = await api.put(
    `/admin/users/${id}`,
    data
  );
  return response.data;
};

// LOCK USER
export const lockUser = async (id: number) => {
  const response = await api.patch(
    `/admin/users/${id}/lock`
  );
  return response.data;
};

// UNLOCK USER
export const unlockUser = async (id: number) => {
  const response = await api.patch(
    `/admin/users/${id}/unlock`
  );
  return response.data;
};

// CHANGE STATUS
export const changeUserStatus = async (
  id: number,
  data: ChangeUserStatusRequestDto
) => {
  const response = await api.patch(
    `/admin/users/${id}/status`,
    data
  );
  return response.data;
};

// DELETE USER
export const deleteUser = async (id: number) => {
  const response = await api.delete(
    `/admin/users/${id}`
  );
  return response.data;
};