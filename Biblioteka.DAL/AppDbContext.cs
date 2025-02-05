﻿using Biblioteka.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.DAL;

public class AppDbContext : DbContext
{
    public DbSet<Avtor> Avtors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Polzak> Polzaks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {
        
    }

    public AppDbContext()
    {
        
    }
}
