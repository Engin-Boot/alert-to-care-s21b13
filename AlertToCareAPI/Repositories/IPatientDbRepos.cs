using System.Collections.Generic;
using AlertToCareAPI.Models;

namespace AlertToCareAPI.Repositories
{
    public interface IPatientDbRepos
    {
        void AddPatient( PatientDetails newState);
        void RemovePatient(string patientId);
        void UpdatePatient(string patientId, PatientDetails state);
        IEnumerable<PatientDetails> GetAllPatients();
    }
}
