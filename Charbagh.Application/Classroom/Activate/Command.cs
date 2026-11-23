using Charbagh.Application.Classrooms.Common;
using MediatR;

namespace Charbagh.Application.Classrooms.Activate;

public sealed record ActivateClassroomCommand(Guid Id) : IRequest<ClassroomDto>;