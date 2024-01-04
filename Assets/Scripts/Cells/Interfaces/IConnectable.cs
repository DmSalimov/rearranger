namespace Cells.Intrfeces
{
    public interface IConnectable
    {
        bool IsConnected();
        bool IsLast();
        void ConnectEvent();
        IConnectable GetTrailer();
        Coordinate GetCoordinate();
    }
}