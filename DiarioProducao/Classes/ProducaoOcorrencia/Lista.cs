using System;
using System.Text;
using Android.App;
using DiarioProducao.Classes.Comum;

namespace DiarioProducao.Classes.ProducaoOcorrencia
{

  public class ListaProducaoOcorrencia
  {

    public AdapterProducaoOcorrencia ListaProducaoOcorrenciaAdapter;

    public ListaProducaoOcorrencia ( Activity activityMestre, AcessoSql acessoSql, Int64 numSerieLmp )
    {
      var filtro = new StringBuilder ( );
      filtro.Clear ( );
      filtro.AppendFormat ( Sql.FiltroProducao, numSerieLmp );
      ListaProducaoOcorrenciaAdapter = new AdapterProducaoOcorrencia ( activityMestre, acessoSql, filtro.ToString ( ) );
    }

  }
}