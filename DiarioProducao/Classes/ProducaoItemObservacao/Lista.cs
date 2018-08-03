using System;
using System.Text;
using Android.App;
using DiarioProducao.Classes.Comum;

namespace DiarioProducao.Classes.ProducaoItemObservacao
{

  public class ListaProducaoItemObservacao
  {

    public readonly AdapterProducaoItemObservacao ListaProducaoItemObservacaoAdapter;

    public ListaProducaoItemObservacao ( Activity activityMestre, AcessoSql acessoSql, Int64 numSerieLmpi )
    {
      var filtro = new StringBuilder ( );
      filtro.Clear ( );
      filtro.AppendFormat ( Sql.FiltroProducaoItem, numSerieLmpi );
      ListaProducaoItemObservacaoAdapter = new AdapterProducaoItemObservacao ( activityMestre, acessoSql, filtro.ToString ( ) );
    }

  }
}