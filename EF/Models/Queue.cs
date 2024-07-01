using System;
using System.Collections.Generic;

namespace DogBarberShopDl.EF.Models;

public partial class Queue
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public DateTime Date { get; set; }

    public string Hour { get; set; } = null!;

    public DateTime ProductionDate { get; set; }

    public virtual User? User { get; set; }
}
