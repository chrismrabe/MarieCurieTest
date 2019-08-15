using System;
using System.Collections.Generic;
using HelperServiceModels.Models;

namespace HelperServices.Services
{
    interface IHelperServiceRepository
    {
        IEnumerable<HelperServiceModel> Get();
        HelperServiceModel Get(Guid id);
    }
}
