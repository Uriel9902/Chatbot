
using API_SISDE.Data;
using API_SISDE.Models.Connection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace API_SISDE.Services.WhatsappCloud.SendMessage
{
    public class WhatsappCloudSendMessage : IWhatsappCloudSendMessage
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://graph.facebook.com/"),
            Timeout = TimeSpan.FromSeconds(15)
        };

        public async Task<bool> Execute(object model, string number)
        {
            try
            {
                var jsonModel = JsonConvert.SerializeObject(model);
                //PRUEBAS
                string phoneNumberId = "392617457271128";
                string accessToken = "EAAGnXYHcJ1MBO2F26k7ObbRBwKVCz8c36B70UGliCZAcJKVVjRtfWSNU2Ad3xV8frsIBMOYjIB0ZBiUyaZAmb5CMEWA98EXlt5dI3TTsNJf5c93eEbUuqlqeiZCDv8f1ZClxtQQO05HwfmMZAvFQ2yyXjZA8jernRw0JFvcEjcbyk2wQEHAufETBUgmofGF5MeTHtibFYhhCa3fY0hawo8AiEiEDJIZD";

                //PRODUCTIVO
                //string phoneNumberId = "378445068691241";
                //string accessToken = "EAAMHjZCMOxp0BO6tLMyOVjCyPDpuo5BxZAiBTs6W2MAjI8r6giguzfn7KvTLnC6Dgzjpf6a1PUj4ECEydwtUALfViw7IQtqK4jSxZAXbSmzalr2wKV36GaXLPyvQTLvBwPAJN6ZBh9kTDtCtyuO5i8fQCbq65Cy4PvHfk1C0TTeNDhTgwOyAgor7SshZA55et";

                System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                string endpoint = $"v21.0/{phoneNumberId}/messages";

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://graph.facebook.com/v21.0/" + phoneNumberId + "/messages");
                request.Headers.Add("Authorization", "Bearer " + accessToken);

                request.Headers.ConnectionClose = true;
                request.Content = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await _httpClient.SendAsync(request);

                // Comprobar si la respuesta es exitosa
                if (response.IsSuccessStatusCode)
                {
                    return true; // Mensaje enviado con éxito
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();

                    // Si se alcanza el límite de tasa
                    if (errorMessage.Contains("rate limit hit"))
                    {
                        Log("Límite de tasa alcanzado, reintentando...", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, number, 0);

                        await Task.Delay(10000); // Esperar 10 segundos antes de reintentar
                        await Execute(model, number); // Reintentar


                        //  Log("Límite de reintentos alcanzado, no se pudo enviar el mensaje", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, number, 0);
                        return false;

                    }

                    // Registrar otros errores en la solicitud POST
                    Log("Error en la solicitud POST", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, errorMessage + "-" + number, 0);
                    return false;
                }
            }
            catch (HttpRequestException ex)
            {
                var line = GetLineNumber(ex);
                Log("Error de red en la solicitud POST", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + "-" + number, line);
                return false;
            }
            catch (Exception ex)
            {
                var line = GetLineNumber(ex);
                Log("Error inesperado", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + "-" + number, line);
                return false;
            }
        }

        public long GetLineNumber(Exception ex)
        {
            var lineNumber = 0;
            const string lineSearch = ":line ";
            var index = ex.StackTrace.LastIndexOf(lineSearch);
            if (index != -1)
            {
                var lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
                if (int.TryParse(lineNumberText, out lineNumber))
                {
                }
            }
            return lineNumber;
        }

        public async void Log(string Type, string Class, string Function, string Message, long Line)
        {

            CHATPRContext DB = new CHATPRContext();
            QueryModelsEntity Querys = new QueryModelsEntity();
            // insertLog(Type, Class, Function, Message, Line);
            Log logs = new Log();
            logs.Tipo = Type;
            logs.Descripcion = Message + " | Line: " + Line;
            logs.Ubicacion = Class + "/" + Function;
            logs.Fecha = DateTime.Now;
            await DB.Logs.AddAsync(logs);
            await DB.SaveChangesAsync();
        }
    }
}
