﻿using System.Net;

namespace ABMClientes.Client.Repositorios
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage) 
        {
            Response = response;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }

        public bool Error { get; set; } 
        public T? Response { get; set; }    
        public HttpResponseMessage? HttpResponseMessage { get; set; }

        public async Task<string?> ObtenerMensajeError()
        {
            if(!Error) 
            {
                return null;    
            }

            var codigoEstatus = HttpResponseMessage.StatusCode;

            // 404
            if(codigoEstatus == HttpStatusCode.NotFound)
            {
                return "Recurso no encontrado";
            }
            // 400
            else if(codigoEstatus == HttpStatusCode.BadRequest)
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }
            // 401
            else if(codigoEstatus==HttpStatusCode.Unauthorized)
            {
                return "Tienees que loguearte primero";
            }
            // 403
            else if(codigoEstatus == HttpStatusCode.Forbidden)
            {
                return "Sin permisos";
            }
            else
            {
                return "Hubo un error inesperado";
            }
        }
    }
}
