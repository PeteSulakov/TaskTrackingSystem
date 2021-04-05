using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly TaskDbContext _context;
		private ITaskRepository _taskRepository;
		private IProjectRepository _projectRepository;
		private IStatusRepository _statusRepository;

		public UnitOfWork(TaskDbContext context)
		{
			_context = context;
		}

		public ITaskRepository TaskRepostitory
		{
			get
			{
				if (_taskRepository == null)
					_taskRepository = new TaskRepository(_context);
				return _taskRepository;
			}
		}

		public IProjectRepository ProjectRepository
		{
			get
			{
				if (_projectRepository == null)
					_projectRepository = new ProjectRepository(_context);
				return _projectRepository;
			}
		}

		public IStatusRepository StatusRepository
		{
			get
			{
				if (_statusRepository == null)
					_statusRepository = new StatusRepository(_context);
				return _statusRepository;
			}
		}

		public async Task<int> SaveAsync() =>
			await _context.SaveChangesAsync();
	}
}
