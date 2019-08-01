using Mortara.Sepsis.Compute.ParameterConfig;
using Mortara.Sepsis.Compute.ParameterConfig.XmlObjects;
using Mortara.Sepsis.Import.Data;
using Mortara.Sepsis.Import.Parser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mortara.Sepsis.Compute.Data
{
    public class TrainingSet
    {
        private Dictionary<BucketSpec, List<Bucket>> _totalTrainingSets = new Dictionary<BucketSpec, List<Bucket>>();

        public TrainingSet(List<Patient> patients)
        {
            var bucketConfig = ParameterStore.GetBucketConfig();

            foreach (var bucketSpec in bucketConfig.Buckets)
            {
                var bucketList = new List<Bucket>();
                // create delegate to perform faster
                var valueAccessor = (Func<PatientSample, double?>)
                    Delegate.CreateDelegate(typeof(Func<PatientSample, double?>), null, typeof(PatientSample).GetProperty(bucketSpec.PropertyName).GetGetMethod());

                foreach (var patient in patients)
                {
                    var bucket = new Bucket(BucketType.Max, bucketSpec.PropertyName, bucketSpec.BucketNumber, patient.IsSeptic);
                    var appropriateSamples = patient.PatientSampleDeltas
                        .Where(ps => ps.TimeBeforeSepsis == bucketSpec.BucketNumber && valueAccessor(ps).HasValue)
                        .Select(valueAccessor);
                    bucket.AddSamples(appropriateSamples);

                    if (!bucket.Empty)
                    {
                        bucketList.Add(bucket);
                    }
                }

                _totalTrainingSets.Add(bucketSpec, new List<Bucket>());
            }
        }

        public TrainingSet(string dir = @"C:\Projects\SepsisChallenge\Samples\training") 
            : this(PatientSampleParser.ParseDirectory(dir, 1000))
        {
        }
    }
}
