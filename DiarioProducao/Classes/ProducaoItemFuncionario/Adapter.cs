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


  public class Detalhe
  {

    public class Colunas
    {

      public Int64 SerieProducaoItemFuncionario { get; set; }
      public Int64 SerieProducaoItem { get; set; }
      public Int64 SerieFuncionario { get; set; }
      public Int64 MatriculaFuncionario { get; set; }
      public String NomeFuncionario { get; set; }

      public Colunas ( )
      {
        SerieProducaoItemFuncionario = 0L;
        SerieProducaoItem = 0L;
        SerieFuncionario = 0L;
        MatriculaFuncionario = 0L;
        NomeFuncionario = String.Empty;
      }

      public Colunas ( Int64 serieProducaoOcorrencia, Int64 serieProducaoItem, Int64 serieFuncionario, Int64 matriculaFuncionario, String nomeFuncionario )
      {
        SerieProducaoItemFuncionario = serieProducaoOcorrencia;
        SerieProducaoItem = serieProducaoItem;
        SerieFuncionario = serieFuncionario;
        MatriculaFuncionario = matriculaFuncionario;
        NomeFuncionario = nomeFuncionario;
      }

      public Colunas ( SqlDataReader reader )
      {
        SerieProducaoItemFuncionario = Convert.ToInt64 ( reader [ "serie_producao_item_funcionario" ] );
        SerieProducaoItem = Convert.ToInt64 ( reader [ "serie_producao_item" ] );
        SerieFuncionario = Convert.ToInt64 ( reader [ "serie_funcionario" ] );
        MatriculaFuncionario = Convert.ToInt64 ( reader [ "matricula_funcionario" ] );
        NomeFuncionario = reader [ "nome_funcionario" ].ToString ( );
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

    public Detalhe ( Int64 serieProducaoItemFuncionario, Int64 serieProducaoItem, Int64 serieFuncionario, Int64 matriculaFuncionario, String nomeFuncionario )
    {
      SerieProducaoItem = serieProducaoItem;
      ColunasDetalhe = new Colunas ( serieProducaoItemFuncionario, serieProducaoItem, serieFuncionario, matriculaFuncionario, nomeFuncionario );
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
        view = ( _activityMestre.LayoutInflater.Inflate ( Resource.Layout.itemfuncionario, parent, false ) ) as LinearLayout;
      }
      if ( view != null )
      {
        if (view.FindViewById( Resource.Id.txtMatriculaFuncionario ) is TextView matriculaFuncionario)
        {
          if (detalhe.ColunasDetalhe.MatriculaFuncionario != 0L)
          {
            matriculaFuncionario.SetText( detalhe.ColunasDetalhe.MatriculaFuncionario.ToString().Trim(), TextView.BufferType.Normal );
          }
          else
          {
            matriculaFuncionario.SetText( " ", TextView.BufferType.Normal );
          }
        }
        if (view.FindViewById( Resource.Id.txtNomeFuncionario ) is TextView nomeFuncionario)
        {
          nomeFuncionario.SetText( detalhe.ColunasDetalhe.NomeFuncionario.Trim(), TextView.BufferType.Normal );
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