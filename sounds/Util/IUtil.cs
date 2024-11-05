namespace API_SISDE.Util
{
    public interface   IUtil
    {
        object TextMessage(string message, string number);
        object ImageMessage(string url, string number);
        object AudioMessage(string url, string number);
        object VideoMessage(string url, string number);
        object DocumentMessage(string url, string number);
        object LocationMessage(string shope,string number);
        object ButtonsMessage(string number);
        object ButtonsReturnService(string number);
        object ButtonsOption(string number);
        object ButtonstratamintoFacial(string number);
        object ButtonstratamintoAp(string number);
        object ButtonGen(string number);
        object  ButtonValidateData(string number);
        object interactiveMessage(string number);
        object interactiveMessageShopes(string number);
        object interactiveMessageService(string id_suc,string number);
        object interactiveMessageServiceBack(string number);
        object interactiveMessageFacialBack(string number);
        object interactiveMessageAPBack(string number);
        object interactiveMessageServiceMa(string number);
        object interactiveMessageServiceFacial(string number);
        object interactiveMessageServiceApparatusology(string number);
        object interactiveMessageQuestions(string number);
        object interactiveMessageServiceDermapen(string message, string number);
        object interactiveMessageServiceHealing(string message, string number);
        object interactiveMessagetest(string id_service, string number);
        object interactiveMessageFamily( string number);
        object interactiveMessageData( string number);
        object AudioMessageLimFacial(string url, string number);


        object Shopes(string number);


    }
}
