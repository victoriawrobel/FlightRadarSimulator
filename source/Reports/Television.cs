using OOD_24L_01180686.source.Objects;

namespace OOD_24L_01180686.source.Reports
{
    public class Television : Reporter
    {
        public Television(string name) : base(name)
        {
        }

        public override string Visit(Airport airport)
        {
            return $"<An image of {airport.Name} airport>";
        }

        public override string Visit(PassengerPlane passengerPlane)
        {
            return $"<An image of {passengerPlane.Model} passenger plane>";
        }

        public override string Visit(CargoPlane cargoPlane)
        {
            return $"<An image of {cargoPlane.SerialNr} cargo plane>";
        }
    }
}
