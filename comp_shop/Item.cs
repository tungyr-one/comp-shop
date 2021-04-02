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
    
    public partial class Item
    {

        public override string ToString()
        {
            return Name;
        }

        public int ItemID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Seller { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> SupplierID { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
