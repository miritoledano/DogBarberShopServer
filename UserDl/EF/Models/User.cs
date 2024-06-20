﻿using System;
using System.Collections.Generic;

namespace DogBarberShopDl.EF.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Queue> Queues { get; set; } = new List<Queue>();
}
