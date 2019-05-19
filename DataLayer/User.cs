
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
    
public partial class User
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public User()
    {

        this.Auction = new HashSet<Auction>();

        this.AuctionLogs = new HashSet<AuctionLogs>();

    }


    public int Id { get; set; }

    public string Fname { get; set; }

    public string Lname { get; set; }

    public string Mobile { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }

    public string Password { get; set; }

    public Nullable<int> VoucherBid { get; set; }

    public Nullable<int> RealBid { get; set; }

    public Nullable<System.DateTime> LastLogin { get; set; }

    public Nullable<int> CountryId { get; set; }

    public Nullable<bool> HideLocation { get; set; }

    public string Image { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Auction> Auction { get; set; }

    public virtual Countries Countries { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<AuctionLogs> AuctionLogs { get; set; }

}

}
