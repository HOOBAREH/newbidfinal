
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace DataLayer
{

using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public partial class Image
{

    public int Id { get; set; }

    public string ImageURL { get; set; }

    public string ImageTitle { get; set; }

    public Nullable<int> ProductId { get; set; }

    public Nullable<int> UserId { get; set; }


        [ScriptIgnore]
        public virtual Product Product { get; set; }
        [ScriptIgnore]
        public virtual User User { get; set; }

}

}
