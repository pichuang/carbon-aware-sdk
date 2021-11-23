﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarbonAware;
using CarbonAware.Data.Sample;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CarbonAware.Plugins.BasicJsonPlugin
{
    public class CarbonAwareStaticJsonDataService : ICarbonAwareStaticDataService
    {

        private string _fileName { get; set; }

        public CarbonAwareStaticJsonDataService()
        {

        }

        public List<EmissionsData> GetData()
        {
            using StreamReader file = File.OpenText(_fileName);
            var jsonObject = JsonConvert.DeserializeObject<EmissionsJsonFile>(file.ReadToEnd());
            return jsonObject.Emissions;
        }

        public void Configure(IConfigurationSection configuration)
        {
            _fileName = configuration.GetSection("data-file").Value;

            if (_fileName == null)
            {
                throw new ArgumentException("Error configuring CarbonAwareStaticJsonDataService.");
            }

            if(!File.Exists(_fileName))
            {
                throw new ArgumentException($"Error configuring CarbonAwareStaticJsonDataService.  The data-file '{_fileName}' does not exist.");
            }
        }
    }
}
