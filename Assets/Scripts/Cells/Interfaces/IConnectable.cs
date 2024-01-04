namespace Cells.Intrfeces
{
    public interface IConnectable
    {
        /**
         * Проверка присоединен ли
         */
        bool IsConnected();
        /**
         * Проверка последний ли элеменнт
         */
        bool IsLast();

        /**
         * Приципляемся в качестве трейлера
         */
        void Connect(IConnectable who);
        IConnectable GetTrailer();
        Coordinate GetCoordinate();
    }
}