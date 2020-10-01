using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Newtonsoft.Json;

namespace ServidorPeliculas
{
    public class PeliculasServer
    {
        public CatalogoPeliculas CatalogoPeliculas { get; set; } = new CatalogoPeliculas();

        HttpListener listener;

        Dispatcher dispatcher;

        public PeliculasServer()
        {
            listener = new HttpListener();
            dispatcher = Dispatcher.CurrentDispatcher;

            listener.Prefixes.Add("http://*:8080/Actividad5/");
            listener.Start();
            listener.BeginGetContext(OnRequest, null);

        }

        private void OnRequest(IAsyncResult ar)
        {
            var context = listener.EndGetContext(ar);
            listener.BeginGetContext(OnRequest, null);

            if (context.Request.Url.LocalPath == "/Actividad5/Tablero")
            {
                if (context.Request.HttpMethod == "GET")
                {
                    var datos = JsonConvert.SerializeObject(CatalogoPeliculas.ListaPeliculas);
                    byte[] buffer = Encoding.UTF8.GetBytes(datos);

                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    context.Response.StatusCode = 200;
                }
                else
                {
                    if (context.Request.ContentType.StartsWith("application/json") & context.Request.ContentLength64 > 0)
                    {
                        StreamReader reader = new StreamReader(context.Request.InputStream);
                        var datos = reader.ReadToEnd();
                        var pelicula = JsonConvert.DeserializeObject<Pelicula>(datos);

                        dispatcher.Invoke(new Action(() =>
                        {
                            if (context.Request.HttpMethod == "POST")
                            {
                                CatalogoPeliculas.Agregar(pelicula);
                            }
                            else if (context.Request.HttpMethod == "PUT")
                            {
                                CatalogoPeliculas.Editar(pelicula);
                            }
                            else if(context.Request.HttpMethod=="DELETE")
                            {
                                CatalogoPeliculas.Eliminar(pelicula);
                            }

                        }));
                        context.Response.StatusCode = 200;
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
            }
            else
            {
                context.Response.StatusCode = 404;
            }
            context.Response.Close();

        }
    }
}
