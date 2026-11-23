
using Charbagh.Application.Students.Common;
using MediatR;

namespace Charbagh.Application.Students.Block;

public sealed record BlockStudentCommand(Guid Id) : IRequest<StudentDto>;
