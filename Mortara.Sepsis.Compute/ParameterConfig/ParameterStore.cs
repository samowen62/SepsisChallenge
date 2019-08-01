using Mortara.Sepsis.Compute.ParameterConfig.XmlObjects;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace Mortara.Sepsis.Compute.ParameterConfig
{
    public static class ParameterStore
    {
        public static BucketData GetBucketConfig()
        {
            return GetResourceObject<BucketData>("BucketConfig");
        }

        private static T GetResourceObject<T>(string fileName) where T : new()
        {
            var data = new T();

            var serializer = new XmlSerializer(typeof(T));
            using (Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                string.Format("Mortara.Sepsis.Compute.ParameterConfig.XmlConfig.{0}.xml", fileName)))
            using (var reader = new StreamReader(fs))
            {
                data = (T)(serializer.Deserialize(reader));
            }

            return data;
        }
    }
}
