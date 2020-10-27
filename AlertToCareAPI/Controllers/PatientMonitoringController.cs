
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AlertToCareAPI.Repositories;


namespace AlertToCareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientMonitoringController : ControllerBase
    {
        private readonly IMonitoringRepo _patientMonitoring;
        public PatientMonitoringController(IMonitoringRepo patientMonitoring)
        {
            _patientMonitoring = patientMonitoring;
        }
        // GET: api/<PatientMonitoringController>
       /* [HttpGet]
        public IActionResult GetVitals()
        {
            var vitals = _patientMonitoring.GetAllVitals();
                return Ok(vitals);
        }*/
        // GET: api/<PatientMonitoringController>/9245fe4a-d402-451c-b9ed-9c1a04247482
        [HttpGet]
        public IActionResult GetAlerts()
        {
            
            var patientVitals = _patientMonitoring.GetAllVitals();
            var vitalCheck= patientVitals.Aggregate("", (current, patient) => current + patient.PatientId + " " + _patientMonitoring.CheckVitals(patient) + "\n");
            return Ok(vitalCheck);
        }

    }
}
