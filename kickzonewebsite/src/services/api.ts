import axios from "axios"; // Thư viện để thực hiện các yêu cầu HTTP

const api = axios.create({   
    baseURL : "http://localhost:5112/api",}); // URL BE chạy
    
export default api;

    