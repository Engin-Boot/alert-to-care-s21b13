
using System.Collections.Generic;
using AlertToCareAPI.Models;

namespace AlertToCareAPI.Repositories
{
    public interface IIcuConfigurationRepo
    {
        void AddIcu(ICUBedDetails newState);
        void RemoveIcu(string icuId);
        void UpdateIcu(string icuId, ICUBedDetails state);
        IEnumerable<ICUBedDetails> GetAllIcu();
    }
}
