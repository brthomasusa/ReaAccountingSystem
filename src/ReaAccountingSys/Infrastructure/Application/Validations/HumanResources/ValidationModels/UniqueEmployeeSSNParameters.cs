using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReaAccountingSys.Infrastructure.Application.Validations.HumanResources.ValidationModels
{
    public readonly record struct UniqueEmployeeSSNParameters(string SSN);
}