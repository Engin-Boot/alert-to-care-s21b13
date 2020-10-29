using System;
using System.Collections.Generic;
using System.Linq;
using AlertToCareAPI.Models;

namespace AlertToCareAPI.Repositories.Field_Validators
{
    public class PatientFieldsValidator
    {
        private readonly CommonFieldValidator _validator = new CommonFieldValidator();
        private readonly VitalFieldsValidator _vitalsValidator = new VitalFieldsValidator();
        private readonly AddressFieldsValidator _addressValidator = new AddressFieldsValidator();
        public void ValidatePatientRecord(PatientDetails patient)
        {
           _validator.IsWhitespaceOrEmptyOrNull(patient.PatientId);
           _validator.IsWhitespaceOrEmptyOrNull(patient.PatientName);
           _validator.IsWhitespaceOrEmptyOrNull(patient.Age.ToString());
           _validator.IsWhitespaceOrEmptyOrNull(patient.ContactNo);
           _validator.IsWhitespaceOrEmptyOrNull(patient.Email);
           _validator.IsWhitespaceOrEmptyOrNull(patient.BedId);
           _validator.IsWhitespaceOrEmptyOrNull(patient.IcuId);
           CheckConsistencyInPatientIdFields(patient);
           _vitalsValidator.ValidateVitalsList(patient.Vitals);
           _addressValidator.ValidateAddressFields(patient.Address);
           CheckConsistencyInIcuIdFields(patient.IcuId, patient.BedId);

        }       //ReSharper disable all

        public void ValidateNewPatientId(string patientId, PatientDetails patientRecord, List<PatientDetails> patients)
        {
            CheckIcuPresence(patientRecord.IcuId);
            if (patients.Any(patient => patient.PatientId == patientId))
            {
                throw new Exception("Invalid Patient Id");
            }

            ValidatePatientRecord(patientRecord);
        }       //ReSharper restore all

        private static void CheckConsistencyInPatientIdFields(PatientDetails patient)
        {
            if (string.Equals(patient.PatientId, patient.Vitals.PatientId, StringComparison.CurrentCultureIgnoreCase))
            {
               return;
            }
            throw new Exception("Invalid data field");
        }

        private static void CheckConsistencyInIcuIdFields(string icuId, string bedId)
        {
            var database = new Database.Database();
            var beds = database.ReadBedsDatabase();
            if (beds.Where(bed => bed.BedId == bedId).Any(bed => bed.IcuId == icuId))
            {
                return;
            }
            throw new Exception("Invalid data field");
        }

        private static void CheckIcuPresence(string icuId)
        {
            var database = new Database.Database();
            var icuList = database.ReadIcuDatabase();
            if (icuList.Any(icu => icu.IcuId == icuId))
            {
                return;
            }

            throw new Exception("Invalid data field");
        }

    }
}
