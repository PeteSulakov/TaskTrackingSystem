using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BLL.Validation;
using System.Net;

namespace BLL.Services
{
	public class StatusService : IStatusService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;

		public StatusService(IUnitOfWork uow, IMapper mapper, ILoggerManager logger)
		{
			_unitOfWork = uow;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<ReadStatusDto> AddAsync(CreateStatusDto model)
		{
			var status = _unitOfWork.StatusRepository
										.FindByCondition(s => s.Title == model.Title, false)
										.FirstOrDefault();
			if (status != null)
				throw new TaskException($"Status with title \"{model.Title}\" already exist.", HttpStatusCode.Conflict);


			var statusToAdd = _mapper.Map<DAL.Entities.Status>(model);
			await _unitOfWork.StatusRepository.AddAsync(statusToAdd);
			var addedStatus = await _unitOfWork.StatusRepository.FindByCondition(s => s.Title == model.Title, false)
																.Include(s=>s.Tasks)
																.FirstOrDefaultAsync();
			_logger.LogInfo($"Created status with id = {addedStatus.Id}.");
			return _mapper.Map<ReadStatusDto>(addedStatus);
		}

		public async Task<ReadStatusDto> DeleteByIdAsync(int id)
		{
			var status = await _unitOfWork.StatusRepository.GetByIdAsync(id);
			if (status == null)
				throw new TaskException($"Status with id = {id} not found.", HttpStatusCode.NotFound);
			await _unitOfWork.StatusRepository.DeleteAsync(status);
			await _unitOfWork.SaveAsync();
			_logger.LogInfo($"Deleted status with id = {status.Id}.");
			return _mapper.Map<ReadStatusDto>(status);
		}


		public async Task<IEnumerable<ReadStatusDto>> GetAllAsync()
		{
			var statuses = await _unitOfWork.StatusRepository.GetAllStatusesWithDetailsAsync(false);
			return _mapper.Map<IEnumerable<ReadStatusDto>>(statuses);
		}

		public async Task<ReadStatusDto> GetByIdAsync(int id)
		{
			var status = await _unitOfWork.StatusRepository.GetByIdAsync(id);
			return _mapper.Map<ReadStatusDto>(status);
		}

		public async Task<ReadStatusDto> UpdateAsync(int id, CreateStatusDto model)
		{

			var status = await _unitOfWork.StatusRepository.GetByIdAsync(id);
			if (status == null)
				throw new TaskException($"Status with id = {id} not found.", HttpStatusCode.NotFound);

			status.Title = model.Title;
			await _unitOfWork.StatusRepository.UpdateAsync(status);
			await _unitOfWork.SaveAsync();
			_logger.LogInfo($"Updated status with id = {id}");
			return _mapper.Map<ReadStatusDto>(status);
		}
	}
}
