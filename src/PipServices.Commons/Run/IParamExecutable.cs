namespace PipServices.Commons.Run
{
    public interface IParamExecutable
    {
        object Execute(string correlationId, Parameters args);
    }
}
