using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IAdminService
	{
		Task<IEnumerable<User>> GetUsersByRole(string roleName);
		Task<User> UpdateUserRole(string userName, string roleName);
		Task<User> DeleteUserRole(string userName, string roleName);

	}
}
