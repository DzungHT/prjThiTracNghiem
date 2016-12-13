namespace prjThiTracNghiem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CauHoiBaiThi")]
    public partial class CauHoiBaiThi
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BaiThiID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CauHoiID { get; set; }

        public int? ThuTu { get; set; }

        public virtual BaiThi BaiThi { get; set; }

        public virtual CauHoi CauHoi { get; set; }
    }
}
