using System;
using DataAccess.Crud;
using Entities;
using System.Collections.Generic;
using System.Linq;
using Exceptions;

namespace CoreAPI
{
    public class RecorridoManager : BaseManager
    {
        private static readonly List<Recorrido> ListaRecorridosActuales = new List<Recorrido>();
        private readonly RecorridoCrudFactory _crudRecorrido;
        private readonly TerminalCrudFactory _crudTerminal;
        private readonly RutaCrudFactory _crudRuta;
        private readonly LineaCrudFactory _crudLinea;
        private readonly ChoferCrudFactory _crudChofer;
        private readonly BusCrudFactory _crudBus;
        private readonly HorarioCrudFactory _crudHorario;

        public RecorridoManager()
        {
            _crudRecorrido = new RecorridoCrudFactory();
            _crudTerminal = new TerminalCrudFactory();
            _crudLinea = new LineaCrudFactory();
            _crudRuta = new RutaCrudFactory();
            _crudHorario = new HorarioCrudFactory();
            _crudChofer = new ChoferCrudFactory();
            _crudBus = new BusCrudFactory();
        }

        public Recorrido RegistrarRecorrido(Recorrido recorrido)
        {
            try
            {
                var ruta = _crudRuta.Retrieve<Ruta>(new Ruta{ Id = recorrido.RutaId});
                if (ruta == null || !ruta.Estado.Equals("Activo")) //No existe la ruta o esta inactiva
                    throw new BusinessException(221);
                
                ruta.Horarios = _crudHorario.RetrieveByRuta<Horario>(new Horario { RutaId = ruta.Id });

                var day = ((int) DateTime.Now.DayOfWeek == 0) ? 7 : (int) DateTime.Now.DayOfWeek;
                if (ruta.Horarios.FirstOrDefault(h => h.Dia == day && h.Hora == recorrido.Horario) == null) //Noexiste el horario indicado.
                    throw new BusinessException(226);

                var linea = _crudLinea.Retrieve<Linea>(new Linea { LineaId = ruta.LineaId });
                if (linea == null || !linea.Estado.Equals("Activo")) //No existe la linea o esta inactiva
                    throw new BusinessException(225);

                var bus = _crudBus.Retrieve<Bus>(new Bus { Id = recorrido.BusPlaca });
                if (bus == null || !bus.Estado.Equals("Activo")) //No existe el bus o esta inactiva
                    throw new BusinessException(222);
                
                var chofer = _crudChofer.Retrieve<Chofer>(new Chofer { Cedula = recorrido.ChoferCedula });
                if (chofer == null || !chofer.Estado.Equals("Activo") || !chofer.Empresa.Equals(linea.Empresa.CedulaJuridica)) //No existe el chofer o esta inactiva
                    throw new BusinessException(223);

                recorrido.RecorridoId = _crudRecorrido.CreateId(recorrido);

                if (recorrido.RecorridoId < 1) //id invalido
                    throw new BusinessException(219);

            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return recorrido;
        }

        public void RegistrarSalida(Recorrido recorrido)
        {
            try
            {
                var recorridoDb = _crudRecorrido.Retrieve<Recorrido>(recorrido);
                if (recorridoDb == null) //No existe
                    throw new BusinessException(217);

                if (ListaRecorridosActuales.FirstOrDefault(r => r.RecorridoId == recorridoDb.RecorridoId) != null || recorridoDb.HoraSalida != DateTime.MinValue) //Ya salio
                    throw new BusinessException(220);

                recorridoDb.HoraSalida = DateTime.Now;

                var ruta = _crudRuta.Retrieve<Ruta>(new Ruta { Id = recorridoDb.RutaId });
                var terminal = _crudTerminal.Retrieve<Terminal>(new Terminal { Id = ruta.TerminalId });

                var time = DateTime.Now.TimeOfDay;

                var tardia = time - recorridoDb.Horario;

                var configManager = new ConfiguracionManager();
                var configuracionTerminal = configManager.RetrieveConfiguracionTerminal(ruta.TerminalId);

                if (tardia.Minutes > configuracionTerminal.CantidadMinutosTardia) // More than 15 minutes late.
                {
                    recorridoDb.MinutosTarde = tardia.Minutes;
                    if (_crudRecorrido.RetrieveTardias<Recorrido>().Count >= configuracionTerminal.CantidadTardiasSancion)
                    {
                        var sancionManager = new SancionManager();
                        var linea = _crudLinea.Retrieve<Linea>(new Linea { LineaId = ruta.LineaId });

                        var sancion = new Sancion
                        {
                            Descripcion =
                                "Sanción por mas de " + configuracionTerminal.CantidadTardiasSancion +
                                " tardias en el mes.",
                            Multa = 5000,
                            Estado = "Activo",
                            TerminalId = ruta.TerminalId,
                            Fecha = DateTime.Now,
                            Suspencion = "Inactivo",
                            FechaReactivacion = DateTime.Now.AddDays(20),
                            Empresa = linea.Empresa.CedulaJuridica
                        };
                        
                        sancionManager.Create(sancion);
                    }
                }

                _crudRecorrido.UpdateSalida(recorridoDb);

                recorridoDb.Posicion = new Posicion
                    {
                        RecorridoId = recorridoDb.RecorridoId,
                        Latitude = terminal.Latitude,
                        Longitude = terminal.Longitude,
                        TimeStamp = DateTime.Now
                    };
                ListaRecorridosActuales.Add(recorridoDb);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public void RegistrarLlegada(Recorrido recorrido)
        {
            try
            {
                var recorridoDb = _crudRecorrido.Retrieve<Recorrido>(recorrido);
                if (recorridoDb == null) //No existe
                    throw new BusinessException(217);

                if (ListaRecorridosActuales.FirstOrDefault(r => r.RecorridoId == recorridoDb.RecorridoId) == null || recorridoDb.HoraSalida == DateTime.MinValue) //no ha salido
                    throw new BusinessException(218);

                recorridoDb.HoraLlegada = DateTime.Now;

                _crudRecorrido.UpdateLlegada(recorridoDb);

                var posicion = ListaRecorridosActuales.FirstOrDefault(r => r.RecorridoId == recorridoDb.RecorridoId);
                ListaRecorridosActuales.Remove(posicion);

            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public void RegistrarIngresoPasajero(IngresoBus ingreso)
        {
            try
            {
                var recorridoDb = _crudRecorrido.Retrieve<Recorrido>(new Recorrido{ RecorridoId = ingreso.RecorridoId});
                if (recorridoDb == null)
                    throw new BusinessException(217);

                var ruta = _crudRuta.Retrieve<Ruta>(new Ruta { Id = recorridoDb.RutaId });

                if(recorridoDb.HoraLlegada != DateTime.MinValue) //EL bus ya llego a su destino
                    throw new BusinessException(225);

                var bus = _crudBus.Retrieve<Bus>(new Bus { Id = recorridoDb.BusPlaca });
                if ((bus.CapacidadDePie + bus.CapacidadSentado) <= recorridoDb.CantidadPasajeros) // Bus lleno
                    throw new BusinessException(224);

                var transactionManager = new TransactionManager();
                var terminalManger =  new TerminalManager();

                var terminal = terminalManger.RetrieveById(new Terminal{Id = ruta.TerminalId});

                transactionManager.CreateUserTransaction(new Transaccion
                {
                    CardUniqueCode =  ingreso.Tarjeta,
                    Charge = Convert.ToInt32(ruta.CostoTotal),
                    Description = $"Pasaje de bus de Ruta: {ruta.RutaName}",
                    Type = "Pago",
                    Terminal = terminal,
                    Status = "Activo",
                    LineaId = ruta.LineaId
                }, true);

                recorridoDb.CantidadPasajeros++;
                _crudRecorrido.UpdatePasajeros(recorridoDb);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public void UpdatePosition(Posicion posicion)
        {
            try
            {
                var posicionList = ListaRecorridosActuales.FirstOrDefault(r => r.RecorridoId == posicion.RecorridoId);
                if (posicionList == null) //no ha salido
                    throw new BusinessException(218);

                posicionList.Posicion.Latitude = posicion.Latitude;
                posicionList.Posicion.Longitude = posicion.Longitude;
                posicionList.Posicion.TimeStamp = DateTime.Now;

            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public List<Recorrido> RetrievePosiciones()
        {
            return ListaRecorridosActuales;
        }
    }
}
