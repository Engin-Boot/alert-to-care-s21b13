using System;
using System.Collections.Generic;
using System.Linq;
using AlertToCareAPI.Models;
using AlertToCareAPI.Repositories.Field_Validators;

namespace AlertToCareAPI.Repositories
{
    public class PatientDbRepo : IPatientDbRepos
    {
        private readonly Database.Database _creator=new Database.Database();
        private readonly PatientFieldsValidator _validator = new PatientFieldsValidator();

        public void AddPatient(PatientDetails newState)
        {
            var patients = _creator.ReadPatientDatabase();
            _validator.ValidateNewPatientId(newState.PatientId, newState, patients);
            patients.Add(newState);
            _creator.WriteToPatientsDatabase(patients);
            ChangeBedStatusToTrue(newState.BedId);
        }
        public void RemovePatient(string patientId)
        {
            var patients = _creator.ReadPatientDatabase();
            for (var i = 0; i < patients.Count; i++)
            {
                if (patients[i].PatientId != patientId) continue;
                patients.Remove(patients[i]);
                _creator.WriteToPatientsDatabase(patients);
                ChangeBedStatusToFalse(patients[i].BedId);
                return;
            }
            throw new Exception("Invalid data field");
        }
        public void UpdatePatient(string patientId, PatientDetails state)
        {
            var patients = _creator.ReadPatientDatabase();
            _validator.ValidatePatientRecord(state);

            for (var i = 0; i < patients.Count; i++)
            {
                if (patients[i].PatientId != patientId) continue;
                patients.Insert(i, state);
                _creator.WriteToPatientsDatabase(patients);
                return;
            }
            throw new Exception("Invalid data field");
        }
        public IEnumerable<PatientDetails> GetAllPatients()
        {
            var patients = _creator.ReadPatientDatabase();
            return patients;
        }
        private void ChangeBedStatusToTrue(string bedId)
        {
            var icuList = _creator.ReadIcuDatabase();
            foreach (var bed in from icu in icuList from bed in icu.Beds where bed.BedId == bedId where bed.Status == false select bed)
            {
                bed.Status = true;
                _creator.WriteToIcuDatabase(icuList);
                return;
            }
            throw new Exception("Invalid data field");
        }

        private void ChangeBedStatusToFalse(string bedId)
        {
            var icuList = _creator.ReadIcuDatabase();
            foreach (var bed in from icu in icuList from bed in icu.Beds where bed.BedId == bedId select bed)
            {
                bed.Status = false;
                _creator.WriteToIcuDatabase(icuList);
            }
        }
    }
}
