using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DiarioProducao.Classes.Comum;
using Lorenzetti.DB;

namespace DiarioProducao.Classes.ProducaoResponsavel
{
  public class Classe
  {
    public class Pk
    {

      public Int64 NumSerieLmpresp { get; set; }

      public Boolean Ok { get; }

      public Pk ( )
      {
        NumSerieLmpresp = 0L;
        Ok = false;
      }

      public Pk ( Int64 numSerieLmpresp )
      {
        if ( numSerieLmpresp != Int64.MinValue && numSerieLmpresp != 0L )
        {
          Ok = true;
          NumSerieLmpresp = numSerieLmpresp;
        }
      }

    }

    public class Ak
    {

      public Int64 NumSerieLmp { get; set; }

      public Boolean Ok { get; }

      public Ak ( )
      {
        NumSerieLmp = 0L;
        Ok = false;
      }

      public Ak ( Int64 numSerieLmp )
      {
        if ( numSerieLmp != 0L )
        {
          Ok = true;
          NumSerieLmp = numSerieLmp;
        }
      }

    }

    private readonly Config    _bcoSql;
    private readonly AcessoSql _acessoSql;

    public Tabela Tabela { get; set; }

    public Pk ChavePrimaria { get; set; }

    public Ak ChaveAlternativa { get; set; }

    public Boolean Ok { get; set; }

    public List<Coluna> Colunas { get; set; }

    public Int64 NumSerieLmpresp
    {
      get
      {
        return Convert.ToInt64 ( GetColumnValue ( "serie_producao_responsavel" ) );
      }
      set
      {
        SetColumnValue ( "serie_producao_responsavel", value );
      }
    }

    public Int64 NumSerieLmp
    {
      get
      {
        return Convert.ToInt64 ( GetColumnValue ( "serie_producao" ) );
      }
      set
      {
        SetColumnValue ( "serie_producao", value );
      }
    }

    public Int64 NumSerieLmf
    {
      get
      {
        return Convert.ToInt64 ( GetColumnValue ( "serie_funcionario" ) );
      }
      set
      {
        SetColumnValue ( "serie_funcionario", value );
      }
    }

    public Classe ( )
    {
      Ok = true;
      _bcoSql = null;
      _acessoSql = null;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( );
      ProducaoResponsavelComum ( );
    }

    public Classe ( Config bcoSql, AcessoSql acessoSql )
    {
      Ok = true;
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( );
      ProducaoResponsavelComum ( );
    }

    public Classe ( Config bcoSql, AcessoSql acessoSql, Pk chavePrimaria )
    {
      Ok = true;
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      ChavePrimaria = chavePrimaria;
      ChaveAlternativa = new Ak ( );
      ProducaoResponsavelComum ( );
    }

    public Classe ( Config bcoSql, AcessoSql acessoSql, Ak chaveAlternativa )
    {
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = chaveAlternativa;
      ProducaoResponsavelComum ( );
    }

    private void ProducaoResponsavelComum ( )
    {
      Ok = false;
      CriaColunas ( );
      if ( ChavePrimaria.Ok )
      {
        Select ( ChavePrimaria );
      }
      else
      {
        if ( ChaveAlternativa.Ok )
        {
          Select ( ChaveAlternativa );
        }
      }
    }

    private void CriaColunas ( )
    {
      Colunas = new List<Coluna>
      {
        new Coluna( 0, "serie_producao_responsavel", "Série", typeof( Int64 ), true, false, false ),
        new Coluna( 1, "serie_producao", "Série Produção", typeof( Int64 ), false, true, false ),
        new Coluna( 2, "serie_funcionario", "Série Funcionário", typeof( Int64 ), false, false, false )
      };
    }

    private void SetColumnValue ( String columnName, Object value )
    {
      Colunas.Find ( item => item.ColumnName == columnName ).Value = value;
    }

    private Object GetColumnValue ( String columnName )
    {
      return Colunas.Find ( item => item.ColumnName == columnName ).Value;
    }

