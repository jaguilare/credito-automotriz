using CreditoAutomotriz.Entities.Entities;
using System;
using System.Collections.Generic;

namespace CreditoAutomotriz.Entities.MapFiles
{
    public class ClientesDtoFile : PersonasDtoFile
    {
        public int IdCliente { get; set; }
        public string EstadoCivil { get; set; }
        public string IdentificacionConyuge { get; set; }
        public string NombresConyuge { get; set; }
        public byte SujetoCredito { get; set; }

        public static List<Clientes> ToClientes(List<ClientesDtoFile> recordsClientes)
        {
            List<Clientes> clientes = new List<Clientes>();
            foreach (var item in recordsClientes)
            {
                clientes.Add(
                    new Clientes()
                    {
                        EstadoCivil = item.EstadoCivil,
                        IdentificacionConyuge = item.IdentificacionConyuge,
                        NombresConyuge = item.NombresConyuge,
                        SujetoCredito = item.SujetoCredito == 1 ? true : false,
                        IdClientesNavigation = new Personas
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
            return clientes;
        }
    }

}
