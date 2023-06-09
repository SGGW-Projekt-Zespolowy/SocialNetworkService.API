﻿using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;
using Domain.ValueObjects;
using ValueObjectErrors = Domain.Errors.DomainErrors.ValueObjects;

namespace Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.userId, cancellationToken);
            if (user is null)
            {
                return Result.Failure(Domain.Errors.ApplicationErrors.User.UserNotFound(request.userId));
            }

            var email = request.email != string.Empty ? Email.Create(request.email) : null;
            var firstName = request.firstName != string.Empty ? FirstName.Create(request.firstName) : null;
            var lastName = request.lastName != string.Empty ? LastName.Create(request.lastName) : null;
            var degree = request.degree != string.Empty ? Degree.Create(request.degree) : null;
            var image = request.profilePic == string.Empty ? null : request.profilePic;

            if (email is not null && email.IsFailure) return Result.Failure(ValueObjectErrors.EmailIsInvalid);
            if (firstName is not null && firstName.IsFailure) return Result.Failure(ValueObjectErrors.FirstNameIsInvalid);
            if (lastName is not null && lastName.IsFailure) return Result.Failure(ValueObjectErrors.LastNameIsInvalid);
            if (degree is not null && degree.IsFailure) return Result.Failure(ValueObjectErrors.DegreeNotDefined);
            
            user.Update(email?.Value, firstName?.Value, lastName?.Value, degree?.Value, image);
            _userRepository.Update(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
