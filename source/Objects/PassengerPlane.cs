
using OOD_24L_01180686.source.Reports;

namespace OOD_24L_01180686.source.Objects
{
    public class PassengerPlane : Plane, IReportable
    {
        public ushort FirstClassSize { get; protected set; }
        public ushort BusinessClassSize { get; protected set; }
        public ushort EconomyClassSize { get; protected set; }

        public PassengerPlane(ulong ID, string serialNr, string countryISO, string model, ushort firstClassSize, ushort businessClassSize, ushort economyClassSize) : 
            base(ID, serialNr, countryISO, model)
        {
            this.FirstClassSize = firstClassSize;
            this.BusinessClassSize = businessClassSize;
            this.EconomyClassSize = economyClassSize;

            FieldMap.Add("FirstClassSize", () => FirstClassSize);
            FieldMap.Add("BusinessClassSize", () => BusinessClassSize);
            FieldMap.Add("EconomyClassSize", () => EconomyClassSize);
        }

        public override string ToString()
        {
            return
                $"PassengerPlane: {ID} {SerialNr} {CountryISO} {Model} {FirstClassSize} {BusinessClassSize} {EconomyClassSize}";
        }

        public string Accept(Reporter reporter)
        {
            return reporter.Visit(this);
        }

        public override string GetTypeCustom()
        {
            return "PassengerPlane";
        }
    }
}