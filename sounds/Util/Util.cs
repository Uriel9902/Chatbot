using API_SISDE.Data;
using API_SISDE.Models.Connection;
using API_SISDE.Models.WhatsappCloud;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace API_SISDE.Util
{
    public class Util
    {
        CHATPRContext DB = new CHATPRContext();
        QueryModelsEntity queryModels = new QueryModelsEntity();
        // Definir HttpClient como estática para su reutilización
        private static readonly HttpClient req = new HttpClient
        {
            BaseAddress = new Uri("http://172.16.71.11") // Establecer la URL base para evitar repetirla
        };
        public async Task<object> TextMessage(string message, string number)
        {
            var messageContent = new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "text",
                text = new
                {
                    body = message
                }
            };
            return await Task.FromResult(messageContent);
        }
        public async Task<object> ImageMessage(string url, string number)
        {
            var Image = new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "image",
                image = new
                {
                    link = url
                }
            };
            return await Task.FromResult(Image);
        }
        public async Task<object> AudioMessage(string url, string number)
        {
            var Audio = new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "audio",
                audio = new
                {
                    link = url
                }
            };
            return await Task.FromResult(Audio);
        }
        public async Task<object> ButtonsOption(string number)
        {
            var template = new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "interactive",
                interactive = new
                {
                    type = "button",
                    body = new
                    {
                        text = "¿Quieres agendar este servicio?"
                    },
                    action = new
                    {
                        buttons = new List<object>
                        {
                            new
                            {
                                type="reply",
                                reply= new
                                {
                                    id="01Si",
                                    title="Si"
                                }
                            },
                            new
                            {
                                type="reply",
                                reply= new
                                {
                                    id="02No",
                                    title="No"
                                }
                            }
                        }
                    }
                }
            };
            return await Task.FromResult(template);

        }
        public async Task<object> interactiveMessageFamily(string number)
        {

            var serviceContet = await queryModels.QueryGetFamily();
            return new
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = number,
                type = "interactive",
                interactive = new
                {
                    type = "list",
                    header = new
                    {
                        type = "text",
                        text = ""
                    },
                    body = new
                    {
                        text = "Para brindarles una atención personalizada, por favor indícanos para quién es la cita: 🤔"
                    },
                    footer = new
                    {
                        text = ""
                    },
                    action = new
                    {
                        button = "Ver opciones",
                        sections = new List<object>
                     {
                 new
                  {

                      title= "Ver opciones",
                      rows= serviceContet


                  }

              }


                    }
                }
            };
        }
        public async Task<object> interactiveMessageData(string number)
        {
            var Data = new
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = number,
                type = "interactive",
                interactive = new
                {
                    type = "list",
                    header = new
                    {
                        type = "text",
                        text = ""
                    },
                    body = new
                    {
                        text = "¿Cuál dato necesitas corregir?"
                    },
                    footer = new
                    {
                        text = ""
                    },
                    action = new
                    {
                        button = "Datos",
                        sections = new List<object>
                   {
                  new
                  {

                      title= "Datos",
                      rows=new List<object>
                      {
                          new
                          {
                            id= "nombre",
                            title= "Nombre",
                            description= ""
                          },
                          new
                          {
                            id= "ap_paterno",
                            title= "Apellido Paterno",
                            description= ""
                          },
                          new
                          {
                            id= "ap_materno",
                            title= "Apellido Materno",
                            description= ""
                          },
                          new
                          {
                            id= "f_nacimiento",
                            title= "Fecha de Nacimiento",
                            description= ""
                          },
                          //new
                          //{
                          //  id= "id_estado",
                          //  title= "Estado",
                          //  description= ""
                          //},
                          new
                          {
                            id= "sexo",
                            title= "Genero",
                            description= ""
                          },
                          //new
                          //{
                          //  id= "id_sucursal",
                          //  title= "Sucursal",
                          //  description= ""
                          //}
                      }

                  }

              }


                    }

                }


            };

            return await Task.FromResult(Data);


        }
        public async Task<object> ButtonValidateData(string number)
        {
            string data = await queryModels.ValidateData(number);

            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "interactive",
                interactive = new
                {
                    type = "button",
                    body = new
                    {
                        text = data
                    },
                    action = new
                    {
                        buttons = new List<object>
              {
                  new
                  {
                      type="reply",
                      reply= new
                      {
                          id="01AG",
                          title="Si,agendar cita"
                      }
                  },
                  new
                  {
                      type="reply",
                      reply= new
                      {
                          id="02CD",
                          title="Corregir datos"
                      }
                  },
                  new
                  {
                      type="reply",
                      reply= new
                      {
                          id="03CN",
                          title="Cancelar"
                      }
                  },

                        }
                    }
                }
            };
        }
        public async Task<object> interactiveMessage(string number)
        {

            var shopes = await queryModels.QueryGetShopes();
            var greeting = await queryModels.QueryGetgreeting();
            return new
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = number,
                type = "interactive",
                interactive = new
                {
                    type = "list",
                    header = new
                    {
                        type = "text",
                        text = ""
                    },
                    body = new
                    {
                        text = greeting,
                    },
                    footer = new
                    {
                        text = ""
                    },
                    action = new
                    {
                        button = "Sucursales",
                        sections = new List<object>
                    {
                  new
                  {

                      title= "Sucursales",
                      rows= shopes


                  }

              }


                    }
                }
            };


        }
        public async Task<object> interactiveMessageOtherServices(string number)
        {

            var Content = "¿Quieres ver otros servicios? 👇 ";

            var serviceContet = await queryModels.QueryGetservice();
            string name = "Servicios";


            return new
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = number,
                type = "interactive",
                interactive = new
                {
                    type = "list",
                    header = new
                    {
                        type = "text",
                        text = ""
                    },
                    body = new
                    {
                        text = Content
                    },
                    footer = new
                    {
                        text = ""
                    },
                    action = new
                    {
                        button = name,

                        sections = new List<object>
                     {
                 new
                  {

                      title= "Cambiar",
                      rows= serviceContet


                  }

              }


                    }
                }
            };


        }
        public async Task<object> interactiveMessageService(string number, string id_suc)
        {
            var service = await queryModels.QueryGetserviceMessage();

            var serviceContent = await queryModels.QueryGetservice();

            var Horario = await queryModels.ShopesList(id_suc);

            return new
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = number,
                type = "interactive",
                interactive = new
                {
                    type = "list",
                    header = new
                    {
                        type = "text",
                        text = ""
                    },
                    body = new
                    {
                        text = Horario + "\n\n" + service
                    },
                    footer = new
                    {
                        text = ""
                    },
                    action = new
                    {
                        button = "Servicios",
                        sections = new List<object>
                    {
                  new
                  {

                      title= "Servicios",
                      rows= serviceContent


                  }

              }


                    }
                }
            };

        }
        public async Task<object> interactiveMessagetest(string id_service, string number)
        {
            var Content = await queryModels.MessageServices(id_service);


            if (Content == "") Content = "Preguntas";

            var isSubTraitmentF = await DB.Faciales.Where(x => x.Clave == id_service).FirstOrDefaultAsync();
            var isSubTraitmentA = await DB.Aparatologia.Where(x => x.Clave == id_service).FirstOrDefaultAsync();

            var serviceContet = await queryModels.QueryGetservice();
            string name = "Servicios";


            if (id_service == "01C")
            {
                var id = await DB.Usuarios.AsNoTracking().Where(x => x.Numero == number && x.Pvte == true).FirstOrDefaultAsync();
                if (id != null && id.IdSucursal != null)
                {
                    // Extraer el número de la sucursal
                    string a = Regex.Match(id.IdSucursal, @"\d+").Value;

                    // Realizar la solicitud HTTP con la URL base ya configurada
                    var response = await req.GetAsync($"/Bot/GetPriceConsultation?id_shope={a}");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var prices = JsonConvert.DeserializeObject<List<Prices>>(jsonString);

                        // Filtrar los precios solo una vez
                        var precioNormal = prices.FirstOrDefault(x => x.nombre == "NORMAL");
                        var precioFestivo = prices.FirstOrDefault(x => x.nombre == "FESTIVO");

                        // Reemplazar contenido en la plantilla
                        Content = Content.Replace("*precio*",
                            $"\n$ {precioNormal?.precio} {precioNormal?.nombre}" +
                            $"\n$ {precioFestivo?.precio} {precioFestivo?.nombre}");
                    }
                   

                }

            }

            if (id_service == "04AP" || isSubTraitmentA != null)
            {
                serviceContet = await queryModels.QueryGetserviceAparatologia();

                name = "Aparatología";
            }
            else if (id_service == "03TF" || isSubTraitmentF != null)
            {
                serviceContet = await queryModels.QueryGetserviceFacial();
                name = "Tratamientos";
            }
            else if (id_service == "07PRE")
            {
                serviceContet = await queryModels.QueryGetQuestions();
                name = "Preguntas";
            }

            return new
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = number,
                type = "interactive",
                interactive = new
                {
                    type = "list",
                    header = new
                    {
                        type = "text",
                        text = ""
                    },
                    body = new
                    {
                        text = Content
                    },
                    footer = new
                    {
                        text = ""
                    },
                    action = new
                    {
                        button = name,

                        sections = new List<object>
                     {
                 new
                  {

                      title= "Cambiar",
                      rows= serviceContet


                  }

              }


                    }
                }
            };

        }
        public async Task<object> LocationMessage(string number, string id_suc)
        {

            var res = await DB.Sucursales.Where(x => x.Clave == id_suc).FirstOrDefaultAsync();


            return new
            {

                messaging_product = "whatsapp",
                to = number,
                type = "location",
                location =
                new
                {
                    latitude = res.Latitud,
                    longitude = res.Longitud,
                    name = res.Nombre,
                    address = res.Direccion
                }
            };


        }
        public async Task<object> ButtonGen(string number)
        {
            var Button = new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "interactive",
                interactive = new
                {
                    type = "button",
                    body = new
                    {
                        text = "Selecciona tú genero"
                    },
                    action = new
                    {
                        buttons = new List<object>
              {
                  new
                  {
                      type="reply",
                      reply= new
                      {
                          id="H",
                          title="Hombre"
                      }
                  },
                  new
                  {
                      type="reply",
                      reply= new
                      {
                          id="M",
                          title="Mujer"
                      }
                  },
                  new
                  {
                      type="reply",
                      reply= new
                      {
                          id="X",
                          title="Otro"
                      }
                  }
                        }
                    }
                }
            };
            return await Task.FromResult(Button);
        }
    }

}









