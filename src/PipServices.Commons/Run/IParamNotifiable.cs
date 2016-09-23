namespace PipServices.Commons.Run
{
    public interface IParamNotifiable
    {
        void Notify(string correlationId, Parameters args);
    }
}
