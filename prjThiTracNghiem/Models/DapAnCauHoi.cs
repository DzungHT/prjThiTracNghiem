namespace prjThiTracNghiem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DapAnCauHoi")]
    public partial class DapAnCauHoi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DapAnCauHoi()
        {
            BaiThis = new HashSet<BaiThi>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DapAnCauHoiID { get; set; }

        public string NoiDungDapAn { get; set; }

        public bool? LaDapAnDung { get; set; }

        public int? CauHoiID { get; set; }

        public virtual CauHoi CauHoi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiThi> BaiThis { get; set; }
    }
}
