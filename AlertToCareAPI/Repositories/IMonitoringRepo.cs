using System.Collections.Generic;
using AlertToCareAPI.Models;

namespace AlertToCareAPI.Repositories
{
    public interface IMonitoringRepo
    {
        public string CheckVitals(VitalsCategory vital);
    
        public IEnumerable<VitalsCategory> GetAllVitals();
    }

}

