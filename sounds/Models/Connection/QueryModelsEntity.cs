using API_SISDE.Data;
using API_SISDE.Models.WhatsappCloud;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Xml.Linq;

namespace API_SISDE.Models.Connection
{
    public class QueryModelsEntity
    {
        CHATPRContext DB = new CHATPRContext();

        public List<Steps> Steps(string numero)
        {
            Stepslist stepslist = new Stepslist();
            stepslist.Steps = new List<Steps>();
            stepslist.Steps.Add(new Steps
            {
                Number = "",
                Step = 0,
                Name = "",
                Ap_paternal = "",
                Ap_maternal = "",
                Date_birth = "",
                State_birth = 0,
                Date = "",
                Sexo = "",
                Clin = "",
                Service = "",
            });

            try
            {

                foreach (var serv in DB.Usuarios.Where(x => x.Numero == numero && x.Pvte == true).ToList())
                {
                    stepslist.Steps = new List<Steps>();
                    stepslist.Steps.Add(new Steps
                    {
                        Number = serv.Numero,
                        Step = serv.IdPaso,
                        Name = serv.Nombre == "" ? null : serv.Nombre,
                        Ap_paternal = serv.ApPaterno == "" ? null : serv.ApPaterno,
                        Ap_maternal = serv.ApMaterno == "" ? null : serv.ApMaterno,
                        Date_birth = serv.FNacimiento == "" ? null : serv.FNacimiento,
                        State_birth = serv.IdEstado,
                        Date = serv.Fecha == "" ? null : serv.Fecha,
                        Sexo = serv.Sexo == "" ? null : serv.Sexo,
                        Clin = serv.IdSucursal == "" ? null : serv.IdSucursal,
                        Service = serv.Servicio == "" ? null : serv.Servicio,
                    });
                }


            }
            catch (Exception)
            {

                throw;
            }
            return stepslist.Steps.ToList();
        }

