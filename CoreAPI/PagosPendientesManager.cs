using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Crud;
using DataAccess.Mapper;
using Entities;
using Exceptions;

namespace CoreAPI
{
    public class PagosPendientesManager : BaseManager
    {
        private readonly PagosPendientesCrudFactory _crudFactory;

        public PagosPendientesManager()
        {
            _crudFactory = new PagosPendientesCrudFactory();
        }

        public void Create(int empresaId)
        {
            try
            {
                var pagosResalizados = ObtenerPagosPorEmpresa(empresaId);
                var pagoCreado = false;

                pagosResalizados.ForEach(p =>
                {
                    var mes = DateTime.Parse(p.Fecha);
                    var now = DateTime.Now;

                    if (mes.Month == now.Month)
                        pagoCreado = true;

                });

                if (pagoCreado)
                    return;

                var lineaManager = new LineaManager();
                var configuracionTerminal = new ConfiguracionManager();
                var lineas = lineaManager.GetAllLines(0, empresaId);
                var pagos = new List<PagoPendiente>();
                lineas.ForEach(l =>
                {
                    var termialConfig = configuracionTerminal.RetrieveConfiguracionTerminal(l.Terminal.Id);
                    pagos.Add(new PagoPendiente
                    {
                        LineaId = l.LineaId,
                        EmpresaId = l.Empresa.CedulaJuridica,
                        Monto = Convert.ToInt32(l.EspaciosParqueo * termialConfig.CostoParqueoBusMes)
                    });
                });

                pagos.ForEach(p => _crudFactory.Create(p));
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }


        public List<PagoPendiente> ObtenerPagosPorEmpresa(int empresaId)
        {
            try
            {
                var lineaManager = new LineaManager();
                var lista = _crudFactory.RetrieveAllByEmpresa<PagoPendiente>(new PagoPendiente {EmpresaId = empresaId});
                lista.ForEach(l => { l.Linea = lineaManager.GetLineById(new Linea { LineaId = l.LineaId}); });
                return lista;
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new List<PagoPendiente>();
        }

        public void UpdatePagosPorEmpresa(PagoPendiente pago)
        {
            try
            {
                _crudFactory.Update(pago);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }
    }
}
