using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DigitalCards.models;

namespace DigitalCards.Data
{
    public class DigitalCardsContext : DbContext
    {
        public DigitalCardsContext (DbContextOptions<DigitalCardsContext> options)
            : base(options)
        {
        }

        public DbSet<DigitalCards.models.Card> Card { get; set; } = default!;
    }
}
