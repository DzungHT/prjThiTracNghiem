namespace prjThiTracNghiem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DeThi")]
    public partial class DeThi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DeThi()
        {
            BaiThis = new HashSet<BaiThi>();
            CauHois = new HashSet<CauHoi>();
        }

        public int DeThiID { get; set; }

        [StringLength(50)]
        public string MaDeThi { get; set; }

        public int? ThoiGian { get; set; }

        public int? DotThiID { get; set; }

        public int? HocPhanID { get; set; }

        public bool? HienThi { get; set; }

        [StringLength(100)]
        public string TenDeThi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiThi> BaiThis { get; set; }

        public virtual DotThi DotThi { get; set; }

        public virtual HocPhan HocPhan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CauHoi> CauHois { get; set; }
    }
}
