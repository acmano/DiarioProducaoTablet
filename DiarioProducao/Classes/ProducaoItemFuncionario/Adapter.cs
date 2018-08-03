using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Android.App;
using Android.Views;
using Android.Widget;
using DiarioProducao.Classes.Comum;

namespace DiarioProducao.Classes.ProducaoItemFuncionario
{
  using Android;

  public class Detalhe
  {

    public class Colunas
    {
      private Int64 _serieProducaoItemFuncionario;
      private Int64 _serieProducaoItem;
      private Int64 _serieFuncionario;
      private Int64 _matriculaFuncionario;
      private String _nomeFuncionario;

      public Int64 SerieProducaoItemFuncionario
      {
        get
        {
          return _serieProducaoItemFuncionario;
        }
        set
        {
          _serieProducaoItemFuncionario = value;
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

      public Int64 SerieFuncionario
      {
        get
        {
          return _serieFuncionario;
        }
        set
        {
          _serieFuncionario = value;
        }
      }

      public Int64 MatriculaFuncionario
      {
        get
        {
          return _matriculaFuncionario;
        }
        set
        {
          _matriculaFuncionario = value;
        }
      }

      public String NomeFuncionario
      {
        get
        {
          return _nomeFuncionario;
        }
        set
        {
          _nomeFuncionario = value;
        }
      }

      public Colunas ( )
      {
        _serieProducaoItemFuncionario = 0L;
        _serieProducaoItem = 0L;
        _serieFuncionario = 0L;
        _matriculaFuncionario = 0L;
        _nomeFuncionario = String.Empty;
      }

      public Colunas ( Int64 serieProducaoOcorrencia, Int64 serieProducaoItem, Int64 serieFuncionario, Int64 matriculaFuncionario, String nomeFuncionario )
      {
        _serieProducaoItemFuncionario = serieProducaoOcorrencia;
        _serieProducaoItem = serieProducaoItem;
        _serieFuncionario = serieFuncionario;
        _matriculaFuncionario = matriculaFuncionario;
        _nomeFuncionario = nomeFuncionario;
      }

      public Colunas ( SqlDataReader reader )
      {
        _serieProducaoItemFuncionario = Convert.ToInt64 ( reader [ "serie_producao_item_funcionario" ] );
        _serieProducaoItem = Convert.ToInt64 ( reader [ "serie_producao_item" ] );
        _serieFuncionario = Convert.ToInt64 ( reader [ "serie_funcionario" ] );
        _matriculaFuncionario = Convert.ToInt64 ( reader [ "matricula_funcionario" ] );
        _nomeFuncionario = reader [ "nome_funcionario" ].ToString ( );
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

    public Detalhe ( Int64 serieProducaoItemFuncionario, Int64 serieProducaoItem, Int64 serieFuncionario, Int64 matriculaFuncionario, String nomeFuncionario )
    {
      _serieProducaoItem = serieProducaoItem;
      _colunasDetalhe = new Colunas ( serieProducaoItemFuncionario, serieProducaoItem, serieFuncionario, matriculaFuncionario, nomeFuncionario );
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

  public class AdapterProducaoItemFuncionario : BaseAdapter
  {
    private readonly Activity _activityMestre;
    private readonly List<Detalhe> _detalhes;

    public AdapterProducaoItemFuncionario ( Activity activityMestre, AcessoSql acessoSql, String filtro )
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
              NomeFuncionario = "Sem funcionários registrados"
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
//aqui        view = ( _activityMestre.LayoutInflater.Inflate ( Resource.Layout.ItemFuncionario, parent, false ) ) as LinearLayout;
      }
      if ( view != null )
      {
        //aquivar matriculaFuncionario = view.FindViewById ( Resource.Id.txtMatriculaFuncionario ) as TextView;
        //if ( matriculaFuncionario != null )
        //{
        //  if ( detalhe.ColunasDetalhe.MatriculaFuncionario != 0L )
        //  {
        //    matriculaFuncionario.SetText ( detalhe.ColunasDetalhe.MatriculaFuncionario.ToString ( ).Trim ( ), TextView.BufferType.Normal );
        //  }
        //  else
        //  {
        //    matriculaFuncionario.SetText ( " ", TextView.BufferType.Normal );
        //  }
        //}
        //var nomeFuncionario = view.FindViewById ( Resource.Id.txtNomeFuncionario ) as TextView;
        //if ( nomeFuncionario != null )
        //{
        //  nomeFuncionario.SetText ( detalhe.ColunasDetalhe.NomeFuncionario.Trim ( ), TextView.BufferType.Normal );
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