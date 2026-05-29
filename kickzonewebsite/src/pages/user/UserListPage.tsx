import { useEffect, useState } from "react";

import {
  getUsers,
  deleteUser,
  lockUser,
  unlockUser,
} from "../../features/users/services/userService";


import type { UserResponseDto } from "../../features/users/types";

const UserListPage = () => {

  const [users, setUsers] =
    useState<UserResponseDto[]>([]);

  // load users
  const fetchUsers = async () => {
    try {
      const data = await getUsers();
      setUsers(data);
    } catch (error) {
      console.log(error);
    }
  };

useEffect(() => {
  const fetchUsers = async () => {
    const res = await getUsers();
    setUsers(res);
  };

  fetchUsers();
}, []);

  // delete
  const handleDelete = async (id: number) => {
    await deleteUser(id);
    fetchUsers();
  };

  // lock
  const handleLock = async (id: number) => {
    await lockUser(id);
    fetchUsers();
  };

  // unlock
  const handleUnlock = async (id: number) => {
    await unlockUser(id);
    fetchUsers();
  };

  return (
    <div>

      <h1>User Management</h1>

      <table border={1}>
        <thead>
          <tr>
            <th>ID</th>
            <th>Username</th>
            <th>Email</th>
            <th>Status</th>
            <th>Actions</th>
          </tr>
        </thead>

        <tbody>
          {users.map((u) => (
            <tr key={u.userId}>
              <td>{u.userId}</td>
              <td>{u.username}</td>
              <td>{u.email}</td>
              <td>
                {u.isActive ? "Active" : "Locked"}
              </td>

              <td>
                <button
                  onClick={() => handleDelete(u.userId)}
                >
                  Delete
                </button>

                <button
                  onClick={() => handleLock(u.userId)}
                >
                  Lock
                </button>

                <button
                  onClick={() => handleUnlock(u.userId)}
                >
                  Unlock
                </button>
              </td>
            </tr>
          ))}
        </tbody>

      </table>

    </div>
  );
};

export default UserListPage;