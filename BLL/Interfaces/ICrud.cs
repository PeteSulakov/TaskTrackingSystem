using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface ICrud<TCreateDto, TReadDto, TUpdateDto> where TReadDto : class
													 where TCreateDto : class
													where TUpdateDto : class
	{
		Task<IEnumerable<TReadDto>> GetAllAsync();
		Task<TReadDto> GetByIdAsync(int id, string userId);
		Task<TReadDto> AddAsync(TCreateDto model);
		Task<TReadDto> UpdateAsync(int id, string userId, TUpdateDto model);
		Task<TReadDto> DeleteByIdAsync(int id, string userId);
	}
}
