using API_SISDE.Data;
using API_SISDE.Models.WhatsappCloud;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Text;

namespace API_SISDE.Models.Connection
{
    public class QueryModelsEntity
    {
        CHATPRContext DB = new CHATPRContext();

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
        public async Task<string> ValidateData(string number)
        {
            string Datos = "";
            string? message = "";

            try
            {
                var Message = await DB.Mensajes.AsNoTracking().Where(x => x.Id == 42).FirstOrDefaultAsync();
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

        public async Task<string> checkPendingAppoiment(string userNumber)
        {
            var user = await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).FirstOrDefaultAsync();
            if (user != null)
            {
                var servicePending = await DB.Usuarios.Where(x => x.Numero == userNumber
                && x.Pvte == false && x.Fam == user.Fam && x.Servicio == user.Servicio && x.IdEstatus == 2).FirstOrDefaultAsync();
                if (servicePending != null) return servicePending.Nombre + " " + servicePending.ApPaterno + " " + servicePending.ApMaterno;
                else return "";
            }
            else return "";

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
