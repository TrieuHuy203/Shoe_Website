import { Routes, Route } from "react-router-dom";

import RegisterPage from "./pages/auth/RegisterPage";
import LoginPage from "./pages/auth/LoginPage";
import VerifyOtpPage from "./pages/auth/VerifyOtpPage";
import UserListPage from "./pages/user/UserListPage";

function App() {
  return (
    <Routes>
      <Route path="/register" element={<RegisterPage />} />

      <Route path="/login" element={<LoginPage />} />

      <Route path="/verify-otp" element={<VerifyOtpPage />} />
      <Route path="/users" element={<UserListPage />} />
    </Routes>
  );
}

export default App;
