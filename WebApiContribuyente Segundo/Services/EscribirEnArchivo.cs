﻿namespace WebApiContribuyente_Segundo.Services
{
    public class EscribirEnArchivo : IHostedService
    {
        private readonly IWebHostEnvironment env;
        private readonly string nombreArchivo = "Archivo 1.txt";
        private Timer timer;

        public EscribirEnArchivo(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Se ejecuta cuando cargamos la aplicación 1 vez
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            Escribir("Proceso iniciado");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Se ejecuta cuando detenemos la aplicación aunque puede que no se ejecute por algún error
            timer.Dispose();
            Escribir("Proceso finalizado");
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            Escribir("Proceso en ejecución: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }

        private void Escribir(string msg)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{nombreArchivo}";
            using (StreamWriter writer = new StreamWriter(ruta, append: true)) { writer.WriteLine(msg); }
        }
    }
}
