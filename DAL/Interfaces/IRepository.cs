using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
	public interface IRepository<TEntity> where TEntity : class
	{
		IQueryable<TEntity> FindAll(bool trackChanges);
		IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges);
		Task<TEntity> GetByIdAsync(int id);
		Task AddAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
		Task DeleteAsync(TEntity entity);
	}
}
