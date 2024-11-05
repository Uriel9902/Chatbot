using API_SISDE.Data;
using API_SISDE.Models.WhatsappCloud;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Dynamic;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.X86;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;


namespace API_SISDE.Models.Connection
{
    public class QueryModels
    {
        SqlConnection conn = new SqlConnection("Data Source=SRV-SISDE-DB\\SISDEBD;Initial Catalog=CHATPR;User ID=sa;Password=Clinicassql123 ;MultipleActiveResultSets=True");

       // CHATPRContext DB = new CHATPRContext();
        public async Task<List<Steps>> Steps(string numero)
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

                CHATPRContext bse = new CHATPRContext();
                var hola = bse.Faciales.Where(x => x.Id == 1).ToList();

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "SRV-SISDE-DB\\SISDEBD";
                builder.UserID = "sa";
                builder.Password = "Clinicassql123";
                builder.InitialCatalog = "CHATPR";
                builder.MultipleActiveResultSets = true;
                using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    using (var cmd = new SqlCommand("select numero,id_paso,nombre,ap_paterno,ap_materno,f_nacimiento,id_estado,fecha,sexo,id_sucursal,Servicio from [dbo].[Usuarios] where numero=" + numero + "and pvte='" + true + "' ", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {


                                stepslist.Steps = new List<Steps>();
                                stepslist.Steps.Add(new Steps
                                {
                                    Number = reader.GetString(0),
                                    Step = reader.GetInt64(1),
                                    Name = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    Ap_paternal = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    Ap_maternal = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    Date_birth = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    State_birth = reader.IsDBNull(6) ? null : reader.GetInt32(6),
                                    Date = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    Sexo = reader.IsDBNull(8) ? null : reader.GetString(8),
                                    Clin = reader.IsDBNull(9) ? null : reader.GetString(9),
                                    Service = reader.IsDBNull(10) ? null : reader.GetString(10),

                                });

                            }
                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


            return stepslist.Steps.ToList();

        }
        public async Task<List<Steps>> StepsName(string numero)
        {
            Stepslist stepslist = new Stepslist();
            try
            {

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "serveridorchatbot.database.windows.net";
                builder.UserID = "devmepiel";
                builder.Password = "mepiel24@";
                builder.InitialCatalog = "CHAT";
                using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    using (var cmd = new SqlCommand("select numero,id_paso,nombre,ap_paterno,ap_materno,f_nacimiento,id_estado,fecha from [dbo].[Usuarios] where numero=" + numero + " ", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                stepslist.Steps = new List<Steps>();
                                stepslist.Steps.Add(new Steps
                                {
                                    Number = reader.GetString(0),
                                    Step = reader.GetInt64(1),
                                    Name = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    Ap_paternal = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    Ap_maternal = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    Date_birth = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    State_birth = reader.GetInt32(6),
                                    Date = reader.IsDBNull(7) ? null : reader.GetString(7),
                                });

                            }
                        }

                    }
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

            ShopesList shopes = new ShopesList();
            List<object> results = new List<object>();
            try
            {


                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select clave,nombre,direccion from [dbo].[Sucursales] ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    shopes.Shopes = new List<Shopes>();
                    results = new List<object>();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            {
                                results.Add(
                                    new

                                    {
                                        id = reader.GetString(0),
                                        title = reader.GetString(1),
                                        description = reader.GetString(2),

                                    }


                               );

                            };





                        }
                    }

                }




            }

            catch (Exception)
            {

                throw;
            }

