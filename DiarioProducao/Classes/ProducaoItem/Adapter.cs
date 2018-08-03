using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Android.App;
using Android.Views;
using Android.Widget;
using DiarioProducao.Classes.Comum;

namespace DiarioProducao.Classes.ProducaoItem
{
  using Android;

  public class Detalhe
  {

    public class Colunas
    {
      private Int64 _serieProducaoItem;
      private Int64 _serieProducao;
      private Int64 _serieItemEmpresa;
      private String _codigoItem;
      private String _descricaoItem;
      private DateTime _horaInicio;
      private DateTime _horaFim;
      private Int64 _quantidadeInformada;

      public Int64 SerieProducaoItem
      {
        get { return _serieProducaoItem; }
        set { _serieProducaoItem = value; }
      }

      public Int64 SerieProducao
      {
        get { return _serieProducao; }
        set { _serieProducao = value; }
      }

      public Int64 SerieItemEmpresa
      {
        get { return _serieItemEmpresa; }
        set { _serieItemEmpresa = value; }
      }

      public String CodigoItem
      {
        get { return _codigoItem; }
        set { _codigoItem = value; }
      }

      public String DescricaoItem
      {
        get { return _descricaoItem; }
        set { _descricaoItem = value; }
      }

      public DateTime HoraInicio
      {
        get { return _horaInicio; }
        set { _horaInicio = value; }
      }

      public DateTime HoraFim
      {
        get { return _horaFim; }
        set { _horaFim = value; }
      }

      public Int64 QuantidadeInformada
      {
        get { return _quantidadeInformada; }
        set { _quantidadeInformada = value; }
      }
      
    
      public Colunas ( )
      {
        _serieProducaoItem = 0L;
        _serieProducao = 0L;
        _serieItemEmpresa = 0L;
        _codigoItem = String.Empty;
        _descricaoItem = String.Empty;
        _horaInicio = DateTime.MinValue;
        _horaFim = DateTime.MinValue;
        _quantidadeInformada = 0L;
      }

      public Colunas ( Int64 serieProducaoItem, Int64 serieProducao, Int64 serieItemEmpresa, String codigoItem, String descricaoItem, DateTime horaInicio, DateTime horaFim, Int64 quantidadeInformada )
      {
        _serieProducaoItem = serieProducaoItem;
        _serieProducao = serieProducao;
        _serieItemEmpresa = serieItemEmpresa;
        _codigoItem = codigoItem;
        _descricaoItem = descricaoItem;
        _horaInicio = horaInicio;
        _horaFim = horaFim;
        _quantidadeInformada = quantidadeInformada;
      }

      public Colunas ( SqlDataReader reader )
      {
        _serieProducaoItem = Convert.ToInt64( reader [ "serie_producao_item" ] );
        _serieProducao = Convert.ToInt64 ( reader [ "serie_producao" ] );
        _serieItemEmpresa = Convert.ToInt64  ( reader [ "serie_item_empresa" ] );
        _codigoItem = reader["codigo_item"].ToString();
        _descricaoItem = reader["descricao_item"].ToString();
        try
        {
          _horaInicio = Convert.ToDateTime(reader["hora_inicio"]);
        }
        catch (Exception e)
        {
          _horaInicio = DateTime.MinValue;
        }
        _horaFim = Convert.ToDateTime(reader["hora_fim"]);
        _quantidadeInformada = Convert.ToInt64(reader["quantidade_informada"]);
      }

    }

    private Int64 _serieProducao;
    private Colunas _colunasDetalhe;

    public Int64 SerieProducao
    {
      get
      {
        return _serieProducao;
      }
      set
      {
        _serieProducao = value;
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
      _serieProducao = 0L;
      _colunasDetalhe = new Colunas();
    }

    public Detalhe ( )
    {
      DetalheVazio ( );
    }

    public Detalhe ( Int64 serieProducaoItem, Int64 serieProducao, Int64 serieItemEmpresa, String codigoItem, String descricaoItem, DateTime horaInicio, DateTime horaFim, Int64 quantidadeInformada )
    {
      _serieProducao = serieProducao;
      _colunasDetalhe = new Colunas ( serieProducaoItem, serieProducao, serieItemEmpresa, codigoItem, descricaoItem, horaInicio, horaFim, quantidadeInformada );
    }

    public Detalhe ( SqlDataReader reader )
    {
      try
      {
        _serieProducao = Convert.ToInt64 ( ( reader [ "serie_producao" ] ) );
        _colunasDetalhe = new Colunas ( reader );
      }
      catch ( Exception e )
      {
        DetalheVazio ( );
      }
    }

  }

  public class AdapterProducaoItem : BaseAdapter
  {
    private readonly Activity _activityMestre;
    private readonly List<Detalhe> _detalhes;

    public AdapterProducaoItem ( Activity activityMestre, AcessoSql acessoSql, String filtro )
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
        _detalhes.Add ( new Detalhe {ColunasDetalhe = {DescricaoItem = "Sem dados para exibir"}} );
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
//aqui        view = ( _activityMestre.LayoutInflater.Inflate ( Resource.Layout.ProducaoItem, parent, false ) ) as LinearLayout;
      }
      if ( view != null )
      {
/*aqui
        var codigoItem = view.FindViewById ( Resource.Id.txtCodigoItem ) as TextView;
        var descricaoItem = view.FindViewById ( Resource.Id.txtDescricaoItem ) as TextView;
        var horaInicio = view.FindViewById ( Resource.Id.txtHoraInicio ) as TextView;
        var horaFim = view.FindViewById ( Resource.Id.txtHoraFim ) as TextView;
        var quantidadeInformada = view.FindViewById ( Resource.Id.txtQuantidadeInformada ) as TextView;
        if ( codigoItem != null )
        {
          codigoItem.SetText ( detalhe.ColunasDetalhe.CodigoItem.Trim ( ), TextView.BufferType.Normal );
        }
        if ( descricaoItem != null )
        {
          descricaoItem.SetText ( detalhe.ColunasDetalhe.DescricaoItem.Trim ( ), TextView.BufferType.Normal );
        }
        if ( horaInicio != null )
        {
          horaInicio.SetText ( detalhe.ColunasDetalhe.HoraInicio.ToString("t").Trim ( ), TextView.BufferType.Normal );
        }
        if ( horaFim != null )
        {
          horaFim.SetText ( detalhe.ColunasDetalhe.HoraFim.ToString ( "t" ).Trim ( ), TextView.BufferType.Normal );
        }
        if ( quantidadeInformada != null )
        {
          quantidadeInformada.SetText ( detalhe.ColunasDetalhe.QuantidadeInformada.ToString ( ).Trim ( ), TextView.BufferType.Normal );
        }
      */
      }
      return view;
    }

    public Detalhe GetItemAtPosition ( int position )
    {
      return _detalhes [ position ];
    }
  }
}