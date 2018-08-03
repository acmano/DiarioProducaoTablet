using System;
using System.Text;
using Android.App;
using DiarioProducao.Classes.Comum;

namespace DiarioProducao.Classes.ProducaoItemFuncionario
{

  public class ListaProducaoItemFuncionario
  {

    public readonly AdapterProducaoItemFuncionario ListaProducaoItemFuncionarioAdapter;

    public ListaProducaoItemFuncionario ( Activity activityMestre, AcessoSql acessoSql, Int64 numSerieLmpi )
    {
      var filtro = new StringBuilder ( );
      filtro.Clear ( );
      filtro.AppendFormat ( Sql.FiltroProducaoItem, numSerieLmpi );
      ListaProducaoItemFuncionarioAdapter = new AdapterProducaoItemFuncionario( activityMestre, acessoSql, filtro.ToString ( ) );
    }

  }
}