
using API_SISDE.Data;
using API_SISDE.Models.Connection;
using Grpc.Core;
using Newtonsoft.Json;
using Polly;
using RestSharp;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Globalization;
using Newtonsoft.Json.Linq;

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

                string phoneNumberId = "392617457271128";
                string accessToken = "EAAGnXYHcJ1MBO50tZCTGuJnke8LWaQSkKKVmFJBa9ozHpiysS4751cqwfwU0aJKq9lisg5AxD1ZBMzf0KNlN9kpxZCASwbqJxyl2nS8yFZCgi6JDnT74AJFRhC9VQmvZCuEV7Y58MFq7ELzpcRIZBoBDKr00bMp0BAtxnZCJF9DaGbjrh6TNwn8AtmCQzDJJLtl";

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
        //public async Task<bool> Execute(object model, string number)
        //{
        //    try
        //    {

        //        var jsonModel = JsonConvert.SerializeObject(model);

        //        string phoneNumberId = "392617457271128";
        //        string accessToken = "EAAGnXYHcJ1MBO50tZCTGuJnke8LWaQSkKKVmFJBa9ozHpiysS4751cqwfwU0aJKq9lisg5AxD1ZBMzf0KNlN9kpxZCASwbqJxyl2nS8yFZCgi6JDnT74AJFRhC9VQmvZCuEV7Y58MFq7ELzpcRIZBoBDKr00bMp0BAtxnZCJF9DaGbjrh6TNwn8AtmCQzDJJLtl";


        //        System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


        //        string url = $"https://graph.facebook.com/v21.0/{phoneNumberId}/messages";

        //        // Crear el WebRequest
        //        WebRequest request = WebRequest.Create(url);
        //        request.Method = "POST";
        //        request.ContentType = "application/json";
        //        request.Headers["Authorization"] = "Bearer " + accessToken;

        //        // Convertir el modelo JSON en bytes y enviarlo en el cuerpo de la solicitud
        //        byte[] byteArray = Encoding.UTF8.GetBytes(jsonModel);
        //        using (Stream dataStream = await request.GetRequestStreamAsync())
        //        {
        //            dataStream.Write(byteArray, 0, byteArray.Length);
        //        }

        //        // Obtener la respuesta
        //        WebResponse response = await request.GetResponseAsync();
        //        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        //        {
        //            string responseFromServer = await reader.ReadToEndAsync();
        //            if (!string.IsNullOrEmpty(responseFromServer))
        //            {
        //                return true; // Mensaje enviado con éxito
        //            }
        //        }

        //        return true; // Si todo va bien, devolver true
        //    }
        //    catch (WebException ex)
        //    {
        //        // Manejo de errores en la solicitud
        //        using (var errorResponse = (HttpWebResponse)ex.Response)
        //        {
        //            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
        //            {
        //                var errorText = await reader.ReadToEndAsync();
        //                Log("Error en la solicitud POST", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, errorText + "-" + number, 0);

        //                // Manejo específico para el límite de tasa alcanzado
        //                if (errorText.Contains("rate limit hit"))
        //                {
        //                    Log("Límite de tasa alcanzado, reintentando...", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, number, 0);
        //                    await Task.Delay(10000); // Esperar 10 segundos antes de reintentar
        //                    await Execute(model, number); // Reintentar
        //                }
        //            }
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        var line = GetLineNumber(ex);
        //        Log("Error inesperado", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + "-" + number, line);
        //        return false;
        //    }
        //}




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
