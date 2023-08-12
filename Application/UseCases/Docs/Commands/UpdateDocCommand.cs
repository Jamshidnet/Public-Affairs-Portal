﻿using Application.Common.Abstraction;
using Application.Common.Exceptions;
using Application.UseCases.Clients.Commands;
using Application.UseCases.Docs.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Docs.Commands;


public class UpdateDocCommand : IRequest<DocResponse>
{
    public Guid Id { get; set; }

    public CreateClientCommand client { get; set; }

    public ClientInDocResponse[] ClientAnswers { get; set; }
}
public class UpdateDocCommandHandler : IRequestHandler<UpdateDocCommand, DocResponse>
{
    private IApplicationDbContext _context;
    public IDocChangeLogger _logger { get; set; }
    private readonly IMapper _mapper;


    public UpdateDocCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IDocChangeLogger logger)
    {
        _context = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<DocResponse> Handle(UpdateDocCommand request, CancellationToken cancellationToken)
    {
        await FilterIfDocExsists(request.Id);
        var foundDoc = await _context.Docs.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(Doc), request.Id);
        _mapper.Map(request, foundDoc);
        _context.Docs.Update(foundDoc);
        await _logger.Log(foundDoc.Id, "Update");

        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<DocResponse>(foundDoc);
    }
    private async Task FilterIfDocExsists(Guid docId)
    {
        if (await _context.Docs.FindAsync(docId) is null)
            throw new NotFoundException("There is no doc with given Id. ");
    }

    private async Task FilterIfClienExsists(Guid clientId)
    {
        if (await _context.Clients.FindAsync(clientId) is null)
            throw new NotFoundException("There is no client with given Id. ");
    }
}
