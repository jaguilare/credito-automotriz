using CreditoAutomotriz.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditoAutomotriz.Entities.MapFiles
{
    public class MarcasVehiculosDtoFile
    {
        public int IdMarcasVehiculos { get; set; }

        public string Marca { get; set; }

        public static List<MarcasVehiculos> ToMarcasVehiculos(List<MarcasVehiculosDtoFile> recordsMarcas)
        {
            List<MarcasVehiculos> marcas = new List<MarcasVehiculos>();
            foreach (var item in recordsMarcas)
            {
                marcas.Add(
                    new MarcasVehiculos()
                    {
                        Marca = item.Marca,
                    }
                );
            }
            return marcas;
        }
    }
}
