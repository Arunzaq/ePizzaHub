﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace epizza.Domain.Models;

public partial class PaymentDetail
{
    public string Id { get; set; }

    public string TransactionId { get; set; }

    public decimal Tax { get; set; }

    public string Currency { get; set; }

    public decimal Total { get; set; }

    public string Email { get; set; }

    public string Status { get; set; }

    public Guid CartId { get; set; }

    public decimal GrandTotal { get; set; }

    public DateTime CreatedDate { get; set; }

    public int UserId { get; set; }

    public virtual Cart Cart { get; set; }

    public virtual User User { get; set; }
}