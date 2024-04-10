using OOD_24L_01180686.source.Objects;

namespace OOD_24L_01180686.source.Reports
{
    public class Radio: Reporter
    {
        public Radio(string name) : base(name)
        {
        }

        public override string Visit(Airport airport)
        {
            return $"Reporting for {Name},  Ladies and Gentlemen, we are at the {airport.Name} airport. ";
        }

        public override string Visit(PassengerPlane passengerPlane)
        {
            return
                $"Reporting for {Name}, Ladies and Gentlemen, we’ve just witnessed {passengerPlane.SerialNr} take off.";
        }

        public override string Visit(CargoPlane cargoPlane)
        {
            return $"Reporting for {Name},  Ladies and Gentlemen, we are seeing the {cargoPlane.SerialNr} aircraft fly above us. ";
        }
    }
}
