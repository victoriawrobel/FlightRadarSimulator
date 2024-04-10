using OOD_24L_01180686.source.Objects;

namespace OOD_24L_01180686.source.Reports
{
    public class Newspaper : Reporter
    {
        public Newspaper(string name) : base(name)
        {
        }

        public override string Visit(Airport airport)
        {
            return $"{Name} - A report from the {airport.Name} airport, {airport.CountryISO}. ";
        }

        public override string Visit(PassengerPlane passengerPlane)
        {
            return
                $"{Name} - Breaking news! {passengerPlane.Model} aircraft loses EASA fails certification after inspection of {passengerPlane.SerialNr}.";
        }

        public override string Visit(CargoPlane cargoPlane)
        {
            return $"{Name} - An interview with the crew of {cargoPlane.SerialNr}.";
        }
    }
}
