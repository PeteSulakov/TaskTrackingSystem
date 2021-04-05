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
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class StatisticService : IStatisticService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public StatisticService(IUnitOfWork uow, IMapper mapper) 
		{
			_unitOfWork = uow;
			_mapper = mapper;
		}

		public async Task<ProjectStatisticDto> GetProjectStatisticAsync(int projectId, string managerId)
		{
			if(!await IsProjectExist(projectId))
				throw new TaskException($"Project with id = {projectId} does not exist.", HttpStatusCode.NotFound);

			var project = await _unitOfWork.ProjectRepository
									.FindByCondition(p=>p.Id == projectId && p.ManagerId == managerId, false)
									.Include(p=>p.Tasks)
									?.ThenInclude(t=>t.Status)
									.FirstAsync();

			if (project.Tasks.Count == 0)
				throw new TaskException($"Project with id = {projectId} has no tasks.", HttpStatusCode.NotFound);

			var finishedTasksIds = project.Tasks.Where(t => t.StatusId == 3).Select(t => t.Id);
			var unfinishedTasksIds = project.Tasks.Where(t => t.StatusId == 1 || t.StatusId == 2).Select(t => t.Id);
			float completionPercentage = (float)finishedTasksIds.Count() / project.Tasks.Count * 100;
			ProjectStatisticDto projectStatisticDto = new ProjectStatisticDto
			{
				ProjectId = projectId,
				Title = project.Title,
				CompletionPercentage = (float)Math.Round(completionPercentage, 2),
				FinishedTasksIds = finishedTasksIds,
				UnfinishedTaksIds = unfinishedTasksIds
			};
			return projectStatisticDto;
		}
		
		private async Task<bool> IsProjectExist(int projectId) 
		{
			var project = await _unitOfWork.ProjectRepository.GetByIdAsync(projectId);
			return project != null;
		}

	}
}
