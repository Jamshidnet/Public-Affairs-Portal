﻿using Application.UseCases.Logs.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Logs.Queries;


public record GetAllDocChangeQuery : IRequest<List<DocChangeResponse>>;


public class GetAllDocChangeQueryHandler : IRequestHandler<GetAllDocChangeQuery, List<DocChangeResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllDocChangeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DocChangeResponse>> Handle(GetAllDocChangeQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.DocChangeLogs.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<DocChangeResponse>>(entities);
        return result;
    }
}

