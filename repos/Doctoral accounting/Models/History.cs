using System;
using Newtonsoft.Json;

namespace Doctoral_accounting
{
    public class History
    {
        public string PatientName { get; set; }
        public string PatientSurName { get; set; }
        public DateTime CurrentDate { get; set; }
        public DateTime DateOfArive { get; set; }
        public string Diagnos { get; set; }
        public string Comentary { get; set; }
        public string Analizes { get; set; }
        public string Treatment { get; set; }

        public History(string patientName, string patientSurName, DateTime currentDate, DateTime dateOfArive, string diagnos, string comentary, string analizes, string treatment)
        {
            PatientName = patientName;
            PatientSurName = patientSurName;
            CurrentDate = currentDate;
            DateOfArive = dateOfArive;
            Diagnos = diagnos;
            Comentary = comentary;
            Analizes = analizes;
            Treatment = treatment;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static History FromJson(string json)
        {
            return JsonConvert.DeserializeObject<History>(json);
        }
    }
}
