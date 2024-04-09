using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    // Đây là một lớp tĩnh có tên Program
    // Nó là điểm nhập chính cho ứng dụng

    public static class Program
    {
        /// <summary>
        /// Điểm nhập chính cho ứng dụng.
        /// </summary>

        [STAThread]
        public static void Main()
        {
            // Kiểm tra xem hệ điều hành có hỗ trợ DPI không
            // Nếu có, hãy gọi SetProcessDPIAware()
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            // Bật giao diện trực quan
            // Thiết lập văn bản hiển thị không tương thích
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Chạy Form4
            Application.Run(new Form1());
        }

        // DLLImport của hàm SetProcessDPIAware()
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }

}
