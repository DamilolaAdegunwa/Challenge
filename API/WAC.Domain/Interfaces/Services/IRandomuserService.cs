using System;
using System.Collections.Generic;
using System.Text;
using WAC.Domain.Model;

namespace WAC.Domain.Interfaces.Services
{
    public interface IRandomUserService
    {
        RootObject Get();
    }
}
