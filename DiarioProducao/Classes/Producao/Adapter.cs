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
  using Android;

  public class Detalhe
  {

    public class Colunas
    {
      private String _datProducao;
      private Int64 _numSerieLmtrn;
      private String _turnoDescricao;
      private Int64 _numSerieLmlm;
      private String _linhaDescricao;

      public string DatProducao
      {
        get { return _datProducao; }
        set { _datProducao = value; }
      }

      public Int64 NumSerieLmtrn
      {
        get
        {
          return _numSerieLmtrn;
        }
        set
        {
          _numSerieLmtrn = value;
        }
      }

      public string TurnoDescricao
      {
        get
        {
          return (_turnoDescricao );
        }
        set { _turnoDescricao = value; }
      }

      public Int64 NumSerieLmlm
      {
        get
        {
          return _numSerieLmlm;
        }
        set
        {
          _numSerieLmlm = value;
        }
      }

      public string LinhaDescricao
      {
        get { return _linhaDescricao; }
        set { _linhaDescricao = value; }
      }

      public Colunas()
      {
        _datProducao = String.Empty;
        _numSerieLmtrn = 0L;
        _turnoDescricao = String.Empty;
        _numSerieLmlm = 0L;
        _linhaDescricao = String.Empty;
      }

      public Colunas(String datProducao, Int64 numSerieLmtrn, String turnoDescricao, Int64 numSerieLmlm, String linhaDescricao)
      {
        _datProducao = datProducao;
        _numSerieLmtrn = numSerieLmtrn;
        _turnoDescricao = turnoDescricao;
        _numSerieLmlm = numSerieLmlm;
        _linhaDescricao = linhaDescricao;
      }

      public Colunas ( SqlDataReader reader )
      {
        _datProducao = reader [ "data_producao" ].ToString ( );
        _numSerieLmtrn = Convert.ToInt64(reader["turno_serie"]);
        _turnoDescricao = reader [ "turno_descricao" ].ToString ( );
        _numSerieLmlm = Convert.ToInt64(reader["linha_serie"]);
        _linhaDescricao = reader [ "linha_descricao" ].ToString ( );
      }

    }
    
    private Int64 _numSerieProducao;
    private Colunas _colunasDetalhe;

    public long NumSerieProducao
    {
      get { return _numSerieProducao; }
      set { _numSerieProducao = value; }
    }

    public Colunas ColunasDetalhe
    {
      get { return _colunasDetalhe; }
      set { _colunasDetalhe = value; }
    }


    private void DetalheVazio ( )
    {
      _numSerieProducao = 0L;
    }

    public Detalhe()
    {
      _colunasDetalhe = new Colunas();
      DetalheVazio();
    }

    public Detalhe(Int64 numSerieProducao, String datProducao, Int64 numSerieLmtrn, String turnoDescricao, Int64 numSerieLmlm, String linhaDescricao)
    {
      _numSerieProducao = numSerieProducao;
      _colunasDetalhe = new Colunas( datProducao, numSerieLmtrn, turnoDescricao, numSerieLmlm, linhaDescricao);
    }

    public Detalhe(SqlDataReader reader)
    {
      try
      {
        _numSerieProducao = Convert.ToInt64((reader["serie_producao"]));
        _colunasDetalhe = new Colunas(reader);
      }
      catch ( Exception )
      {
        DetalheVazio();
      }
    }

  }

  public class AdapterProducao : BaseAdapter
  {
    private readonly Activity _activityMestre;
    private readonly List<Detalhe> _detalhes;

    public AdapterProducao ( Activity activityMestre, AcessoSql acessoSql, String codEmpresa, String filtro )
    {
      _activityMestre = activityMestre;
      var query = new StringBuilder ( );
      query.AppendFormat ( Sql.QueryAdapter, acessoSql.OpenQuery, codEmpresa, filtro );
      var banco = new Msde ( acessoSql );
      banco.Open ( );
      var reader = banco.DataReader ( query.ToString ( ) );
      _detalhes = new List<Detalhe> ( );
      while ( reader.Read ( ) )
      {
        var detalhe = new Detalhe(reader);
        _detalhes.Add ( detalhe );
      }
      if ( _detalhes.Count <= 0 )
      {
        _detalhes.Add ( new Detalhe { ColunasDetalhe = { TurnoDescricao = "Sem dados para exibir" } } );
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
//aqui        view = ( _activityMestre.LayoutInflater.Inflate ( Resource.Layout.DiarioProducao, parent, false ) ) as LinearLayout;
      }
      if ( view != null )
      {
        //aquivar dataProducao = view.FindViewById ( Resource.Id.txtDatProducao ) as TextView;
        //var linhaDescricao = view.FindViewById ( Resource.Id.txtLinhaDescricao ) as TextView;
        //var turnoDescricao = view.FindViewById ( Resource.Id.txtTurnoDescricao ) as TextView;
        //if ( dataProducao != null )
        //{
        //  dataProducao.SetText ( detalhe.ColunasDetalhe.DatProducao.Trim ( ), TextView.BufferType.Normal );
        //}
        //if ( linhaDescricao != null )
        //{
        //  linhaDescricao.SetText ( detalhe.ColunasDetalhe.LinhaDescricao.Trim ( ), TextView.BufferType.Normal );
        //}
        //if ( turnoDescricao != null )
        //{
        //  turnoDescricao.SetText ( detalhe.ColunasDetalhe.TurnoDescricao.Trim ( ), TextView.BufferType.Normal );
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