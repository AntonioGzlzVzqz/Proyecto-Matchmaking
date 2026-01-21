using System;
using System.Collections.Generic;

namespace Riot.Data.Entities;

public partial class Player
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public int Mmr { get; set; }

    public bool IsInQueue { get; set; }

    public DateTime? LastLogin { get; set; }

    public int? CurrentMatchId { get; set; }

    public virtual Match? CurrentMatch { get; set; }
}
