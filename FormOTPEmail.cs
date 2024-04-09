using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Login
{
    public partial class FormOTPEmail : Form
    {
        KhachHang khachhang = new KhachHang();
        TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        public string tentaikhoan { get; set; }
        public string tenkhachhang { get; set; }
        public string email { get; set; }
        public string matkhau { get; set; }
        public string xacnhanmatkhau { get; set; }
        public string gioitinh { get; set; }
        public string dienthoai { get; set; }
        public string trangthai { get; set; }
        public FormOTPEmail(string tentk, string tenkhachhang, string email, string matkhau, string xacnhanmatkhau, string gioitinh, string dienthoai, string trangthai)
        {
            InitializeComponent();

            this.tentaikhoan = tentk;
            this.tenkhachhang = tenkhachhang;
            this.email = email;
            this.matkhau = matkhau;
            this.xacnhanmatkhau = xacnhanmatkhau;
            this.gioitinh = gioitinh;
            this.dienthoai = dienthoai;
            this.trangthai = trangthai;

            //Hàm random mã
            Random_OTP();

            //Hàm gửi mail
            Send_OTP_Email();
        }
        //Biến cục bộ chứa mã otp
        static string otp_email = "";

        //Hàm random code
        private void Random_OTP()
        {
            // Tạo một đối tượng Random để sinh số ngẫu nhiên
            Random random = new Random();

            // Tạo một chuỗi StringBuilder để xây dựng chuỗi ngẫu nhiên
            StringBuilder sb = new StringBuilder();

            // Tạo một mảng chứa tất cả các ký tự được cho phép
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            // Tạo một vòng lặp để thêm mỗi ký tự vào chuỗi ngẫu nhiên
            for (int i = 0; i < 6; i++)
            {
                // Lấy một ký tự ngẫu nhiên từ mảng characters
                char randomChar = characters[random.Next(characters.Length)];

                // Thêm ký tự ngẫu nhiên vào chuỗi
                sb.Append(randomChar);
            }

            otp_email = sb.ToString();
        }

        //Hàm gửi mã
        private void Send_OTP_Email()
        {
            string form, to, pass, content;
            form = "kienhttd00367@fpt.edu.vn";
            to = this.email;
            pass = "ldjw pdqo tbnj rftk";
            //Nội dung gửi mail
            content = "Mã OTP để đăng ký tài khoản là: " + otp_email;

            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(form);
            //Tiêu đề cho nội dung gửi
            mail.Subject = "MÃ ĐĂNG KÝ TÀI KHOẢN TẠI SUNNY BOOK";
            mail.Body = content;

            // khởi tạo nó với địa chỉ của máy chủ SMTP. Trong trường hợp này, địa chỉ máy chủ SMTP là "smtp.gmail.com"
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            //Bật tính năng SSL để tạo một kết nối an toàn với máy chủ SMTP. SSL (Secure Sockets Layer) cung cấp một lớp bảo mật cho việc truyền thông tin giữa ứng dụng và máy chủ.
            smtp.EnableSsl = true;
            //Thiết lập cổng kết nối SMTP. Trong trường hợp này, sử dụng cổng 587, được sử dụng phổ biến cho kết nối SMTP bảo mật.
            smtp.Port = 587;
            //Thiết lập phương thức gửi email là qua mạng (Network).
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //Cung cấp thông tin xác thực cho máy chủ SMTP. Đối tượng NetworkCredential được tạo với tên đăng nhập (form) và mật khẩu (pass).
            smtp.Credentials = new NetworkCredential(form, pass);

            try
            {
                smtp.Send(mail);
                Console.WriteLine("Gửi Mail Thành Công!!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (txt_nhapotp.Text != otp_email)
            {
                MessageBox.Show("MÃ OTP ĐÃ SAI BẠN SẼ KHÔNG THỂ TẠO TK !!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            khachhang.Ten_TK = this.tentaikhoan;
            khachhang.Ten_Khach_Hang = this.tenkhachhang;
            khachhang.Email = this.email;
            khachhang.MatKhau = this.matkhau;
            khachhang.XacNhanMatKhau = this.xacnhanmatkhau;
            khachhang.Gioi_Tinh = this.gioitinh;

            if (this.gioitinh == "Nam")
            {
                khachhang.Gioi_Tinh = "1";
            }
            else if (this.gioitinh == "Nữ")
            {
                khachhang.Gioi_Tinh = "0";
            }

            khachhang.DienThoai = this.dienthoai;
            khachhang.TrangThai = this.trangthai;


            string get = TKBLL.CheckInsertDangKy(khachhang);

            switch (get)
            {
                case "requeid_botrong":
                    {
                        MessageBox.Show("TÀI KHOẢN KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }

                case "requeid_Email":
                    {
                        MessageBox.Show("EMAIL SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }

                case "requeid_Ten_Tk":
                    {
                        MessageBox.Show("TÊN TÀI KHOẢN SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case "requeid_MatKhau":
                    {
                        MessageBox.Show("MẬT KHẨU SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case "requeid_Xacnhanmatkhau":
                    {
                        MessageBox.Show("XÁC NHẬN MẬT KHẨU SAI !!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case "requeid_DienThoai":
                    {
                        MessageBox.Show("SỐ ĐIỆN THOẠI SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("ĐĂNG KÝ THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Hide();
                        /*PanleMenu openMain = new PanleMenu();
                        openMain.ShowDialog();*/
                        break;
                    }
            }
        }

        private void btn_guilai_Click(object sender, EventArgs e)
        {
            //Hàm random mã
            Random_OTP();

            //Hàm gửi mail
            Send_OTP_Email();
        }
    }
}
