using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Crud;
using Entities;
using Exceptions;

namespace CoreAPI
{
    public class LineaManager : BaseManager
    {
        private readonly LineaCrudFactory _crudFactory;

        public LineaManager()
        {
            _crudFactory = new LineaCrudFactory();
        }

        public List<Linea> GetAllLines(int terminal, int empresaId = 0)
        {
            try
            {
                var lineas =  _crudFactory.RetrieveAll<Linea>();
                var terminalManager = new TerminalManager();
                var empresaManager = new EmpresaManager();
                lineas.ForEach(l =>
                    {
                        l.Terminal = terminalManager.RetrieveById(l.Terminal);
                        l.Empresa  = empresaManager.GetEmpresaById(l.Empresa);
                    }
                );

                if (empresaId != 0)
                    return lineas.Where(l => l.Empresa.CedulaJuridica == empresaId).ToList();

                return lineas.Where(l => l.Terminal.Id == terminal).ToList();
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new List<Linea>();
        }

        public void CreateLine(Linea line)
        {
            try
            {
                var available = GetTotalSpaces(line);

                if (available < line.EspaciosParqueo)
                    throw new BusinessException(307);

                _crudFactory.Create(line);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public void UpdateLine(Linea line)
        {
            try
            {
                var available = GetTotalSpaces(line);

                if (available < line.EspaciosParqueo)
                    throw new BusinessException(307);

                _crudFactory.Update(line);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public Linea GetLineById(Linea line)
        {
            try
            {
                var lineas = _crudFactory.Retrieve<Linea>(line);
                var terminalManager = new TerminalManager();
                var empresaManager = new EmpresaManager();
                if (lineas != null)
                {
                    lineas.Terminal = terminalManager.RetrieveById(lineas.Terminal);
                    lineas.Empresa  = empresaManager.GetEmpresaById(lineas.Empresa);
                }

                return lineas;
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new Linea();
        }

        public void DeleteLine(Linea line)
        {
            try
            {
                _crudFactory.Delete(line);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public int GetTotalSpaces(Linea line)
        {
            try
            {
                var managerTerminal = new TerminalManager();
                var terminal = managerTerminal.RetrieveById(line.Terminal);
                var getSoldSpaces = _crudFactory.GetSpaces(line);
                return  terminal.EspaciosParqueoBus - getSoldSpaces.EspaciosParqueo;
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return 0;
        }

    }
}
