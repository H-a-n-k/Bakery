//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bakery.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class KhuyenMai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhuyenMai()
        {
            this.SanPhams = new HashSet<SanPham>();
        }
    
        public int MaKM { get; set; }
        public string TenKM { get; set; }
        public Nullable<double> TiLeKM { get; set; }
        public Nullable<System.DateTime> NgayBD { get; set; }
        public Nullable<System.DateTime> NgayKT { get; set; }
        public string pr_img { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
