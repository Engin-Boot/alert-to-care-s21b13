using System;
using System.Collections.Generic;
using AlertToCareAPI.Models;

namespace AlertToCareAPI.Repositories.Field_Validators
{
    public class IcuFieldsValidator
    {
        private readonly CommonFieldValidator _validator = new CommonFieldValidator();
        public void ValidateIcuRecord(ICUBedDetails icu)
        {
            _validator.IsWhitespaceOrEmptyOrNull(icu.IcuId);
            _validator.IsWhitespaceOrEmptyOrNull(icu.BedsCount.ToString());
            _validator.IsWhitespaceOrEmptyOrNull(icu.LayoutId);
            
            
        }

        public void ValidateNewIcuId(string icuId, ICUBedDetails icuRecord, List<ICUBedDetails> icuStore)
        {
            
            foreach (var icu in icuStore)
            {
                if (icu.IcuId == icuId)
                {
                    throw new Exception("Invalid Patient Id");
                }
            }

            ValidateIcuRecord(icuRecord);
        }

    }
}
