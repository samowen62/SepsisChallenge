using Mortara.Sepsis.Import.Data;
using Mortara.Sepsis.Import.Parser;
using Mortara.Sepsis.Import.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System;

namespace Mortara.Sepsis
{
    public partial class Graphs : Form
    {
        public Graphs()
        {
            InitializeComponent();

            var properties = typeof(PatientSample).GetProperties();
            var propertyCountDictionary = properties.ToDictionary(p => p.Name, p => 0);

            var patients = PatientSampleParser.ParseDirectory(@"C:\Projects\SepsisChallenge\Samples\training");

            var septicPatients = patients.Where(p => p.IsSeptic);
            var nonSepticPatietns = patients.Where(p => !p.IsSeptic);
            int maxTime = septicPatients.Max(p => p.SepticHour);

            var points = new List<PatientSample>();
            var pointsDictionaryNonSeptic = new Dictionary<int, List<PatientSample>>();
            var pointsDictionarySeptic = new Dictionary<int, List<PatientSample>>();
            foreach (var patient in septicPatients)
            {
                foreach (var prop in properties)
                {
                    if (prop.PropertyType.Name != "Nullable`1")
                        continue;
                    if (patient.PatientSamples.Exists(p => ((double?)prop.GetValue(p, null)).HasValue))
                        propertyCountDictionary[prop.Name] += 1;
                }

                foreach (var ps in patient.PatientSampleDeltas)
                {
                    int key = ps.TimeBeforeSepsis;
                    if (pointsDictionarySeptic.ContainsKey(key))
                        pointsDictionarySeptic[key].Add(ps);
                    else
                        pointsDictionarySeptic.Add(key, new List<PatientSample>() { ps });
                }
            }

            foreach (var patient in nonSepticPatietns)
            {
                foreach (var ps in patient.PatientSampleDeltas)
                {
                    int key = ps.TimeBeforeSepsis;
                    if (pointsDictionaryNonSeptic.ContainsKey(key))
                        pointsDictionaryNonSeptic[key].Add(ps);
                    else
                        pointsDictionaryNonSeptic.Add(key, new List<PatientSample>() { ps });
                }
            }

            // TODO: filters can be applied here
            var averages = pointsDictionarySeptic.Select(p => mapToPoint(p)).ToDictionary(p => p.TimeBeforeSepsis, p => p);
            var averageNonSeptic = pointsDictionaryNonSeptic.Select(p => mapToPoint(p)).ToDictionary(p => p.TimeBeforeSepsis, p => p);
            int maxDay = Math.Max(averageNonSeptic.Max(p => p.Key), averages.Max(p => p.Key));
            int minDay = Math.Min(averageNonSeptic.Min(p => p.Key), averages.Min(p => p.Key));

            // combine dictionaries
            var graphPoints = new List<GraphPoint>();
            for (int i = minDay; i <= maxDay; i++)
            {
                var graphPoint = new GraphPoint
                {
                    Hour = i,
                    HR_NS = averageNonSeptic.ContainsKey(i) ? averageNonSeptic[i].HR ?? 0.0 : 0.0,
                    DBP_NS = averageNonSeptic.ContainsKey(i) ? averageNonSeptic[i].DBP ?? 0.0 : 0.0,
                    SBP_NS = averageNonSeptic.ContainsKey(i) ? averageNonSeptic[i].SBP ?? 0.0 : 0.0,
                    HR = averages.ContainsKey(i) ? averages[i].HR ?? 0.0 : 0.0,
                    DBP = averages.ContainsKey(i) ? averages[i].DBP ?? 0.0 : 0.0,
                    SBP = averages.ContainsKey(i) ? averages[i].SBP ?? 0.0 : 0.0
                };
                graphPoints.Add(graphPoint);
            }

            this.indicatorChart.ChartAreas[0].AxisX.Minimum = -92;
            this.indicatorChart.ChartAreas[0].AxisX.Maximum = 10;
            this.indicatorChart.ChartAreas[0].AxisY.Minimum = 50;
            this.indicatorChart.ChartAreas[0].AxisY.Maximum = 150;
            this.indicatorChart.DataSource = graphPoints;
            this.indicatorChart.DataBind();

            foreach (var item in propertyCountDictionary.Where(kvp => kvp.Value != 0).OrderByDescending(kvp => kvp.Value))
            {
                this.samplesListView.Items.Add(string.Format(@"{0}: {1}", item.Key, item.Value));
            }
        }

        public class GraphPoint
        {
            public int Hour { get; set; }
            public double SBP { get; set; }
            public double DBP { get; set; }
            public double HR { get; set; }
            public double SBP_NS { get; set; }
            public double DBP_NS { get; set; }
            public double HR_NS { get; set; }
        }

