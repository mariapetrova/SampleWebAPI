using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nomenclatures.Application.IntegrationTests.Builders.Subareas;
public class SubareaBuilder
{
    private readonly Subarea _subarea = new()
    {
        Name = "name",
    };

    public Subarea Build()
    {
        return _subarea;
    }

    public SubareaBuilder WithId(string pinCode)
    {
        _subarea.PINCode = pinCode;
        return this;
    }
}