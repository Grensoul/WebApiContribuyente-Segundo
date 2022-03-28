namespace WebApiContribuyente_Segundo.Services
{
    public class EscribirAlabanzaAlProfeArchivo : IHostedService
    {
        private readonly IWebHostEnvironment env;
        private readonly string nombreArchivo = "AlabanzaAlProfe.txt";
        private Timer timer;

        public EscribirAlabanzaAlProfeArchivo(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(HacerJale, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
            Escribir("Proceso iniciado");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Dispose();
            Escribir("Proceso finalizado");
            return Task.CompletedTask;
        }
        private void Escribir(string msg)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{nombreArchivo}";
            using (StreamWriter writer = new StreamWriter(ruta, append: true)) { writer.WriteLine(msg); }
        }

        private void HacerJale(object state)
        {
            Escribir("El Profe Gustavo Rodriguez es el mejor.   " + DateTime.Now.ToString("hh:mm:ss"));
        }

        
    }
}
