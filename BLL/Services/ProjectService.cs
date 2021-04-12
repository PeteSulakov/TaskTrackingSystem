using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class ProjectService : IProjectService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;

		public ProjectService(IUnitOfWork uow, IMapper mapper, ILoggerManager logger)
		{
			_unitOfWork = uow;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<ReadProjectDto> AddAsync(CreateProjectDto model, string userId)
		{
			var project = _mapper.Map<DAL.Entities.Project>(model);
			project.ManagerId = userId;
			await _unitOfWork.ProjectRepository.AddAsync(project);
			await _unitOfWork.SaveAsync();
			var addedProject = await _unitOfWork.ProjectRepository
									.FindByCondition(p => p.Title == model.Title && p.ManagerId == userId, false)
									.Include(p => p.Manager)
									.Include(p => p.Tasks)
									.FirstOrDefaultAsync();
			_logger.LogInfo($"Created project with id = {addedProject.Id}.");
			return _mapper.Map<ReadProjectDto>(addedProject);
		}

		public async Task<ReadProjectDto> DeleteByIdAsync(int id, string userId)
		{
			var project = await _unitOfWork.ProjectRepository.GetProjectByIdWithDetailsAsync(id, false);
			if (project == null)
				throw new TaskException($"Can't find project with id = {id}", HttpStatusCode.NotFound);
			if (project.ManagerId != userId)
				throw new TaskException($"Don't have permission to delete project with id = {id}.", HttpStatusCode.Forbidden);

			await _unitOfWork.ProjectRepository.DeleteAsync(project);
			await _unitOfWork.SaveAsync();
			_logger.LogInfo($"Deleted project with id = {project.Id}.");
			return _mapper.Map<ReadProjectDto>(project);
		}

		public async Task<IEnumerable<ReadProjectDto>> GetAllAsync()
		{
			var projects = await _unitOfWork.ProjectRepository.GetAllProjectsAsync(false);
			return _mapper.Map<IEnumerable<ReadProjectDto>>(projects);
		}

		public async Task<IEnumerable<ReadProjectDto>> GetProjectsByManagerIdAsync(string managerId)
		{
			var projects = await _unitOfWork.ProjectRepository
									.FindByCondition(p=>p.ManagerId == managerId, false)
									.Include(p=>p.Manager)
									.Include(p=>p.Tasks)
									.ToListAsync();
			return _mapper.Map<IEnumerable<ReadProjectDto>>(projects);
		}

		public async Task<ReadProjectDto> GetByIdAsync(int id, string userId)
		{
			var project = await _unitOfWork.ProjectRepository.GetProjectByIdWithDetailsAsync(id, false);
			if (project == null)
				throw new TaskException($"Can't find project with id = {id}", HttpStatusCode.NotFound);
			if (project.ManagerId != userId)
				throw new TaskException($"Don't have permission to get project with id = {id}.", HttpStatusCode.Forbidden);
			return _mapper.Map<ReadProjectDto>(project);
		}

		public async Task<ReadProjectDto> UpdateAsync(int id, string userId, UpdateProjectDto model)
		{
			var project = await _unitOfWork.ProjectRepository.GetProjectByIdWithDetailsAsync(id, true);
			if (project == null)
				throw new TaskException($"Can't find project with id = {id}", HttpStatusCode.NotFound);
			if (project.ManagerId != userId)
				throw new TaskException($"Don't have permission to edit this project.", HttpStatusCode.Forbidden);
			project.Title = model.Title;
			project.EndDate = model.EndDate;
			await _unitOfWork.SaveAsync();

			var updatedProject = await _unitOfWork.ProjectRepository.GetProjectByIdWithDetailsAsync(id, false);
			_logger.LogInfo($"Updated project with id = {project.Id}.");
			return _mapper.Map<ReadProjectDto>(updatedProject);
		}

		public async Task<ReadTaskDto> GetTaskInProjectAsync(int projectId, int taskId, string managerId)
		{
			var task = await _unitOfWork.TaskRepostitory.FindByCondition(t => t.Id == taskId && t.ProjectId == projectId, false)
															.Include(t => t.Developer)
															.Include(t => t.Status)
															.Include(t => t.Project)
															.ThenInclude(p => p.Manager)
															.FirstOrDefaultAsync();
			if (task == null)
				throw new TaskException($"Can't find task with id = {taskId}", HttpStatusCode.NotFound);
			if (task.Project.ManagerId != managerId)
				throw new TaskException($"Don't have permission to edit this project.", HttpStatusCode.Forbidden);
			return _mapper.Map<ReadTaskDto>(task);
		}
	}
}
