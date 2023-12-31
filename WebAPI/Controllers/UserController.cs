﻿using Application.UseCases.Users.Commands.CreateUser;
using Application.UseCases.Users.Commands.DeleteUser;
using Application.UseCases.Users.Commands.LoginUser;
using Application.UseCases.Users.Commands.RegesterUser;
using Application.UseCases.Users.Commands.UpdateUser;
using Application.UseCases.Users.Queries;
using Application.UseCases.Users.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewProject.JWT.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace NewProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ApiBaseController
{


    [Authorize(Roles = "GetUserById")]
    [HttpGet("[action]")]
    public async ValueTask<UserResponse> GetUserById(Guid UserId)
    {
        return await _mediator.Send(new GetByIdUserQuery(UserId));
    }

    [Authorize(Roles = "GetAllUser")]
    [HttpGet("[action]")]
    public async ValueTask<IEnumerable<GetListUserResponse>> GetAllUser(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<GetListUserResponse> query = (await _mediator
             .Send(new GetAllUserQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public async ValueTask<TokenResponse> RegisterUser(RegisterUserCommand command)
    {
        return await _mediator.Send(command);
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public async ValueTask<TokenResponse> LoginUser(LoginUserCommand command)
    {
        return await _mediator.Send(command);
    }

    [Authorize(Roles = "CreateUser")]
    [HttpPost("[action]")]
    public async ValueTask<Guid> CreateUser(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    [Authorize(Roles = "UpdateUser")]
    [HttpPut("[action]")]
    public async ValueTask<IActionResult> UpdateUser(UpdateUserCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "DeleteUser")]
    [HttpDelete("[action]")]
    public async ValueTask<IActionResult> DeleteUser(DeleteUserCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    //[HttpGet("[action]")]
    //public async ValueTask<IEnumerable<UserResponse>> GetFilteredUsersByBirthDate(int PageNumber = 1, int PageSize = 10)
    //{
    //    IPagedList<UserResponse> query = (await _mediator
    //         .Send(new FilterUserByBirthDate()))
    //         .ToPagedList(PageNumber, PageSize);
    //    return query;
    //}
}
