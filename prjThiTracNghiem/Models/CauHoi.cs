namespace prjThiTracNghiem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CauHoi")]
    public partial class CauHoi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CauHoi()
        {
            CauHoiBaiThis = new HashSet<CauHoiBaiThi>();
            DapAnCauHois = new HashSet<DapAnCauHoi>();
            DeThis = new HashSet<DeThi>();
        }

        public int CauHoiID { get; set; }

        public string NoiDung { get; set; }

        public int? GiaoVienID { get; set; }

        public int? ChuongID { get; set; }

        public int? DoKhoID { get; set; }

        public bool? CoNhieuDapAnDung { get; set; }

        public virtual Chuong Chuong { get; set; }

        public virtual DoKho DoKho { get; set; }

        public virtual GiaoVien GiaoVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CauHoiBaiThi> CauHoiBaiThis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DapAnCauHoi> DapAnCauHois { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeThi> DeThis { get; set; }
    }
}
