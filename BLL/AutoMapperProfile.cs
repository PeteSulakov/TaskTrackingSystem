using AutoMapper;
using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Task, ReadTaskDto>()
				.ForMember(td => td.DeveloperEmail, c => c.MapFrom(t => t.Developer.Email))
				.ForMember(td => td.ProjectId, c => c.MapFrom(t => t.Project.Id))
				.ForMember(td => td.StatusId, c => c.MapFrom(t => t.Status.Id));

			CreateMap<CreateTaskDto, DAL.Entities.Task>()
				.ForMember(t=>t.DeveloperId, c=>c.MapFrom(td=>td.DeveloperEmail));

			CreateMap<Project, ReadProjectDto>()
				.ForMember(pd => pd.TasksIds, c => c.MapFrom(p => p.Tasks.Select(t => t.Id)))
				.ForMember(pd => pd.ManagerId, c => c.MapFrom(p => p.Manager.Id));

			CreateMap<CreateProjectDto, Project>();

			CreateMap<CreateStatusDto, Status>();

			CreateMap<Status, ReadStatusDto>()
				.ForMember(sd => sd.TasksIds, c => c.MapFrom(s => s.Tasks.Select(t => t.Id)));


			CreateMap<UserForRegistrationDto, User>();
		}
	}
}
