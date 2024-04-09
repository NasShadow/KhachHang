using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login.Forms
{
    public partial class FormDoiMatKhau : Form
    {

        KhachHang khachhang = new KhachHang();
        static TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        //Kết nối
        static SqlConnection conn = TKBLL.Load();

        public string tkhoan { get; set; }

        public FormDoiMatKhau(string tk)
        {
            InitializeComponent();
            this.tkhoan = tk;

            //Lấy email của tài khoản
            Value_Email();

            //Hàm random mã
            Random_OTP();

            //Hàm gửi mail
            Send_OTP_Email();
        }


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
            to = email_send;
            pass = "ldjw pdqo tbnj rftk";
            //Nội dung gửi mail
            content = "Mã OTP để đổi mật khẩu là: " + otp_email;

            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(form);
            //Tiêu đề cho nội dung gửi
            mail.Subject = $"MÃ ĐỔI MẬT KHẨU CỦA TÀI KHOẢN {this.tkhoan} TẠI SUNNY BOOK";
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






        //Đóng form
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
        //Biến cục bộ mã otp
        static string otp_email = "";
        static string email_send = "";

        //Load Email
        private void Value_Email()
        {
            try
            {
                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT Email FROM dbo.KhachHang\r\nWHERE Ten_TK = N'{this.tkhoan}' OR Email = N'{this.tkhoan}'", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        email_send = "";
                        email_send += reader.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }



        //Button xác nhận
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string makhachhang = "";
                string matkhaunow = "";

                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT Ma_Khach_Hang, MatKhau FROM dbo.KhachHang\r\nWHERE Ten_TK = N'{this.tkhoan}' OR Email = N'{this.tkhoan}'", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if(reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        makhachhang += reader.GetString(0);
                        matkhaunow += reader.GetString(1);
                    }
                }

                khachhang.Ten_TK = this.tkhoan;
                khachhang.MatKhau = txt_matkhaumoi.Text;
                khachhang.Ma_Khach_Hang = makhachhang;
                khachhang.XacNhanMatKhau = txt_xacnhanmatkhau.Text;

                if (txt_matkhauhientai.Text == "")
                {
                    MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ THÔNG TIN!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (txt_matkhauhientai.Text != matkhaunow)
                {
                    MessageBox.Show("MẬT KHẨU HIỆN TẠI CỦA BẠN ĐÃ SAI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (txt_otp.Text != otp_email)
                {
                    MessageBox.Show("MÃ OTP CỦA BẠN ĐÃ SAI HÃY KIỂM TRA LẠI MÃ Ở GMAIL ĐĂNG KÝ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Update mật khẩu
                string check_value_1 = TKBLL.Change_PassKhachHang_Past(khachhang);

                switch (check_value_1)
                {
                    case "requeid_botrong":
                        {
                            MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ THÔNG TIN!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case "requeid_MatKhau":
                        {
                            MessageBox.Show("MẬT KHẨU CẦN CÓ KÝ TỰ ĐẶC BIỆT!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case "requeid_Xacnhanmatkhau":
                        {
                            MessageBox.Show("MẬT KHẨU CỦA BẠN KHÔNG TRÙNG KHỚP NHAU!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case "requeid_short":
                        {
                            MessageBox.Show("MẬT KHẨU CỦA BẠN QUÁ NGẮN!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("CẬP NHẬT THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private void guna2Button1_Click_2(object sender, EventArgs e)
        {
            //Lấy email của tài khoản
            Value_Email();

            //Hàm random mã
            Random_OTP();

            //Hàm gửi mail
            Send_OTP_Email();
        }

        //Form Load
        private void FormDoiMatKhau_Load_1(object sender, EventArgs e)
        {

        }
    }
}
