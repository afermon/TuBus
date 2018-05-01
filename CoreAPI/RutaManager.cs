using System;
using DataAccess.Crud;
using Entities;
using System.Collections.Generic;
using System.Linq;
using Exceptions;

namespace CoreAPI
{
    public class RutaManager : BaseManager
    {
        private readonly RutaCrudFactory _crudRuta;
        private readonly HorarioCrudFactory _crudHorario;

        public RutaManager()
        {
            _crudRuta = new RutaCrudFactory();
            _crudHorario = new HorarioCrudFactory();
        }

        public void Create(Ruta ruta)
        {
            try
            {
                var rutaDb = _crudRuta.Retrieve<Ruta>(ruta);
                if (rutaDb != null)
                    throw new BusinessException(214);

                if (ruta.Estado == null) ruta.Estado = "Activo";

               ruta.Id = _crudRuta.CreateId(ruta);

                if(ruta.Id < 1)
                    throw new BusinessException(215);

               foreach (var horario in ruta.Horarios)
               {
                   horario.RutaId = ruta.Id;
                    _crudHorario.Create(horario);
               }

            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public void Delete(Ruta ruta)
        {
            try
            {
                var rutaDb = _crudRuta.Retrieve<Ruta>(ruta);
                if (rutaDb == null)
                    throw new BusinessException(213);
                _crudRuta.Delete(ruta);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public void Update(Ruta ruta)
        {
            try
            {
                var rutaDb = _crudRuta.Retrieve<Ruta>(ruta);
                if (rutaDb == null)
                    throw new BusinessException(213);

                if (ruta.Estado == null) ruta.Estado = rutaDb.Estado;

                _crudRuta.Update(ruta);

                _crudHorario.Delete(new Horario{ RutaId = ruta.Id});

                foreach (var horario in ruta.Horarios)
                {
                    horario.RutaId = ruta.Id;
                    _crudHorario.Create(horario);
                }
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public void Activate(Ruta ruta)
        {
            try
            {
                var rutaDb = _crudRuta.Retrieve<Ruta>(ruta);
                if (rutaDb == null)
                    throw new BusinessException(213);

                rutaDb.Estado = "Activo";
                _crudRuta.Update(ruta);

            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public Ruta Retrieve(Ruta ruta)
        {
            Ruta rutaDb = null;
            try
            {
                rutaDb = _crudRuta.Retrieve<Ruta>(ruta);

                if (rutaDb == null)
                    throw new BusinessException(213);

                rutaDb.Horarios = _crudHorario.RetrieveByRuta<Horario>(new Horario{RutaId = ruta.Id});
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return rutaDb;
        }

        public List<Ruta> RetrieveAll(int terminal, int empresaId = 0)
        {
            var rutas = _crudRuta.RetrieveAll<Ruta>();

            if (empresaId != 0)
                rutas = rutas.Where(r => r.EmpresaId == empresaId).ToList();
            
            if (terminal > 0)
                rutas = rutas.Where(r => r.TerminalId == terminal).ToList();

            foreach (var ruta in rutas)
            {
                ruta.Horarios = _crudHorario.RetrieveByRuta<Horario>(new Horario { RutaId = ruta.Id });
            }

            return rutas;
        }

        public List<Ruta> RetrieveByTerminal(Ruta ruta)
        {
            return _crudRuta.RetrieveByTerminal<Ruta>(ruta);
        }
    }
}
