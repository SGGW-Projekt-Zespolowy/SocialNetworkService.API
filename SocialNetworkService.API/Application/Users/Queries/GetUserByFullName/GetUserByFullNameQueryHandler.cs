﻿using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Users.Queries.GetUserByFullName
{
    public class GetUserByFullNameQueryHandler: IQueryHandler<GetUserByFullNameQuery,GetUserByFullNameResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByFullNameQueryHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<GetUserByFullNameResponse>> Handle(GetUserByFullNameQuery request, CancellationToken cancellationToken)
        {
            if(request.fullName == string.Empty)
            {
                return Result.Failure<GetUserByFullNameResponse>(Domain.Errors.ApplicationErrors.Request.EmptyRequest);
            }

            var user = await _userRepository.GetByFullNameAsync(request.fullName, cancellationToken);
            if (user is null)
            {
                return Result.Failure<GetUserByFullNameResponse>(Domain.Errors.ApplicationErrors.User.UserNameNotFound(request.fullName));
            }

            var response = new GetUserByFullNameResponse(
                user.Id, user.Email, user.LastLoginDate, user.FirstName, user.LastName,
                user.DateOfBirth, user.Degree, user.IsVerified, user.ProfilePicture, user.Specializations);
            return Result.Success(response);

        }
    }
}
