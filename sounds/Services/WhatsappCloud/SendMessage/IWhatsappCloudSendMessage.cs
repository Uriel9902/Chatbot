namespace API_SISDE.Services.WhatsappCloud.SendMessage
{
    public interface IWhatsappCloudSendMessage
    {
        Task<bool> Execute(object model,string number);
    }
}
