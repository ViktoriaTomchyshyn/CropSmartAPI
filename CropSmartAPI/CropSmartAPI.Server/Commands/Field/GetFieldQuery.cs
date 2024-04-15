﻿using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.Server.Commands.User;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Field;

public class GetFieldQuery : IRequest<Result<FieldDto, string>>
{
    //public string Key { get; set; }
    public int Id { get; set; }

    public class Handler : IRequestHandler<GetFieldQuery, Result<FieldDto, string>>
    {
        private readonly IFieldService _fieldService;
        private readonly ISessionControlService _sessionControlService;

        public Handler(IFieldService service, ISessionControlService sessionControlService)
        {
            _fieldService = service;
            _sessionControlService = sessionControlService;
        }

        public async Task<Result<FieldDto, string>> Handle(GetFieldQuery request,
            CancellationToken cancellationToken)
        {
            //_sessionControlService.IsLoggedIn(request.Key);
            var obj = await _fieldService.Get(request.Id);

            if (obj == null)
            {
                return Result.Failure<FieldDto, string>("Field not found");
            }

            var result = new FieldDto
            {
                Id = obj.Id,
                Name = obj.Name,
                Area = obj.Area,
                CadastralNumber = obj.CadastralNumber,
                PropertyRight = obj.PropertyRight,
                SoilType = obj.SoilType,
                CoordinateX = obj.CoordinateX,
                CoordinateY = obj.CoordinateY,
                Userid = obj.Userid,
            };

            return Result.Success<FieldDto, string>(result);
        }
    }
}