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
                var bucketType = translateBucketType(bucketSpec);

                // create delegate value accessor to perform faster
                var valueAccessor = (Func<PatientSample, double?>)
                    Delegate.CreateDelegate(typeof(Func<PatientSample, double?>), null, typeof(PatientSample).GetProperty(bucketSpec.PropertyName).GetGetMethod());

                // create a lambda for bucket number filtering. The first bucket is 6 hours and the rest are 4
                Func<PatientSample, BucketSpec, bool> bucketNumberFilter = (ps, bs) => ps.TimeBeforeSepsis > (bs.BucketNumber == 1 ? -6 : bs.BucketNumber * -4 - 2);

                // for each patient, create a bucket of their samples
                foreach (var patient in patients)
                {
                    var bucket = new Bucket(bucketType, bucketSpec.PropertyName, bucketSpec.BucketNumber, patient.IsSeptic);
                    var appropriateSamples = patient.PatientSampleDeltas
                        .Where(ps => bucketNumberFilter(ps, bucketSpec) && valueAccessor(ps).HasValue)
                        .Select(valueAccessor);
                    bucket.AddSamples(appropriateSamples);

                    if (!bucket.Empty /* to check */ && bucketSpec.BucketNumber == 1)
                    {
                        bucketList.Add(bucket);
                    }
                }

                _totalTrainingSets.Add(bucketSpec, bucketList);
            }
        }

        public TrainingSet(string dir = @"C:\Projects\SepsisChallenge\Samples\training") 
            : this(PatientSampleParser.ParseDirectory(dir, 1000))
        {
        }
        
        private BucketType translateBucketType(BucketSpec bucketSpec)
        {
            switch (bucketSpec.BucketType)
            {
                case ParameterConfig.XmlObjects.BucketType.Max:
                    return BucketType.Max;
                case ParameterConfig.XmlObjects.BucketType.Min:
                    return BucketType.Min;
                case ParameterConfig.XmlObjects.BucketType.Mean:
                default:
                    return BucketType.Mean;
            }
        }
    }
}
