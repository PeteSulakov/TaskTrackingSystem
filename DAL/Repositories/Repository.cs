using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public abstract class Repository<T> : IRepository<T> where T : class
	{
		protected TaskDbContext TaskDbContext;

		protected Repository(TaskDbContext taskDbContext)
		{
			TaskDbContext = taskDbContext;
		}

		public IQueryable<T> FindAll(bool trackChanges) =>
			!trackChanges ?
				TaskDbContext.Set<T>()
					.AsNoTracking() :
				TaskDbContext.Set<T>();

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
			!trackChanges ?
				TaskDbContext.Set<T>()
					.Where(expression)
					.AsNoTracking() :
				TaskDbContext.Set<T>()
					.Where(expression);

		public async Task AddAsync(T entity) =>
			await TaskDbContext.Set<T>().AddAsync(entity);

		public async Task DeleteAsync(T entity) =>
			await Task.Run(() => 
			{
				TaskDbContext.Set<T>().Remove(entity);
			});

		
		public async Task<T> GetByIdAsync(int id) =>
			await TaskDbContext.Set<T>().FindAsync(id);

		public async Task UpdateAsync(T entity) =>
			await Task.Run(() =>
			{
				TaskDbContext.Set<T>().Update(entity);
			});
	}
}
