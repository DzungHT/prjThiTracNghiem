namespace prjThiTracNghiem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SinhVien")]
    public partial class SinhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SinhVien()
        {
            BaiThis = new HashSet<BaiThi>();
        }

        public int SinhVienID { get; set; }

        [StringLength(50)]
        public string HoTen { get; set; }

        [StringLength(10)]
        public string NgaySinh { get; set; }

        [StringLength(10)]
        public string DiaChi { get; set; }

        [StringLength(10)]
        public string KhoaHoc { get; set; }

        public int TaiKhoanID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiThi> BaiThis { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
