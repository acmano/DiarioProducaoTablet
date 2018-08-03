using System;
using System.Text;
using Android.App;
using DiarioProducao.Classes.Comum;

namespace DiarioProducao.Classes.ProducaoItemOcorrencia
{

  public class ListaProducaoItemOcorrencia
  {

    public readonly AdapterProducaoItemOcorrencia ListaProducaoItemOcorrenciaAdapter;

    public ListaProducaoItemOcorrencia ( Activity activityMestre, AcessoSql acessoSql, Int64 numSerieLmpi )
    {
      var filtro = new StringBuilder ( );
      filtro.Clear ( );
      filtro.AppendFormat ( Sql.FiltroProducaoItem, numSerieLmpi );
      ListaProducaoItemOcorrenciaAdapter = new AdapterProducaoItemOcorrencia ( activityMestre, acessoSql, filtro.ToString ( ) );
    }

  }
}