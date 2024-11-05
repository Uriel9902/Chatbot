using API_SISDE.Data;
using API_SISDE.Models.Connection;
using API_SISDE.Models.WhatsappCloud;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml.Schema;
using static System.Net.WebRequestMethods;

namespace API_SISDE.Util
{
    public class Util
    {
        CHATPRContext DB = new CHATPRContext();
        QueryModelsEntity queryModels = new QueryModelsEntity();
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
        public object VideoMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "video",
                video = new
                {
                    link = url
                }
            };
        }
        public object DocumentMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "document",
                document = new
                {
                    link = url
                }
            };
        }

        public object ButtonsMessage(string number)
        {
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
                        text = "¿ Estas de acuerdo ?"
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
                                    id="1",
                                    title="Si"
                                }
                            },
                            new
                            {
                                type="reply",
                                reply= new
                                {
                                    id="2",
                                    title="No"
                                }
                            }
                        }
                    }
                }
            };


        }
        public object ButtonsReturnService(string number)
        {

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
                        text = "¿Quieres ver otro servicios?"
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
                                    id="01S",
                                    title="Otros servicios"
                                }
                            }
                            //new
                            //{
                            //    type="reply",
                            //    reply= new
                            //    {
                            //        id="02AG",
                            //        title="¡Lo quiero!"
                            //    }
                            //}
                        }
                    }
                }
            };

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

        public object interactiveMessageServiceMa(string number)
        {
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
                        text = "En el boton 👇🏻 de abajo puedes ver menu de los servicios disponibles en esta sucursal 🏬 \n "
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
                      rows=new List<object>
                      {
                          new
                          {
                            id= "01CMA",
                            title= "👨🏻‍⚕️ Consulta dermatológica",
                            description= ""
                          },
                          new
                          {
                            id= "02TMA",
                            title= "💆🏻‍ Tratamientos Faciales",
                            description= ""
                          },
                          new
                          {
                            id= "03AMA",
                            title= "🧪 Aparatología",
                            description= ""
                          },
                          new
                          {
                            id= "04DMA",
                            title= "Dermapen – Micropunción",
                            description= ""
                          },
                          new
                          {
                            id= "05CMA",
                            title= "Curaciones",
                            description= ""
                          },
                          new
                          {
                            id= "06EMA",
                            title= "🦵🏻 Estética",
                            description= ""
                          },
                          new
                          {
                            id= "07PMA",
                            title= "🤑 Promociones",
                            description= ""
                          },
                          new
                          {
                            id= "08PFMA",
                            title= "❓ Preguntas frecuentes",
                            description= ""
                          }
                      }

                  }

              }


                    }

                }


            };


        }


        public object interactiveMessageQuestions(string number)
        {
            //string service = queryModels.QueryGetserviceMessageaparatologia();
            var serviceContet = queryModels.QueryGetQuestions();
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
                        text = "Preguntas frecuentes"
                    },
                    footer = new
                    {
                        text = ""
                    },
                    action = new
                    {
                        button = "Preguntas",
                        sections = new List<object>
                     {
                 new
                  {

                      title= "Preguntas",
                      rows= serviceContet


                  }

              }


                    }
                }
            };
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



        public object interactiveMessageServiceDermapen(string message, string number)
        {

            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "text",
                text = new
                {
                    body = message
                }

            };
        }
        public object interactiveMessageServiceHealing(string message, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "text",
                text = new
                {
                    body = message
                }
            };
        }
        public object AudioMessageLimFacial(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "audio",
                audio = new
                {
                    link = url
                }
            };
        }
        public object AudioMessageHidraly(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "audio",
                audio = new
                {
                    link = url
                }
            };
        }


        object ButtonstratamintoFacial(string number)
        {



            var service = queryModels.QueryGetserviceMessage();


            var serviceContet = queryModels.QueryGetservice();
            ;


            var Tratamientos = queryModels.QueryGetserviceFacial();



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
                        text = "¿Quieres ver otro servicio?\n\n o si deseas agendar este servicio presiona el boton 'agendar' "
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
                          id="02MS",
                          title="Servicios"
                      }
                  },
                  new
                  {
                      type="reply",
                      reply= new
                      {
                          id="01AGF",
                          title="Agendar"
                      }
                  }

                        }
                    }
                }
            };

            throw new NotImplementedException();
        }

        object ButtonstratamintoAp(string number)
        {

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
                        text = "¿Quieres ver otro servicio?\n\n o si deseas agendar este servicio presiona el boton 'agendar' "
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
                          id="02MSP",
                          title="Servicios"
                      }
                  },
                  new
                  {
                      type="reply",
                      reply= new
                      {
                          id="01AGAP",
                          title="Agendar"
                      }
                  }
                        }
                    }
                }
            };
            throw new NotImplementedException();
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



        object interactiveMessageShopes(string number)
        {



            var shopes = queryModels.QueryGetShopes();




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
                        text = "Selecciona una sucursal"
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
            throw new NotImplementedException();
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

        object interactiveMessageServiceBack(string number)
        {

            var serviceContet = queryModels.QueryGetservice();

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
                        text = "Ver otros servicios"
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
                      rows= serviceContet


                  }

              }


                    }
                }
            };
            throw new NotImplementedException();
        }

        object interactiveMessageFacialBack(string number)
        {

            var Tratamientos = queryModels.QueryGetserviceFacial();
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
                        text = "Ver otros tratamientos faciales"
                    },
                    footer = new
                    {
                        text = ""
                    },
                    action = new
                    {
                        button = "Trataminetos",
                        sections = new List<object>
                     {
                 new
                  {

                      title= "Servicios",
                      rows= Tratamientos


                  }

              }


                    }
                }
            };
            throw new NotImplementedException();
        }

        object interactiveMessageAPBack(string number)
        {


            var aparatologia = queryModels.QueryGetserviceAparatologia();




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
                        text = "Ver otros tratamientos de aparatología"
                    },
                    footer = new
                    {
                        text = ""
                    },
                    action = new
                    {
                        button = "aparatología",
                        sections = new List<object>
                     {
                 new
                  {

                      title= "aparatología",
                      rows= aparatologia


                  }

              }


                    }
                }
            };
            throw new NotImplementedException();
        }

        object interactiveMessageServiceFacial(string number)
        {



            var service = queryModels.QueryGetserviceMessageFacial();


            var FacialContet = queryModels.QueryGetserviceFacial();
            // var serviceContet = queryModels.QueryGetservice();
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
                        text = service.ToString()
                    },
                    footer = new
                    {
                        text = ""
                    },
                    action = new
                    {
                        button = "Tratamientos",
                        sections = new List<object>
                     {
                 new
                  {

                      title= "Tratamientos",
                      rows= FacialContet


                  }
              }


                    }
                }
            };
            throw new NotImplementedException();
        }

        object interactiveMessageServiceApparatusology(string number)
        {


            var service = queryModels.QueryGetserviceMessageaparatologia();


            var serviceContet = queryModels.QueryGetserviceAparatologia();
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
                        text = service.ToString()
                    },
                    footer = new
                    {
                        text = ""
                    },
                    action = new
                    {
                        button = "Aparatología",
                        sections = new List<object>
                     {
                 new
                  {

                      title= "Aparatología",
                      rows= serviceContet


                  }

              }


                    }
                }
            };
            throw new NotImplementedException();
        }

        public async Task<object> interactiveMessagetest(string id_service, string number)
        {
            var Content = await queryModels.MessageServices(id_service);

            //queryModels.StepsServiceConsultations(id_service, number).ToString();
            if (Content == "") Content = "Preguntas";

            var isSubTraitmentF = await DB.Faciales.Where(x => x.Clave == id_service).FirstOrDefaultAsync();
            var isSubTraitmentA = await DB.Aparatologia.Where(x => x.Clave == id_service).FirstOrDefaultAsync();

            var serviceContet = await queryModels.QueryGetservice();
            string name = "Servicios";

            // var serviceContet = new object { };

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


            //List<Location> locations = new List<Location>();


            //var res = queryModels.GetlocationShope(shope);
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


        object Shopes(string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = number,
                type = "interactive",
                interactive = new
                {
                    type = "button",
                    body = new
                    {
                        text = "<BUTTON_TEXT>"
                    },
                    action = new
                    {
                        buttons = new List<object>
            {
                new {
                    type= "reply",
                    reply= new {
                        id= "<UNIQUE_BUTTON_ID_1>",
                        title= "<BUTTON_TITLE_1>"
                    }
                },
                new {
                    type= "reply",
                    reply= new {
                        id= "<UNIQUE_BUTTON_ID_2>",
                        title= "<BUTTON_TITLE_2>"
                    }
                }
            }
                    }
                }
            };


        }
    }

}









