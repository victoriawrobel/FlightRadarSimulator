using OOD_24L_01180686.source.Objects;
using System.Text;


namespace OOD_24L_01180686.source.Readers
{
    internal static class Reader
    {
        public static readonly Dictionary<string, Func<string[], object>> objectCreators =
            new Dictionary<string, Func<string[], object>>
            {
                {
                    "C",
                    attributes => new Crew(UInt64.Parse(attributes[1]), attributes[2], UInt64.Parse(attributes[3]),
                        attributes[4], attributes[5], UInt16.Parse(attributes[6]), attributes[7])
                },
                {
                    "P",
                    attributes => new Passenger(UInt64.Parse(attributes[1]), attributes[2], UInt64.Parse(attributes[3]),
                        attributes[4], attributes[5], attributes[6], UInt64.Parse(attributes[7]))
                },
                {
                    "CA",
                    attributes => new Cargo(UInt64.Parse(attributes[1]), Single.Parse(attributes[2]), attributes[3],
                        attributes[4])
                },
                {
                    "CP",
                    attributes => new CargoPlane(UInt64.Parse(attributes[1]), attributes[2], attributes[3],
                        attributes[4], Single.Parse(attributes[5]))
                },
                {
                    "PP",
                    attributes => new PassengerPlane(UInt64.Parse(attributes[1]), attributes[2], attributes[3],
                        attributes[4], UInt16.Parse(attributes[5]), UInt16.Parse(attributes[6]),
                        UInt16.Parse(attributes[7]))
                },
                {
                    "AI",
                    attributes => new Airport(UInt64.Parse(attributes[1]), attributes[2], attributes[3],
                        Single.Parse(attributes[4]), Single.Parse(attributes[5]), Single.Parse(attributes[6]),
                        attributes[7])
                },
                {
                    "FL", attributes =>
                    {
                        var crewIDs = attributes[10].Trim('[', ']').Split(';').Select(ulong.Parse).ToArray();
                        var loadIDs = attributes[11].Trim('[', ']').Split(';').Select(ulong.Parse).ToArray();
                        return new Flight(UInt64.Parse(attributes[1]), UInt64.Parse(attributes[2]),
                            UInt64.Parse(attributes[3]), attributes[4], attributes[5], Single.Parse(attributes[6]),
                            Single.Parse(attributes[7]), Single.Parse(attributes[8]), UInt64.Parse(attributes[9]),
                            crewIDs, loadIDs);
                    }
                }
            };


