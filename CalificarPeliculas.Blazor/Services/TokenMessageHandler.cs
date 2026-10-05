using System.Net.Http.Headers;

namespace CalificarPeliculas.Blazor.Services
{
    // Adjunta el token JWT de la sesion a cada request saliente hacia la API.
    public class TokenMessageHandler : DelegatingHandler
    {
        private readonly SesionState sesion;

        public TokenMessageHandler(SesionState sesion)
        {
            this.sesion = sesion;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(sesion.Token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", sesion.Token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
