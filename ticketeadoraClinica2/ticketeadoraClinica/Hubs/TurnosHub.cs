using Microsoft.AspNetCore.SignalR;

namespace ticketeadoraClinica.Hubs
{
    public class TurnosHub : Hub
    {
        public async Task EnviarTurno(string codigo,string dni,string tipo,string hora)
        {
            await Clients.All.SendAsync("RecibirTurno", codigo, dni, tipo, hora);
        }
    }
}