    private void PopulaRecord ( SqlDataReader dataReader = null )
    {
      if ( dataReader != null )
      {
        SetColumnValue ( "serie_producao_responsavel", Convert.ToInt64 ( dataReader [ "serie_producao_responsavel" ] ) );
        SetColumnValue ( "serie_producao", Convert.ToInt64 ( dataReader [ "serie_producao" ] ) );
        SetColumnValue ( "serie_funcionario", Convert.ToInt64 ( dataReader [ "serie_funcionario" ] ) );
      }
      else
      {
        SetColumnValue ( "serie_producao_responsavel", 0L );
        SetColumnValue ( "serie_producao", 0L );
        SetColumnValue ( "serie_funcionario", 0L );
      }
      ChavePrimaria = new Pk ( NumSerieLmpresp );
      ChaveAlternativa = new Ak ( NumSerieLmp );
    }

    private void Select ( Pk chavePrimaria )
    {
      if ( chavePrimaria.Ok )
      {
        var sql = new StringBuilder ( );
        sql.AppendFormat ( Sql.QueryRecordPk, _acessoSql.OpenQuery, chavePrimaria.NumSerieLmpresp );
        SelectComum ( sql.ToString ( ) );
      }
      else
      {
        PopulaRecord ( );
      }
    }

    private void Select ( Ak chaveAlternativa )
    {
      if ( chaveAlternativa.Ok )
      {
        var sql = new StringBuilder ( );
        sql.AppendFormat
        (
          Sql.QueryRecordAk
          , _acessoSql.OpenQuery
          , chaveAlternativa.NumSerieLmp
        );
        SelectComum ( sql.ToString ( ) );
      }
      else
      {
        PopulaRecord ( );
      }
    }

    private void SelectComum ( String sql )
    {
      using ( var dbSql = new Banco ( _bcoSql ) )
      {
        using ( SqlDataReader dR = dbSql.DataReader ( sql ) )
        {
          if ( dR.Read ( ) )
          {
            PopulaRecord ( dR );
            Ok = true;
          }
          else
          {
            PopulaRecord ( );
          }
        }
      }
    }

    public void Insert ( )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat
      (
        Sql.InsertRecord
        , _acessoSql.OpenQuery
        , NumSerieLmp
        , NumSerieLmf
      );
      var numSerie = 0L;
      Ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
      if ( Ok )
      {
        NumSerieLmpresp = numSerie;
        ChavePrimaria = new Pk ( NumSerieLmpresp );
        ChaveAlternativa = new Ak ( NumSerieLmp );
        Select ( ChavePrimaria );
      }
    }

    public void Update ( )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat
      (
        Sql.UpdateRecord
        , _acessoSql.OpenQuery
        , NumSerieLmpresp
        , NumSerieLmp
        , NumSerieLmf
      );
      Ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    public void Delete ( )
    {
      Ok = false;
      if ( NumSerieLmpresp != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _acessoSql.OpenQuery, NumSerieLmpresp );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, _acessoSql.OpenQuery, NumSerieLmpresp );
        Ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
      }
    }

    public Boolean ExisteAk ( Int64 numSerieLmp )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExisteAk, _acessoSql.OpenQuery, numSerieLmp );
      return ExisteComum ( sql.ToString ( ) );
    }

    public Boolean ExisteAk ( Ak chaveAlternativa )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExisteAk, _acessoSql.OpenQuery, chaveAlternativa.NumSerieLmp );
      return ExisteComum ( sql.ToString ( ) );
    }

    public Boolean ExistePk ( Int64 numSerieLmpresp )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _acessoSql.OpenQuery, numSerieLmpresp );
      return ExisteComum ( sql.ToString ( ) );
    }

    public Boolean ExistePk ( Pk chavePrimaria )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _acessoSql.OpenQuery, chavePrimaria.NumSerieLmpresp );
      return ExisteComum ( sql.ToString ( ) );
    }

    private Boolean ExisteComum ( String sql )
    {
      return Db.Existe ( _bcoSql, sql );
    }

    public Boolean Valido ( )
    {
      var valido = NumSerieLmpresp != 0L && NumSerieLmpresp != Int64.MinValue
                    && NumSerieLmp != 0L && NumSerieLmp != Int64.MinValue
                    && NumSerieLmf != 0L && NumSerieLmf != Int64.MinValue;
      return valido;
    }
  }
}
