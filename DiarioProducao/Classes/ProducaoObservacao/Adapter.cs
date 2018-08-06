using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Android.App;
using Android.Views;
using Android.Widget;
using DiarioProducao.Classes.Comum;

namespace DiarioProducao.Classes.ProducaoObservacao
{


  public class Detalhe
  {

    public class Colunas
    {

      public Int64 SerieProducaoObservacao { get; set; }
      public Int64 SerieProducao { get; set; }
      public Int64 SerieFuncionario { get; set; }
      public DateTime DataObservacao { get; set; }
      public String TextoObservacao { get; set; }

      public Colunas ( )
      {
        SerieProducaoObservacao = 0L;
        SerieProducao = 0L;
        SerieFuncionario = 0L;
        DataObservacao = DateTime.MinValue;
        TextoObservacao = String.Empty;
      }

      public Colunas ( Int64 serieProducaoObservacao, Int64 serieProducao, Int64 serieFuncionario, DateTime dataObservacao, String textoObservacao )
      {
        SerieProducaoObservacao = serieProducaoObservacao;
        SerieProducao = serieProducao;
        SerieFuncionario = serieFuncionario;
        DataObservacao = dataObservacao;
        TextoObservacao = textoObservacao;
      }

      public Colunas ( SqlDataReader reader )
      {
        SerieProducaoObservacao = Convert.ToInt64 ( reader [ "serie_producao_observacao" ] );
        SerieProducao = Convert.ToInt64 ( reader [ "serie_producao" ] );
        SerieFuncionario = Convert.ToInt64 ( reader [ "serie_funcionario" ] );
        DataObservacao = Convert.ToDateTime ( reader [ "data_observacao" ] );
        TextoObservacao = reader [ "texto_observacao" ].ToString ( );
      }

    }

    public Int64 SerieProducao { get; set; }

    public Colunas ColunasDetalhe { get; set; }


    private void DetalheVazio ( )
    {
      SerieProducao = 0L;
      ColunasDetalhe = new Colunas ( );
    }

    public Detalhe ( )
    {
      DetalheVazio ( );
    }

    public Detalhe ( Int64 serieProducaoObservacao, Int64 serieProducao, Int64 serieFuncionario, String textoObservacao, DateTime dataObservacao )
    {
      SerieProducao = serieProducao;
      ColunasDetalhe = new Colunas ( serieProducaoObservacao, serieProducao, serieFuncionario, dataObservacao, textoObservacao );
    }

    public Detalhe ( SqlDataReader reader )
    {
      try
      {
        SerieProducao = Convert.ToInt64 ( ( reader [ "serie_producao" ] ) );
        ColunasDetalhe = new Colunas ( reader );
      }
      catch ( Exception )
      {
        DetalheVazio ( );
      }
    }

  }

  public class AdapterProducaoObservacao : BaseAdapter
  {
    private readonly Activity _activityMestre;
    private readonly List<Detalhe> _detalhes;

    public AdapterProducaoObservacao ( Activity activityMestre, AcessoSql acessoSql, String filtro )
    {
      _activityMestre = activityMestre;
      var query = new StringBuilder ( );
      query.AppendFormat ( Sql.QueryRecordAll, acessoSql.OpenQuery,   filtro );
      var banco = new Msde ( acessoSql );
      banco.Open ( );
      var reader = banco.DataReader ( query.ToString ( ) );
      _detalhes = new List<Detalhe> ( );
      while ( reader.Read ( ) )
      {
        var detalhe = new Detalhe ( reader );
        _detalhes.Add ( detalhe );
      }
      if ( _detalhes.Count <= 0 )
      {
        _detalhes.Add ( new Detalhe
        {
          ColunasDetalhe =
          {
            TextoObservacao = "Sem dados para exibir"
          }
        } );
      }
      reader.Close ( );
      reader.Dispose ( );
      banco.Close ( );
    }

    public override int Count
    {
      get
      {
        return _detalhes.Count;
      }
    }

    public override Java.Lang.Object GetItem ( int position )
    {
      return position;
    }

    public override long GetItemId ( int position )
    {
      return position;
    }

    public override View GetView ( int position, View convertView, ViewGroup parent )
    {
      var detalhe = _detalhes [ position ];
      var view = convertView;
      if ( convertView == null )
      {
        view = ( _activityMestre.LayoutInflater.Inflate ( Resource.Layout.producaoobservacao, parent, false ) ) as LinearLayout;
      }
      if ( view != null )
      {
        if (view.FindViewById( Resource.Id.txtObservacao ) is TextView textoObservacao)
        {
          textoObservacao.SetText( detalhe.ColunasDetalhe.TextoObservacao.Trim(), TextView.BufferType.Normal );
        }
      }
      return view;
    }

    public Detalhe GetItemAtPosition ( int position )
    {
      return _detalhes [ position ];
    }
  }
}