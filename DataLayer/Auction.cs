
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
    
public partial class Auction
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Auction()
    {

        this.AuctionLogs = new HashSet<AuctionLogs>();

    }


    public int Id { get; set; }

    public int ProductId { get; set; }

    public System.TimeSpan Auction_Time { get; set; }

    public int Close_Time { get; set; }

    public Nullable<int> Reserve_Price { get; set; }

    public Nullable<int> EndPrice { get; set; }

    public Nullable<int> CurrentBid_Id { get; set; }

    public Nullable<int> Current_UserId { get; set; }

    public bool StartStatus { get; set; }

    public bool IsClose { get; set; }



    public virtual User User { get; set; }

    public virtual Product Product { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<AuctionLogs> AuctionLogs { get; set; }

}

}
