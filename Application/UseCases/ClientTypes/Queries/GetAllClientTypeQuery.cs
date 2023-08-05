﻿using Application.UseCases.ClientTypes.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.ClientTypes.Queries;


public record GetAllClientTypeQuery : IRequest<List<ClientTypeResponse>>;


public class GetAllClientTypeQueryHandler : IRequestHandler<GetAllClientTypeQuery, List<ClientTypeResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllClientTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ClientTypeResponse>> Handle(GetAllClientTypeQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.ClientTypes.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<ClientTypeResponse>>(entities);
        return result;
    }
}
