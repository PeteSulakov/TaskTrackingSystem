using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
	public interface IUnitOfWork
	{
		ITaskRepository TaskRepostitory { get; }
		IProjectRepository ProjectRepository { get; }
		IStatusRepository StatusRepository { get; }
		Task<int> SaveAsync();
	}
}
