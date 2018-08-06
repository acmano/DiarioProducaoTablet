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


  public class Detalhe
  {

    public class Colunas
    {

      public Int64 SerieProducaoItem { get; set; }
      public Int64 SerieProducao { get; set; }
      public Int64 SerieItemEmpresa { get; set; }
      public String CodigoItem { get; set; }
      public String DescricaoItem { get; set; }
      public DateTime HoraInicio { get; set; }
      public DateTime HoraFim { get; set; }
      public Int64 QuantidadeInformada { get; set; }


      public Colunas ( )
      {
        SerieProducaoItem = 0L;
        SerieProducao = 0L;
        SerieItemEmpresa = 0L;
        CodigoItem = String.Empty;
        DescricaoItem = String.Empty;
        HoraInicio = DateTime.MinValue;
        HoraFim = DateTime.MinValue;
        QuantidadeInformada = 0L;
      }

      public Colunas ( Int64 serieProducaoItem, Int64 serieProducao, Int64 serieItemEmpresa, String codigoItem, String descricaoItem, DateTime horaInicio, DateTime horaFim, Int64 quantidadeInformada )
      {
        SerieProducaoItem = serieProducaoItem;
        SerieProducao = serieProducao;
        SerieItemEmpresa = serieItemEmpresa;
        CodigoItem = codigoItem;
        DescricaoItem = descricaoItem;
        HoraInicio = horaInicio;
        HoraFim = horaFim;
        QuantidadeInformada = quantidadeInformada;
      }

      public Colunas ( SqlDataReader reader )
      {
        SerieProducaoItem = Convert.ToInt64( reader [ "serie_producao_item" ] );
        SerieProducao = Convert.ToInt64 ( reader [ "serie_producao" ] );
        SerieItemEmpresa = Convert.ToInt64  ( reader [ "serie_item_empresa" ] );
        CodigoItem = reader["codigo_item"].ToString();
        DescricaoItem = reader["descricao_item"].ToString();
        try
        {
          HoraInicio = Convert.ToDateTime(reader["hora_inicio"]);
        }
        catch (Exception )
        {
          HoraInicio = DateTime.MinValue;
        }
        HoraFim = Convert.ToDateTime(reader["hora_fim"]);
        QuantidadeInformada = Convert.ToInt64(reader["quantidade_informada"]);
      }

    }

    public Int64 SerieProducao { get; set; }
    public Colunas ColunasDetalhe { get; set; }


    private void DetalheVazio ( )
    {
      SerieProducao = 0L;
      ColunasDetalhe = new Colunas();
    }

    public Detalhe ( )
    {
      DetalheVazio ( );
    }

    public Detalhe ( Int64 serieProducaoItem, Int64 serieProducao, Int64 serieItemEmpresa, String codigoItem, String descricaoItem, DateTime horaInicio, DateTime horaFim, Int64 quantidadeInformada )
    {
      SerieProducao = serieProducao;
      ColunasDetalhe = new Colunas ( serieProducaoItem, serieProducao, serieItemEmpresa, codigoItem, descricaoItem, horaInicio, horaFim, quantidadeInformada );
    }

    public Detalhe ( SqlDataReader reader )
    {
      try
      {
        SerieProducao = Convert.ToInt64 ( ( reader [ "serie_producao" ] ) );
        ColunasDetalhe = new Colunas ( reader );
      }
      catch ( Exception  )
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
        view = ( _activityMestre.LayoutInflater.Inflate ( Resource.Layout.producaoitem, parent, false ) ) as LinearLayout;
      }
      if ( view != null )
      {
        if (view.FindViewById( Resource.Id.txtCodigoItem ) is TextView codigoItem)
        {
          codigoItem.SetText( detalhe.ColunasDetalhe.CodigoItem.Trim(), TextView.BufferType.Normal );
        }
        if (view.FindViewById( Resource.Id.txtDescricaoItem ) is TextView descricaoItem)
        {
          descricaoItem.SetText( detalhe.ColunasDetalhe.DescricaoItem.Trim(), TextView.BufferType.Normal );
        }
        if (view.FindViewById( Resource.Id.txtHoraInicio ) is TextView horaInicio)
        {
          horaInicio.SetText( detalhe.ColunasDetalhe.HoraInicio.ToString( "t" ).Trim(), TextView.BufferType.Normal );
        }
        if (view.FindViewById( Resource.Id.txtHoraFim ) is TextView horaFim)
        {
          horaFim.SetText( detalhe.ColunasDetalhe.HoraFim.ToString( "t" ).Trim(), TextView.BufferType.Normal );
        }
        if (view.FindViewById( Resource.Id.txtQuantidadeInformada ) is TextView quantidadeInformada)
        {
          quantidadeInformada.SetText( detalhe.ColunasDetalhe.QuantidadeInformada.ToString().Trim(), TextView.BufferType.Normal );
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