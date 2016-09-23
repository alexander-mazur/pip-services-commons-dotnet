namespace PipServices.Commons.Run
{
    public interface INotifiable
    {
        void Notify(string correlationId);
    }
}