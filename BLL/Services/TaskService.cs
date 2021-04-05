using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BLL.Services
{
	public class TaskService : ITaskService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;

		public TaskService(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager)
		{
			_unitOfWork = uow;
			_mapper = mapper;
			_userManager = userManager;
		}

		public async System.Threading.Tasks.Task<ReadTaskDto> AddAsync(CreateTaskDto model)
		{
			model.StatusId = 1;
			var developer = await _userManager.FindByEmailAsync(model.DeveloperEmail);
			var project = await _unitOfWork.ProjectRepository.GetByIdAsync(model.ProjectId);

			if (developer == null)
				throw new TaskException($"Developer with email {model.DeveloperEmail} not found!", HttpStatusCode.NotFound);
			if (project == null)
				throw new TaskException($"Project with id = {model.ProjectId} not found!", HttpStatusCode.NotFound);

			model.DeveloperEmail = developer.Id;
			var task = _mapper.Map<Task>(model);
			await _unitOfWork.TaskRepostitory.AddAsync(task);
			await _unitOfWork.SaveAsync();

			var addedTask = await _unitOfWork.TaskRepostitory
								.FindByCondition(t => t.Title == model.Title && t.Description == model.Description
													&& t.DeveloperId == developer.Id, false)
								.Include(t => t.Developer)
								.Include(t => t.Project)
								.Include(t => t.Status)
								.FirstOrDefaultAsync();
			return _mapper.Map<ReadTaskDto>(addedTask);
		}

		public async System.Threading.Tasks.Task<ReadTaskDto> DeleteByIdAsync(int id, string userId)
		{
			var task = await _unitOfWork.TaskRepostitory.FindByCondition(t => t.Id == id, false)
														.Include(t=>t.Status)
														.Include(t=>t.Developer)
														.Include(t => t.Project)
														.ThenInclude(p => p.Manager)
														.FirstOrDefaultAsync();
			if(task == null)
				throw new TaskException($"Can't find task with id = {id}", HttpStatusCode.NotFound);
			if (task.Project.ManagerId != userId)
				throw new TaskException($"Don't have permission to delete task with id = {id}.", HttpStatusCode.Forbidden);

			await _unitOfWork.TaskRepostitory.DeleteAsync(task);
			await _unitOfWork.SaveAsync();
			return _mapper.Map<ReadTaskDto>(task);
		}

		public async System.Threading.Tasks.Task<IEnumerable<ReadTaskDto>> GetAllAsync()
		{
			var tasks = await _unitOfWork.TaskRepostitory.GetAllTasksAsync(false);
			return _mapper.Map<IEnumerable<ReadTaskDto>>(tasks);
		}

		public async System.Threading.Tasks.Task<ReadTaskDto> GetByIdAsync(int id, string userId)
		{
			var task = await _unitOfWork.TaskRepostitory.GetTaskByIdWithDetailsAsync(id, false);

			if (task == null)
				throw new TaskException($"Can't find task with id = {id}", HttpStatusCode.NotFound);
			if (task.DeveloperId != userId)
				throw new TaskException($"Don't have permission to get task with id = {id}.", HttpStatusCode.Forbidden);

			return _mapper.Map<ReadTaskDto>(task);
		}

		public async System.Threading.Tasks.Task<IEnumerable<ReadTaskDto>> GetTasksByDeveloperIdAsync(string developerId)
		{
			var tasks = await _unitOfWork.TaskRepostitory
									.FindByCondition(t => t.DeveloperId == developerId, false)
									.Include(t => t.Developer)
									.Include(t => t.Project)
									.Include(t => t.Status)
									.ToListAsync();
			return _mapper.Map<IEnumerable<ReadTaskDto>>(tasks);
		}

		public async System.Threading.Tasks.Task<ReadTaskDto> UpdateAsync(int id, string userId, UpdateTaskDto model)
		{
			var task = await _unitOfWork.TaskRepostitory.FindByCondition(t=>t.Id == id, true)
														.Include(t=>t.Developer)
														.Include(t=>t.Project)
														.ThenInclude(p=>p.Manager)
														.FirstAsync();
			if (task == null)
				throw new TaskException($"Can't find task with id = {id}", HttpStatusCode.NotFound);
			if (task.Project.ManagerId != userId)
				throw new TaskException($"Don't have permission to edit this task.", HttpStatusCode.Forbidden);

			var developer = await _userManager.FindByEmailAsync(model.DeveloperEmail);
			if (developer == null)
				throw new TaskException($"Developer with email {model.DeveloperEmail} not found!", HttpStatusCode.NotFound);
			task.Title = model.Title;
			task.Description = model.Description;
			task.IssueDate = model.IssueDate;
			task.DeadLine = model.DeadLine;
			task.DeveloperId = developer.Id;
			task.StatusId = model.StatusId;

			await _unitOfWork.SaveAsync();

			var updatedTask = await _unitOfWork.TaskRepostitory.GetTaskByIdWithDetailsAsync(id, false);
			return _mapper.Map<ReadTaskDto>(updatedTask);
		}

		public async System.Threading.Tasks.Task<ReadTaskDto> UpdateTaskStatus(int taskId, int statusId, string developerId)
		{
			var task = await _unitOfWork.TaskRepostitory.GetTaskByIdWithDetailsAsync(taskId, true);
			if (task == null)
				throw new TaskException($"Task with id = {taskId} not found.", HttpStatusCode.NotFound);
			if (task.DeveloperId != developerId)
				throw new TaskException($"Don't have permission to edit this task status.", HttpStatusCode.Forbidden);

			task.StatusId = statusId;
			await _unitOfWork.SaveAsync();
			return _mapper.Map<ReadTaskDto>(task);
		}
	}
}
