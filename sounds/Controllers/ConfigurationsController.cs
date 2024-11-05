using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Speech.Synthesis;
using System.Text;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Text.Json;

using Newtonsoft.Json;

using System.Reflection.PortableExecutable;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using System.Dynamic;
using Microsoft.SqlServer.Server;
using Microsoft.AspNetCore.Hosting.Server;
using Grpc.Core;

using Microsoft.AspNetCore.Http;

using API_SISDE.Models.WhatsappCloud;
using API_SISDE.Services.WhatsappCloud.SendMessage;
using API_SISDE.Util;
using System.Net.Mail;
using API_SISDE.Models.Connection;

using System.Data.SqlClient;
using System.Text.RegularExpressions;
using API_SISDE.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Forms;

namespace WhatsApp_Api.Controllers
{
    [ApiController]
    [Route("api/whatsapp")]
    public class WhatsappController : Controller
    {

        private readonly IWhatsappCloudSendMessage _whatsappCloudSendMessage;
        private readonly Util _util;
        private readonly CHATPRContext cHATPR = new CHATPRContext();
        private object objectMessage;

        private QueryModelsEntity QueryModelsEntity = new QueryModelsEntity();
        public WhatsappController(IWhatsappCloudSendMessage whatsappCloudSendMessage, Util util)
        {
            _whatsappCloudSendMessage = whatsappCloudSendMessage;
            _util = util;
        }
        [HttpGet]
        public IActionResult VerifyToken()
        {
            string AccessToken = "jJMiusmOTRYV3eUB77qJffRcxw7crCuIxYRT9Hcf4EBTz1BgHhQLCciLlKiWJST1";
            var token = Request.Query["hub.verify_token"].ToString();
            var challenge = Request.Query["hub.challenge"].ToString();
            if (challenge != null && token != null && token == AccessToken)
            {

                return Ok(challenge);
            }
            else
            {
                return BadRequest();

            }
        }
        [HttpPost]
        public async Task<IActionResult> RecivedMessage([FromBody] WhatsAppCloudModel body)
        {
            try
            {
                var Message = body?.Entry?[0]?.Changes?[0]?.Value?.Messages?[0];
                var Saludo = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 12).FirstOrDefaultAsync();
                if (Message != null)
                {
                    var userNumber = Message.From?.Replace("521", "52");

                    long userState = await GetUserState(userNumber);

                    if (!string.IsNullOrEmpty(Message.Type) /*&& Message.Type.ToLower() == "text"*/)
                    {
                        string? Body = Message.Text?.Body;
                        string? bodyInt = Message?.Interactive?.List_Reply?.Id;
                        string? bodyBut = Message?.Interactive?.Button_Reply?.Id;
                        if (Body != null || bodyInt != null || bodyBut != null)
                        {
                            var a = Message;
                            //Primer Mensaje / Hola
                            if (userState == 1)
                            {
                                if (bodyInt != null)
                                {
                                    await selectedShope(userNumber, bodyInt);
                                }
                                else if (bodyBut != null)
                                {
                                    if (bodyBut == "01Si") await Register(bodyBut, userNumber);
                                    else await greeting(userNumber);
                                }
                                else if (Body != null)
                                {
                                    if (Body.ToLower().Contains("hola"))
                                        await greeting(userNumber);
                                    else
                                    {
                                        objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                    }
                                }

                            }
                            //Iniciar Registro
                            else if (userState == 2)
                            {
                                if (bodyInt != null)
                                {
                                    await Register(bodyInt, userNumber);
                                }
                                else if (bodyBut != null)
                                {
                                    await greeting(userNumber);
                                }
                                else if (Body != null)
                                {
                                    if (Body.ToLower().Contains("hola"))
                                        await greeting(userNumber);
                                    else
                                    {
                                        objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                    }
                                }
                            }
                            //Registrar Nombre
                            else if (userState == 3)
                            {

                                if (Body != null)
                                {
                                    if (Body.ToLower() == "hola") await greeting(userNumber);
                                    else await RegisterName(Body, userNumber);
                                }
                                else
                                {
                                    objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                }
                            }
                            //Registrar Apellido Paterno
                            else if (userState == 4)
                            {
                                if (Body != null)
                                {
                                    if (Body.ToLower() == "hola") await greeting(userNumber);
                                    else await RegisterLastName(Body, userNumber);
                                }
                                else
                                {
                                    objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                }


                            }
                            //Registrar Apellido M
                            else if (userState == 5)
                            {
                                if (Body != null)
                                {
                                    if (Body.ToLower() == "hola") await greeting(userNumber);
                                    else await RegisterLastNameM(Body, userNumber);
                                }
                                else
                                {
                                    objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                }


                            }
                            //Registrar Genero
                            else if (userState == 6)
                            {
                                if (Body != null)
                                {
                                    if (Body.ToLower() == "hola") await greeting(userNumber);
                                    else
                                    {
                                        objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                    }
                                }
                                else if (bodyBut != null)
                                {
                                    await RegisterGender(bodyBut, userNumber);

                                }
                                else
                                {
                                    objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                }


                            }
                            //Registrar Fecha de Nacimiento
                            else if (userState == 7)
                            {
                                if (Body != null)
                                {
                                    if (Body.ToLower() == "hola") await greeting(userNumber);
                                    else await RegisterBirthDate(Body, userNumber);
                                }
                                else
                                {
                                    objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                }


                            }
                            //Registrar Estado de Nacimiento
                            else if (userState == 8)
                            {
                                if (Body != null)
                                {
                                    if (Body.ToLower() == "hola") await greeting(userNumber);
                                    else await RegisterStateDate(Body, userNumber);
                                }
                                else
                                {
                                    objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                }


                            }
                            //Confirmar Cita
                            else if (userState == 9)
                            {
                                if (Body != null)
                                {

                                    if (Body.ToLower() == "hola") await greeting(userNumber);
                                    else
                                    {
                                        objectMessage = await _util.TextMessage("No puedo entenderte", userNumber);
                                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                                        objectMessage = await _util.ButtonValidateData(userNumber);
                                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                    }

                                }
                                else if (bodyBut != null)
                                {
                                    if (bodyBut == "01AG")
                                    {
                                        await InsertAppoiment(userNumber);
                                    }
                                    else if (bodyBut == "02CD")
                                    {
                                        await EditData(userNumber, bodyBut);
                                    }
                                    else
                                    {
                                        await greeting(userNumber);
                                    }
                                }
                                else
                                {
                                    objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                }


                            }

                            //Editar Datos
                            else if (userState == 10)
                            {
                                if (Body != null)
                                {

                                    if (Body.ToLower() == "hola") await greeting(userNumber);
                                    else
                                    {
                                        objectMessage = await _util.TextMessage("No puedo entenderte", userNumber);
                                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                                        //objectMessage = await _util.ButtonValidateData(userNumber);
                                        //await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                    }

                                }
                                else if (bodyInt != null)
                                {

                                    await EditData(userNumber, bodyInt);
                                }
                                else
                                {
                                    objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                }


                            }

                            //Editar Nombre
                            else if (userState == 11)
                            {
                                if (Body != null)
                                {

                                    if (Body.ToLower() == "hola") await greeting(userNumber);
                                    else
                                    {

                                        await QueryModelsEntity.updatename(Body, userNumber);

                                        await QueryModelsEntity.Updatesteps(9, userNumber);

                                        objectMessage = await _util.ButtonValidateData(userNumber);
                                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                                    }

                                }
                                else
                                {
                                    objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                }


                            }
                            //Editar Apellido Paterno
                            else if (userState == 12)
                            {
                                if (Body != null)
                                {

                                    if (Body.ToLower() == "hola") await greeting(userNumber);
                                    else
                                    {

                                        await QueryModelsEntity.updatelast_name(Body, userNumber);

                                        await QueryModelsEntity.Updatesteps(9, userNumber);

                                        objectMessage = await _util.ButtonValidateData(userNumber);
                                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                                    }

                                }
                                else
                                {
                                    objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                }


                            }
                            //Editar Apellido Materno
                            else if (userState == 13)
                            {
                                if (Body != null)
                                {

                                    if (Body.ToLower() == "hola") await greeting(userNumber);
                                    else
                                    {

                                        await QueryModelsEntity.updatelast_nameM(Body, userNumber);

                                        await QueryModelsEntity.Updatesteps(9, userNumber);

                                        objectMessage = await _util.ButtonValidateData(userNumber);
                                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                                    }

                                }
                                else
                                {
                                    objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                }


                            }
                            //Editar Fecha de Nacimiento
                            else if (userState == 14)
                            {
                                if (Body != null)
                                {

                                    if (Body.ToLower() == "hola") await greeting(userNumber);
                                    else
                                    {
                                        DateTime fecha;
                                        if (DateTime.TryParseExact(Body, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out fecha) && fecha < DateTime.Now && fecha.Year > DateTime.Now.Year - 120)
                                        {
                                            await QueryModelsEntity.updatedate_birth(Body, userNumber);

                                            await QueryModelsEntity.Updatesteps(9, userNumber);

                                            objectMessage = await _util.ButtonValidateData(userNumber);
                                            await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                        }
                                        else
                                        {
                                            var MessageDateBirth = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 40).FirstOrDefaultAsync();
                                            objectMessage = await _util.TextMessage(MessageDateBirth?.Descripcion ?? "", userNumber);
                                            await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                                        }


                                    }

                                }
                                else
                                {
                                    objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                }


                            }
                            //Editar Genero
                            else if (userState == 15)
                            {
                                if (Body != null)
                                {

                                    if (Body.ToLower() == "hola") await greeting(userNumber);
                                    else
                                    {
                                        objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                    }

                                }
                                else if (bodyBut !=null) 
                                {
                                    await QueryModelsEntity.updateGen(bodyBut, userNumber);

                                    await QueryModelsEntity.Updatesteps(9, userNumber);

                                    objectMessage = await _util.ButtonValidateData(userNumber);
                                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                                }
                                else
                                {
                                    objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                                }


                            }



                            else
                            {
                                objectMessage = _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                                await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                            }
                        }
                    }


                    return Ok("EVENT_RECEIVED");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                var line = GetLineNumber(ex);
                Log("Error RecivedMessage", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, line);
                return Ok("EVENT_FAILED");
            }
        }

