using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjThiTracNghiem
{
    /// <summary>
    /// Cấu hình kết nối đến CSDL
    /// </summary>
    public class DbConfig
    {
        /// <summary>
        /// Chuỗi kết nối đến cơ sở dữ liệu
        /// </summary>
        public static string ConnectionString { get { return ConfigurationManager.ConnectionStrings["MyDbContext"].ConnectionString; } }
    }
}
