﻿namespace OOD_24L_01180686.source.Objects
{
    public class Passenger : Person
    {
        public string CabinClass { get; set; }
        public ulong Miles { get; set; }

        public Passenger() : base()
        {
            CabinClass = "Economy";
            Miles = 0;

            FieldMap.Add("CabinClass", () => CabinClass);
            FieldMap.Add("Miles", () => Miles);
        }

        public Passenger(ulong ID, string name, ulong age, string phone, string email, string cabinClass, ulong miles) :
            base(ID, name, age, phone, email)
        {
            this.CabinClass = cabinClass;
            this.Miles = miles;

            FieldMap.Add("CabinClass", () => CabinClass);
            FieldMap.Add("Miles", () => Miles);
        }

        public override string ToString()
        {
            return $"Passenger: {ID} {Name} {Age} {Phone} {Email} {CabinClass} {Miles}";
        }

        public override string GetTypeCustom()
        {
            return "Passenger";
        }
    }
}