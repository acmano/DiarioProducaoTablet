using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Android.App;
using Android.Views;
using Android.Widget;
using DiarioProducao.Classes.Comum;


namespace DiarioProducao.Classes.Producao
{
  public class Detalhe
  {

    public class Colunas
    {
      private String _turnoDescricao;
      public string DatProducao { get; set; }
      public Int64 NumSerieLmtrn { get; set; }

      public string TurnoDescricao
      {
        get
        {
          return ( _turnoDescricao );
        }
        set
        { _turnoDescricao = value; }
      }

      public Int64 NumSerieLmlm { get; set; }

      public string LinhaDescricao { get; set; }

      public Colunas()
      {
        DatProducao = String.Empty;
        NumSerieLmtrn = 0L;
        _turnoDescricao = String.Empty;
        NumSerieLmlm = 0L;
        LinhaDescricao = String.Empty;
      }

      public Colunas(String datProducao, Int64 numSerieLmtrn, String turnoDescricao, Int64 numSerieLmlm, String linhaDescricao)
      {
        DatProducao = datProducao;
        NumSerieLmtrn = numSerieLmtrn;
        _turnoDescricao = turnoDescricao;
        NumSerieLmlm = numSerieLmlm;
        LinhaDescricao = linhaDescricao;
      }

      public Colunas(SqlDataReader reader)
      {
        DatProducao = reader[ "data_producao" ].ToString();
        NumSerieLmtrn = Convert.ToInt64( reader[ "turno_serie" ] );
        _turnoDescricao = reader[ "turno_descricao" ].ToString();
        NumSerieLmlm = Convert.ToInt64( reader[ "linha_serie" ] );
        LinhaDescricao = reader[ "linha_descricao" ].ToString();
      }

    }

    public long NumSerieProducao { get; set; }

    public Colunas ColunasDetalhe { get; set; }


    private void DetalheVazio()
    {
      NumSerieProducao = 0L;
    }

    public Detalhe()
    {
      ColunasDetalhe = new Colunas();
      DetalheVazio();
    }

    public Detalhe(Int64 numSerieProducao, String datProducao, Int64 numSerieLmtrn, String turnoDescricao, Int64 numSerieLmlm, String linhaDescricao)
    {
      NumSerieProducao = numSerieProducao;
      ColunasDetalhe = new Colunas( datProducao, numSerieLmtrn, turnoDescricao, numSerieLmlm, linhaDescricao );
    }

    public Detalhe(SqlDataReader reader)
    {
      try
      {
        NumSerieProducao = Convert.ToInt64( ( reader[ "serie_producao" ] ) );
        ColunasDetalhe = new Colunas( reader );
      }
      catch (Exception)
      {
        DetalheVazio();
      }
    }

  }

  public class AdapterProducao : BaseAdapter
  {
    private readonly Activity _activityMestre;
    private readonly List<Detalhe> _detalhes;

    public AdapterProducao(Activity activityMestre, AcessoSql acessoSql, String codEmpresa, String filtro)
    {
      _activityMestre = activityMestre;
      var query = new StringBuilder();
      query.AppendFormat( Sql.QueryAdapter, acessoSql.OpenQuery, codEmpresa, filtro );
      var banco = new Msde( acessoSql );
      banco.Open();
      var reader = banco.DataReader( query.ToString() );
      _detalhes = new List<Detalhe>();
      while (reader.Read())
      {
        var detalhe = new Detalhe( reader );
        _detalhes.Add( detalhe );
      }
      if (_detalhes.Count <= 0)
      {
        _detalhes.Add( new Detalhe { ColunasDetalhe = { TurnoDescricao = "Sem dados para exibir" } } );
      }
      reader.Close();
      reader.Dispose();
      banco.Close();
    }

    public override int Count
    {
      get
      {
        return _detalhes.Count;
      }
    }

    public override Java.Lang.Object GetItem(int position)
    {
      return position;
    }

    public override long GetItemId(int position)
    {
      return position;
    }

    public override View GetView(int position, View convertView, ViewGroup parent)
    {
      var detalhe = _detalhes[ position ];
      var view = convertView;
      if (convertView == null)
      {
        view = ( _activityMestre.LayoutInflater.Inflate( Resource.Layout.diarioproducao, parent, false ) ) as LinearLayout;
      }
      if (view != null)
      {
        if (view.FindViewById( Resource.Id.txtDatProducao ) is TextView dataProducao)
        {
          dataProducao.SetText( detalhe.ColunasDetalhe.DatProducao.Trim(), TextView.BufferType.Normal );
        }
        if (view.FindViewById( Resource.Id.txtLinhaDescricao ) is TextView linhaDescricao)
        {
          linhaDescricao.SetText( detalhe.ColunasDetalhe.LinhaDescricao.Trim(), TextView.BufferType.Normal );
        }
        if (view.FindViewById( Resource.Id.txtTurnoDescricao ) is TextView turnoDescricao)
        {
          turnoDescricao.SetText( detalhe.ColunasDetalhe.TurnoDescricao.Trim(), TextView.BufferType.Normal );
        }
      }
      return view;
    }

    public Detalhe GetItemAtPosition(int position)
    {
      return _detalhes[ position ];
    }
  }
}