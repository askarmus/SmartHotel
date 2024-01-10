﻿using Service.Shared.Enum;
using SmartHotel.Core;

namespace Persistance.Entities;

public class Booking : EntityBase
{
    public int RoomId { get; set; }
    public DateTime BookingDate { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}
