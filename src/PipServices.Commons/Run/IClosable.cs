namespace PipServices.Commons.Run
{
    public interface IClosable
    {
        void Close(string correlationId);
    }
}
