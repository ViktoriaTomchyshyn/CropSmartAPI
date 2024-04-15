﻿using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.Server.Commands.Field;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Crop;

public class GetCropQuery : IRequest<Result<CropDto, string>>
{
    public string Key { get; set; }
    public int Id { get; set; }

    public class Handler : IRequestHandler<GetCropQuery, Result<CropDto, string>>
    {
        private readonly ICropService _cropService;
        private readonly ISessionControlService _sessionControlService;

        public Handler(ICropService service, ISessionControlService sessionControlService)
        {
            _cropService = service;
            _sessionControlService = sessionControlService;
        }

        public async Task<Result<CropDto, string>> Handle(GetCropQuery request,
            CancellationToken cancellationToken)
        {
            _sessionControlService.IsLoggedIn(request.Key);
            var obj = await _cropService.Get(request.Id);

            if (obj == null)
            {
                return Result.Failure<CropDto, string>("Crop not found");
            }

            var result = new CropDto
            {
                Id = obj.Id,
                Name = obj.Name,
                SowingDate = obj.SowingDate,
                HarverstDate = obj.HarverstDate,
                Fertility = obj.Fertility,
                Notes = obj.Notes,
                FieldId = obj.FieldId
            };

            return Result.Success<CropDto, string>(result);
        }
    }
}