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
            DapAnCauHois = new HashSet<DapAnCauHoi>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BaiThiID { get; set; }

        public int? DethiID { get; set; }

        public int? SinhVienID { get; set; }

        [StringLength(14)]
        public string NgayLamBai { get; set; }

        public virtual DeThi DeThi { get; set; }

        public virtual SinhVien SinhVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DapAnCauHoi> DapAnCauHois { get; set; }
    }
}
