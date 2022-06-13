using System;
using System.Collections.Generic;
using System.Text;

namespace CreditoAutomotriz.Entities.App
{
    public static class MensajesRespuesta
    {

        public const string OK = "Proceso exitoso";
        public const string ERROR = "Error, proceso no ejecutado";

        public const string OK_REGISTROS_CLIENTES_CSV_GUARDADOS = "Archivo de clientes cargado con éxito";
        public const string OK_REGISTROS_EJECUTIVOS_CSV_GUARDADOS = "Archivo de ejecutivos cargado con éxito";
        public const string OK_REGISTROS_MARCAS_CSV_GUARDADOS = "Archivo de marcas cargado con éxito";

        public const string ERROR_REGISTROS_CLIENTES_CSV_REPETIDOS = "Error, existen clientes repetidos en el archivo csv";
        public const string ERROR_REGISTROS_EJECUTIVOS_CSV_REPETIDOS = "Error, existen ejecutivos repetidos en el archivo csv";
        public const string ERROR_REGISTROS_MARCAS_CSV_REPETIDAS = "Error, existen marcas repetidas en el archivo csv";

        public const string ERROR_CONYUGES_IDENTIFICACIONES_IGUALES = "Error, las identificaciones entre conyuges no pueden ser iguales";

        public const string WARNING_CLIENTE_YA_EXISTE = "Error, el cliente ya se encuentra registrado en la base de datos ";
        public const string WARNING_CLIENTES_NO_ENCONTRADO = "Error, el cliente no se encontró";

        public const string WARNING_VEHICULO_YA_EXISTE = "Error, el vehículo ya se encuentra registrado en la base de datos";
        public const string WARNING_VEHICULO_NO_ENCONTRADO = "Error, el vehículo no se encontró";

        public const string WARNING_PATIO_AUTOS_YA_EXISTE = "Error, ya existe un patio de autos en la base de datos con dicho punto de venta";
        public const string WARNING_PATIO_AUTOS_NO_ENCONTRADO = "Error, el patio de autos no se encontró";

        public const string WARNING_REGISTRO_DEPENDIENTE = "Error, no se puede eliminar el registro porque esta relacionado con otros";


        public const string WARNING_REGISTROS_EJECUTIVOS_DB_REPETIDOS = "Error, el ejecutivo ya se encuentra registrado en la base de datos";
        public const string WARNING_REGISTROS_MARCAS_VEHICULOS_DB_REPETIDOS = "Error, la marca ya se encuentra registrado en la base de datos";
        public const string WARNING_REGISTROS_PATIO_AUTOS_DB_REPETIDOS = "Error, ya se encuentra registrado en la base de datos el patio de autos";
        public const string WARNING_EJECUTIVOS_NO_EXISTE_PATIO_AUTOS_LABORA = "Error, no se encontró el patio de autos";

        public const string WARNING_SOLICITUD_CREDITO_ACTIVA_DIA_HOY= "Error, ya existe una solicitud activa el día de hoy para el cliente";
        public const string WARNING_SOLICITUD_CREDITO_VEHICULO_EN_OTRA_SOLICITUD = "Error, ya existe una solicitud activa con el vehículo";
        public const string WARNING_SOLICITUD_CREDITO_CLIENTE_NO_SUJETO_A_CREDITO = "Error, el cliente no es sujeto a credito";

        public const string WARNING_CLIENTE_PATIO_AUTO_NO_ENCONTRADO = "No se encontraron registros con el cliente";

    }
}
