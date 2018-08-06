using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Android.App;
using Android.Views;
using Android.Widget;
using DiarioProducao.Classes.Comum;

namespace DiarioProducao.Classes.ProducaoItemOcorrencia
{


  public class Detalhe
  {

    public class Colunas
    {

      public Int64 SerieProducaoItemOcorrencia { get; set; }
      public Int64 SerieProducaoItem { get; set; }
      public Int64 SerieOcorrencia { get; set; }
      public DateTime DataOcorrencia { get; set; }
      public String CodigoOcorrencia { get; set; }
      public String DescricaoOcorrencia { get; set; }

      public Colunas ( )
      {
        SerieProducaoItemOcorrencia = 0L;
        SerieProducaoItem = 0L;
        SerieOcorrencia = 0L;
        DataOcorrencia = DateTime.MinValue;
        CodigoOcorrencia = String.Empty;
        DescricaoOcorrencia = String.Empty;
      }

      public Colunas ( Int64 serieProducaoItemOcorrencia, Int64 serieProducaoItem, Int64 serieOcorrencia, DateTime dataOcorrencia, String codigoOcorrencia, String descricaoOcorrencia )
      {
        SerieProducaoItemOcorrencia = serieProducaoItemOcorrencia;
        SerieProducaoItem = serieProducaoItem;
        SerieOcorrencia = serieOcorrencia;
        DataOcorrencia = dataOcorrencia;
        CodigoOcorrencia = codigoOcorrencia;
        DescricaoOcorrencia = descricaoOcorrencia;
      }

      public Colunas ( SqlDataReader reader )
      {
        SerieProducaoItemOcorrencia = Convert.ToInt64 ( reader [ "serie_producao_item_ocorrencia" ] );
        SerieProducaoItem = Convert.ToInt64 ( reader [ "serie_producao_item" ] );
        SerieOcorrencia = Convert.ToInt64 ( reader [ "serie_ocorrencia" ] );
        DataOcorrencia = Convert.ToDateTime ( reader [ "data_Ocorrencia" ] );
        CodigoOcorrencia = reader [ "codigo_ocorrencia" ].ToString ( );
        DescricaoOcorrencia = reader [ "descricao_ocorrencia" ].ToString ( );
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

    public Detalhe ( Int64 serieProducaoOcorrencia, Int64 serieProducaoItem, Int64 serieOcorrencia, DateTime dataOcorrencia, String codigoOcorrencia, String descricaoOcorrencia )
    {
      SerieProducaoItem = serieProducaoItem;
      ColunasDetalhe = new Colunas ( serieProducaoOcorrencia, serieProducaoItem, serieOcorrencia, dataOcorrencia, codigoOcorrencia, descricaoOcorrencia );
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

  public class AdapterProducaoItemOcorrencia : BaseAdapter
  {
    private readonly Activity _activityMestre;
    private readonly List<Detalhe> _detalhes;

    public AdapterProducaoItemOcorrencia ( Activity activityMestre, AcessoSql acessoSql, String filtro )
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
              DescricaoOcorrencia = "Sem ocorrências"
            }
          }
        );
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
        view = ( _activityMestre.LayoutInflater.Inflate ( Resource.Layout.itemocorrencia, parent, false ) ) as LinearLayout;
      }
      if ( view != null )
      {
        if (view.FindViewById( Resource.Id.txtDataOcorrencia ) is TextView dataOcorrencia)
        {
          dataOcorrencia.SetText( detalhe.ColunasDetalhe.DataOcorrencia.ToString( "G" ).Trim(), TextView.BufferType.Normal );
        }
        if (view.FindViewById( Resource.Id.txtCodigoOcorrencia ) is TextView codigoOcorrencia)
        {
          codigoOcorrencia.SetText( detalhe.ColunasDetalhe.CodigoOcorrencia.Trim(), TextView.BufferType.Normal );
        }
        if (view.FindViewById( Resource.Id.txtDescricaoOcorrencia ) is TextView descricaoOcorrencia)
        {
          descricaoOcorrencia.SetText( detalhe.ColunasDetalhe.DescricaoOcorrencia.Trim(), TextView.BufferType.Normal );
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