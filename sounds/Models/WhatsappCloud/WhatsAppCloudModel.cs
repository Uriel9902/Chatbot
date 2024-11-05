namespace API_SISDE.Models.WhatsappCloud
{
    public class WhatsAppCloudModel
    {
        public List<Entry>? Entry { get; set; }
    }
    public class Entry
    {
        public List<Change>? Changes { get; set; }
    }
    public class Change
    {
        public Value? Value { get; set; }
        public string? Field { get; set; }
    }
    public class Value
    {
        public string? Messaging_Product { get; set; }
        public List<Message>? Messages { get; set; }
    }
    public class Message
    {
        public string? From { get; set; }
        public string? Id { get; set; }
        public string? Timestamp { get; set; }
        public string? Type { get; set; }
       
        public Text? Text { get; set; }
        public Interactive? Interactive { get; set; }

    }
    public class Interactive
    {
        public string? Type { get; set; }
        public ListReply? List_Reply { get; set; }
        public ButtonReply? Button_Reply { get; set; }
    }
    public class ListReply
    {
      
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
    public class ButtonReply
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
    }

    public class Text
    {
        public string? Body { get; set; }
    }
    public class Stepslist
    {
        public List<Steps>? Steps { get; set; }
    }
    public class Steps
    {
        public string? Number { get; set;}
        public long? Step { get; set;}
        public string? Name { get; set;}
        public string? Ap_paternal { get; set;}
        public string? Ap_maternal { get; set;}
        public string? Date_birth { get; set;}
        public int? State_birth { get; set;}
        public string? Date { get; set;}
        public string? Sexo { get; set;}
        public string? Clin { get; set;}
        public string? Service { get; set;}
    }
    public class Shopes
    {
        public string? id { get; set; }  
        public string? title { get; set; }
        public string? description { get; set; }
    }
    public class ShopesList
    {
        public List<Shopes>? Shopes { get; set; }
    }

    public class Location 
    {
        public string? latitude { get; set;}
        public string? longitude { get; set;}
        public string? name { get; set;}
        public string? address { get; set;}

    } 
    public class Prices
    {
        public string? nombre { get; set;}
        public decimal? precio { get; set;}
       

    }
    public class Locations
    {
        public List<Location>? Location { get; set; }
    }
    public class validateService 
    {
        public object? Service {  get; set; }    
    }
    public class Service
    {
        public List<validateService>? services { get; set; }
    }

    public class Price
    {
        public List<Prices>? Prices { get; set; }
    }
}
