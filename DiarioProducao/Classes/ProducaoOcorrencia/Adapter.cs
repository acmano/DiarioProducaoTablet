using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Android.App;
using Android.Views;
using Android.Widget;
using DiarioProducao.Classes.Comum;

namespace DiarioProducao.Classes.ProducaoOcorrencia
{
  using Android;

  public class Detalhe
  {

    public class Colunas
    {
      private Int64 _serieProducaoOcorrencia;
      private Int64 _serieProducao;
      private Int64 _serieOcorrencia;
      private Int64 _serieFuncionario;
      private String _codigoOcorrencia;
      private String _descricaoOcorrencia;
      private Int64 _matriculaFuncionario;
      private String _nomeFuncionario;

      private DateTime _dataOcorrencia;

      public Int64 SerieProducaoOcorrencia
      {
        get
        {
          return _serieProducaoOcorrencia;
        }
        set
        {
          _serieProducaoOcorrencia = value;
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

      public String CodigoOcorrencia
      {
        get { return _codigoOcorrencia; }
        set { _codigoOcorrencia = value; }
      }

      public String DescricaoOcorrencia
      {
        get { return _descricaoOcorrencia; }
        set { _descricaoOcorrencia = value; }
      }

      public Int64 MatriculaFuncionario
      {
        get { return _matriculaFuncionario; }
        set { _matriculaFuncionario = value; }
      }

      public String NomeFuncionario
      {
        get { return _nomeFuncionario; }
        set { _nomeFuncionario = value; }
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

      public Colunas ( )
      {
        _serieProducaoOcorrencia = 0L;
        _serieProducao = 0L;
        _serieOcorrencia = 0L;
        _serieFuncionario = 0L;
        _codigoOcorrencia = String.Empty;
        _descricaoOcorrencia = String.Empty;
        _matriculaFuncionario = 0L;
        _nomeFuncionario = String.Empty;
        _dataOcorrencia = DateTime.MinValue;
      }

      public Colunas ( Int64 serieProducaoOcorrencia, Int64 serieProducao, Int64 serieOcorrencia, Int64 serieFuncionario, String codigoOcorrencia, String descricaoOcorrencia, Int64 matriculaFuncionario, String nomeFuncionario, DateTime dataOcorrencia )
      {
        _serieProducaoOcorrencia = serieProducaoOcorrencia;
        _serieProducao = serieProducao;
        _serieOcorrencia = serieOcorrencia;
        _serieFuncionario = serieFuncionario;
        _codigoOcorrencia = codigoOcorrencia;
        _descricaoOcorrencia = descricaoOcorrencia;
        _matriculaFuncionario = matriculaFuncionario;
        _nomeFuncionario = nomeFuncionario;
        _dataOcorrencia = dataOcorrencia;
      }

      public Colunas ( SqlDataReader reader )
      {
        _serieProducaoOcorrencia = Convert.ToInt64 ( reader [ "serie_producao_ocorrencia" ] );
        _serieProducao = Convert.ToInt64 ( reader [ "serie_producao" ] );
        _serieOcorrencia = Convert.ToInt64 ( reader [ "serie_ocorrencia" ] );
        _serieFuncionario = Convert.ToInt64 ( reader [ "serie_funcionario" ] );
        _codigoOcorrencia = reader["codigo_ocorrencia"].ToString();
        _descricaoOcorrencia = reader["descricao_ocorrencia"].ToString();
        _matriculaFuncionario = Convert.ToInt64(reader["matricula_funcionario"]);
        _nomeFuncionario = reader["nome_funcionario"].ToString();
        _dataOcorrencia = Convert.ToDateTime ( reader [ "data_Ocorrencia" ] );
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

    public Detalhe ( Int64 serieProducaoOcorrencia, Int64 serieProducao, Int64 serieocorrencia, Int64 serieFuncionario, String codigoOcorrencia, String descricaoOcorrencia, Int64 matriculaFuncionario, String nomeFuncionario,  DateTime dataOcorrencia )
    {
      _serieProducao = serieProducao;
      _colunasDetalhe = new Colunas ( serieProducaoOcorrencia, serieProducao, serieocorrencia, serieFuncionario, codigoOcorrencia, descricaoOcorrencia, matriculaFuncionario, nomeFuncionario, dataOcorrencia );
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

  public class AdapterProducaoOcorrencia : BaseAdapter
  {
    private readonly Activity _activityMestre;
    private readonly List<Detalhe> _detalhes;

    public AdapterProducaoOcorrencia ( Activity activityMestre, AcessoSql acessoSql, String filtro )
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
        _detalhes.Add ( new Detalhe
        {
          ColunasDetalhe =
          {
            DescricaoOcorrencia = "Sem dados para exibir"
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
//        view = ( _activityMestre.LayoutInflater.Inflate ( Resource.Layout.ProducaoOcorrencia , parent, false ) ) as LinearLayout;
      }
      if ( view != null )
      {
        //aqui
        //var dataOcorrencia = view.FindViewById ( Resource.Id.txtDataOcorrencia ) as TextView;
        //if ( dataOcorrencia != null )
        //{
        //  dataOcorrencia.SetText ( detalhe.ColunasDetalhe.DataOcorrencia.ToString("G").Trim ( ), TextView.BufferType.Normal );
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