using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IStatusService
	{
		Task<IEnumerable<ReadStatusDto>> GetAllAsync();
		Task<ReadStatusDto> GetByIdAsync(int id);
		Task<ReadStatusDto> AddAsync(CreateStatusDto model);
		Task<ReadStatusDto> UpdateAsync(int id, CreateStatusDto model);
		Task<ReadStatusDto> DeleteByIdAsync(int id);
	}
}
