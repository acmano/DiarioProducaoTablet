using System;
using System.Text;
using Android.App;
using DiarioProducao.Classes.Comum;

namespace DiarioProducao.Classes.Producao
{

  public class ListaProducao
  {

    public readonly AdapterProducao ListaProducaoAdapter;

    public ListaProducao ( Activity activityMestre, AcessoSql acessoSql, String codEmpresa, String datProducao, Int64 numSerieLmlm, Int64 numSerieLmtrn )
    {
      var filtro = new StringBuilder();
      filtro.Clear();
      filtro.AppendFormat(Sql.FiltroDatProducao , Convert.ToDateTime(datProducao).ToString("MM/dd/yyyy") );
      if ( numSerieLmtrn != Int64.MinValue && numSerieLmtrn != 0L )
      {
        filtro.AppendFormat(Sql.FiltroLinhaSerie, numSerieLmlm );
      }
      if ( numSerieLmtrn != Int64.MinValue && numSerieLmtrn != 0L )
      {
        filtro.AppendFormat(Sql.FiltroTurnoSerie, numSerieLmtrn );
      }
      ListaProducaoAdapter = new AdapterProducao ( activityMestre, acessoSql, codEmpresa, filtro.ToString() );
    }

  }
}