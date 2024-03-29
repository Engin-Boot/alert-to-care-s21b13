﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            
            
        }           //ReSharper disable all

        public void ValidateNewIcuId(string icuId, ICUBedDetails icuRecord, List<ICUBedDetails> icuStore)
        {
            if (icuStore.Any(icu => icu.IcuId == icuId))
            {
                throw new Exception("Invalid Patient Id");
            }

            ValidateIcuRecord(icuRecord);
        }       //ReSharper restore all

    }
}
