using System;
using System.Collections.Generic;
using HelperServiceModels.Models;

namespace InterviewTask.Services
{
    interface IHelperServiceRepository
    {
        IEnumerable<HelperServiceModel> Get();
        HelperServiceModel Get(Guid id);
    }
}