        public static readonly Dictionary<string, Func<byte[], object>> objectCreatorsFromMessages =
            new Dictionary<string, Func<byte[], object>>
            {
                {
                    "NCR",
                    message =>
                    {
                        return new Crew(
                            BitConverter.ToUInt64(message, 7),
                            Encoding.ASCII.GetString(message, 17, BitConverter.ToUInt16(message, 15)),
                            BitConverter.ToUInt16(message, 17 + BitConverter.ToUInt16(message, 15)),
                            Encoding.ASCII.GetString(message, 19 + BitConverter.ToUInt16(message, 15), 12),
                            Encoding.ASCII.GetString(message, 33 + BitConverter.ToUInt16(message, 15),
                                BitConverter.ToUInt16(message, 31 + BitConverter.ToUInt16(message, 15))),
                            BitConverter.ToUInt16(message,
                                33 + BitConverter.ToUInt16(message, 15) +
                                BitConverter.ToUInt16(message, 31 + BitConverter.ToUInt16(message, 15))),
                            Encoding.ASCII.GetString(message,
                                35 + BitConverter.ToUInt16(message, 15) +
                                BitConverter.ToUInt16(message, 31 + BitConverter.ToUInt16(message, 15)), 1)
                        );
                    }
                },
                {
                    "NPA",
                    message =>
                    {
                        return new Passenger(
                            BitConverter.ToUInt64(message, 7),
                            Encoding.ASCII.GetString(message, 17, BitConverter.ToUInt16(message, 15)),
                            BitConverter.ToUInt16(message, 17 + BitConverter.ToUInt16(message, 15)),
                            Encoding.ASCII.GetString(message, 19 + BitConverter.ToUInt16(message, 15), 12),
                            Encoding.ASCII.GetString(message, 33 + BitConverter.ToUInt16(message, 15),
                                BitConverter.ToUInt16(message, 31 + BitConverter.ToUInt16(message, 15))),
                            Encoding.ASCII.GetString(message,
                                33 + BitConverter.ToUInt16(message, 15) +
                                BitConverter.ToUInt16(message, 31 + BitConverter.ToUInt16(message, 15)), 1),
                            BitConverter.ToUInt64(message,
                                34 + BitConverter.ToUInt16(message, 15) +
                                BitConverter.ToUInt16(message, 31 + BitConverter.ToUInt16(message, 15)))
                        );
                    }
                },
                {
                    "NCA", message => new Cargo(
                        BitConverter.ToUInt64(message, 7),
                        BitConverter.ToSingle(message, 15),
                        Encoding.UTF8.GetString(message, 19, 6),
                        Encoding.UTF8.GetString(message, 27, BitConverter.ToUInt16(message, 25)))
                },
                {
                    "NCP", message =>
                    {
                        ushort modelLength = BitConverter.ToUInt16(message, 28);
                        return new CargoPlane(
                            BitConverter.ToUInt64(message, 7),
                            Encoding.ASCII.GetString(message, 15, 10).TrimEnd('\0'),
                            Encoding.ASCII.GetString(message, 25, 3),
                            Encoding.ASCII.GetString(message, 30, modelLength),
                            BitConverter.ToSingle(message, 30 + modelLength)
                        );
                    }
                },
                {
                    "NPP", message =>
                    {
                        ushort modelLength = BitConverter.ToUInt16(message, 28);
                        return new PassengerPlane(
                            BitConverter.ToUInt64(message, 7),
                            Encoding.UTF8.GetString(message, 15, 10),
                            Encoding.UTF8.GetString(message, 25, 3),
                            Encoding.UTF8.GetString(message, 30, modelLength),
                            BitConverter.ToUInt16(message, 30 + modelLength),
                            BitConverter.ToUInt16(message, 32 + modelLength),
                            BitConverter.ToUInt16(message, 34 + modelLength)
                        );
                    }
                },
                {
                    "NAI", message =>
                    {
                        ushort nameLength = BitConverter.ToUInt16(message, 15);
                        return new Airport(
                            BitConverter.ToUInt64(message, 7),
                            Encoding.UTF8.GetString(message, 17, nameLength),
                            Encoding.ASCII.GetString(message, 17 + nameLength, 3),
                            BitConverter.ToSingle(message, 20 + nameLength),
                            BitConverter.ToSingle(message, 24 + nameLength),
                            BitConverter.ToSingle(message, 28 + nameLength),
                            Encoding.ASCII.GetString(message, 32 + nameLength, 3)
                        );
                    }
                },
                {
                    "NFL", message =>
                    {
                        uint messageLength = BitConverter.ToUInt32(message, 3);
                        ulong id = BitConverter.ToUInt64(message, 7);
                        ulong originID = BitConverter.ToUInt64(message, 15);
                        ulong targetID = BitConverter.ToUInt64(message, 23);
                        DateTimeOffset takeOffTimeDate = DateTimeOffset.FromUnixTimeMilliseconds(BitConverter.ToInt64(message, 31));
                        DateTimeOffset landingTimeDate = DateTimeOffset.FromUnixTimeMilliseconds(BitConverter.ToInt64(message, 39));
                        string takeOffTime = takeOffTimeDate.ToString("yyyy-MM-dd HH:mm:ss");
                        string landingTime = landingTimeDate.ToString("yyyy-MM-dd HH:mm:ss");
                        ulong planeID = BitConverter.ToUInt64(message, 47);
                        ulong[] crewID = Enumerable.Range(0, BitConverter.ToUInt16(message, 55))
                                .Select(i => BitConverter.ToUInt64(message, 57 + (8 * i))).ToArray();
                        ulong[] loadID = Enumerable
                                .Range(0, BitConverter.ToUInt16(message, 57 + (8 * BitConverter.ToUInt16(message, 55))))
                                .Select(i => BitConverter.ToUInt64(message,
                                    59 + (8 * BitConverter.ToUInt16(message, 55)) + (8 * i))).ToArray();

                        return new Flight(id, originID, targetID, takeOffTime, landingTime, 0, 0, 0,
                        planeID, crewID, loadID);
                    }
                },
            };
    }
}