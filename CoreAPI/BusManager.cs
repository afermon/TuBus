using System;
using DataAccess.Crud;
using Entities;
using System.Collections.Generic;
using Exceptions;

namespace CoreAPI
{
    public class BusManager : BaseManager
    {
        private readonly BusCrudFactory _busCrudFactory;
        private readonly RequisitoCrudFactory _requisitoCrudFactory;

        public BusManager()
        {
            _busCrudFactory = new BusCrudFactory();
            _requisitoCrudFactory = new RequisitoCrudFactory();
        }

        public void CreateBus(Bus bus)
        {
            try
            {
                var registroBus = _busCrudFactory.Retrieve<Bus>(bus);

                if (registroBus != null)
                    throw new BusinessException(404);

                _busCrudFactory.Create(bus);

                foreach (var requisito in bus.Requisitos)
                {
                    _requisitoCrudFactory.Create(requisito);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Bus> RetrieveAll()
        {
            var buses = _busCrudFactory.RetrieveAll<Bus>();

            foreach (var bus in buses)
            {
                bus.Requisitos = _requisitoCrudFactory.RetriveAllByPlacaStatement<Requisito>(new Requisito { Placa = bus.Id });
            }

            return buses;
        }

        public void Disable(Bus bus)
        {
            _busCrudFactory.UpdateEstado(bus);
        }

        public void Update(Bus bus)
        {
            _busCrudFactory.Update(bus);

            foreach (var requisito in bus.Requisitos)
            {
                _requisitoCrudFactory.Update(requisito);
            }
        }

        public Bus Retrieve(Bus bus)
        {
            var requisitoCrud = new RequisitoCrudFactory();
            var busResultado = _busCrudFactory.Retrieve<Bus>(bus);
            var requisitoResultado = requisitoCrud.RetriveAllByPlacaStatement<Requisito>(new Requisito { Placa = bus.Id });

            busResultado.Requisitos = requisitoResultado;

            return busResultado;
        }

    }
}
