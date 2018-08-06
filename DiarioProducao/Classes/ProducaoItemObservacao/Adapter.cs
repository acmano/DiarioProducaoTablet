using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Android.App;
using Android.Views;
using Android.Widget;
using DiarioProducao.Classes.Comum;

namespace DiarioProducao.Classes.ProducaoItemObservacao
{


  public class Detalhe
  {

    public class Colunas
    {

      public Int64 SerieProducaoItemObservacao { get; set; }
      public Int64 SerieProducaoItem { get; set; }
      public Int64 SerieFuncionario { get; set; }
      public Int64 MatriculaFuncionario { get; set; }
      public String NomeFuncionario { get; set; }
      public DateTime DataObservacao { get; set; }
      public String TextoObservacao { get; set; }

      public Colunas ( )
      {
        SerieProducaoItemObservacao = 0L;
        SerieProducaoItem = 0L;
        SerieFuncionario = 0L;
        MatriculaFuncionario = 0L;
        NomeFuncionario = String.Empty;
        DataObservacao = DateTime.MinValue;
        TextoObservacao = String.Empty;
      }

      public Colunas ( Int64 serieProducaoItemObservacao, Int64 serieProducaoItem, Int64 serieFuncionario, Int64 matriculaFuncionario, String nomeFuncionario, DateTime dataObservacao, String textoObservacao )
      {
        SerieProducaoItemObservacao = serieProducaoItemObservacao;
        SerieProducaoItem = serieProducaoItem;
        SerieFuncionario = serieFuncionario;
        MatriculaFuncionario = matriculaFuncionario;
        NomeFuncionario = nomeFuncionario;
        DataObservacao = dataObservacao;
        TextoObservacao = textoObservacao;
      }

      public Colunas ( SqlDataReader reader )
      {
        SerieProducaoItemObservacao = Convert.ToInt64 ( reader [ "serie_producao_item_observacao" ] );
        SerieProducaoItem = Convert.ToInt64 ( reader [ "serie_producao_item" ] );
        SerieFuncionario = Convert.ToInt64 ( reader [ "serie_funcionario" ] );
        MatriculaFuncionario = Convert.ToInt64 ( reader [ "matricula_funcionario" ] );
        NomeFuncionario = reader [ "nome_funcionario" ].ToString() ;
        DataObservacao = Convert.ToDateTime ( reader [ "data_observacao" ] );
        TextoObservacao = reader [ "texto_observacao" ].ToString ( );
      }

    }

    public Int64 SerieProducaoItem { get; set; }
    public Colunas ColunasDetalhe { get; set; }


    private void DetalheVazio ( )
    {
      SerieProducaoItem = 0L;
      ColunasDetalhe = new Colunas ( );
    }

    public Detalhe ( )
    {
      DetalheVazio ( );
    }

    public Detalhe ( Int64 serieProducaoObservacao, Int64 serieProducaoItem, Int64 serieFuncionario, Int64 matriculaFuncionario, String nomeFuncionario, String textoObservacao, DateTime dataObservacao )
    {
      SerieProducaoItem = serieProducaoItem;
      ColunasDetalhe = new Colunas ( serieProducaoObservacao, serieProducaoItem, serieFuncionario, matriculaFuncionario, nomeFuncionario, dataObservacao, textoObservacao );
    }

    public Detalhe ( SqlDataReader reader )
    {
      try
      {
        SerieProducaoItem = Convert.ToInt64 ( ( reader [ "serie_producao_item" ] ) );
        ColunasDetalhe = new Colunas ( reader );
      }
      catch ( Exception )
      {
        DetalheVazio ( );
      }
    }

  }

  public class AdapterProducaoItemObservacao : BaseAdapter
  {
    private readonly Activity _activityMestre;
    private readonly List<Detalhe> _detalhes;

    public AdapterProducaoItemObservacao ( Activity activityMestre, AcessoSql acessoSql, String filtro )
    {
      _activityMestre = activityMestre;
      var query = new StringBuilder ( );
      query.AppendFormat ( Sql.QueryRecordAll, acessoSql.OpenQuery, filtro );
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
        _detalhes.Add
        (
          new Detalhe
          {
            ColunasDetalhe =
            {
              TextoObservacao = "Sem Observacões"
            }
          }
        ) ;
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
        view = ( _activityMestre.LayoutInflater.Inflate ( Resource.Layout.itemobservacao, parent, false ) ) as LinearLayout;
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