﻿using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.Server.Commands.Crop;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Fertilizer;

public class AddFertilizerQuery : IRequest<Result<int, string>>
{
    public string Key { get; set; }
    public string Name { get; set; }
    public long Quantity { get; set; }
    public int CropId { get; set; }

    public class Handler : IRequestHandler<AddFertilizerQuery, Result<int, string>>
    {
        private readonly IFertilizerService _fertilizerService;
        private readonly ISessionControlService _sessionControlService;

        public Handler(IFertilizerService service, ISessionControlService sessionControlService)
        {
            _fertilizerService = service;
            _sessionControlService = sessionControlService;
        }

        public async Task<Result<int, string>> Handle(AddFertilizerQuery request,
            CancellationToken cancellationToken)
        {
            _sessionControlService.IsLoggedIn(request.Key);
            var obj = new FertilizerDto
            {
                Name = request.Name,
                Quantity = request.Quantity,
                CropId = request.CropId
            };

            var id = await _fertilizerService.Create(obj);

            return Result.Success<int, string>(id);
        }
    }
}

