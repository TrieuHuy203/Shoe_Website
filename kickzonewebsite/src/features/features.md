Features/ = " theo chức năng ( quan trọng nhất ) "

 Đây là cách làm dự án lớn chuẩn nhất 
 ví dụ :
    + auth (  đăng nhập  ) 
    + products (  sản phẩm  )
    + cart (  giỏ hàng  )

- Trong mỗi feature có gì ?????
ví dụ : features/auth/
    -> component/ ->> UI riêng của auth
    -> api/ ->> gọi api login
    -> hooks/ ->> logic riêng auth
    -> types ->> kiểu dữ liệu  auth
  
  hiểu đơn giản: 
  Mỗi features = 1 "mini app " bên trong app lớn
  