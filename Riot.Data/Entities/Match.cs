using System;
using System.Collections.Generic;

namespace Riot.Data.Entities;

public partial class Match
{
    public int Id { get; set; }

    public string Region { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public bool IsFinished { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
