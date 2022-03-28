using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiContribuyente_Segundo.DTOs;
using WebApiContribuyente_Segundo.Entidades;

namespace WebApiContribuyente_Segundo.Controllers
{
    [ApiController]
    [Route("api/contribuyentes")]
    public class ContribuyentesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;

        public ContribuyentesController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            this.dbContext = context;
            this.mapper = mapper;
            this.env = env;
        }

        // Método para escribir en los archivos
        private void Escribir(string nombreArchivo, string msg)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{nombreArchivo}";
            using (StreamWriter writer = new StreamWriter(ruta, append: true)) 
            { 
                writer.WriteLine(msg);
                writer.Close(); 
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<GetContribuyenteDTO>>> Get()
        {
            var contribuyente = await dbContext.Contribuyentes.ToListAsync();
            return mapper.Map<List<GetContribuyenteDTO>>(contribuyente);
        }


        [HttpGet("{id:int}")] //Se puede usar ? para que no sea obligatorio el parametro /{param=Manuel}  getContribuyente/{id:int}/
        public async Task<ActionResult<GetContribuyenteDTO>> Get(int id)
        {
            var contribuyente = await dbContext.Contribuyentes.FirstOrDefaultAsync(contribuyenteBD => contribuyenteBD.Id == id);

            if (contribuyente == null)
            {
                return NotFound();
            }

            return mapper.Map<GetContribuyenteDTO>(contribuyente);

        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<GetContribuyenteDTO>>> Get([FromRoute] string nombre)
        {
            var contribuyentes = await dbContext.Contribuyentes.Where(contribuyenteBD => contribuyenteBD.Nombre.Contains(nombre)).ToListAsync();
            

            //nombre del archivo
            string nombreArchivo = "registroConsultado.txt";

            //manda llamar la funcion y pasa los parametros
            Escribir(nombreArchivo, nombre);


            return mapper.Map<List<GetContribuyenteDTO>>(contribuyentes);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ContribuyenteDTO contribuyenteDto)
        {
            //Ejemplo para validar desde el controlador con la BD con ayuda del dbContext

            var existeAlumnoMismoNombre = await dbContext.Contribuyentes.AnyAsync(x => x.Nombre == contribuyenteDto.Nombre);

            if (existeAlumnoMismoNombre)
            {
                return BadRequest($"Ya existe un autor con el nombre {contribuyenteDto.Nombre}");
            }

            //var contribuyente = new Contribuyente()
            //{
            //    Nombre = contribuyenteDto.Nombre
            //};

            var contribuyente = mapper.Map<Contribuyente>(contribuyenteDto);

            //se nombra el archivo
            string nombreArchivo = "nuevosRegistros.txt";

            //manda llamar la funcion y pasa los parametros
            Escribir(nombreArchivo, contribuyente.Nombre);

            dbContext.Add(contribuyente);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] // api/alumnos/1
        public async Task<ActionResult> Put(Contribuyente contribuyente, int id)
        {
            var exist = await dbContext.Contribuyentes.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            if (contribuyente.Id != id)
            {
                return BadRequest("El id del contribuyente no coincide con el establecido en la url.");
            }

            dbContext.Update(contribuyente);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Contribuyentes.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }

            dbContext.Remove(new Contribuyente()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
