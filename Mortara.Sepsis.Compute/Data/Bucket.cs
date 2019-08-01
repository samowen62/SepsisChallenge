using Mortara.Sepsis.Import.Data;
using System.Collections.Generic;
using System.Linq;

namespace Mortara.Sepsis.Compute.Data
{
    public enum BucketType { Mean, Min, Max }

    public class Bucket
    {
        #region Constants
        private static string BUCKET_SPEC_FORMAT = "{0} (bucket {1} {2})";
        #endregion

        #region Constructor
        public Bucket(BucketType bucketType, string propertyName, int bucketNumber, bool sepsis)
        {
            BucketType = bucketType;
            PropertyName = propertyName;
            BucketNumber = bucketNumber;
            IsSepsis = sepsis;
        }
        #endregion

        #region Public Properties
        public BucketType BucketType { get; private set; }
        public int BucketNumber { get; private set; }
        public bool IsSepsis { get; private set; }
        public string PropertyName { get; private set; }
        public double? Value { get; private set; }
        public bool Empty => !Value.HasValue;
        #endregion

        #region Public Methods
        public void AddSamples(IEnumerable<PatientSample> patientSamples)
        {
            var bucketData = patientSamples.Select(ps => (double?)typeof(PatientSample).GetProperty(PropertyName).GetValue(ps)).Where(b => b.HasValue);
            AddSamples(bucketData);
        }

        public void AddSamples(IEnumerable<double?> bucketData)
        {
            if (!bucketData.Any())
                return;

            switch (BucketType)
            {
                case BucketType.Max:
                    Value = bucketData.Max().Value;
                    break;
                case BucketType.Min:
                    Value = bucketData.Min().Value;
                    break;
                case BucketType.Mean:
                    Value = bucketData.Average().Value;
                    break;
            }
        }
        
        public override string ToString()
        {
            return string.Format(BUCKET_SPEC_FORMAT, PropertyName, BucketNumber, BucketType);
        }
        #endregion
    }
}
