using CreditoAutomotriz.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditoAutomotriz.Entities.MapFiles
{
    public class EjecutivosDtoFile : PersonasDtoFile
    {
        public int PatioLabora { get; set; }
        public string Celular { get; set; }

        public static List<Ejecutivos> ToEjecutivos(List<EjecutivosDtoFile> recordsEjecutivos)
        {
            List<Ejecutivos> ejecutivos = new List<Ejecutivos>();
            foreach (var item in recordsEjecutivos)
            {
                ejecutivos.Add(
                    new Ejecutivos()
                    {
                        Celular = item.Celular,
                        IdPatiosAutos = item.PatioLabora,
                        IdEjecutivosNavigation = new Personas
                        {
                            Identificacion = item.Identificacion,
                            Nombres = item.Nombres,
                            Apellidos = item.Apellidos,
                            Direccion = item.Direccion,
                            Telefono = item.Telefono,
                            FechaNacimiento = item.FechaNacimiento,
                            Edad = item.Edad
                        }
                    }
                );
            }
            return ejecutivos;
        }
    }
}
