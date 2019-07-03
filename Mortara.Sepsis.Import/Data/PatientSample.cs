namespace Mortara.Sepsis.Import.Data
{
    public class PatientSample
    {
        public int Hour { get; set; }
        public double? HR { get; set; }
        public double? O2Sat { get; set; }
        public double? Temp { get; set; }
        public double? SBP { get; set; }
        public double? MAP { get; set; }
        public double? DBP { get; set; }
        public double? Resp { get; set; }
        public double? EtCO2 { get; set; }
        public double? BaseExcess { get; set; }
        public double? HCO3 { get; set; }
        public double? FiO2 { get; set; }
        public double? pH { get; set; }
        public double? PaCO2 { get; set; }
        public double? SaO2 { get; set; }
        public double? AST { get; set; }
        public double? BUN { get; set; }
        public double? Alkalinephos { get; set; }
        public double? Calcium { get; set; }
        public double? Chloride { get; set; }
        public double? Creatinine { get; set; }
        public double? Bilirubin_direct { get; set; }
        public double? Glucose { get; set; }
        public double? Lactate { get; set; }
        public double? Magnesium { get; set; }
        public double? Phosphate { get; set; }
        public double? Potassium { get; set; }
        public double? Bilirubin_total { get; set; }
        public double? TroponinI { get; set; }
        public double? Hct { get; set; }
        public double? Hgb { get; set; }
        public double? PTT { get; set; }
        public double? WBC { get; set; }
        public double? Fibrinogen { get; set; }
        public double? Platelets { get; set; }
        public double Age { get; set; }
        public int Gender { get; set; }
        public int Unit1 { get; set; }
        public int Unit2 { get; set; }
        public double HospAdmTime { get; set; }
        public int ICULOS { get; set; }
        public int SepsisLabel { get; set; }

        // other fields

        public double? HRDelta { get; set; }
        public double? DBPDelta { get; set; }
        public double? SBPDelta { get; set; }
        public double? LactateDelta { get; set; }
        public double? CalciumDelta { get; set; }
        public double? GlucoseDelta { get; set; }
        public int TimeBeforeSepsis { get; set; }

    }
}