        public List<Steps> StepsName(string numero)
        {
            Stepslist stepslist = new Stepslist();
            try
            {
                foreach (var serv in DB.Usuarios.Where(x => x.Numero == numero).ToList())
                {
                    stepslist.Steps = new List<Steps>();
                    stepslist.Steps.Add(new Steps
                    {
                        Number = serv.Numero,
                        Step = serv.IdPaso,
                        Name = serv.Nombre == "" ? null : serv.Nombre,
                        Ap_paternal = serv.ApPaterno == "" ? null : serv.ApPaterno,
                        Ap_maternal = serv.ApMaterno == "" ? null : serv.ApMaterno,
                        Date_birth = serv.FNacimiento == "" ? null : serv.FNacimiento,
                        State_birth = serv.IdEstado,
                        Date = serv.Fecha == "" ? null : serv.Fecha,

                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return stepslist.Steps.ToList();
        }

        public async Task<object> QueryGetShopes()
        {

            try
            {
                return await DB.Sucursales.Select(x => new
                {
                    id = x.Clave,
                    title = x.Nombre,
                    description = x.Direccion,

                }).ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Shopes> QueryGetShopesUpdate(string cli)
        {
            List<Shopes> shopes = new List<Shopes>();

            try
            {
                //shopes.Shopes = new List<Shopes>();
                foreach (var suc in await DB.Sucursales.Where(x => x.Clave == cli).ToListAsync())
                {

                    shopes.Add(new Shopes
                    {
                        id = suc.Clave,
                        title = suc.Nombre,
                        description = suc.Direccion.Replace("📍", ""),
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return shopes.ToList().FirstOrDefault();

        }

        public async Task<object> QueryGetservice()
        {
            try
            {
                return await DB.Servicios.Select(x => new
                {
                    id = x.Clave,
                    title = x.Titulo,
                    description = x.Descripcion == "" ? null : x.Descripcion,

                }).ToListAsync();


            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<object> QueryGetserviceFacial()
        {
            try
            {
                return await DB.Faciales.Select(x => new
                {
                    id = x.Clave,
                    title = x.Titulo,
                    description = x.Descripcion == "" ? null : x.Descripcion,
                }).ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<object> QueryGetserviceAparatologia()
        {
            //List<object> results = new List<object>();
            try
            {
                return await DB.Aparatologia.Select(x => new
                {
                    id = x.Clave,
                    title = x.Titulo,
                    description = x.Descripcion == "" ? null : x.Descripcion,
                }).ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<object> QueryGetQuestions()
        {

            try
            {
                return await DB.Preguntas.Select(x => new
                {
                    id = x.Clave,
                    title = x.Pregunta1,
                    description = x.Descripcion,

                }).ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<string> QueryGetgreeting()
        {


            try
            {
                var Saludo = await DB.Mensajes.AsNoTracking().Where(x => x.Id == 2).FirstOrDefaultAsync();
                return Saludo?.Descripcion ?? " ";
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<string> QueryGetserviceMessage()
        {
            try
            {
                var Service = await DB.Mensajes.AsNoTracking().Where(x => x.Nombre == "Servicio").FirstOrDefaultAsync();
                if (Service != null)
                {
                    return Service.Descripcion;
                }
                return "";

            }
            catch (Exception ex)
            {

                return "";
            }

        }

        public string QueryGetserviceMessageFacial()
        {
            string Service = "";
            try
            {
                foreach (var msj in DB.Mensajes.Where(x => x.Nombre == "03TF").ToList())
                {
                    Service = msj.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Service;
        }

        public string QueryGetserviceMessageaparatologia()
        {
            string? Service = "";
            try
            {
                foreach (var msj in DB.Mensajes.Where(x => x.Nombre == "04AP").ToList())
                {
                    Service = msj.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;

            }
            return Service;
        }

        public string QueryGetShopesValidate(string id_suc)
        {
            string greeting = "";
            try
            {
                foreach (var suc in DB.Sucursales.Where(x => x.Clave == id_suc).ToList())
                {
                    greeting = suc.Nombre;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return greeting;
        }

        public string QueryGetServiceFValidate(string id_service)
        {
            string facial = "";
            try
            {
                foreach (var serv in DB.Servicios.Where(x => x.Clave == id_service).ToList())
                {
                    facial = serv.Clave;
                }
            }
            catch (Exception)
            {

                throw;

            }
            return facial;
        }

        public string QueryGetServiceApValidate(string id_service)
        {
            string Aparatologia = "";
            try
            {
                foreach (var ap in DB.Servicios.Where(x => x.Clave == id_service).ToList())
                {
                    Aparatologia = ap.Clave;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Aparatologia;
        }

        public string QueryGetQuestion(string id_service)
        {
            string? Preguntas = "";
            try
            {
                foreach (var pre in DB.Preguntas.Where(x => x.Clave == id_service).ToList())
                {
                    Preguntas = pre.Clave;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Preguntas;
        }

        public int QueryGetShope(string Shope)
        {
            int id_shope = 0;
            try
            {

                foreach (var shop in DB.Sucursales.Where(x => x.Clave == Shope).ToList())
                {
                    id_shope = shop.Id;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return id_shope;
        }

        public string QueryValidateService(string id_service, string usernumber)
        {

            string Service_res = "";
            int? Estado_nac = 0;
            try
            {
                foreach (var serv in DB.Usuarios.Where(x => x.Numero == usernumber && x.Pvte == false && x.Fam == id_service).ToList())
                {
                    Service_res = serv.Servicio;
                    Estado_nac = serv.IdEstado;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Service_res;
        }

        public string QueryValidateServiceRegistrer(string id_service, string usernumber)
        {

            string Service_res = "";
            int? Estado_nac = 0;
            try
            {
                foreach (var ser in DB.Usuarios.Where(x => x.Numero == usernumber && x.Pvte == false && x.Fam == id_service && x.Servicio == ServiceList(usernumber)).ToList())
                {
                    Service_res = ser.Servicio;
                    Estado_nac = ser.IdEstado == 0 ? null : ser.IdEstado;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Service_res;
        }


        public async Task<object> QueryGetFamily()
        {
            try
            {
                var List = await DB.Fams.Select(x => new
                {
                    id = x.Codigo,
                    title = x.Titulo,
                    description = "",
                }).ToListAsync();

                return List;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public string ValidateFamily(string id_service, string userNumber)
        {
            string results = "";
            try
            {
                foreach (var res in DB.Fams.Where(x => x.Codigo == id_service).ToList())
                {
                    results = res.Codigo;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return results;
        }


        public string QueryGetValidateregister(string usernumber)
        {
            string? register = "";
            try
            {
                foreach (var res in DB.Usuarios.Where(x => x.Numero == usernumber).ToList())
                {
                    register = res.IdEstado.ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return register;
        }


        public string GetValidateService(string usernumber, string id_service)
        {
            string Servicio = null;
            try
            {
                foreach (var ser in DB.Usuarios.Where(x => x.Numero == usernumber && x.Pvte == false && x.Servicio == id_service).ToList())
                {
                    Servicio = ser.Servicio;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Servicio;
        }

        public string? GetValidateState(string body)
        {
            byte? id_estado = 0;
            try
            {
                foreach (var estado in DB.Estados.Where(x => EF.Functions.Collate(x.Nombre == body, "COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI")).ToList())
                {
                    id_estado = estado.IdEstado == 0 ? null : estado.IdEstado;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return id_estado.ToString();
        }


        public List<object> QueryGetValidateService(string usernumber)
        {
            List<object> results = new List<object>();
            try
            {
                foreach (var res in DB.Usuarios.Where(x => x.Numero == usernumber).ToList())
                {
                    {
                        results.Add(
                            new

                            {

                                Service = res.Servicio == "" ? null : res.Servicio
                            }


                       );
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            return results;
        }


        public string QueryGetValidateregisterFirst(string usernumber)
        {
            string register = "";
            try
            {
                foreach (var reg in DB.Usuarios.Where(x => x.Numero == usernumber).ToList())
                {
                    register = reg.Numero;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return register;
        }



        public string QueryGetServiceF(string id_service)
        {
            string facial = "";
            try
            {
                foreach (var faciales in DB.Faciales.Where(x => x.Clave == id_service).ToList())
                {
                    facial = faciales.Clave;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return facial;
        }


        public async Task<string> QueryGetServiceAp(string id_service)
        {
            string ap = "";
            try
            {
                var aparatologia = await DB.Servicios.Where(x => x.Clave == id_service).FirstOrDefaultAsync();
                if (aparatologia != null)
                {
                    ap = aparatologia.Clave;
                }
                //foreach (var aparatologia in DB.Aparatologia.Where(x => x.Clave == id_service).ToList())
                //{
                //    ap = aparatologia.Clave;
                //}
            }
            catch (Exception ex)
            {

            }
            return ap;
        }

        public string GetShopeSelect(long id_sucursal)
        {
            string greeting = "";
            string Message = "";
            string nom_suc = "";
            try
            {
                foreach (var shope in DB.Horarios.Where(x => x.IdSucursal == id_sucursal).ToList())
                {
                    greeting += shope.Dias + " " + shope.Horario1 + "\n";
                }

                foreach (var msj in DB.Sucursales.Where(x => x.Id == id_sucursal).ToList())


                {
                    nom_suc = msj.Nombre;
                }

                var msg = MessageShopes(nom_suc).Replace(".", ".\n\n") + greeting;


                Message = msg.ToString();
            }
            catch (Exception)
            {

                throw;
            }

            return Message;
        }

        public async Task<string> ShopesList(string clave)
        {
            string HorarioResult = "";

            try
            {

                var Shope = await DB.Sucursales.Where(x => x.Clave == clave).FirstOrDefaultAsync();
                var Mensaje = await DB.Mensajes.AsNoTracking().Where(x => x.Id == 1).FirstOrDefaultAsync();
                if (Shope != null && Mensaje != null)
                {
                    // var Horari = await DB.Horarios.Where(x => x.IdSucursal == ).FirstOrDefaultAsync();
                    foreach (var shope in await DB.Horarios.Where(x => x.IdSucursal == Shope.Id).ToListAsync())
                    {
                        HorarioResult += shope.Dias + " " + shope.Horario1 + "\n";
                    }

                    return Mensaje.Descripcion + HorarioResult;
                }
                //foreach (var shop in DB.Sucursales.Where(x => x.Clave == clave).ToList())
                //{
                //    id_sucursal = shop.Id;
                //    nombre = shop.Nombre;
                //}

                // var Horario = GetShopeSelect(id_sucursal);

                //HorarioResult = Horario.ToString();

            }
            catch (Exception ex)
            {

                throw;
            }
            return HorarioResult;
        }

        public string QueryGetShopeid(string usernumber)
        {
            string register = "";
            try
            {
                foreach (var reg in DB.Usuarios.Where(x => x.Numero == usernumber && x.Pvte == true).ToList())
                {
                    register = reg.IdSucursal;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return register;
        }



        public string MessageShopes(string nom_suc)
        {
            string message = "";
            try
            {
                foreach (var msj in DB.Mensajes.Where(x => x.Nombre == "Horarios").ToList())
                {
                    message = msj.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return message.Replace("contamos", " " + nom_suc + " contamos con ");
        }



        public string MessageService(string name, string Service)
        {
            string message = "";
            string Services = "";
            try
            {
                foreach (var msj in DB.Mensajes.Where(x => x.Nombre == "D_completos").ToList())
                {
                    message = msj.Descripcion;
                }

                foreach (var serv in DB.Servicios.Where(x => x.Clave == Service).ToList())
                {
                    Service = serv.Titulo == "" ? null : serv.Titulo;
                }

                if (Services == null || Services == "")
                {
                    foreach (var serv in DB.Aparatologia.Where(x => x.Clave == Service).ToList())
                    {
                        Service = serv.Titulo == "" ? null : serv.Titulo;
                    }
                }

                if (Services == null || Services == "")
                {
                    foreach (var serv in DB.Faciales.Where(x => x.Clave == Service).ToList())
                    {
                        Service = serv.Titulo == "" ? null : serv.Titulo;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return message.Replace("name", " " + name + "").Replace("servicio", " " + Service + " ");
        }

        public string Messageregister()
        {
            string message = "";
            try
            {
                foreach (var msg in DB.Mensajes.Where(x => x.Nombre == "Registrar").ToList())
                {
                    message = msg.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }

        public string MessageAcept()
        {
            string? message = "";
            try
            {
                foreach (var msg in DB.Mensajes.Where(x => x.Nombre == "Aceptar_registro").ToList())
                {
                    message = msg.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }

        public string MessageRegistrerName()
        {
            string message = "";
            try
            {
                foreach (var msg in DB.Mensajes.Where(x => x.Nombre == "Registro_nombre").ToList())
                {
                    message = msg.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }

        public string MessageRegistrerLastName()
        {
            string message = "";
            try
            {
                foreach (var msg in DB.Mensajes.Where(x => x.Nombre == "Registro_apellidoP").ToList())
                {
                    message = msg.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }

        public string MessageRegistrerLastNameM()
        {
            string message = "";
            try
            {
                foreach (var msg in DB.Mensajes.Where(x => x.Nombre == "Registro_apellidoM").ToList())
                {
                    message = msg.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }

        public string MessageRegistrerbirth()
        {
            string message = "";
            try
            {
                foreach (var msg in DB.Mensajes.Where(x => x.Nombre == "Registro_FNac").ToList())

                {
                    message = msg.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }

        public string MessageValidateDate()
        {
            string message = "";
            try
            {
                foreach (var msg in DB.Mensajes.Where(x => x.Nombre == "validar_fecha").ToList())
                {
                    message = msg.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }

        public string MessageValidateState()
        {
            string message = "";
            try
            {
                foreach (var msg in DB.Mensajes.Where(x => x.Nombre == "Validar_Estado").ToList())
                {
                    message = msg.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return message;
        }


        public string MessageRegistrerstate_birth()
        {
            string message = "";
            try
            {
                foreach (var msg in DB.Mensajes.Where(x => x.Nombre == "Registro_Estado").ToList())
                {
                    message = msg.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }


        public string MessageRegistrerComplete()
        {
            string message = "";
            try
            {
                foreach (var msg in DB.Mensajes.Where(x => x.Nombre == "Registro_completo").ToList())
                {
                    message = msg.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }


        public string MessageDescriptionService(string idServicio, string numero)
        {
            string message = "";
            string clave = "";
            string? Normal = "";
            string? Festive = "";
            try
            {
                foreach (var msg in DB.Mensajes.Where(x => x.Nombre == idServicio).ToList())
                {
                    message = msg.Descripcion;
                    clave = msg.Nombre;

                }
                if (clave == "01C")
                {
                    int shope = Convert.ToInt16(QueryGetShope(QueryGetShopeid(numero)).ToString());

                    var url = "http://172.16.71.11:8080/Bot/GetPriceConsultation?id_shope=" + shope + "";

                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.ContentType = "Content-Type: json/plain";

                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    using (WebResponse respuesta = request.GetResponse())
                    {
                        using (Stream strReader = respuesta.GetResponseStream())
                        {
                            using (StreamReader str = new StreamReader(strReader))
                            {

                                string line;

                                using (var jsonTextReader = new JsonTextReader(str))
                                {
                                    while ((line = str.ReadLine()) != null)
                                    {
                                        var Json = JsonConvert.DeserializeObject<List<Prices>>(line).ToList();
                                        Normal = "\n" + "$ " + Json.Where(x => x.nombre == "NORMAL").Select(x => x.precio).FirstOrDefault() + " " + Json.Where(x => x.nombre == "NORMAL").Select(x => x.nombre).FirstOrDefault();
                                        Festive = "\n" + "$ " + Json.Where(x => x.nombre == "FESTIVO").Select(x => x.precio).FirstOrDefault() + " " + Json.Where(x => x.nombre == "FESTIVO").Select(x => x.nombre).FirstOrDefault();



                                       
                                    }

                                }


                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return message.Replace("💲380.", " " + Normal + "\n " + Festive);
        }


        public string MessageHelp()
        {
            string message = "";
            try
            {
                foreach (var msg in DB.Mensajes.Where(x => x.Nombre == "Ayuda").ToList())
                {
                    message = msg.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }


        public string MessageAudio()
        {
            string message = "";
            try
            {
                foreach (var msg in DB.Mensajes.Where(x => x.Nombre == "Audio").ToList())
                {
                    message = msg.Descripcion;
                }

            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }

        public string MessagePromo()
        {
            string? message = "";
            try
            {
                foreach (var msg in DB.Mensajes.Where(x => x.Nombre == "Promocion").ToList())
                {
                    message = msg.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }

        public string MessageotherService(string usernumber)
        {
            string? Service = "";
            string? name = "";
            string? Fam = "";
            try
            {
                foreach (var msg in DB.Usuarios.Where(x => x.Numero == usernumber && x.Pvte == true).ToList())
                {
                    Service = msg.Servicio == "" ? null : msg.Servicio;
                    Fam = msg.Fam == "" ? null : msg.Fam;

                }
                if (Service != "" && Service != null)
                {
                    foreach (var msg in DB.Usuarios.Where(x => x.Numero == usernumber && x.Pvte == false && x.Servicio == Service && x.Fam == Fam).ToList())
                    {
                        name = msg.Nombre + " " + msg.ApPaterno;
                        Service = msg.Servicio;
                    }
                    name = MessageService(name, Service);
                    Updatesteps(2, usernumber);
                }
                else
                {
                    name = Messageregister();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return name;

        }

        public string MessageValidateCharacter()
        {
            string message = "";
            try
            {
                foreach (var msg in DB.Mensajes.Where(x => x.Nombre == "Validar_Caracter").ToList())
                {
                    message = msg.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return message;

        }
        public string MessageRegistrerPrevious()
        {
            string message = "";
            try
            {
                foreach (var msg in DB.Mensajes.Where(x => x.Nombre == "Registro_pendiente").ToList())
                {
                    message = msg.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }


        public string MessageRegistrerEmpty()
        {
            string message = "";
            try
            {
                foreach (var msg in DB.Mensajes.Where(x => x.Nombre == "Sin_registros").ToList())
                {
                    message = msg.Descripcion;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return message;

        }

        public async Task<string> MessageServices(string idServicio)
        {

            try
            {
                var message = await DB.Mensajes.AsNoTracking().Where(x => x.Nombre == idServicio).FirstOrDefaultAsync();

                if (message != null)
                {
                    return message.Descripcion;
                }
                else
                {
                    return "";

                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public string StepsServiceConsultations(string id_service, string numero)
        {
            string? Shope = "";
            string name = "";
            string? Servicio = "";

            try
            {
                foreach (var msg in DB.Usuarios.Where(x => x.Numero == numero && x.Pvte == false && x.Servicio == id_service).ToList())
                {
                    Servicio = msg.Servicio == "" ? null : msg.Servicio;
                    Shope = msg.IdSucursal == "" ? null : msg.IdSucursal;
                }

                if (Servicio == id_service)
                {
                    foreach (var msg in DB.Usuarios.Where(x => x.Numero == numero && x.Pvte == false).ToList())
                    {
                        name = msg.Nombre + " " + msg.ApPaterno;
                    }
                    name = MessageDescriptionService(id_service, numero);
                    Updatesteps(2, numero);

                }
                else
                {
                    name = MessageDescriptionService(id_service, numero);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return name;

        }

        public List<Location> GetlocationShope(string shope)
        {
            Locations location = new Locations();
            location.Location = new List<Location>();
            location.Location.Add(new Location
            {
                latitude = "",
                longitude = "",
                name = "",
                address = ""
            });
            try
            {
                foreach (var shopes in DB.Sucursales.Where(x => x.Clave == shope).ToList())
                {
                    {
                        location.Location = new List<Location>();
                        location.Location.Add(new Location


                        {
                            latitude = shopes.Latitud,
                            longitude = shopes.Longitud,
                            name = shopes.Direccion.Replace("📍", ""),
                            address = shopes.Direccion

                        }


                       );

                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
            return location.Location.ToList();

        }
        public async Task<string> AudioServiceFacial(string id_service)
        {
            try
            {
                var audio = await DB.Faciales.Where(x => x.Clave == id_service).FirstOrDefaultAsync();

                if (audio != null) return audio.RutaAudio == null ? "" : audio.RutaAudio;
                else return "";

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<string> PriceServiceFacial(string id_service)
        {
            try
            {
                var Price = await DB.Faciales.Where(x => x.Clave == id_service).FirstOrDefaultAsync();

                if (Price != null) return Price.RutaPrecios == null ? "" : Price.RutaPrecios;
                else return "";
            }
            catch (Exception)
            {
                throw;
            }


        }

        public async Task<string> PriceServiceAP(string id_service)
        {
            try
            {
                var Price = await DB.Aparatologia.Where(x => x.Clave == id_service).FirstOrDefaultAsync();

                if (Price != null) return Price.RutaPrecios == null ? "" : Price.RutaPrecios;
                else return "";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> AudioServiceAp(string id_service)
        {
            try
            {
                var audio = await DB.Aparatologia.Where(x => x.Clave == id_service).FirstOrDefaultAsync();

                if (audio != null) return audio.RutaAudio == null ? "" : audio.RutaAudio;
                else return "";
            }
            catch (Exception)
            {

                throw;
            }


        }


        public async Task<string> PromoService(string id_service, string userNumber)
        {
            string? promo = "";
            DateTime fecha = DateTime.Today;
            try
            {
                var idSucursal = await idShope(userNumber);
                var Promo = await DB.Promociones
                    .Where(x => x.Clave == id_service && x.Hasta >= fecha && x.ClaveSucursal == idSucursal)
                    .FirstOrDefaultAsync();

                if (Promo != null)
                    promo = Promo.RutaImagen == "" || Promo.RutaImagen == null ? "" : Promo.RutaImagen;

            }
            catch (Exception)
            {

                throw;
            }

            return promo;
        }

        public async Task<string> idShope(string userNumber)
        {

            try
            {
                var Shop = await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).FirstOrDefaultAsync();
                if (Shop != null)
                {
                    return Shop.IdSucursal ?? "";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {

                throw;
            }



        }


        public string FamList(string userNumber)
        {
            string? Fam = "";
            try
            {
                foreach (var fam in DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).ToList())
                {
                    Fam = fam.Fam == "" ? null : fam.Fam;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Fam;

        }

        public string ServiceList(string userNumber)
        {
            string? Service = "";

            try
            {
                foreach (var serv in DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).ToList())
                {
                    Service = serv.Servicio == "" ? null : serv.Servicio;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Service;
        }

        public string SucursalList(string userNumber)
        {
            string? sucursal = "";

            try
            {
                foreach (var suc in DB.Usuarios.Where(x => x.Numero == userNumber).ToList())
                {
                    sucursal = suc.IdSucursal == "" ? null : suc.IdSucursal;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return sucursal;
        }
        public async Task<string> ValidateData(string number)
        {
            string Datos = "";
            string? message = "";

            try
            {
                var Message = await DB.Mensajes.AsNoTracking().Where(x=>x.Id==42).FirstOrDefaultAsync();
                var User = await DB.Usuarios.AsNoTracking().Where(x => x.Numero == number && x.Pvte == true).FirstOrDefaultAsync();
                if (Message != null && User != null)
                {
                    var Shope = await QueryGetShopeEntity(User.IdSucursal);
                    var Address = await QueryGetAddress(User.IdSucursal);
                    var Service = await QueryGetService(number);
                    message = Message.Descripcion;
                    message = message?
                    .Replace(" gen", " " + User.Sexo + " ")
                    .Replace(" name", " " + User.Nombre + " " + User.ApPaterno + " " + User.ApMaterno)
                    .Replace(" fch", " " + User.FNacimiento)
                    .Replace(" cli", " " + Shope)
                    .Replace(" dir", " " + Address)
                    .Replace(" service", " " + Service);
                    Datos = message ?? "";
                }
               
            }
            catch (Exception)
            {

                throw;
            }

            return Datos ?? "";
        }


        public async Task<string> QueryGetShopeEntity(string id_shope)
        {
            try
            {
                var Shope = await DB.Sucursales.Where(x => x.Clave == id_shope).FirstOrDefaultAsync();
                return Shope?.Nombre ?? "";
            }
            catch (Exception ex)
            {

                throw;
            }

        }  
        public async Task<string> QueryGetService(string number)
        {
            try
            {
                var Service = await DB.Usuarios.AsNoTracking().Where(x => x.Numero == number && x.Pvte == true).FirstOrDefaultAsync();
                if (Service != null)
                {
                    var Message = await DB.Servicios.Where(x => x.Clave == Service.Servicio).FirstOrDefaultAsync();
                    if (Message != null) return Message?.Titulo ?? "";
                    else
                    {
                        var MessageF = await DB.Faciales.Where(x => x.Clave == Service.Servicio).FirstOrDefaultAsync();
                        if (MessageF != null) return MessageF?.Titulo ?? "";
                        else
                        {
                            var MessageA = await DB.Aparatologia.Where(x => x.Clave == Service.Servicio).FirstOrDefaultAsync();
                            return MessageA?.Titulo ?? "";
                        }
                    }

                }
                else return "";

            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<string> QueryGetAddress(string id_shope)
        {
            try
            {
                var Shope = await DB.Sucursales.Where(x => x.Clave == id_shope).FirstOrDefaultAsync();
                return Shope?.Direccion ?? "";
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public string updateData(string data, string userNumber)
        {
            try
            {
                var usuario = DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).FirstOrDefaultAsync();
                if (usuario != null)
                {
                    // Obtener el tipo del objeto 'usuario'
                    var usuarioType = usuario.GetType();

                    // Buscar la propiedad que corresponde al nombre del campo (fieldName)
                    var propertyInfo = usuarioType.GetProperty(data);

                    if (propertyInfo != null && propertyInfo.CanWrite)
                    {
                        // Asignar el nuevo valor a la propiedad
                        propertyInfo.SetValue(usuario, Convert.ChangeType(null, propertyInfo.PropertyType), null);

                        // Guardar los cambios en la base de datos
                        DB.SaveChangesAsync();


                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }

        public string updateReset(string userNumber)
        {
            try
            {
                foreach (var a in DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).ToList())
                {
                    a.Nombre = null;
                    a.ApPaterno = null;
                    a.ApMaterno = null;
                    a.FNacimiento = null;
                    a.IdEstado = null;
                    a.Fecha = null;
                    a.IdPaso = 1;
                    a.Sexo = null;
                    DB.SaveChangesAsync();
                }


            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }

        public string updateResetpvteinteractive(string userNumber)
        {
            try
            {
                foreach (var a in DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).ToList())
                {
                    a.Nombre = null;
                    a.ApPaterno = null;
                    a.ApMaterno = null;
                    a.FNacimiento = null;
                    a.IdEstado = null;
                    a.Fecha = null;
                    a.IdPaso = 3;
                    a.Sexo = null;
                    a.Fam = null;
                    DB.SaveChangesAsync();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }

        public string updateResetpvte(string userNumber)
        {
            try
            {
                foreach (var a in DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).ToList())
                {
                    a.Nombre = null;
                    a.ApPaterno = null;
                    a.ApMaterno = null;
                    a.FNacimiento = null;
                    a.IdEstado = null;
                    a.Fecha = null;
                    a.IdPaso = 1;
                    a.Sexo = null;
                    a.Servicio = null;
                    a.Fam = null;
                    DB.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }
        public async Task<string> updateFamily(string id_Fam, string userNumber)
        {
            try
            {
                var User = await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).FirstOrDefaultAsync();
                if (User != null)
                {
                    User.Fam = id_Fam;
                    await DB.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }

        public async Task<string> updateService(string id_service, string userNumber)
        {
            try
            {
                var user = await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.Servicio = id_service;

                    await DB.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }

        public async Task<string> updateShope(string userNumber, string id_suc)
        {
            try
            {

                var user = await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.IdSucursal = id_suc;

                    await DB.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return "";
        }

        public async Task<string> updatestate_birth(int Body, string userNumber)
        {
            var User = await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).FirstOrDefaultAsync();

            if (User != null)
            {
                User.IdEstado = Body;
                await DB.SaveChangesAsync();
                return "Ok";
            }
            else return "Error";
        }

        public async Task<string> updatedate_birth(string Body, string userNumber)
        {
            try
            {
                var User = await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).FirstOrDefaultAsync();

                if (User != null)
                {
                    User.FNacimiento = Body;
                    await DB.SaveChangesAsync();
                    return "Ok";
                }
                else return "Error";
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<string> updateGen(string Sexo, string userNumber)
        {
            try
            {
                var User = await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).FirstOrDefaultAsync();

                if (User != null)
                {
                    User.Sexo = Sexo;
                    await DB.SaveChangesAsync();
                    return "Ok";
                }
                else return "Error";
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<string> updatelast_name(string Body, string userNumber)
        {
            try
            {
                var User = await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).FirstOrDefaultAsync();

                if (User != null)
                {
                    User.ApPaterno = Body;
                    await DB.SaveChangesAsync();
                    return "Ok";
                }
                else return "Error";
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<string> updatelast_nameM(string Body, string userNumber)
        {
            try
            {
                var User = await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).FirstOrDefaultAsync();

                if (User != null)
                {
                    User.ApMaterno = Body;
                    await DB.SaveChangesAsync();
                    return "Ok";
                }
                else return "Error";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> updatename(string Body, string userNumber)
        {
            try
            {

                var User = await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).FirstOrDefaultAsync();

                if (User != null)
                {
                    User.Nombre = Body;
                    await DB.SaveChangesAsync();
                    return "Ok";
                }
                else return "Error";
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string insertNewServiceRegistrer(string userNumber)
        {
            try
            {
                Usuario usuario = new Usuario();
                foreach (var item in DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).Take(1).ToList())
                {

                    usuario.Numero = string.IsNullOrEmpty(item.Numero) ? null : item.Numero;
                    usuario.Nombre = string.IsNullOrEmpty(item.Nombre) ? null : item.Nombre;
                    usuario.ApPaterno = string.IsNullOrEmpty(item.ApPaterno) ? null : item.ApPaterno;
                    usuario.ApMaterno = string.IsNullOrEmpty(item.ApMaterno) ? null : item.ApMaterno;
                    usuario.FNacimiento = string.IsNullOrEmpty(item.FNacimiento) ? null : item.FNacimiento;
                    usuario.Fecha = DateTime.Today.ToString("yyyy-MM-dd");
                    usuario.IdEstado = item.IdEstado == 0 ? null : item.IdEstado;
                    usuario.IdSucursal = string.IsNullOrEmpty(item.IdSucursal) ? null : item.IdSucursal;
                    usuario.Servicio = string.IsNullOrEmpty(item.Servicio) ? null : item.Servicio;
                    usuario.Pvte = false;
                    usuario.Fam = string.IsNullOrEmpty(item.Fam) ? null : item.Fam;
                    usuario.IdEstatus = 2;
                    usuario.Sexo = string.IsNullOrEmpty(item.Sexo) ? null : item.Sexo;
                    DB.Usuarios.AddAsync(usuario);
                    DB.SaveChangesAsync();
                }



            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }


        public async Task<string> insertNewService(string userNumber)
        {
            try
            {
                Usuario usuario = new Usuario();
                var UserPvt = await DB.Usuarios.AsNoTracking().Where(x => x.Numero == userNumber && x.Pvte == true).FirstOrDefaultAsync();
                if (UserPvt != null)
                {
                    usuario.Numero = UserPvt.Numero;
                    usuario.Nombre = UserPvt.Nombre;
                    usuario.ApPaterno = UserPvt.ApPaterno;
                    usuario.ApMaterno = UserPvt.ApMaterno;
                    usuario.FNacimiento = UserPvt.FNacimiento;
                    usuario.IdEstado = UserPvt.IdEstado;
                    usuario.IdSucursal = UserPvt.IdSucursal;
                    usuario.Servicio = UserPvt.Servicio;
                    usuario.Fam = UserPvt.Fam;
                    usuario.Sexo = UserPvt.Sexo;
                    usuario.Pvte = false;
                    usuario.Fecha = DateTime.Now.ToString("yyyy-MM-dd");
                    usuario.IdEstatus = 2;
                    usuario.Hora = DateTime.Now.TimeOfDay;
                    await DB.Usuarios.AddAsync(usuario);
                    await DB.SaveChangesAsync();
                }

                //foreach (var item in DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true && x.Fam == FamList(userNumber)).Take(1).ToList())
                //{

                //    usuario.Numero = string.IsNullOrEmpty(item.Numero) ? null : item.Numero;
                //    usuario.Nombre = string.IsNullOrEmpty(item.Nombre) ? null : item.Nombre;
                //    usuario.ApPaterno = string.IsNullOrEmpty(item.ApPaterno) ? null : item.ApPaterno;
                //    usuario.ApMaterno = string.IsNullOrEmpty(item.ApMaterno) ? null : item.ApMaterno;
                //    usuario.FNacimiento = string.IsNullOrEmpty(item.FNacimiento) ? null : item.FNacimiento;
                //    usuario.Fecha = DateTime.Today.ToString("yyyy-MM-dd");
                //    usuario.IdEstado = item.IdEstado == 0 ? null : item.IdEstado;
                //    usuario.IdSucursal = string.IsNullOrEmpty(SucursalList(userNumber)) ? null : item.IdSucursal;
                //    usuario.Servicio = string.IsNullOrEmpty(ServiceList(userNumber)) ? null : item.Servicio;
                //    usuario.Pvte = false;
                //    usuario.Fam = string.IsNullOrEmpty(item.Fam) ? null : item.Fam;
                //    usuario.IdEstatus = 2;
                //    usuario.Sexo = string.IsNullOrEmpty(item.Sexo) ? null : item.Sexo;
                //    DB.Usuarios.AddAsync(usuario);
                //    DB.SaveChangesAsync();
                //}



            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }

        public string insertgreeting(string userNumber)
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario.Numero = userNumber;
                usuario.IdPaso = 1;
                usuario.Fecha = DateTime.Today.ToString("yyyy-MM-dd");
                usuario.Pvte = true;
                usuario.IdEstatus = 2;
                usuario.IdCallcenter = 0;
                usuario.Curp = "xxxxxxxx";
                DB.Usuarios.AddAsync(usuario);
                DB.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }
        public string updategreeting(string userNumber)
        {
            try
            {
                var user = DB.Usuarios.Where(x => x.Numero == userNumber && x.IdPaso == 2).FirstOrDefaultAsync();
                if (user.Result != null)
                {
                    user.Result.Fecha = DateTime.Today.ToString("yyyy-MM-dd");
                    DB.SaveChangesAsync();
                }


            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }

        public async Task<string> Updatesteps(int step, string userNumber)
        {
            try
            {
                var user = await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.IdPaso = step;
                    await DB.SaveChangesAsync();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }

        public string ValidateDataColumns(string Column)
        {
            string? Datos = "";
            string usr = "Usuarios";
            try
            {
                var entityType = DB.Model.FindEntityType(usr);

                // Table info 
                // var tableName = entityType.GetTableName();
                //var tableSchema = entityType.GetSchema();
                if (entityType != null)
                {
                    // Column info 
                    foreach (var property in entityType.GetProperties())
                    {

                        var columnName = property.GetColumnName();
                        var columnType = property.GetColumnType();
                        if (columnName == Column) Datos = string.IsNullOrEmpty(columnName) ? null : columnName;


                    };

                }

                //if (entityType != null)
                //{
                //    // Recorre todas las propiedades (columnas) de la entidad
                //    foreach (var property in entityType.GetProperties())
                //    {
                //        // Obtiene el nombre de la columna
                //        var columnName = property.GetColumnName(StoreObjectIdentifier.Table(entityType.GetTableName(), entityType.GetSchema()));

                //        // Si el nombre de la columna coincide con el que estás buscando, se asigna a 'Datos'
                //        if (columnName == Column)
                //        {
                //            Datos = string.IsNullOrEmpty(columnName) ? null : columnName;
                //        }
                //    }
                //}



            }
            catch (Exception)
            {

                throw;
            }
            return Datos;

        }

        public static string RemoveDiacritics(string text)
        {
            string normalized = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }




    }
}
