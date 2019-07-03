using Mortara.Sepsis.Import.Data;
using System;

namespace Mortara.Sepsis.Import.Extensions
{
    public static class PatientExtensions
    {
        public static double? DeltaAt(this Patient patient, Func<PatientSample, double?> accessor, int index)
        {
            if (index <= 0 || index >= patient.PatientSamples.Count) // can't define derivatives at beginning
                return null;

            var prevVal = accessor(patient.PatientSamples[index - 1]);
            var currVal = accessor(patient.PatientSamples[index]);

            if (!prevVal.HasValue || !currVal.HasValue)
                return null;

            return currVal.Value - prevVal.Value;
        }
    }
}
