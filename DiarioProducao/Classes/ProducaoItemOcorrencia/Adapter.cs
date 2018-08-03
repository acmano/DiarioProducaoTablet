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
  using Android;

  public class Detalhe
  {

    public class Colunas
    {
      private Int64 _serieProducaoItemOcorrencia;
      private Int64 _serieProducaoItem;
      private Int64 _serieOcorrencia;
      private DateTime _dataOcorrencia;
      private String _codigoOcorrencia;
      private String _descricaoOcorrencia;

      public Int64 SerieProducaoItemOcorrencia
      {
        get
        {
          return _serieProducaoItemOcorrencia;
        }
        set
        {
          _serieProducaoItemOcorrencia = value;
        }
      }

      public Int64 SerieProducaoItem
      {
        get
        {
          return _serieProducaoItem;
        }
        set
        {
          _serieProducaoItem = value;
        }
      }

      public Int64 SerieOcorrencia
      {
        get
        {
          return _serieOcorrencia;
        }
        set
        {
          _serieOcorrencia = value;
        }
      }

      public DateTime DataOcorrencia
      {
        get
        {
          return _dataOcorrencia;
        }
        set
        {
          _dataOcorrencia = value;
        }
      }

      public String CodigoOcorrencia
      {
        get
        {
          return _codigoOcorrencia;
        }
        set
        {
          _codigoOcorrencia = value;
        }
      }

      public String DescricaoOcorrencia
      {
        get
        {
          return _descricaoOcorrencia;
        }
        set
        {
          _descricaoOcorrencia = value;
        }
      }

      public Colunas ( )
      {
        _serieProducaoItemOcorrencia = 0L;
        _serieProducaoItem = 0L;
        _serieOcorrencia = 0L;
        _dataOcorrencia = DateTime.MinValue;
        _codigoOcorrencia = String.Empty;
        _descricaoOcorrencia = String.Empty;
      }

      public Colunas ( Int64 serieProducaoItemOcorrencia, Int64 serieProducaoItem, Int64 serieOcorrencia, DateTime dataOcorrencia, String codigoOcorrencia, String descricaoOcorrencia )
      {
        _serieProducaoItemOcorrencia = serieProducaoItemOcorrencia;
        _serieProducaoItem = serieProducaoItem;
        _serieOcorrencia = serieOcorrencia;
        _dataOcorrencia = dataOcorrencia;
        _codigoOcorrencia = codigoOcorrencia;
        _descricaoOcorrencia = descricaoOcorrencia;
      }

      public Colunas ( SqlDataReader reader )
      {
        _serieProducaoItemOcorrencia = Convert.ToInt64 ( reader [ "serie_producao_item_ocorrencia" ] );
        _serieProducaoItem = Convert.ToInt64 ( reader [ "serie_producao_item" ] );
        _serieOcorrencia = Convert.ToInt64 ( reader [ "serie_ocorrencia" ] );
        _dataOcorrencia = Convert.ToDateTime ( reader [ "data_Ocorrencia" ] );
        _codigoOcorrencia = reader [ "codigo_ocorrencia" ].ToString ( );
        _descricaoOcorrencia = reader [ "descricao_ocorrencia" ].ToString ( );
      }

    }

    private Int64 _serieProducaoItem;
    private Colunas _colunasDetalhe;

    public Int64 SerieProducaoItem
    {
      get
      {
        return _serieProducaoItem;
      }
      set
      {
        _serieProducaoItem = value;
      }
    }

    public Colunas ColunasDetalhe
    {
      get
      {
        return _colunasDetalhe;
      }
      set
      {
        _colunasDetalhe = value;
      }
    }


    private void DetalheVazio ( )
    {
      _serieProducaoItem = 0L;
      _colunasDetalhe = new Colunas ( );
    }

    public Detalhe ( )
    {
      DetalheVazio ( );
    }

    public Detalhe ( Int64 serieProducaoOcorrencia, Int64 serieProducaoItem, Int64 serieOcorrencia, DateTime dataOcorrencia, String codigoOcorrencia, String descricaoOcorrencia )
    {
      _serieProducaoItem = serieProducaoItem;
      _colunasDetalhe = new Colunas ( serieProducaoOcorrencia, serieProducaoItem, serieOcorrencia, dataOcorrencia, codigoOcorrencia, descricaoOcorrencia );
    }

    public Detalhe ( SqlDataReader reader )
    {
      try
      {
        _serieProducaoItem = Convert.ToInt64 ( ( reader [ "serie_producao_item" ] ) );
        _colunasDetalhe = new Colunas ( reader );
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
//aqui        view = ( _activityMestre.LayoutInflater.Inflate ( Resource.Layout.ItemOcorrencia, parent, false ) ) as LinearLayout;
      }
      if ( view != null )
      {
        //aqui
        //var dataOcorrencia = view.FindViewById ( Resource.Id.txtDataOcorrencia ) as TextView;
        //if ( dataOcorrencia != null )
        //{
        //  dataOcorrencia.SetText ( detalhe.ColunasDetalhe.DataOcorrencia.ToString ( "G" ).Trim ( ), TextView.BufferType.Normal );
        //}
        //var codigoOcorrencia = view.FindViewById ( Resource.Id.txtCodigoOcorrencia ) as TextView;
        //if ( codigoOcorrencia != null )
        //{
        //  codigoOcorrencia.SetText ( detalhe.ColunasDetalhe.CodigoOcorrencia.Trim ( ), TextView.BufferType.Normal );
        //}
        //var descricaoOcorrencia = view.FindViewById ( Resource.Id.txtDescricaoOcorrencia ) as TextView;
        //if ( descricaoOcorrencia != null )
        //{
        //  descricaoOcorrencia.SetText ( detalhe.ColunasDetalhe.DescricaoOcorrencia.Trim ( ), TextView.BufferType.Normal );
        //}
      }
      return view;
    }

    public Detalhe GetItemAtPosition ( int position )
    {
      return _detalhes [ position ];
    }
  }
}