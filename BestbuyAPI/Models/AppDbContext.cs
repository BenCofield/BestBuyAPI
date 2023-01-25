using System;
using Microsoft.EntityFrameworkCore;
using MySql.Data;
using MySql.EntityFrameworkCore;

namespace BestbuyAPI.Models
{
	public class AppDbContext : DbContext
	{
		public DbSet<Product> Products { get; set; }

		public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuiler)
		{
			modelBuiler.Entity<Product>(entity =>
			{
				entity.HasKey(e => e.ProductID);
				entity.Property(e => e.Name).IsRequired();
				entity.Property(e => e.Price).IsRequired();
				entity.Property(e => e.CategoryID).IsRequired();
			});
		}
	}
}

