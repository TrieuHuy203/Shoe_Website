import { useState } from "react";

import { login } from "../../features/auth/services/authService";

import type { LoginRequestDto } from "../../features/auth/types";

const LoginPage = () => {
  const [formData, setFormData] = useState<LoginRequestDto>({
    email: "",
    password: "",
  });

  // cập nhật input
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  // submit form
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const result = await login(formData);

      console.log(result);

      alert("Login success");

      // lưu token
      localStorage.setItem("token", result.token);
    } catch (error) {
      console.log(error);

      alert("Login failed");
    }
  };

  return (
    <div>
      <h1>Login</h1>

      <form onSubmit={handleSubmit}>
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

        <button type="submit">Login</button>
      </form>
    </div>
  );
};

export default LoginPage;
