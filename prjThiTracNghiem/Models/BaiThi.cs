namespace prjThiTracNghiem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiThi")]
    public partial class BaiThi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaiThi()
        {
            CauHoiBaiThis = new HashSet<CauHoiBaiThi>();
            DapAnCauHois = new HashSet<DapAnCauHoi>();
        }

        public int BaiThiID { get; set; }

        public int? DethiID { get; set; }

        public int? SinhVienID { get; set; }

        public DateTime? NgayLamBai { get; set; }

        public float? Diem { get; set; }

        public virtual DeThi DeThi { get; set; }

        public virtual SinhVien SinhVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CauHoiBaiThi> CauHoiBaiThis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DapAnCauHoi> DapAnCauHois { get; set; }
    }
}
