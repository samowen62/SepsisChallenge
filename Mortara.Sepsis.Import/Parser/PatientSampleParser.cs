using Mortara.Sepsis.Import.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortara.Sepsis.Import.Parser
{
    public class PatientSampleParser
    {
        public static List<Patient> ParseDirectory(string directory, int? maxFiles = null)
        {
            var patients = new List<Patient>();
            int i = 0;

            foreach(var file in Directory.EnumerateFiles(directory))
            {
                if (maxFiles.HasValue && maxFiles.Value <= i) break;
                i++;

                patients.Add(ParseFile(file));
            }
            return patients;
        }

        public static Patient ParseFile(string file)
        {
            var patient = new Patient
            {
                PatientName = file
            };
            var firstLine = true;
            int i = 0;

            foreach(var line in File.ReadLines(file))
            {
                // skip the first line
                if (firstLine)
                {
                    firstLine = false;
                    continue;
                }
                
                var samples = ParseLine(line);
                samples.Hour = i;
                patient.PatientSamples.Add(samples);
                i++;
            }

            return patient;
        }

        public static PatientSample ParseLine(string line)
        {
            var vals = line.Split('|');
            if(vals.Length != 41)
            {
                //TODO:log
                return null;
            }

            return new PatientSample
            {
                HR = parseDouble(vals[0]),
                O2Sat = parseDouble(vals[1]),
                Temp = parseDouble(vals[2]),
                SBP = parseDouble(vals[3]),
                MAP = parseDouble(vals[4]),
                DBP = parseDouble(vals[5]),
                Resp = parseDouble(vals[6]),
                EtCO2 = parseDouble(vals[7]),
                BaseExcess = parseDouble(vals[8]),
                HCO3 = parseDouble(vals[9]),
                FiO2 = parseDouble(vals[10]),
                pH = parseDouble(vals[11]),
                PaCO2 = parseDouble(vals[12]),
                SaO2 = parseDouble(vals[13]),
                AST = parseDouble(vals[14]),
                BUN = parseDouble(vals[15]),
                Alkalinephos = parseDouble(vals[16]),
                Calcium = parseDouble(vals[17]),
                Chloride = parseDouble(vals[18]),
                Creatinine = parseDouble(vals[19]),
                Bilirubin_direct = parseDouble(vals[20]),
                Glucose = parseDouble(vals[21]),
                Lactate = parseDouble(vals[22]),
                Magnesium = parseDouble(vals[23]),
                Phosphate = parseDouble(vals[24]),
                Potassium = parseDouble(vals[25]),
                Bilirubin_total = parseDouble(vals[26]),
                TroponinI = parseDouble(vals[27]),
                Hct = parseDouble(vals[28]),
                Hgb = parseDouble(vals[29]),
                PTT = parseDouble(vals[30]),
                WBC = parseDouble(vals[31]),
                Fibrinogen = parseDouble(vals[32]),
                Platelets = parseDouble(vals[33]),
                Age = parseDouble(vals[34]).Value,
                Gender = parseInt(vals[35]),
                Unit1 = parseInt(vals[36]),
                Unit2 = parseInt(vals[37]),
                HospAdmTime = parseDouble(vals[38]).Value,
                ICULOS = parseInt(vals[39]),
                SepsisLabel = parseInt(vals[40])
            };
        }

        private static double? parseDouble(string val)
        {
            if ("NaN" == val)
                return null;

            return double.Parse(val);
        }

        private static int parseInt(string val)
        {
            if ("NaN" == val)
                return 0;

            return int.Parse(val);
        }
    }
}
