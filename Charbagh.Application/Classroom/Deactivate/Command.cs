using Charbagh.Application.Classrooms.Common;
using MediatR;

namespace Charbagh.Application.Classrooms.Deactivate;

public sealed record DeactivateClassroomCommand(Guid Id) : IRequest<ClassroomDto>;