import { useState } from "react";

import { register } from "../../features/auth/services/authService";

import type { RegisterRequestDto } from "../../features/auth/types";

const RegisterPage = () => {
  const [formData, setFormData] = useState<RegisterRequestDto>({
    username: "",
    email: "",
    password: "",
    confirmPassword: "",
    fullName: "",
  });

  // cập nhật input
  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement>, // Định nghĩa kiểu sự kiện cho input
  ) => {
    setFormData({
      // Cập nhật dữ liệu form khi người dùng nhập vào input
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  // submit form
  const handleSubmit = async (
    e: React.FormEvent<HTMLFormElement>, // Định nghĩa kiểu sự kiện cho form
  ) => {
    e.preventDefault();

    try {
      const result = await register(formData);
      localStorage.setItem("email", formData.email); // Lưu email vào localStorage để sử dụng sau này

      console.log(result);

      alert(result.message);
    } catch (error) {
      console.log(error);

      alert("Register failed");
    }
  };

  return (
    <div>
      <h1>Register</h1>
      {/*    

onSubmit={handleSubmit} : Gọi hàm handleSubmit khi người dùng submit form

<button type="submit">
          Register
        </button> */}
      <form onSubmit={handleSubmit}>
        <div>
          <input
            type="text"
            name="username"
            placeholder="Username"
            value={formData.username}
            onChange={handleChange} // Gọi hàm handleChange khi người dùng nhập vào input
          />
        </div>

        <div>
          <input
            type="text"
            name="fullName"
            placeholder="Full Name"
            value={formData.fullName}
            onChange={handleChange}
          />
        </div>

        <div>
          <input
            type="email"
            name="email"
            placeholder="Email"
            value={formData.email}
            onChange={handleChange}
          />
        </div>

        <div>
          <input
            type="password"
            name="password"
            placeholder="Password"
            value={formData.password}
            onChange={handleChange}
          />
        </div>

        <div>
          <input
            type="password"
            name="confirmPassword"
            placeholder="Confirm Password"
            value={formData.confirmPassword}
            onChange={handleChange}
          />
        </div>

        <button type="submit">Register</button>
      </form>
    </div>
  );
};

export default RegisterPage;