            return results;

        }

        public async Task<object> QueryGetShopesUpdate(string cli)
        {

            ShopesList shopes = new ShopesList();

            try
            {


                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select clave,nombre,direccion from [dbo].[Sucursales] where clave ='" + cli + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {



                            shopes.Shopes = new List<Shopes>();
                            shopes.Shopes.Add(new Shopes
                            {
                                id = reader.GetString(0),
                                title = reader.GetString(1),
                                description = reader.GetString(2).Replace("📍", ""),
                            });




                        }
                    }

                }




            }

            catch (Exception)
            {

                throw;
            }

            return shopes.Shopes.ToList();

        }

        public async Task<object> QueryGetservice()
        {
            List<object> results = new List<object>();
            try
            {


                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Clave,Titulo,Descripcion from [dbo].[Servicios] ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    results = new List<object>();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            {
                                results.Add(
                                    new

                                    {
                                        id = reader.GetString(0),
                                        title = reader.GetString(1),
                                        description = reader.IsDBNull(2) ? null : reader.GetString(2),

                                    }


                               );

                            };

                        }
                    }

                }

            }

            catch (Exception)
            {

                throw;
            }

            return results;
        }
        public async Task<object> QueryGetserviceFacial()
        {
            List<object> results = new List<object>();
            try
            {


                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Clave,Titulo,Descripcion from [dbo].[Faciales] ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    results = new List<object>();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            {
                                results.Add(
                                    new

                                    {
                                        id = reader.GetString(0),
                                        title = reader.GetString(1),
                                        description = reader.IsDBNull(2) ? null : reader.GetString(2),

                                    }


                               );

                            };

                        }
                    }

                }

            }

            catch (Exception)
            {

                throw;
            }

            return results;
        }
        public async Task<object> QueryGetserviceAparatologia()
        {
            List<object> results = new List<object>();
            try
            {


                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Clave,Titulo,Descripcion from [dbo].[Aparatologia] ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    results = new List<object>();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            {
                                results.Add(
                                    new

                                    {
                                        id = reader.GetString(0),
                                        title = reader.GetString(1),
                                        description = reader.IsDBNull(2) ? null : reader.GetString(2),

                                    }


                               );

                            };

                        }
                    }

                }

            }

            catch (Exception)
            {

                throw;
            }

            return results;
        }
        public async Task<object> QueryGetQuestions()
        {
            List<object> results = new List<object>();
            try
            {


                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select clave,pregunta,descripcion from [dbo].[Preguntas] ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    results = new List<object>();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            {
                                results.Add(
                                    new

                                    {
                                        id = reader.GetString(0),
                                        title = reader.GetString(1),
                                        description = reader.GetString(2),

                                    }


                               );

                            };

                        }
                    }

                }

            }

            catch (Exception)
            {

                throw;
            }

            return results;
        }
        public async Task<string> QueryGetgreeting()
        {
            string greeting = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Saludo' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            greeting = reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return greeting;
        }
        public async Task<string> QueryGetserviceMessage()
        {
            string Service = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Servicio' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Service = reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return Service;
        }
        public async Task<string> QueryGetserviceMessageFacial()
        {
            string Service = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='03TF' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Service = reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return Service;
        }
        public async Task<string> QueryGetserviceMessageaparatologia()
        {
            string Service = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='04AP' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Service = reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return Service;
        }
        public async Task<string> QueryGetShopesValidate(string id_suc)
        {
            string greeting = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select nombre from [dbo].[Sucursales] where clave='" + id_suc + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            greeting = reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return greeting;
        }
        public async Task<string> QueryGetServiceFValidate(string id_service)
        {
            string facial = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Clave from [dbo].[Servicios] where clave='" + id_service + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            facial = reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return facial;
        }
        public async Task<string> QueryGetServiceApValidate(string id_service)
        {
            string Aparatologia = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Clave from [dbo].[Servicios] where clave='" + id_service + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Aparatologia = reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return Aparatologia;
        }
        public async Task<string> QueryGetQuestion(string id_service)
        {
            string Preguntas = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Clave from [dbo].[Preguntas] where clave='" + id_service + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Preguntas = reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return Preguntas;
        }

        public async Task<int> QueryGetShope(string Shope)
        {
            int id_shope = 0;
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select id from [dbo].[Sucursales] where clave='" + Shope + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            id_shope = reader.GetByte(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return id_shope;
        }


        public async Task<string> QueryValidateService(string id_service, string usernumber)
        {

            string Service_res = "";
            int? Estado_nac = 0;

            try
            {
                using (var cmd1 = new SqlCommand("select Servicio,id_estado from [dbo].[Usuarios] where numero=" + usernumber + " and pvte= '" + false + "' and Fam='" + id_service + "'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd1);


                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            Service_res = reader.GetString(0);
                            Estado_nac = reader.IsDBNull(1) ? null : reader.GetInt32(1);

                        }
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return Service_res;
        }
        public async Task<string> QueryValidateServiceRegistrer(string id_service, string usernumber)
        {

            string Service_res = "";
            int? Estado_nac = 0;

            try
            {
                using (var cmd1 = new SqlCommand("select Servicio,id_estado from [dbo].[Usuarios] where numero=" + usernumber + " and pvte= '" + false + "' and Fam='" + id_service + "'and Servicio='" + ServiceList(usernumber).Result + "'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd1);


                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            Service_res = reader.GetString(0);
                            Estado_nac = reader.IsDBNull(1) ? null : reader.GetInt32(1);

                        }
                    }

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
            List<object> results = new List<object>();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Codigo,Titulo from [dbo].[Fam]", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            {
                                results.Add(
                                    new

                                    {
                                        id = reader.GetString(0),
                                        title = reader.GetString(1),
                                        description = "",

                                    }


                               );

                            };

                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return results;
        }
        public async Task<string> ValidateFamily(string id_service, string userNumber)
        {
            string results = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Codigo from [dbo].[Fam] where Codigo = '" + id_service + "'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            results = reader.GetString(0);

                        }
                    }
                    if (results.Count() > 0)
                    {

                        updateFamily(id_service, userNumber);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return results;
        }
        public async Task<string> QueryGetValidateregister(string usernumber)
        {
            string? register = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select id_estado from [dbo].[Usuarios] where numero='" + usernumber + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            register = reader.IsDBNull(0) ? null : reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return register;
        }
        public async Task<string> GetValidateService(string usernumber, string id_service)
        {
            string? Servicio = null;
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Servicio from [dbo].[Usuarios] where numero=" + usernumber + " and pvte= '" + false + "' and Servicio='" + id_service + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {



                            Servicio = reader.IsDBNull(0) ? null : reader.GetString(0);




                        }

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return Servicio;
        }

        public async Task<string?> GetValidateState(string body)
        {
            byte? id_estado = 0;
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select id_estado from [dbo].[Estados] where nombre='" + body + "' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI  ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {



                            id_estado = reader.IsDBNull(0) ? null : reader.GetByte(0);




                        }

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return id_estado.ToString();
        }

        public async Task<List<object>> QueryGetValidateService(string usernumber)
        {
            List<object> results = new List<object>();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Servicio from [dbo].[Usuarios] where numero='" + usernumber + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            {
                                results.Add(
                                    new

                                    {

                                        Service = reader.IsDBNull(0) ? null : reader.GetString(0)
                                    }


                               );

                            };

                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return results.ToList();
        }
        public async Task<string> QueryGetValidateregisterFirst(string usernumber)
        {
            string register = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select numero from [dbo].[Usuarios] where numero='" + usernumber + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            register = reader.IsDBNull(0) ? null : reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return register;
        }
        public async Task<string> QueryGetServiceF(string id_service)
        {
            string facial = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Clave from [dbo].[Faciales] where clave='" + id_service + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            facial = reader.GetString(0);
                        }
                    }

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
            string facial = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Clave from [dbo].[Aparatologia] where clave='" + id_service + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            facial = reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return facial;
        }

        public async Task<string> GetShopeSelect(long id_sucursal)
        {
            string greeting = "";
            string Message = "";
            string nom_suc = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select dias,horario from [dbo].[Horarios] where id_sucursal='" + id_sucursal + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            greeting += reader.GetString(0) + " " + reader.GetString(1) + "\n";
                        }
                    }

                }

                using (var cmd = new SqlCommand("select nombre from [dbo].[Sucursales] where id='" + id_sucursal + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            nom_suc = reader.GetString(0);
                        }
                    }

                }

                var msg = Task<string>.Run(() =>
                {
                    var i = MessageShopes(nom_suc).Result.Replace(".", ".\n\n") + greeting;

                    return i;
                });


                Message = msg.Result.ToString();



            }
            catch (Exception)
            {

                throw;
            }
            return Message;
        }


        public async Task<string> ShopesList(string clave)
        {
            int id_sucursal = 0;
            string HorarioResult = "";
            string nombre = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select id,nombre from [dbo].[Sucursales] where clave='" + clave + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            id_sucursal = reader.GetByte(0);
                            nombre = reader.GetString(1);
                        }
                    }

                }

                var Horario = Task<string>.Run(() =>
                 {
                     var i = GetShopeSelect(id_sucursal);

                     return i;
                 });


                HorarioResult = Horario.Result.ToString();

            }
            catch (Exception)
            {

                throw;
            }
            return HorarioResult;
        }

        public async Task<string> QueryGetShopeid(string usernumber)
        {
            string register = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select id_sucursal from [dbo].[Usuarios] where numero=" + usernumber + " and pvte= '" + true + "'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            register = reader.IsDBNull(0) ? null : reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return register;
        }
        public async Task<string> MessageShopes(string nom_suc)
        {
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Horarios'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return message.Replace("contamos", " " + nom_suc + " contamos con ");
        }
        public async Task<string> MessageService(string name, string Service)
        {
            string message = "";
            string Services = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();



                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='D_completos'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }
                using (var cmd = new SqlCommand("select Titulo from [dbo].[Servicios] where Clave='" + Service + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Service = reader.IsDBNull(0) ? null : reader.GetString(0);

                        }
                    }

                }

                if (Services == null || Services == "")
                {
                    using (var cmd = new SqlCommand("select Titulo from [dbo].[Aparatologia] where Clave='" + Service + "' ", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                Service = reader.IsDBNull(0) ? null : reader.GetString(0);

                            }
                        }

                    }
                }
                if (Services == null || Services == "")
                {
                    using (var cmd = new SqlCommand("select Titulo from [dbo].[Faciales] where Clave='" + Service + "' ", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                Service = reader.IsDBNull(0) ? null : reader.GetString(0);

                            }
                        }

                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            return message.Replace("name", " " + name + "").Replace("servicio", " " + Service + " ");

        }
        /// aqui me quede 
        public async Task<string> Messageregister()
        {
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Registrar'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }
        public async Task<string> MessageAcept()
        {
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Aceptar_registro'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }
        public async Task<string> MessageRegistrerName()
        {
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Registro_nombre'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }
        public async Task<string> MessageRegistrerLastName()
        {
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Registro_apellidoP'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }
        public async Task<string> MessageRegistrerLastNameM()
        {
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Registro_apellidoM'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }
        public async Task<string> MessageRegistrerbirth()
        {
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Registro_FNac'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }
        public async Task<string> MessageValidateDate()
        {
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='validar_fecha'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }

        public async Task<string> MessageValidateState()
        {
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Validar_Estado'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }
        public async Task<string> MessageRegistrerstate_birth()
        {
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Registro_Estado'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }
        public async Task<string> MessageRegistrerComplete()
        {
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Registro_completo'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }



            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }
        public async Task<string> MessageDescriptionService(string idServicio, string numero)
        {
            string message = "";
            string clave = "";
            string? Normal = "";
            string? Festive = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion,nombre from [dbo].[Mensajes] where nombre='" + idServicio + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);
                            clave = reader.GetString(1);

                        }
                    }

                }

                if (clave == "01C")
                {



                    int shope = Convert.ToInt16(QueryGetShope(await QueryGetShopeid(numero)).Result.ToString());

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



                                        // GetSites = Json.ToArray();
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
        public async Task<string> MessageHelp()
        {
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Ayuda' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }
        public async Task<string> MessageAudio()
        {
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Audio' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }
        public async Task<string> MessagePromo()
        {
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Promocion' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }
        public async Task<string> MessageotherService(string usernumber)
        {
            string Servicie = "";
            string name = "";
            string Fam = "";


            try
            {

                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Servicio,Fam from [dbo].[Usuarios] where numero=" + usernumber + " and pvte='" + true + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Servicie = reader.IsDBNull(0) ? null : reader.GetString(0);
                            Fam = reader.IsDBNull(1) ? null : reader.GetString(1);
                        }
                    }

                }
                if (Servicie != "" && Servicie != null)
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    using (var cmd = new SqlCommand("select nombre,ap_paterno,Servicio from [dbo].[Usuarios] where numero=" + usernumber + " and pvte='" + false + "'and Servicio ='" + Servicie + "' and Fam='" + Fam + "' ", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                name = reader.GetString(0) + " " + reader.GetString(1);
                                Servicie = reader.GetString(2);

                            }
                        }

                    }
                    name = await MessageService(name, Servicie);
                    Updatesteps("2", usernumber);
                }
                else
                {
                    name = await Messageregister();
                }








            }
            catch (Exception)
            {

                throw;
            }
            return name;
        }
        public async Task<string> MessageValidateCharacter()
        {
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Validar_Caracter'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }
        public async Task<string> MessageRegistrerPrevious()
        {
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Registro_pendiente'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }
        public async Task<string> MessageRegistrerEmpty()
        {
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre='Sin_registros'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

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
            string message = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Clave from [dbo].[Servicios] where Clave='" + idServicio + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            message = reader.GetString(0);

                        }
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }

        public async Task<string> StepsServiceConsultations(string id_service, string numero)
        {
            string? Shope = "";
            string name = "";
            string? Servicio = "";

            try
            {

                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Servicio,id_sucursal from [dbo].[Usuarios] where numero=" + numero + " and pvte= '" + false + "' and Servicio='" + id_service + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {



                            Servicio = reader.IsDBNull(0) ? null : reader.GetString(0);
                            Shope = reader.IsDBNull(1) ? null : reader.GetString(1);



                        }

                    }

                }

                if (Servicio == id_service)
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    using (var cmd = new SqlCommand("select nombre,ap_paterno,id_estado from [dbo].[Usuarios] where numero='" + numero + "' and pvte = '" + false + "' ", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                name = reader.GetString(0) + " " + reader.GetString(1);


                            }
                        }

                    }
                    name = await MessageDescriptionService(id_service, numero);
                    Updatesteps("2", numero);
                }

                else
                {

                    name = await MessageDescriptionService(id_service, numero);
                }





            }
            catch (Exception)
            {

                throw;
            }
            return name;
        }
        public async Task<List<Location>> GetlocationShope(string shope)
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
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Latitud,Longitud,direccion from [dbo].[Sucursales] where clave='" + shope + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);



                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            {
                                location.Location = new List<Location>();
                                location.Location.Add(new Location


                                {
                                    latitude = reader.GetString(0),
                                    longitude = reader.GetString(1),
                                    name = reader.GetString(2).Replace("📍", ""),
                                    address = reader.GetString(2),

                                }


                               );

                            };






                        }
                    }

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
            string audio = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select ruta_audio from [dbo].[Faciales] where clave='" + id_service + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            audio = reader.IsDBNull(0) ? "" : reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return audio;
        }
        public async Task<string> PriceServiceFacial(string id_service)
        {
            string price = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select ruta_precios from [dbo].[Faciales] where clave='" + id_service + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            price = reader.IsDBNull(0) ? "" : reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return price;
        }
        public async Task<string> PriceServiceAP(string id_service)
        {
            string price = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select ruta_precios from [dbo].[Aparatologia] where clave='" + id_service + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            price = reader.IsDBNull(0) ? "" : reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return price;
        }
        public async Task<string> AudioServiceAp(string id_service)
        {
            string audio = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select ruta_audio from [dbo].[Aparatologia] where clave='" + id_service + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            audio = reader.IsDBNull(0) ? "" : reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return audio;
        }
        public async Task<string> PromoService(string id_service, string userNumber)
        {
            string promo = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                DateTime fecha = DateTime.Today;

                using (var cmd = new SqlCommand("select ruta_imagen from [dbo].[Promociones] where clave='" + id_service + "' and hasta >='" + fecha.ToString("MM/dd/yyyy") + "'and clave_sucursal='" + await idShope(userNumber) + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            promo = reader.IsDBNull(0) ? "" : reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return promo;
        }
        public async Task<string> idShope(string userNumber)
        {
            string promo = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select id_sucursal from [dbo].[Usuarios] where numero='" + userNumber + "'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            promo = reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return promo;
        }
        public async Task<string> FamList(string userNumber)
        {
            string? Fam = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Fam from [dbo].[Usuarios] where numero=" + userNumber + "and pvte='" + true + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {




                        while (reader.Read())
                        {




                            Fam = reader.IsDBNull(0) ? null : reader.GetString(0);




                            //message = reader.GetString(0);

                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return Fam;
        }

        public async Task<string> ServiceList(string userNumber)
        {
            string? Service = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select Servicio from [dbo].[Usuarios] where numero=" + userNumber + "and pvte='" + true + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {




                        while (reader.Read())
                        {




                            Service = reader.IsDBNull(0) ? null : reader.GetString(0);




                            //message = reader.GetString(0);

                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return Service;
        }


        public async Task<string> SucursalList(string userNumber)
        {
            string? sucursal = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select id_sucursal from [dbo].[Usuarios] where numero=" + userNumber + "and pvte='" + true + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {




                        while (reader.Read())
                        {




                            sucursal = reader.IsDBNull(0) ? null : reader.GetString(0);




                            //message = reader.GetString(0);

                        }
                    }

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
            string? Datos = "";
            string? message = "";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select descripcion from [dbo].[Mensajes] where nombre= 'Datos_cita'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {




                        while (reader.Read())
                        {




                            Datos = reader.IsDBNull(0) ? null : reader.GetString(0);




                            //message = reader.GetString(0);

                        }
                    }

                }

                List<Steps> steps = new List<Steps>();
                List<Shopes> cli = new List<Shopes>();
                QueryModels Querys = new QueryModels();
                steps = await Querys.Steps(number);
                cli = (List<Shopes>)await Querys.QueryGetShopesUpdate(steps.Select(x => x.Clin).FirstOrDefault());

                message = Datos.Replace(" name", " " + steps.Select(x => x.Name).FirstOrDefault() + " " + " " + steps.Select(x => x.Ap_paternal).FirstOrDefault() + " " + " " + steps.Select(x => x.Ap_maternal).FirstOrDefault() + " ")
                               .Replace(" gen", " " + steps.Select(x => x.Sexo).FirstOrDefault())
                               .Replace(" fch", " " + steps.Select(x => x.Date_birth).FirstOrDefault())
                               .Replace(" cli", " " + cli.Select(x => x.title).FirstOrDefault())
                               .Replace(" dir", " " + cli.Select(x => x.description).FirstOrDefault());


            }
            catch (Exception)
            {

                throw;
            }
            return message;

        }
        public async Task<string> ValidateDataColumns(string Column)
        {
            string? Datos = "";
            string? Vdatos = "";
            string usr = "Usuarios";
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select COLUMN_NAME from information_schema.columns where TABLE_NAME='" + usr + "'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            Vdatos = (string)reader.GetString(0);
                            if (Vdatos == Column)
                            {

                                Datos = reader.IsDBNull(0) ? null : reader.GetString(0);
                            }

                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return Datos;

        }


        public async Task<string> Updatesteps(string greeting, string userNumber)
        {
            try
            {


                string query = "UPDATE dbo.Usuarios set id_paso = '" + greeting + "' WHERE numero = " + userNumber + "and pvte='" + true + "'";
                SqlCommand command = new SqlCommand(query, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                command.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }
        public async Task<string> updategreeting(string userNumber)
        {
            try
            {


                string query = "UPDATE dbo.Usuarios set fecha ='" + DateTime.Today.ToString("yyyy-MM-dd") + "' WHERE numero = " + userNumber + " and id_paso = '2' ";
                SqlCommand command = new SqlCommand(query, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                command.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }
        public async Task<string> insertgreeting(string userNumber)
        {
            try
            {
                string query = "INSERT INTO dbo.Usuarios (numero,id_paso,fecha,pvte,id_estatus,id_callcenter,curp) " +
                               "VALUES ('" + userNumber +
                                        "','" + '1' +
                                        "','" + DateTime.Today.ToString("yyyy-MM-dd") +
                                        "','" + true +
                                        "','" + 2 +
                                        "','" + 0 +
                                        "','" + "xxxxxxxx" +

                                        "')";
                SqlCommand command = new SqlCommand(query, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                command.ExecuteReader();
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

                string? numero = "";
                string? nombre = "";
                string? ap_paterno = "";
                string? ap_materno = "";
                string? f_nacimiento = "";
                string? fecha = "";
                int? estado_nac = 0;
                string? id_sucursal = "";
                string? Servicio = "";
                string? Fam = "";
                string? sexo = "";

                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select top 1 numero,nombre,ap_paterno,ap_materno,f_nacimiento,id_estado,id_sucursal,Servicio,Fam,sexo from [dbo].[Usuarios] where numero=" + userNumber + "and pvte='" + false + "'and Fam='" + FamList(userNumber).Result + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {




                        while (reader.Read())
                        {



                            numero = reader.IsDBNull(0) ? null : reader.GetString(0);
                            nombre = reader.IsDBNull(1) ? null : reader.GetString(1);
                            ap_paterno = reader.IsDBNull(2) ? null : reader.GetString(2);
                            ap_materno = reader.IsDBNull(3) ? null : reader.GetString(3);
                            f_nacimiento = reader.IsDBNull(4) ? null : reader.GetString(4);
                            fecha = DateTime.Today.ToString("yyyy-MM-dd");
                            estado_nac = reader.IsDBNull(5) ? null : reader.GetInt32(5);
                            id_sucursal = reader.IsDBNull(6) ? null : reader.GetString(6);
                            Servicio = reader.IsDBNull(7) ? null : reader.GetString(7);
                            Fam = reader.IsDBNull(8) ? null : reader.GetString(8);
                            sexo = reader.IsDBNull(9) ? null : reader.GetString(9);




                            //message = reader.GetString(0);

                        }
                    }

                }

                string query = "INSERT INTO dbo.Usuarios (numero,nombre,ap_paterno,ap_materno,f_nacimiento,fecha,id_estado,id_sucursal,Servicio,pvte,Fam,id_estatus,sexo)" +
                               "VALUES ('" + numero +
                               "','" + nombre +
                               "','" + ap_paterno +
                               "','" + ap_materno +
                               "','" + f_nacimiento +
                               "','" + fecha +
                               "','" + estado_nac +
                               "','" + SucursalList(userNumber).Result +
                               "','" + ServiceList(userNumber).Result +
                               "','" + false +
                               "','" + Fam +
                               "','" + 2 +
                               "','" + sexo +
                               "')";
                SqlCommand command = new SqlCommand(query, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }

        public async Task<string> insertNewServiceRegistrer(string userNumber)
        {
            try
            {

                string? numero = "";
                string? nombre = "";
                string? ap_paterno = "";
                string? ap_materno = "";
                string? f_nacimiento = "";
                string? fecha = "";
                int? estado_nac = 0;
                string? id_sucursal = "";
                string? Servicio = "";
                string? Fam = "";
                string? sexo = "";

                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = new SqlCommand("select top 1 numero,nombre,ap_paterno,ap_materno,f_nacimiento,id_estado,id_sucursal,Servicio,Fam,sexo from [dbo].[Usuarios] where numero=" + userNumber + "and pvte='" + true + "' ", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {




                        while (reader.Read())
                        {



                            numero = reader.IsDBNull(0) ? null : reader.GetString(0);
                            nombre = reader.IsDBNull(1) ? null : reader.GetString(1);
                            ap_paterno = reader.IsDBNull(2) ? null : reader.GetString(2);
                            ap_materno = reader.IsDBNull(3) ? null : reader.GetString(3);
                            f_nacimiento = reader.IsDBNull(4) ? null : reader.GetString(4);
                            fecha = DateTime.Today.ToString("yyyy-MM-dd");
                            estado_nac = reader.IsDBNull(5) ? null : reader.GetInt32(5);
                            id_sucursal = reader.IsDBNull(6) ? null : reader.GetString(6);
                            Servicio = reader.IsDBNull(7) ? null : reader.GetString(7);
                            Fam = reader.IsDBNull(8) ? null : reader.GetString(8);
                            sexo = reader.IsDBNull(9) ? null : reader.GetString(9);




                            //message = reader.GetString(0);

                        }
                    }

                }

                string query = "INSERT INTO dbo.Usuarios (numero,nombre,ap_paterno,ap_materno,f_nacimiento,fecha,id_estado,id_sucursal,Servicio,pvte,Fam,id_estatus,sexo)" +
                               "VALUES ('" + numero +
                               "','" + nombre +
                               "','" + ap_paterno +
                               "','" + ap_materno +
                               "','" + f_nacimiento +
                               "','" + fecha +
                               "','" + estado_nac +
                               "','" + id_sucursal +
                               "','" + Servicio +
                               "','" + false +
                               "','" + Fam +
                               "','" + 2 +
                               "','" + sexo +
                               "')";
                SqlCommand command = new SqlCommand(query, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }
        public async Task<string> updatename(string Body, string userNumber)
        {
            try
            {
                string query = "UPDATE dbo.Usuarios set nombre ='" + Body + "' WHERE numero = " + userNumber + "and pvte='" + true + "' ";
                SqlCommand command = new SqlCommand(query, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                command.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            return "";

        }
        public async Task<string> updatelast_name(string Body, string userNumber)
        {
            try
            {
                string queryName = "UPDATE dbo.Usuarios set ap_paterno ='" + Body + "' WHERE numero = " + userNumber + " and pvte='" + true + "' ";
                SqlCommand commandName = new SqlCommand(queryName, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                commandName.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }
        public async Task<string> updatelast_nameM(string Body, string userNumber)
        {
            try
            {
                string queryName = "UPDATE dbo.Usuarios set ap_materno ='" + Body + "' WHERE numero = " + userNumber + "and pvte='" + true + "'  ";
                SqlCommand commandName = new SqlCommand(queryName, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                commandName.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }

        public async Task<string> updateGen(string Sexo, string userNumber)
        {
            try
            {
                string queryName = "UPDATE dbo.Usuarios set sexo ='" + Sexo + "' WHERE numero = " + userNumber + "and pvte='" + true + "'  ";
                SqlCommand commandName = new SqlCommand(queryName, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                commandName.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }
        public async Task<string> updatedate_birth(string Body, string userNumber)
        {
            try
            {
                string queryName = "UPDATE dbo.Usuarios set f_nacimiento ='" + Body + "' WHERE numero = " + userNumber + " and pvte='" + true + "' ";
                SqlCommand commandName = new SqlCommand(queryName, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                commandName.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }
        public async Task<string> updatestate_birth(string Body, string userNumber)
        {
            try
            {

                string queryName = "UPDATE dbo.Usuarios set id_estado ='" + Body + "' WHERE numero = " + userNumber + "and pvte='" + true + "' ";
                SqlCommand commandName = new SqlCommand(queryName, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                commandName.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }
        public async Task<string> updateShope(string id_suc, string userNumber)
        {
            try
            {
                string queryName = "UPDATE dbo.Usuarios set id_sucursal ='" + id_suc + "' WHERE numero = " + userNumber + "and pvte='" + true + "' ";
                SqlCommand commandName = new SqlCommand(queryName, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                commandName.ExecuteReader();
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
                string queryName = "UPDATE dbo.Usuarios set Servicio ='" + id_service + "' WHERE numero = " + userNumber + "and pvte = '" + true + "' ";
                SqlCommand commandName = new SqlCommand(queryName, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                commandName.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }
        public async Task<string> updateFamily(string id_service, string userNumber)
        {
            try
            {
                string queryName = "UPDATE dbo.Usuarios set Fam ='" + id_service + "' WHERE numero = " + userNumber + "and pvte = '" + true + "' ";
                SqlCommand commandName = new SqlCommand(queryName, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                commandName.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }
        public async Task<string> updateResetpvte(string userNumber)
        {
            try
            {
                string queryName = "UPDATE dbo.Usuarios set nombre =NULL ," +
                                    "ap_paterno=NULL," +
                                    "ap_materno=NULL," +
                                    "f_nacimiento=NULL," +
                                    "id_estado=NULL," +
                                    "fecha=NULL," +
                                    "id_paso='" + 1 + "'," +
                                    "sexo=Null," +

                                    "Servicio =NULL," +
                                    "Fam=NULL WHERE numero = " + userNumber + "and pvte = '" + true + "' ";
                SqlCommand commandName = new SqlCommand(queryName, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                commandName.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }
        public async Task<string> updateResetpvteinteractive(string userNumber)
        {
            try
            {
                string queryName = "UPDATE dbo.Usuarios set nombre =NULL ," +
                                    "ap_paterno=NULL," +
                                    "ap_materno=NULL," +
                                    "f_nacimiento=NULL," +
                                    "id_estado=NULL," +
                                    "fecha=NULL," +
                                    "id_paso='" + 3 + "'," +
                                    "sexo=Null," +

                                    "Fam=NULL WHERE numero = " + userNumber + "and pvte = '" + true + "' ";
                SqlCommand commandName = new SqlCommand(queryName, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                commandName.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }
        public async Task<string> updateReset(string userNumber)
        {
            try
            {
                string queryName = "UPDATE dbo.Usuarios set nombre =NULL ," +
                                    "ap_paterno=NULL," +
                                    "ap_materno=NULL," +
                                    "f_nacimiento=NULL," +
                                    "id_estado=NULL," +
                                    "fecha=NULL," +
                                    "id_paso='" + 1 + "'," +
                                    "sexo=Null" +
                                    " WHERE numero = " + userNumber + "and pvte = '" + true + "' ";


                SqlCommand commandName = new SqlCommand(queryName, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                commandName.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }
        public async Task<string> updateData(string data, string userNumber)
        {
            try
            {
                string queryName = "UPDATE dbo.Usuarios set " + data + " =NULL " +
                                   "WHERE numero = " + userNumber + "and pvte = '" + true + "' ";
                SqlCommand commandName = new SqlCommand(queryName, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                commandName.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }

        ///////////////////////////////////
        ///

        //public async Task<string> updateData2(string data, string userNumber)
        //{
        //    try
        //    {
        //        var usuario = DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).FirstOrDefaultAsync();
        //        if (usuario != null)
        //        {
        //            // Obtener el tipo del objeto 'usuario'
        //            var usuarioType = usuario.GetType();

        //            // Buscar la propiedad que corresponde al nombre del campo (fieldName)
        //            var propertyInfo = usuarioType.GetProperty(data);

        //            if (propertyInfo != null && propertyInfo.CanWrite)
        //            {
        //                // Asignar el nuevo valor a la propiedad
        //                propertyInfo.SetValue(usuario, Convert.ChangeType(null, propertyInfo.PropertyType), null);

        //                // Guardar los cambios en la base de datos
        //                await DB.SaveChangesAsync();


        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";
        //}

        //public async Task<string> updateReset2(string userNumber)
        //{
        //    try
        //    {
        //        foreach (var a in await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).ToListAsync())
        //        {
        //            a.Nombre = null;
        //            a.ApPaterno = null;
        //            a.ApMaterno = null;
        //            a.FNacimiento = null;
        //            a.IdEstado = null;
        //            a.Fecha = null;
        //            a.IdPaso = 1;
        //            a.Sexo = null;
        //            await DB.SaveChangesAsync();
        //        }


        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";
        //}

        //public async Task<string> updateResetpvteinteractive2(string userNumber)
        //{
        //    try
        //    {
        //        foreach (var a in await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).ToListAsync())
        //        {
        //            a.Nombre = null;
        //            a.ApPaterno = null;
        //            a.ApMaterno = null;
        //            a.FNacimiento = null;
        //            a.IdEstado = null;
        //            a.Fecha = null;
        //            a.IdPaso = 3;
        //            a.Sexo = null;
        //            a.Fam = null;
        //            await DB.SaveChangesAsync();
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";
        //}

        //public async Task<string> updateResetpvte2(string userNumber)
        //{
        //    try
        //    {
        //        foreach (var a in await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).ToListAsync())
        //        {
        //            a.Nombre = null;
        //            a.ApPaterno = null;
        //            a.ApMaterno = null;
        //            a.FNacimiento = null;
        //            a.IdEstado = null;
        //            a.Fecha = null;
        //            a.IdPaso = 1;
        //            a.Sexo = null;
        //            a.Servicio = null;
        //            a.Fam = null;
        //            await DB.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";
        //}
        //public async Task<string> updateFamily2(string id_service, string userNumber)
        //{
        //    try
        //    {
        //        foreach (var a in await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).ToListAsync())
        //        {
        //            a.Fam = id_service;
        //            await DB.SaveChangesAsync();
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";
        //}

        //public async Task<string> updateService2(string id_service, string userNumber)
        //{
        //    try
        //    {
        //        foreach (var a in await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).ToListAsync())
        //        {
        //            a.Servicio = id_service;
        //            await DB.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";
        //}

        //public async Task<string> updateShope2(string id_suc, string userNumber)
        //{
        //    try
        //    {
        //        foreach (var a in await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).ToListAsync())
        //        {
        //            a.IdSucursal = id_suc;
        //            await DB.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";
        //}

        //public async Task<string> updatestate_birth2(string Body, string userNumber)
        //{
        //    try
        //    {
        //        foreach (var a in await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).ToListAsync())
        //        {
        //            a.IdEstado = Convert.ToInt32(Body);
        //            await DB.SaveChangesAsync();
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";
        //}

        //public async Task<string> updatedate_birth2(string Body, string userNumber)
        //{
        //    try
        //    {
        //        foreach (var a in await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).ToListAsync())
        //        {
        //            a.FNacimiento = Body;
        //            await DB.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";
        //}

        //public async Task<string> updateGen2(string Sexo, string userNumber)
        //{
        //    try
        //    {
        //        foreach (var a in await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).ToListAsync())
        //        {
        //            a.Sexo = Sexo;
        //            await DB.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";
        //}

        //public async Task<string> updatelast_nameM2(string Body, string userNumber)
        //{
        //    try
        //    {
        //        foreach (var a in await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).ToListAsync())
        //        {
        //            a.ApMaterno = Body;
        //            await DB.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";
        //}
         

        //public async Task<string> updatename2(string Body, string userNumber)
        //{
        //    try
        //    {
        //        string query = "UPDATE dbo.Usuarios set nombre ='" + Body + "' WHERE numero = " + userNumber + "and pvte='" + true + "' ";
        //        SqlCommand command = new SqlCommand(query, conn);
        //        if (conn.State != System.Data.ConnectionState.Open)
        //            conn.Open();
        //        command.ExecuteReader();

        //        foreach (var a in await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).ToListAsync())
        //        {
        //            a.Nombre = Body;
        //            await DB.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";

        //}

        //public async Task<string> insertNewServiceRegistrer2(string userNumber)
        //{
        //    try
        //    {
        //        Usuario usuario = new Usuario();
        //        foreach (var item in await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).Take(1).ToListAsync())
        //        {

        //            usuario.Numero = string.IsNullOrEmpty(item.Numero) ? null : item.Numero;
        //            usuario.Nombre = string.IsNullOrEmpty(item.Nombre) ? null : item.Nombre;
        //            usuario.ApPaterno = string.IsNullOrEmpty(item.ApPaterno) ? null : item.ApPaterno;
        //            usuario.ApMaterno = string.IsNullOrEmpty(item.ApMaterno) ? null : item.ApMaterno;
        //            usuario.FNacimiento = string.IsNullOrEmpty(item.FNacimiento) ? null : item.FNacimiento;
        //            usuario.Fecha = DateTime.Today.ToString("yyyy-MM-dd");
        //            usuario.IdEstado = item.IdEstado == 0 ? null : item.IdEstado;
        //            usuario.IdSucursal = string.IsNullOrEmpty(item.IdSucursal) ? null : item.IdSucursal;
        //            usuario.Servicio = string.IsNullOrEmpty(item.Servicio) ? null : item.Servicio;
        //            usuario.Pvte = false;
        //            usuario.Fam = string.IsNullOrEmpty(item.Fam) ? null : item.Fam;
        //            usuario.IdEstatus = 2;
        //            usuario.Sexo = string.IsNullOrEmpty(item.Sexo) ? null : item.Sexo;
        //            await DB.Usuarios.AddAsync(usuario);
        //            await DB.SaveChangesAsync();
        //        }



        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";
        //}


        //public async Task<string> insertNewService2(string userNumber)
        //{
        //    try
        //    {
        //        Usuario usuario = new Usuario();
        //        foreach (var item in await DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true && x.Fam == FamList(userNumber).Result).Take(1).ToListAsync())
        //        {

        //            usuario.Numero = string.IsNullOrEmpty(item.Numero) ? null : item.Numero;
        //            usuario.Nombre = string.IsNullOrEmpty(item.Nombre) ? null : item.Nombre;
        //            usuario.ApPaterno = string.IsNullOrEmpty(item.ApPaterno) ? null : item.ApPaterno;
        //            usuario.ApMaterno = string.IsNullOrEmpty(item.ApMaterno) ? null : item.ApMaterno;
        //            usuario.FNacimiento = string.IsNullOrEmpty(item.FNacimiento) ? null : item.FNacimiento;
        //            usuario.Fecha = DateTime.Today.ToString("yyyy-MM-dd");
        //            usuario.IdEstado = item.IdEstado == 0 ? null : item.IdEstado;
        //            usuario.IdSucursal = string.IsNullOrEmpty(SucursalList(userNumber).Result) ? null : item.IdSucursal;
        //            usuario.Servicio = string.IsNullOrEmpty(ServiceList(userNumber).Result) ? null : item.Servicio;
        //            usuario.Pvte = false;
        //            usuario.Fam = string.IsNullOrEmpty(item.Fam) ? null : item.Fam;
        //            usuario.IdEstatus = 2;
        //            usuario.Sexo = string.IsNullOrEmpty(item.Sexo) ? null : item.Sexo;
        //            await DB.Usuarios.AddAsync(usuario);
        //            await DB.SaveChangesAsync();
        //        }



        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";
        //}

        //public async Task<string> insertgreeting2(string userNumber)
        //{
        //    try
        //    {
        //        Usuario usuario = new Usuario();
        //        usuario.Numero = userNumber;
        //        usuario.IdPaso = 1;
        //        usuario.Fecha = DateTime.Today.ToString("yyyy-MM-dd");
        //        usuario.Pvte = true;
        //        usuario.IdEstatus = 2;
        //        usuario.IdCallcenter = 0;
        //        usuario.Curp = "xxxxxxxx";
        //        await DB.Usuarios.AddAsync(usuario);
        //        await DB.SaveChangesAsync();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";
        //}
        //public async Task<string> updategreeting2(string userNumber)
        //{
        //    try
        //    {
        //        var user = DB.Usuarios.Where(x => x.Numero == userNumber && x.IdPaso == 2).FirstOrDefaultAsync();
        //        if (user.Result != null)
        //        {
        //            user.Result.Fecha = DateTime.Today.ToString("yyyy-MM-dd");
        //            await DB.SaveChangesAsync();
        //        }


        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";
        //}


        //public async Task<string> Updatesteps2(string greeting, string userNumber)
        //{
        //    try
        //    {
        //        var user = DB.Usuarios.Where(x => x.Numero == userNumber && x.Pvte == true).FirstOrDefaultAsync();
        //        if (user.Result != null)
        //        {
        //            user.Result.IdPaso = Convert.ToInt64(greeting);
        //            await DB.SaveChangesAsync();
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";
        //}

        //public async Task<string> ValidateDataColumns2(string Column)
        //{
        //    string? Datos = "";
        //    string usr = "Usuarios";
        //    try
        //    {
        //        var entityType = DB.Model.FindEntityType(usr);

        //        // Table info 
        //        // var tableName = entityType.GetTableName();
        //        //var tableSchema = entityType.GetSchema();
        //        if (entityType != null)
        //        {
        //            // Column info 
        //            foreach (var property in entityType.GetProperties())
        //            {

        //                var columnName = property.GetColumnName();
        //                var columnType = property.GetColumnType();
        //                if (columnName == Column) Datos = string.IsNullOrEmpty(columnName) ? null : columnName;


        //            };

        //        }

        //        //if (entityType != null)
        //        //{
        //        //    // Recorre todas las propiedades (columnas) de la entidad
        //        //    foreach (var property in entityType.GetProperties())
        //        //    {
        //        //        // Obtiene el nombre de la columna
        //        //        var columnName = property.GetColumnName(StoreObjectIdentifier.Table(entityType.GetTableName(), entityType.GetSchema()));

        //        //        // Si el nombre de la columna coincide con el que estás buscando, se asigna a 'Datos'
        //        //        if (columnName == Column)
        //        //        {
        //        //            Datos = string.IsNullOrEmpty(columnName) ? null : columnName;
        //        //        }
        //        //    }
        //        //}



        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return Datos;

        //}


        //public async Task<string> ValidateData2(string number)
        //{
        //    string? Datos = "";
        //    string? message = "";
        //    try
        //    {
                
        //        var mess = DB.Mensajes.Where(x => x.Nombre == "Datos_cita").FirstOrDefaultAsync();

        //        List<Steps> steps = new List<Steps>();
        //        List<Shopes> cli = new List<Shopes>();
        //        QueryModels Querys = new QueryModels();
        //        steps = await Querys.Steps(number);
        //        cli = (List<Shopes>)await Querys.QueryGetShopesUpdate(steps.Select(x => x.Clin).FirstOrDefault());

        //        if (mess.Result.Descripcion != null)
        //        {
        //            message = mess.Result.Descripcion.Replace(" name", " " + steps.Select(x => x.Name).FirstOrDefault() + " " + " " + steps.Select(x => x.Ap_paternal).FirstOrDefault() + " " + " " + steps.Select(x => x.Ap_maternal).FirstOrDefault() + " ")
        //                            .Replace(" gen", " " + steps.Select(x => x.Sexo).FirstOrDefault())
        //                            .Replace(" fch", " " + steps.Select(x => x.Date_birth).FirstOrDefault())
        //                            .Replace(" cli", " " + cli.Select(x => x.title).FirstOrDefault())
        //                            .Replace(" dir", " " + cli.Select(x => x.description).FirstOrDefault());


        //        }
               
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return message;

        //}


    }
}