        //TODO: see if we can remove the "Where"s. Compare graphs before/after
        private PatientSample mapToPoint(KeyValuePair<int, List<PatientSample>> p)
        {
            return new PatientSample
            {
                HR = p.Value.Where(gp => gp.HR.HasValue).Average(gp => gp.HR),
                SBP = p.Value.Where(gp => gp.SBP.HasValue).Average(gp => gp.SBP),
                DBP = p.Value.Where(gp => gp.DBP.HasValue).Average(gp => gp.DBP),
                Temp = p.Value.Where(gp => gp.Temp.HasValue).Average(gp => gp.Temp),
                O2Sat = p.Value.Where(gp => gp.O2Sat.HasValue).Average(gp => gp.O2Sat),
                MAP = p.Value.Where(gp => gp.MAP.HasValue).Average(gp => gp.MAP),
                Resp = p.Value.Where(gp => gp.Resp.HasValue).Average(gp => gp.Resp),
                EtCO2 = p.Value.Where(gp => gp.EtCO2.HasValue).Average(gp => gp.EtCO2),
                BaseExcess = p.Value.Where(gp => gp.BaseExcess.HasValue).Average(gp => gp.BaseExcess),
                HCO3 = p.Value.Where(gp => gp.HCO3.HasValue).Average(gp => gp.HCO3),
                FiO2 = p.Value.Where(gp => gp.FiO2.HasValue).Average(gp => gp.FiO2),
                pH = p.Value.Where(gp => gp.pH.HasValue).Average(gp => gp.pH),
                PaCO2 = p.Value.Where(gp => gp.PaCO2.HasValue).Average(gp => gp.PaCO2),
                SaO2 = p.Value.Where(gp => gp.SaO2.HasValue).Average(gp => gp.SaO2),
                AST = p.Value.Where(gp => gp.AST.HasValue).Average(gp => gp.AST),
                BUN = p.Value.Where(gp => gp.BUN.HasValue).Average(gp => gp.BUN),
                Alkalinephos = p.Value.Where(gp => gp.Alkalinephos.HasValue).Average(gp => gp.Alkalinephos),
                Calcium = p.Value.Where(gp => gp.Calcium.HasValue).Average(gp => gp.Calcium),
                Chloride = p.Value.Where(gp => gp.Chloride.HasValue).Average(gp => gp.Chloride),
                Creatinine = p.Value.Where(gp => gp.Creatinine.HasValue).Average(gp => gp.Creatinine),
                Bilirubin_direct = p.Value.Where(gp => gp.Bilirubin_direct.HasValue).Average(gp => gp.Bilirubin_direct),
                Glucose = p.Value.Where(gp => gp.Glucose.HasValue).Average(gp => gp.Glucose),
                Lactate = p.Value.Where(gp => gp.Lactate.HasValue).Average(gp => gp.Lactate),
                Magnesium = p.Value.Where(gp => gp.Magnesium.HasValue).Average(gp => gp.Magnesium),
                Phosphate = p.Value.Where(gp => gp.Phosphate.HasValue).Average(gp => gp.Phosphate),
                Potassium = p.Value.Where(gp => gp.Potassium.HasValue).Average(gp => gp.Potassium),
                Bilirubin_total = p.Value.Where(gp => gp.Bilirubin_total.HasValue).Average(gp => gp.Bilirubin_total),
                TroponinI = p.Value.Where(gp => gp.TroponinI.HasValue).Average(gp => gp.TroponinI),
                Hct = p.Value.Where(gp => gp.Hct.HasValue).Average(gp => gp.Hct),
                Hgb = p.Value.Where(gp => gp.Hgb.HasValue).Average(gp => gp.Hgb),
                PTT = p.Value.Where(gp => gp.PTT.HasValue).Average(gp => gp.PTT),
                WBC = p.Value.Where(gp => gp.WBC.HasValue).Average(gp => gp.WBC),
                Fibrinogen = p.Value.Where(gp => gp.Fibrinogen.HasValue).Average(gp => gp.Fibrinogen),
                Platelets = p.Value.Where(gp => gp.Platelets.HasValue).Average(gp => gp.Platelets),
                HRDelta = p.Value.Where(gp => gp.HRDelta.HasValue).Average(gp => gp.HRDelta),
                SBPDelta = p.Value.Where(gp => gp.SBPDelta.HasValue).Average(gp => gp.SBPDelta),
                DBPDelta = p.Value.Where(gp => gp.DBPDelta.HasValue).Average(gp => gp.DBPDelta),
                TimeBeforeSepsis = p.Key
            };
        }
    }
}
