﻿using Application.UseCases.Categories.Commands;
using Application.UseCases.Categories.Queries;
using Application.UseCases.Categories.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace NewProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ApiBaseController
{
    [HttpGet("[action]")]
    [Authorize(Roles = "GetCategoryById")]
    public async ValueTask<CategoryResponse> GetCategoryById(Guid CategoryId)
    {
        return await _mediator.Send(new GetByIdCategoryQuery(CategoryId));
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "GetAllCategory")]
    public async ValueTask<IEnumerable<GetListCategoryResponse>> GetAllCategory(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<GetListCategoryResponse> query = (await _mediator
             .Send(new GetAllCategoryQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "CreateCategory")]
    public async ValueTask<Guid> CreateCategory(CreateCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut("[action]")]
    [Authorize(Roles = "UpdateCategory")]
    public async ValueTask<IActionResult> UpdateCategory(UpdateCategoryCommand command)
    {
         await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete("[action]")]
    [Authorize(Roles = "DeleteCategory")]
    public async ValueTask<IActionResult> DeleteCategory(DeleteCategoryCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

}
