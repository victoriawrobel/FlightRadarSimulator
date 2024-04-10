using OOD_24L_01180686.source.Objects;

namespace OOD_24L_01180686.source.Reports
{
    public abstract class Reporter
    {

        public string Name { get; set; }

        public Reporter(string name)
        {
            Name = name;
        }

        public abstract string Visit(Airport airport);
        public abstract string Visit(PassengerPlane passengerPlane);
        public abstract string Visit(CargoPlane cargoPlane);

    }
}
