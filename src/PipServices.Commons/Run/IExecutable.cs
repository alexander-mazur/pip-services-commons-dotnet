namespace PipServices.Commons.Run
{
    public interface IExecutable
    {
        object Execute(string correlationId);
    }
}
