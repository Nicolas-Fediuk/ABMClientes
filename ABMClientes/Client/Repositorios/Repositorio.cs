using System.Text;
using System.Text.Json;

namespace ABMClientes.Client.Repositorios
{
    public class Repositorio : IRepositorio
    {
        public HttpClient HttpClient { get; }

        public Repositorio(HttpClient httpClient) 
        {
            HttpClient = httpClient;
        }

        private JsonSerializerOptions OpcionesPorDefectoJSON => new JsonSerializerOptions
        {
            //para que no distinga de las mayusculas de las minusculas
            PropertyNameCaseInsensitive = true,
        };

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var respuestaHTTP = await HttpClient.GetAsync(url);

            if (respuestaHTTP.IsSuccessStatusCode)
            {
                var respuesta = await DeserializarRespuesta<T>(respuestaHTTP, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<T>(respuesta, error: false, respuestaHTTP);
            }
            else
            {
                return new HttpResponseWrapper<T>(default,error:false, respuestaHTTP);
            }
        } 

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await HttpClient.PostAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode,responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await HttpClient.PutAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var responseHttp = await HttpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await HttpClient.PostAsync(url, enviarContent);

            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializarRespuesta<TResponse>(responseHttp, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<TResponse>(response, error: false, responseHttp);
            }

            return new HttpResponseWrapper<TResponse>(default, !responseHttp.IsSuccessStatusCode, responseHttp);

        }

        //Deserializar: tengo un string y lo quiero como objeto 
        private async Task<T> DeserializarRespuesta<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            try
            {
                var respuestaString = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(respuestaString, jsonSerializerOptions);
            }
            catch(Exception ex)
            {
                var mensaje = ex.Message;
                var a = 0;
                throw new Exception($"Error de deserialización: {ex.Message}", ex);
            }
        }
    }
}
