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
  using Android;

  public class Detalhe
  {

    public class Colunas
    {
      private Int64 _serieProducaoObservacao;
      private Int64 _serieProducao;
      private Int64 _serieFuncionario;
      private DateTime _dataObservacao;
      private String _textoObservacao;

      public Int64 SerieProducaoObservacao
      {
        get
        {
          return _serieProducaoObservacao;
        }
        set
        {
          _serieProducaoObservacao = value;
        }
      }

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

      public DateTime DataObservacao
      {
        get
        {
          return _dataObservacao;
        }
        set
        {
          _dataObservacao = value;
        }
      }

      public String TextoObservacao
      {
        get
        {
          return _textoObservacao;
        }
        set
        {
          _textoObservacao = value;
        }
      }

      public Colunas ( )
      {
        _serieProducaoObservacao = 0L;
        _serieProducao = 0L;
        _serieFuncionario = 0L;
        _dataObservacao = DateTime.MinValue;
        _textoObservacao = String.Empty;
      }

      public Colunas ( Int64 serieProducaoObservacao, Int64 serieProducao, Int64 serieFuncionario, DateTime dataObservacao, String textoObservacao )
      {
        _serieProducaoObservacao = serieProducaoObservacao;
        _serieProducao = serieProducao;
        _serieFuncionario = serieFuncionario;
        _dataObservacao = dataObservacao;
        _textoObservacao = textoObservacao;
      }

      public Colunas ( SqlDataReader reader )
      {
        _serieProducaoObservacao = Convert.ToInt64 ( reader [ "serie_producao_observacao" ] );
        _serieProducao = Convert.ToInt64 ( reader [ "serie_producao" ] );
        _serieFuncionario = Convert.ToInt64 ( reader [ "serie_funcionario" ] );
        _dataObservacao = Convert.ToDateTime ( reader [ "data_observacao" ] );
        _textoObservacao = reader [ "texto_observacao" ].ToString ( );
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
      _colunasDetalhe = new Colunas ( );
    }

    public Detalhe ( )
    {
      DetalheVazio ( );
    }

    public Detalhe ( Int64 serieProducaoObservacao, Int64 serieProducao, Int64 serieFuncionario, String textoObservacao, DateTime dataObservacao )
    {
      _serieProducao = serieProducao;
      _colunasDetalhe = new Colunas ( serieProducaoObservacao, serieProducao, serieFuncionario, dataObservacao, textoObservacao );
    }

    public Detalhe ( SqlDataReader reader )
    {
      try
      {
        _serieProducao = Convert.ToInt64 ( ( reader [ "serie_producao" ] ) );
        _colunasDetalhe = new Colunas ( reader );
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
//aqui        view = ( _activityMestre.LayoutInflater.Inflate ( Resource.Layout.ProducaoObservacao, parent, false ) ) as LinearLayout;
      }
      if ( view != null )
      {
        //aqui
        //var textoObservacao = view.FindViewById ( Resource.Id.txtObservacao ) as TextView;
        //if ( textoObservacao != null )
        //{
        //  textoObservacao.SetText ( detalhe.ColunasDetalhe.TextoObservacao.Trim ( ), TextView.BufferType.Normal );
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