        public async Task<IActionResult> greeting(string userNumber)
        {
            try
            {
                await QueryModelsEntity.Updatesteps(1, userNumber);
                objectMessage = await _util.interactiveMessage(userNumber);
                await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
            }
            catch (Exception ex)
            {
                var line = GetLineNumber(ex);

                Log("Error Greeting", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, line);
                throw;
            }
            return Ok();
        }

        public async Task<long> GetUserState(string userNumber)
        {
            try
            {
                Usuario User = new Usuario();
                var Registro = await cHATPR.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).FirstOrDefaultAsync();
                if (Registro == null)
                {
                    User.Numero = userNumber;
                    User.IdPaso = 1;
                    User.Pvte = true;
                    User.Fecha = DateTime.Now.ToString("yyyy-MM-dd");
                    await cHATPR.Usuarios.AddAsync(User);
                    await cHATPR.SaveChangesAsync();
                    return User.IdPaso ?? 1;
                }
                else
                {
                    return Registro.IdPaso ?? 0;
                }

            }
            catch (Exception ex)
            {
                var line = GetLineNumber(ex);

                Log("Error", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, line);
                return 0;
            }

        }

        public async Task<IActionResult> selectedShope(string userNumber, string bodyInt)
        {
            try
            {
                var isService = await cHATPR.Servicios.Where(x => x.Clave == bodyInt).FirstOrDefaultAsync();

                var isShope = await cHATPR.Sucursales.Where(x => x.Clave == bodyInt).FirstOrDefaultAsync();

                var isAparato = await cHATPR.Aparatologia.Where(x => x.Clave == bodyInt).FirstOrDefaultAsync();
                var isFacial = await cHATPR.Faciales.Where(x => x.Clave == bodyInt).FirstOrDefaultAsync();
                var isQuestion = await cHATPR.Preguntas.Where(x => x.Clave == bodyInt).FirstOrDefaultAsync();

                if (isService != null || isQuestion != null)
                {
                    await QueryModelsEntity.updateService(bodyInt, userNumber);

                    objectMessage = await _util.interactiveMessagetest(bodyInt, userNumber);
                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                    //Mostrar 'Otros Servicios'
                    if (bodyInt == "03TF" || bodyInt == "04AP")
                    {
                        objectMessage = await _util.interactiveMessageOtherServices(userNumber);
                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                    }
                    //Desea Agendar?
                    if (bodyInt == "01C" || bodyInt == "06MCR")
                    {
                        objectMessage = await _util.ButtonsOption(userNumber);
                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                    }


                }
                else if (isShope != null)
                {
                    await QueryModelsEntity.updateShope(userNumber, bodyInt);

                    objectMessage = await _util.LocationMessage(userNumber, bodyInt);
                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                    objectMessage = await _util.interactiveMessageService(userNumber, bodyInt);
                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                }

                else if (isAparato != null)
                {
                    await QueryModelsEntity.updateService(bodyInt, userNumber);
                    //Mostrar Promos
                    if (await QueryModelsEntity.PromoService(bodyInt, userNumber) != "")
                    {
                        objectMessage = await _util.ImageMessage(await QueryModelsEntity.PromoService(bodyInt, userNumber), userNumber);
                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                    }

                    objectMessage = await _util.interactiveMessagetest(bodyInt, userNumber);
                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                    //Mostrar Precios
                    if (await QueryModelsEntity.PriceServiceAP(bodyInt) != "")
                    {
                        objectMessage = await _util.ImageMessage(await QueryModelsEntity.PriceServiceAP(bodyInt), userNumber);
                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                    }
                    //Mostrar Audios
                    if (await QueryModelsEntity.AudioServiceAp(bodyInt) != "")
                    {
                        await Task.Delay(1000);
                        var MessageAudio = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 13).FirstOrDefaultAsync();

                        objectMessage = await _util.TextMessage(MessageAudio?.Descripcion ?? "Escucha el siguiente audio", userNumber);
                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                        objectMessage = await _util.AudioMessage(await QueryModelsEntity.AudioServiceAp(bodyInt), userNumber);
                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                    }
                    //Mostrar 'Otros Servicios'
                    await Task.Delay(1000);
                    objectMessage = await _util.interactiveMessageOtherServices(userNumber);
                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                    //Desea Agendar?
                    objectMessage = await _util.ButtonsOption(userNumber);
                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                }
                else if (isFacial != null)
                {
                    await QueryModelsEntity.updateService(bodyInt, userNumber);
                    //Mostrar Promos
                    if (await QueryModelsEntity.PromoService(bodyInt, userNumber) != "")
                    {
                        objectMessage = await _util.ImageMessage(await QueryModelsEntity.PromoService(bodyInt, userNumber), userNumber);
                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                    }

                    objectMessage = await _util.interactiveMessagetest(bodyInt, userNumber);
                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                    //Mostrar Precios
                    if (await QueryModelsEntity.PriceServiceFacial(bodyInt) != "")
                    {
                        objectMessage = await _util.ImageMessage(await QueryModelsEntity.PriceServiceFacial(bodyInt), userNumber);
                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                    }
                    //Mostrar Audios
                    if (await QueryModelsEntity.AudioServiceFacial(bodyInt) != "")
                    {
                        await Task.Delay(1000);
                        var MessageAudio = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 13).FirstOrDefaultAsync();

                        objectMessage = await _util.TextMessage(MessageAudio?.Descripcion ?? "Escucha el siguiente audio", userNumber);
                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                        objectMessage = await _util.AudioMessage(await QueryModelsEntity.AudioServiceFacial(bodyInt), userNumber);
                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                    }

                    //Mostrar 'Otros Servicios'
                    await Task.Delay(1000);
                    objectMessage = await _util.interactiveMessageOtherServices(userNumber);
                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                    //Desea Agendar?
                    objectMessage = await _util.ButtonsOption(userNumber);
                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                }
                else
                {
                    var Saludo = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 12).FirstOrDefaultAsync();
                    objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                }


                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public async Task<IActionResult> Register(string body, string userNumber)
        {
            try
            {
                var SelectingFamily = await cHATPR.Fams.Where(x => x.Codigo == body).FirstOrDefaultAsync();

                if (body == "01Si")
                {
                    await QueryModelsEntity.Updatesteps(2, userNumber);

                    objectMessage = await _util.interactiveMessageFamily(userNumber);
                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                }
                else if (SelectingFamily != null)
                {
                    await QueryModelsEntity.Updatesteps(3, userNumber);
                    await QueryModelsEntity.updateFamily(body, userNumber);

                    var MessageInfo = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 5).FirstOrDefaultAsync();
                    objectMessage = await _util.TextMessage(MessageInfo?.Descripcion ?? "", userNumber);
                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                    var MessageName = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 6).FirstOrDefaultAsync();
                    objectMessage = await _util.TextMessage(MessageName?.Descripcion ?? "", userNumber);
                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                }

                else if (body.ToLower().Contains("hola"))
                {
                    await greeting(userNumber);
                }
                else
                {
                    var Saludo = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 12).FirstOrDefaultAsync();
                    objectMessage = await _util.TextMessage(Saludo?.Descripcion ?? "No puedo entenderte", userNumber);
                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);
                }

                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public async Task<IActionResult> RegisterName(string body, string userNumber)
        {
            try
            {
                await QueryModelsEntity.updatename(body, userNumber);

                var MessageLastName = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 7).FirstOrDefaultAsync();
                objectMessage = await _util.TextMessage(MessageLastName?.Descripcion ?? "", userNumber);
                await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                await QueryModelsEntity.Updatesteps(4, userNumber);
                return Ok();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IActionResult> RegisterLastName(string body, string userNumber)
        {
            try
            {
                await QueryModelsEntity.updatelast_name(body, userNumber);

                var MessageLastName = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 8).FirstOrDefaultAsync();
                objectMessage = await _util.TextMessage(MessageLastName?.Descripcion ?? "", userNumber);
                await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                await QueryModelsEntity.Updatesteps(5, userNumber);
                return Ok();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> RegisterLastNameM(string body, string userNumber)
        {
            try
            {
                await QueryModelsEntity.updatelast_nameM(body, userNumber);

                objectMessage = await _util.ButtonGen(userNumber);
                await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                await QueryModelsEntity.Updatesteps(6, userNumber);
                return Ok();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> RegisterGender(string body, string userNumber)
        {
            try
            {
                await QueryModelsEntity.updateGen(body, userNumber);

                var MessageLastName = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 9).FirstOrDefaultAsync();
                objectMessage = await _util.TextMessage(MessageLastName?.Descripcion ?? "", userNumber);
                await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                await QueryModelsEntity.Updatesteps(7, userNumber);
                return Ok();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> RegisterBirthDate(string body, string userNumber)
        {
            try
            {
                DateTime fecha;
                if (DateTime.TryParseExact(body, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out fecha) && fecha < DateTime.Now && fecha.Year > DateTime.Now.Year - 120)
                {
                    await QueryModelsEntity.updatedate_birth(body, userNumber);

                    var MessageState = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 10).FirstOrDefaultAsync();
                    objectMessage = await _util.TextMessage(MessageState?.Descripcion ?? "", userNumber);
                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                    await QueryModelsEntity.Updatesteps(8, userNumber);
                }
                else
                {
                    var MessageDateBirth = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 40).FirstOrDefaultAsync();
                    objectMessage = await _util.TextMessage(MessageDateBirth?.Descripcion ?? "", userNumber);
                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                }


                return Ok();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> RegisterStateDate(string body, string userNumber)
        {
            try
            {
                var States = await cHATPR.Estados.ToListAsync();
                byte StateConfirm = 0;
                foreach (var state in States)
                {
                    if (QueryModelsEntity.RemoveDiacritics(body).ToLower() == QueryModelsEntity.RemoveDiacritics(state.Nombre).ToLower())
                    {
                        StateConfirm = state.IdEstado;
                        await QueryModelsEntity.updatestate_birth(StateConfirm, userNumber);

                        objectMessage = await _util.ButtonValidateData(userNumber);
                        await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                        await QueryModelsEntity.Updatesteps(9, userNumber);
                    }
                }
                if (StateConfirm == 0)
                {
                    var MessageStateError = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 41).FirstOrDefaultAsync();
                    objectMessage = await _util.TextMessage(MessageStateError?.Descripcion ?? "", userNumber);
                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                }


                return Ok();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> InsertAppoiment(string userNumber)
        {
            try
            {
                await QueryModelsEntity.insertNewService(userNumber);

                var MessageStateError = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 11).FirstOrDefaultAsync();
                objectMessage = await _util.TextMessage(MessageStateError?.Descripcion ?? "", userNumber);
                await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                await QueryModelsEntity.Updatesteps(1, userNumber);

                return Ok();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IActionResult> EditData(string userNumber, string body)
        {
            try
            {
                if (body == "02CD")
                {

                    objectMessage = await _util.interactiveMessageData(userNumber);

                    await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                    await QueryModelsEntity.Updatesteps(10, userNumber);


                }
                else
                {
                    switch (body)
                    {
                        case "nombre":
                            var MessageName = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 6).FirstOrDefaultAsync();
                            objectMessage = await _util.TextMessage(MessageName?.Descripcion ?? "", userNumber);
                            await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                            await QueryModelsEntity.Updatesteps(11, userNumber);

                            break;
                        case "ap_paterno":
                            var MessageLName = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 7).FirstOrDefaultAsync();
                            objectMessage = await _util.TextMessage(MessageLName?.Descripcion ?? "", userNumber);
                            await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                            await QueryModelsEntity.Updatesteps(12, userNumber);

                            break;
                        case "ap_materno":
                            var MessageLNameM = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 8).FirstOrDefaultAsync();
                            objectMessage = await _util.TextMessage(MessageLNameM?.Descripcion ?? "", userNumber);
                            await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                            await QueryModelsEntity.Updatesteps(13, userNumber);

                            break;
                        case "f_nacimiento":
                            var MessageBirth = await cHATPR.Mensajes.AsNoTracking().Where(x => x.Id == 9).FirstOrDefaultAsync();
                            objectMessage = await _util.TextMessage(MessageBirth?.Descripcion ?? "", userNumber);
                            await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                            await QueryModelsEntity.Updatesteps(14, userNumber);

                            break;
                        case "sexo":
                            
                            objectMessage = await _util.ButtonGen(userNumber);
                            await _whatsappCloudSendMessage.Execute(objectMessage, userNumber);

                            await QueryModelsEntity.Updatesteps(15, userNumber);

                            break;

                    }



                }
                return Ok();

            }
            catch (Exception)
            {

                throw;
            }
        }



        //public async Task<IActionResult> list_interactive(string id_suc, string id_service, string Title_suc, string userNumber)
        //{
        //    try
        //    {




        //        //Proceso de sucursales
        //        QueryModelsEntity Querys = new QueryModelsEntity();

        //        object objectMessage;
        //        List<Steps> steps = new List<Steps>();
        //        steps = Querys.Steps(userNumber);
        //        if (steps.Select(x => x.State_birth != null).FirstOrDefault()
        //            && id_service != "sexo"
        //            && id_service != "nombre"
        //            && id_service != "ap_materno"
        //            && id_service != "ap_paterno"
        //            && id_service != "f_nacimiento"
        //            && Querys.QueryGetShopesValidate(id_suc).ToString() == ""
        //            && Querys.QueryGetServiceAp(id_suc).ToString() != ""
        //            && Querys.QueryGetServiceF(id_suc).ToString() != ""
        //            && Querys.MessageServices(id_service).ToString() != ""
        //            && id_service != "id_estado"
        //            && id_service != "id_sucursal")
        //        {
        //            objectMessage = _util.ButtonValidateData(userNumber);
        //            await _whatsappCloudSendMessage.Execute(objectMessage);
        //        }
        //        else
        //        {



        //            if (Querys.QueryGetShopesValidate(id_suc).ToString() != "")
        //            {
        //                var i = Querys.QueryGetShopesValidate(id_suc).ToString();
        //                //Querys.updateResetpvteinteractive(userNumber);
        //                steps = new List<Steps>();
        //                steps = Querys.Steps(userNumber);
        //                if (steps.Select(x => x.Step == 2).FirstOrDefault() && validatesteps(userNumber) == "clin")
        //                {

        //                    Querys.updateShope(id_suc, userNumber);
        //                    objectMessage = _util.ButtonValidateData(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);

        //                }

        //                else
        //                {
        //                    Querys.Updatesteps("3", userNumber);
        //                    Querys.updateShope(id_suc, userNumber);




        //                    objectMessage = _util.LocationMessage(id_suc, userNumber);

        //                    await _whatsappCloudSendMessage.Execute(objectMessage);

        //                    Querys.updateShope(id_suc, userNumber);

        //                    objectMessage = _util.interactiveMessageService(id_suc, userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);


        //                }



        //            }
        //            else
        //            {

        //                //Proceso de Servicios



        //                Querys.Updatesteps("3", userNumber);
        //                if (Querys.QueryGetServiceFValidate(id_service) == "03TF")
        //                {
        //                    Querys.updateResetpvteinteractive(userNumber);
        //                    objectMessage = _util.interactiveMessageServiceFacial(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);
        //                    objectMessage = _util.ButtonsReturnService(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);

        //                }
        //                else if (Querys.QueryGetServiceApValidate(id_service) == "04AP")
        //                {
        //                    Querys.updateResetpvteinteractive(userNumber);
        //                    objectMessage = _util.interactiveMessageServiceApparatusology(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);
        //                    objectMessage = _util.ButtonsReturnService(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);

        //                }
        //                else if (Querys.QueryGetServiceApValidate(id_service) == "07PRE")
        //                {
        //                    Querys.updateResetpvteinteractive(userNumber);
        //                    objectMessage = _util.interactiveMessageQuestions(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);
        //                    objectMessage = _util.ButtonsReturnService(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);

        //                }

        //                else if ((Querys.QueryGetServiceAp(id_service).ToString() != ""
        //                    && Querys.GetValidateService(userNumber, id_service) == null)
        //                    || Querys.GetValidateService(userNumber, id_service) != null
        //                    && Querys.QueryGetServiceAp(id_service).ToString() != "")
        //                {
        //                    Querys.updateResetpvteinteractive(userNumber);
        //                    if (Querys.PromoService(id_service, userNumber) != "")
        //                    {


        //                        objectMessage = _util.ImageMessage(Querys.PromoService(id_service, userNumber), userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);

        //                    }




        //                    Querys.updateService(id_service, userNumber);


        //                    objectMessage = _util.interactiveMessagetest(id_service, userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);

        //                    if (Querys.PriceServiceAP(id_service) != "")
        //                    {


        //                        objectMessage = _util.ImageMessage(Querys.PriceServiceAP(id_service), userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);

        //                    }


        //                    Thread.Sleep(1000);
        //                    Querys.updateService(id_service, userNumber);

        //                    objectMessage = _util.ButtonstratamintoAp(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);



        //                    if (Querys.AudioServiceAp(id_service) != "")
        //                    {
        //                        objectMessage = _util.TextMessage(Querys.MessageAudio(), userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);

        //                        objectMessage = _util.AudioMessage(Querys.AudioServiceAp(id_service), userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);


        //                    }

        //                }
        //                else if (Querys.QueryGetServiceF(id_service).ToString() != "")
        //                //&& Querys.GetValidateService(userNumber, id_service) == null)

        //                {
        //                    Querys.updateResetpvteinteractive(userNumber);
        //                    if (Querys.PromoService(id_service, userNumber) != "")
        //                    {


        //                        objectMessage = _util.ImageMessage(Querys.PromoService(id_service, userNumber), userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);
        //                    }


        //                    Querys.updateService(id_service, userNumber);




        //                    objectMessage = _util.interactiveMessagetest(id_service, userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);

        //                    if (Querys.PriceServiceFacial(id_service) != "")
        //                    {


        //                        objectMessage = _util.ImageMessage(Querys.PriceServiceFacial(id_service), userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);
        //                    }




        //                    objectMessage = _util.ButtonstratamintoFacial(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);

        //                    if (Querys.AudioServiceFacial(id_service) != "")
        //                    {
        //                        objectMessage = _util.TextMessage(Querys.MessageAudio(), userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);

        //                        objectMessage = _util.AudioMessage(Querys.AudioServiceFacial(id_service), userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);

        //                    }




        //                }
        //                else if (Querys.QueryGetQuestion(id_service).ToString() != "")
        //                {
        //                    objectMessage = _util.interactiveMessagetest(id_service, userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);
        //                    objectMessage = _util.ButtonsReturnService(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);


        //                }
        //                else if (id_service == Querys.ValidateFamily(id_service, userNumber))
        //                {
        //                    steps = new List<Steps>();
        //                    steps = Querys.Steps(userNumber);

        //                    if (steps.Select(x => x.State_birth == null).FirstOrDefault())
        //                    {
        //                        Querys.updateReset(userNumber);
        //                    }
        //                    else if (steps.Select(x => x.Service == null).FirstOrDefault())

        //                    {
        //                        objectMessage = _util.TextMessage(Querys.MessageRegistrerEmpty(), userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);
        //                    }
        //                    else
        //                    {
        //                        objectMessage = _util.ButtonValidateData(userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);
        //                    }

        //                    if (Querys.QueryValidateService(id_service, userNumber) == "" || Querys.QueryValidateService(id_service, userNumber) == null)
        //                    {



        //                        objectMessage = _util.TextMessage(Querys.MessageAcept(), userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);
        //                        objectMessage = _util.TextMessage(Querys.MessageRegistrerName(), userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);

        //                        Querys.Updatesteps("2", userNumber);
        //                    }
        //                    else if (Querys.QueryValidateServiceRegistrer(id_service, userNumber) == "" || Querys.QueryValidateServiceRegistrer(id_service, userNumber) == null)
        //                    {

        //                        Querys.insertNewService(userNumber);
        //                        objectMessage = _util.TextMessage(Querys.MessageotherService(userNumber), userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);
        //                        objectMessage = _util.interactiveMessageServiceBack(userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);
        //                        Querys.Updatesteps("3", userNumber);
        //                    }
        //                    else
        //                    {
        //                        objectMessage = _util.TextMessage(Querys.MessageotherService(userNumber), userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);
        //                        objectMessage = _util.interactiveMessageServiceBack(userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);

        //                        Querys.Updatesteps("3", userNumber);
        //                    }

        //                }
        //                else if (id_service == "02CRD")
        //                {
        //                    Querys.updateResetpvteinteractive(userNumber);
        //                    objectMessage = _util.interactiveMessagetest(id_service, userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);


        //                }
        //                else if (id_service == "05EST")
        //                {



        //                    Querys.updateResetpvteinteractive(userNumber);
        //                    objectMessage = _util.interactiveMessagetest(id_service, userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);

        //                    if (Querys.PromoService(id_service, userNumber) != "")
        //                    {


        //                        objectMessage = _util.ImageMessage(Querys.PromoService(id_service, userNumber), userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);
        //                    }
        //                }
        //                else if (validatesteps(userNumber) == "" && Querys.ValidateDataColumns(id_service) != "")
        //                {

        //                    if (steps.Select(x => x.Step == 1).FirstOrDefault() || steps.Select(x => x.Step == 3).FirstOrDefault())
        //                    {
        //                        objectMessage = _util.TextMessage(Querys.MessageRegistrerEmpty(), userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);
        //                    }
        //                    else if (id_service == "id_sucursal")
        //                    {
        //                        Querys.updateData(id_service, userNumber);
        //                        Querys.Updatesteps("2", userNumber);
        //                        objectMessage = _util.interactiveMessageShopes(userNumber);
        //                        await _whatsappCloudSendMessage.Execute(objectMessage);
        //                    }


        //                    else
        //                    {


        //                        Querys.Updatesteps("2", userNumber);


        //                        Querys.updateData(id_service, userNumber);
        //                        if (validatesteps(userNumber) == "sexo")
        //                        {
        //                            objectMessage = _util.ButtonGen(userNumber);
        //                            await _whatsappCloudSendMessage.Execute(objectMessage);

        //                        }

        //                        else
        //                        {

        //                            objectMessage = _util.TextMessage(validatesteps(userNumber), userNumber);
        //                            await _whatsappCloudSendMessage.Execute(objectMessage);
        //                        }

        //                    }

        //                }
        //                else if (id_service == "01C" || id_service == "06MCR" || id_service == "07PRE")
        //                {
        //                    Querys.updateResetpvteinteractive(userNumber);
        //                    Querys.updateService(id_service, userNumber);
        //                    objectMessage = _util.interactiveMessagetest(id_service, userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);
        //                    objectMessage = _util.ButtonsOption(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);
        //                }

        //                else
        //                {


        //                    objectMessage = _util.TextMessage(Querys.MessageRegistrerEmpty(), userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);
        //                    objectMessage = _util.interactiveMessagetest(id_service, userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);
        //                    //  Querys.updateReset(userNumber);
        //                    //Querys.updateService(id_service, userNumber);

        //                    // objectMessage = _util.ButtonsOption(userNumber);
        //                    // await _whatsappCloudSendMessage.Execute(objectMessage);





        //                }

        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        var line = GetLineNumber(ex);

        //        Log("Error", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, line);
        //        throw;
        //    }
        //    return Ok();
        //}
        //public async Task<IActionResult> button_interactive(string id_option, string userNumber)
        //{
        //    try
        //    {
        //        QueryModelsEntity Querys = new QueryModelsEntity();
        //        object objectMessage;

        //        List<Steps> steps = new List<Steps>();
        //        steps = Querys.Steps(userNumber);
        //        if (validatesteps(userNumber) != ""
        //            && id_option != "01CD"
        //            && id_option != "02FD"
        //            && id_option != "03Can"
        //            && id_option != "01H"
        //            && id_option != "02M"
        //            && id_option != "03S"
        //            && id_option != "01AGF"
        //            && id_option != "01AGAP"
        //            && id_option != "01SA"
        //            && id_option != "01Si"
        //            && id_option != "02No"
        //            && id_option != "01S"
        //            && id_option != "1"
        //            && id_option != "2"
        //            && id_option != "01SF"
        //            && id_option != "02MS"
        //            && id_option != "02MSP"

        //            )
        //        {
        //            objectMessage = _util.ButtonValidateData(userNumber);
        //            await _whatsappCloudSendMessage.Execute(objectMessage);
        //        }
        //        else
        //        {




        //            //Querys.updateReset(userNumber);
        //            if (id_option == "01S")
        //            {
        //                objectMessage = _util.interactiveMessageServiceBack(userNumber);
        //                await _whatsappCloudSendMessage.Execute(objectMessage);
        //            }
        //            else if (id_option == "1")
        //            {

        //                objectMessage = _util.TextMessage(Querys.MessageAcept(), userNumber);
        //                await _whatsappCloudSendMessage.Execute(objectMessage);
        //                objectMessage = _util.TextMessage(Querys.MessageRegistrerName(), userNumber);
        //                await _whatsappCloudSendMessage.Execute(objectMessage);
        //                Querys.Updatesteps("2", userNumber);

        //            }
        //            else if (id_option == "2")
        //            {
        //                objectMessage = _util.interactiveMessageServiceBack(userNumber);
        //                await _whatsappCloudSendMessage.Execute(objectMessage);
        //                Querys.Updatesteps("3", userNumber);
        //            }
        //            else if (id_option == "01SF")
        //            {
        //                objectMessage = _util.interactiveMessageFacialBack(userNumber);
        //                await _whatsappCloudSendMessage.Execute(objectMessage);
        //                Querys.Updatesteps("3", userNumber);
        //            }
        //            else if (id_option == "02MS")
        //            {
        //                objectMessage = _util.interactiveMessageServiceBack(userNumber);
        //                await _whatsappCloudSendMessage.Execute(objectMessage);
        //                Querys.Updatesteps("3", userNumber);
        //            }
        //            else if (id_option == "01SA")
        //            {
        //                objectMessage = _util.interactiveMessageAPBack(userNumber);
        //                await _whatsappCloudSendMessage.Execute(objectMessage);
        //                Querys.Updatesteps("3", userNumber);
        //            }
        //            else if (id_option == "02MSP")
        //            {
        //                objectMessage = _util.interactiveMessageServiceBack(userNumber);
        //                await _whatsappCloudSendMessage.Execute(objectMessage);
        //                Querys.Updatesteps("3", userNumber);
        //            }
        //            else if (validatesteps(userNumber) == "" && id_option == "01AGF")
        //            {

        //                objectMessage = _util.interactiveMessageFamily(userNumber);
        //                await _whatsappCloudSendMessage.Execute(objectMessage);


        //            }
        //            else if (id_option == "01AGAP")
        //            {


        //                objectMessage = _util.interactiveMessageFamily(userNumber);
        //                await _whatsappCloudSendMessage.Execute(objectMessage);


        //            }
        //            else if (validatesteps(userNumber) == "" && id_option == "01Si")
        //            {


        //                objectMessage = _util.interactiveMessageFamily(userNumber);
        //                await _whatsappCloudSendMessage.Execute(objectMessage);



        //            }
        //            else if (id_option == "02No")
        //            {



        //                objectMessage = _util.interactiveMessageServiceBack(userNumber);
        //                await _whatsappCloudSendMessage.Execute(objectMessage);
        //                Querys.updateResetpvte(userNumber);
        //                Querys.Updatesteps("3", userNumber);


        //            }
        //            else if (validatesteps(userNumber) != "" && id_option == "01H")
        //            {
        //                steps = new List<Steps>();
        //                steps = Querys.Steps(userNumber);

        //                Querys.updateGen("H", userNumber);
        //                if (validatesteps(userNumber) != "")
        //                {

        //                    Querys.Updatesteps("2", userNumber);
        //                    objectMessage = _util.TextMessage(validatesteps(userNumber), userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);

        //                }
        //                else
        //                {
        //                    // Querys.updateGen("H", userNumber);
        //                    objectMessage = _util.ButtonValidateData(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);
        //                }


        //            }
        //            else if (validatesteps(userNumber) != "" && id_option == "02M")
        //            {


        //                steps = new List<Steps>();
        //                steps = Querys.Steps(userNumber);
        //                Querys.updateGen("M", userNumber);

        //                if (validatesteps(userNumber) != "")
        //                {

        //                    Querys.Updatesteps("2", userNumber);
        //                    objectMessage = _util.TextMessage(validatesteps(userNumber), userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);

        //                }
        //                else
        //                {
        //                    // Querys.updateGen("M", userNumber);
        //                    objectMessage = _util.ButtonValidateData(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);
        //                }

        //            }
        //            else if (validatesteps(userNumber) != "" && id_option == "03S")
        //            {


        //                steps = new List<Steps>();
        //                steps = Querys.Steps(userNumber);

        //                Querys.updateGen("X", userNumber);
        //                if (validatesteps(userNumber) != "")
        //                {
        //                    Querys.Updatesteps("2", userNumber);
        //                    objectMessage = _util.TextMessage(validatesteps(userNumber), userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);
        //                }
        //                else
        //                {
        //                    objectMessage = _util.ButtonValidateData(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);
        //                    //  Querys.updateGen("X", userNumber);
        //                }
        //            }

        //            else if (validatesteps(userNumber) == "" && id_option == "01CD")
        //            {



        //                objectMessage = _util.TextMessage(Querys.MessageRegistrerComplete(), userNumber);
        //                await _whatsappCloudSendMessage.Execute(objectMessage);
        //                objectMessage = _util.interactiveMessageServiceBack(userNumber);
        //                await _whatsappCloudSendMessage.Execute(objectMessage);


        //                Querys.insertNewServiceRegistrer(userNumber);
        //                Querys.updateReset(userNumber);






        //            }
        //            else if (validatesteps(userNumber) == "" && id_option == "02FD")
        //            {

        //                steps = new List<Steps>();
        //                steps = Querys.Steps(userNumber);
        //                if (steps.Select(x => x.Step == 1).FirstOrDefault())
        //                {
        //                    objectMessage = _util.TextMessage(Querys.MessageRegistrerEmpty(), userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);
        //                }
        //                else
        //                {

        //                    objectMessage = _util.interactiveMessageData(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);


        //                }



        //            }
        //            else if (validatesteps(userNumber) == "" && id_option == "03Can" || validatesteps(userNumber) != "" && id_option == "03Can")
        //            {

        //                steps = new List<Steps>();
        //                steps = Querys.Steps(userNumber);
        //                if (steps.Select(x => x.Step == 1).FirstOrDefault())
        //                {
        //                    objectMessage = _util.TextMessage(Querys.MessageRegistrerEmpty(), userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);
        //                }
        //                else
        //                {

        //                    objectMessage = _util.interactiveMessageServiceBack(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);

        //                    Querys.updateResetpvte(userNumber);

        //                }


        //            }
        //            else if (validatesteps(userNumber) == "")
        //            {
        //                objectMessage = _util.TextMessage(Querys.MessageRegistrerEmpty(), userNumber);
        //                await _whatsappCloudSendMessage.Execute(objectMessage);
        //            }
        //            else if (validatesteps(userNumber) != "")
        //            {
        //                if (validatesteps(userNumber) == "sexo")
        //                {
        //                    objectMessage = _util.ButtonGen(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);
        //                }
        //                else if (validatesteps(userNumber) == "clin")
        //                {
        //                    objectMessage = _util.interactiveMessageShopes(userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);
        //                }
        //                else
        //                {

        //                    objectMessage = _util.TextMessage(validatesteps(userNumber), userNumber);
        //                    await _whatsappCloudSendMessage.Execute(objectMessage);
        //                }



        //            }



        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var line = GetLineNumber(ex);

        //        Log("Error", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, line);
        //        throw;
        //    }
        //    return Ok();
        //}
        //public string validatesteps(string userNumber)
        //{
        //    string? objectMessage = "";
        //    try
        //    {
        //        List<Steps> steps = new List<Steps>();
        //        QueryModelsEntity Querys = new QueryModelsEntity();
        //        steps = Querys.Steps(userNumber);
        //        if (steps.Select(x => x.Step == 2).FirstOrDefault() && steps.Select(x => x.Name == null).FirstOrDefault())
        //        {

        //            objectMessage = Querys.MessageRegistrerName();


        //        }

        //        else if ((steps.Select(x => x.Step == 2).FirstOrDefault() && steps.Select(x => x.Ap_paternal == null).FirstOrDefault()))
        //        {

        //            objectMessage = Querys.MessageRegistrerLastName();

        //        }


        //        else if (steps.Select(x => x.Name != null).FirstOrDefault() && steps.Select(x => x.Ap_maternal == null).FirstOrDefault())
        //        {



        //            if ((steps.Select(x => x.Step == 2).FirstOrDefault() && steps.Select(x => x.Ap_maternal).FirstOrDefault() == null))
        //            {

        //                objectMessage = Querys.MessageRegistrerLastNameM();

        //            }


        //        }

        //        else if (steps.Select(x => x.Ap_maternal != null).FirstOrDefault() && steps.Select(x => x.Date_birth == null).FirstOrDefault())
        //        {



        //            if ((steps.Select(x => x.Step == 2).FirstOrDefault() && steps.Select(x => x.Date_birth).FirstOrDefault() == null))
        //            {

        //                objectMessage = Querys.MessageRegistrerbirth();

        //            }





        //        }
        //        else if (steps.Select(x => x.Step == 2).FirstOrDefault() && steps.Select(x => x.Date_birth != null).FirstOrDefault() && steps.Select(x => x.State_birth == null).FirstOrDefault())
        //        {



        //            if (steps.Select(x => x.State_birth).FirstOrDefault() == null)
        //            {

        //                objectMessage = Querys.MessageRegistrerstate_birth();

        //            }





        //        }
        //        else if (steps.Select(x => x.Step == 2).FirstOrDefault() && steps.Select(x => x.Sexo == null).FirstOrDefault() && steps.Select(x => x.Service != null).FirstOrDefault())
        //        {



        //            objectMessage = "sexo";







        //        }
        //        else if (steps.Select(x => x.Clin == null).FirstOrDefault())
        //        {
        //            objectMessage = "clin";
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        var line = GetLineNumber(ex);

        //        Log("Error", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, line);
        //        throw;
        //    }
        //    return objectMessage;
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

        public void Log(string Type, string Class, string Function, string Message, long Line)
        {

            CHATPRContext DB = new CHATPRContext();
            QueryModelsEntity Querys = new QueryModelsEntity();
            // insertLog(Type, Class, Function, Message, Line);
            Log logs = new Log();
            logs.Tipo = Type;
            logs.Descripcion = Message + " | Line: " + Line;
            logs.Ubicacion = Class + "/" + Function;
            logs.Fecha = DateTime.Now;
            DB.Logs.Add(logs);
            DB.SaveChanges();
        }



    }

}
