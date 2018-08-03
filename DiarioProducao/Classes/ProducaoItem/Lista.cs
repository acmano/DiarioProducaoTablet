using System;
using System.Text;
using Android.App;
using DiarioProducao.Classes.Comum;

namespace DiarioProducao.Classes.ProducaoItem
{

  public class ListaProducaoItem
  {
  
    public AdapterProducaoItem ListaProducaoItemAdapter;

    public ListaProducaoItem(Activity activityMestre, AcessoSql acessoSql, Int64 numSerieLmp)
    {
      var filtro = new StringBuilder();
      filtro.Clear();
      filtro.AppendFormat(Sql.FilproProducao, numSerieLmp);
      ListaProducaoItemAdapter = new AdapterProducaoItem(activityMestre, acessoSql, filtro.ToString());
    }

  }
}