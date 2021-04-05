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

		public ProjectService(IUnitOfWork uow, IMapper mapper)
		{
			_unitOfWork = uow;
			_mapper = mapper;
		}

		public async Task<ReadProjectDto> AddAsync(CreateProjectDto model)
		{
			var project = _mapper.Map<DAL.Entities.Project>(model);
			await _unitOfWork.ProjectRepository.AddAsync(project);
			await _unitOfWork.SaveAsync();
			var addedProject = await _unitOfWork.ProjectRepository
									.FindByCondition(p => p.Title == model.Title && p.ManagerId == model.ManagerId, false)
									.Include(p => p.Manager)
									.Include(p => p.Tasks)
									.FirstOrDefaultAsync();
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
			return _mapper.Map<ReadProjectDto>(updatedProject);
		}

		public async Task<ReadTaskDto> GetTaskInProjectAsync(int projectId, int taskId, string managerId)
		{
			var task = await _unitOfWork.TaskRepostitory.FindByCondition(t => t.Id == taskId && t.ProjectId == projectId, false)
															.Include(t => t.Developer)
															.Include(t => t.Status)
															.Include(t => t.Project)
															.ThenInclude(p => p.Manager)
															.FirstAsync();
			if (task == null)
				throw new TaskException($"Can't find task with id = {taskId}", HttpStatusCode.NotFound);
			if (task.Project.ManagerId != managerId)
				throw new TaskException($"Don't have permission to edit this project.", HttpStatusCode.Forbidden);
			return _mapper.Map<ReadTaskDto>(task);
		}
	}
}
