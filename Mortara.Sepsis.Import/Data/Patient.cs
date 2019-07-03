using System.Collections.Generic;
using Mortara.Sepsis.Import.Extensions;
using System.Linq;

namespace Mortara.Sepsis.Import.Data
{
    public class Patient
    {
        public string PatientName { get; set; }
        public List<PatientSample> PatientSamples { get; set; } = new List<PatientSample>();
        public bool IsSeptic => PatientSamples.Any(ps => ps.SepsisLabel == 1);
        public int SepticHour => IsSeptic ? PatientSamples.OrderBy(ps => ps.Hour).First(ps => ps.SepsisLabel == 1).Hour : PatientSamples.Max(ps => ps.Hour);

        private List<PatientSample> _patientSampleDeltas;
        public List<PatientSample> PatientSampleDeltas
        {
            get
            {
                if (_patientSampleDeltas != null)
                    return _patientSampleDeltas;

                _patientSampleDeltas = new List<PatientSample>();
                if (PatientSamples.Count == 0)
                {
                    return _patientSampleDeltas;
                }

                // private variables used here to avoid re-computing values. Not sure if the compiler optimizes away
                var isSeptic = IsSeptic;
                var septicHour = SepticHour;

                for(int i = 0; i < PatientSamples.Count; i++)
                {
                    var sampleDelta = PatientSamples[i];
                    sampleDelta.CalciumDelta = this.DeltaAt(s => s.Calcium, i);
                    sampleDelta.LactateDelta = this.DeltaAt(s => s.Lactate, i);
                    sampleDelta.GlucoseDelta = this.DeltaAt(s => s.Glucose, i);
                    sampleDelta.TimeBeforeSepsis = sampleDelta.Hour - septicHour;
                    _patientSampleDeltas.Add(sampleDelta);
                }
                
                return _patientSampleDeltas;
            }
        }
    }
}
