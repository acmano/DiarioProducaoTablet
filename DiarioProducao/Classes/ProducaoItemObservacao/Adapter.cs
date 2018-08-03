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
  using Android;

  public class Detalhe
  {

    public class Colunas
    {
      private Int64 _serieProducaoItemObservacao;
      private Int64 _serieProducaoItem;
      private Int64 _serieFuncionario;
      private Int64 _matriculaFuncionario;
      private String _nomeFuncionario;
      private DateTime _dataObservacao;
      private String _textoObservacao;

      public Int64 SerieProducaoItemObservacao
      {
        get
        {
          return _serieProducaoItemObservacao;
        }
        set
        {
          _serieProducaoItemObservacao = value;
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
        _serieProducaoItemObservacao = 0L;
        _serieProducaoItem = 0L;
        _serieFuncionario = 0L;
        _matriculaFuncionario = 0L;
        _nomeFuncionario = String.Empty;
        _dataObservacao = DateTime.MinValue;
        _textoObservacao = String.Empty;
      }

      public Colunas ( Int64 serieProducaoItemObservacao, Int64 serieProducaoItem, Int64 serieFuncionario, Int64 matriculaFuncionario, String nomeFuncionario, DateTime dataObservacao, String textoObservacao )
      {
        _serieProducaoItemObservacao = serieProducaoItemObservacao;
        _serieProducaoItem = serieProducaoItem;
        _serieFuncionario = serieFuncionario;
        _matriculaFuncionario = matriculaFuncionario;
        _nomeFuncionario = nomeFuncionario;
        _dataObservacao = dataObservacao;
        _textoObservacao = textoObservacao;
      }

      public Colunas ( SqlDataReader reader )
      {
        _serieProducaoItemObservacao = Convert.ToInt64 ( reader [ "serie_producao_item_observacao" ] );
        _serieProducaoItem = Convert.ToInt64 ( reader [ "serie_producao_item" ] );
        _serieFuncionario = Convert.ToInt64 ( reader [ "serie_funcionario" ] );
        _matriculaFuncionario = Convert.ToInt64 ( reader [ "matricula_funcionario" ] );
        _nomeFuncionario = reader [ "nome_funcionario" ].ToString() ;
        _dataObservacao = Convert.ToDateTime ( reader [ "data_observacao" ] );
        _textoObservacao = reader [ "texto_observacao" ].ToString ( );
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

    public Detalhe ( Int64 serieProducaoObservacao, Int64 serieProducaoItem, Int64 serieFuncionario, Int64 matriculaFuncionario, String nomeFuncionario, String textoObservacao, DateTime dataObservacao )
    {
      _serieProducaoItem = serieProducaoItem;
      _colunasDetalhe = new Colunas ( serieProducaoObservacao, serieProducaoItem, serieFuncionario, matriculaFuncionario, nomeFuncionario, dataObservacao, textoObservacao );
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
//aqui        view = ( _activityMestre.LayoutInflater.Inflate ( Resource.Layout.ItemObservacao, parent, false ) ) as LinearLayout;
      }
      if ( view != null )
      {
        //aquivar textoObservacao = view.FindViewById ( Resource.Id.txtObservacao ) as TextView;
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