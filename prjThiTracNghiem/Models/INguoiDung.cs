using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjThiTracNghiem.Models
{
    public interface INguoiDung
    {
        string HoTen { get; set; }
        TaiKhoan TaiKhoan { get; set; }
    }
}
