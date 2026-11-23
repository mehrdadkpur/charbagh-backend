using Charbagh.Application.InstrumentCategories.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charbagh.Application.InstrumentCategories.Update
{
    public sealed record UpdateInstrumentCategoryCommand(Guid Id , string Name , string Description ) : IRequest<InstrumentCategoryDto>;
}
