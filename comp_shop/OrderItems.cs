//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace comp_shop
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderItems
    {
        public int ItemID { get; set; }
        public int OrderID { get; set; }
        public int ItemsQuantity { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
    }
}
