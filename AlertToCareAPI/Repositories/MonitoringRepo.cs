using System.Collections.Generic;
using AlertToCareAPI.Models;

namespace AlertToCareAPI.Repositories
{
    public class MonitoringRepo : IMonitoringRepo
    {
        private readonly Database.Database _creator = new Database.Database();
        private readonly List<VitalsCategory> _vitals;
        public MonitoringRepo()
        {
            _vitals = _creator.ReadVitalsDatabase();
        }
      
        public IEnumerable<VitalsCategory> GetAllVitals()
        {
            return _vitals;
        }
        public string CheckVitals(VitalsCategory vital)
           {
            var a=CheckSpo2(vital.Spo2);
            var b=CheckBpm(vital.Bpm);
            var c=CheckRespRate(vital.RespRate);
            var s= a + b + c;
            // SendMail(s);
            return s;
           }
        private static string CheckSpo2(float spo2)
        {
            return spo2 < 90 ? "Spo2 is low, " : "";
        }
        private static string CheckBpm(float bpm)
        {
            if (bpm < 70)
                return "bpm is low, ";
            return bpm > 150 ? "bpm is high, " : "";
        }
        private static string CheckRespRate(float respRate)
        {
            if (respRate < 30)
                return "respRate is low. ";
            return respRate > 95 ? "respRate is high. " : "";
        }
        
    }
}
