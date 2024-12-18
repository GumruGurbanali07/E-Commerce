﻿using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Context
{
	public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Basket> Baskets { get; set; }
		public DbSet<BasketItem> BasketItems { get; set; }
	
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Order>()
				.HasKey(x => x.Id);

			modelBuilder.Entity<Order>()
				.HasIndex(x => x.OrderCode)
				.IsUnique(); // ordercode unikaldir 

			modelBuilder.Entity<Basket>()
			.HasOne(x => x.Order)
			.WithOne(y => y.Basket)
			.HasForeignKey<Order>(x => x.Id);//hem foreign hemde primary key olacaq


			base.OnModelCreating(modelBuilder);//identity teleb edir
		}
		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var datas = ChangeTracker.Entries<BaseEntity>();
			foreach (var data in datas)
			{
				_ = data.State switch
				{
					EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
					EntityState.Modified => data.Entity.UpdateDate = DateTime.UtcNow,
					_=>DateTime.UtcNow
				};
			}
			return await base.SaveChangesAsync(cancellationToken);

//Bu kod hər dəfə bir dəyişiklik bazaya yazılanda(yeni obyekt əlavə ediləndə və ya mövcud obyekt dəyişdiriləndə):
//Yeni əlavə edilən obyektlər üçün CreatedDate avtomatik olaraq indiki tarixə təyin edilir.
//Dəyişdirilən obyektlər üçün isə UpdateDate cari tarixə yenilənir.
		}
	}
}
