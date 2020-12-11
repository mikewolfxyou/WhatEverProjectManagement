namespace ProjectManagement.Api.Entities
{
    public interface IMessage<out TContent>
    {
        TContent GetContent();
    }
}