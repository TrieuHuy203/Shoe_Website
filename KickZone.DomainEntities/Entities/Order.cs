using System;
using System.Collections.Generic;

namespace KickZone.DomainEntities.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public decimal? ShippingFee { get; set; }

    public int? PaymentMethodId { get; set; }

    public int? ShippingInfoId { get; set; }

    public string? Note { get; set; }

    public string? CancelReason { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? VoucherId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; } = new List<OrderStatusHistory>();

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    public virtual ShippingInfo? ShippingInfo { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Voucher? Voucher { get; set; }
}
