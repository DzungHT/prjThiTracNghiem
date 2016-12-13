namespace prjThiTracNghiem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CauHoiThi")]
    public partial class CauHoiThi
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DeThiID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CauhoiID { get; set; }

        public int? ThuTu { get; set; }

        public virtual CauHoi CauHoi { get; set; }

        public virtual DeThi DeThi { get; set; }
    }
}
