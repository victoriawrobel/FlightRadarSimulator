using NetworkSourceSimulator;

namespace OOD_24L_01180686.source.Updates
{
    public interface IObserver
    {
        void Update(IDUpdateArgs args);
        void Update(PositionUpdateArgs args);
        void Update(ContactInfoUpdateArgs args);
    }
}
