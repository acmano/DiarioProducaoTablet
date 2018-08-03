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
      private Int64 _numSerieLmpresp;
      private readonly Boolean _ok;

      public Int64 NumSerieLmpresp
      {
        get
        {
          return _numSerieLmpresp;
        }
        set
        {
          _numSerieLmpresp = value;
        }
      }

      public Boolean Ok
      {
        get
        {
          return _ok;
        }
      }

      public Pk ( )
      {
        _numSerieLmpresp = 0L;
        _ok = false;
      }

      public Pk ( Int64 numSerieLmpresp )
      {
        if ( numSerieLmpresp != Int64.MinValue && numSerieLmpresp != 0L )
        {
          _ok = true;
          _numSerieLmpresp = numSerieLmpresp;
        }
      }

    }

    public class Ak
    {
      private Int64 _numSerieLmp;
      private readonly Boolean _ok;

      public Int64 NumSerieLmp
      {
        get
        {
          return _numSerieLmp;
        }
        set
        {
          _numSerieLmp = value;
        }
      }

      public Boolean Ok
      {
        get
        {
          return _ok;
        }
      }

      public Ak ( )
      {
        _numSerieLmp = 0L;
        _ok = false;
      }

      public Ak ( Int64 numSerieLmp )
      {
        if ( numSerieLmp != 0L )
        {
          _ok = true;
          _numSerieLmp = numSerieLmp;
        }
      }

    }

    private readonly Config    _bcoSql;
    private readonly AcessoSql _acessoSql;
    private Boolean            _ok;
    private Tabela             _producaoResponsavel;
    private Pk                 _chavePrimaria;
    private Ak                 _chaveAlternativa;
    private List<Coluna>       _colunas;

    public Tabela Tabela
    {
      get
      {
        return _producaoResponsavel;
      }
      set
      {
        _producaoResponsavel = value;
      }
    }

    public Pk ChavePrimaria
    {
      get
      {
        return _chavePrimaria;
      }
      set
      {
        _chavePrimaria = value;
      }
    }

    public Ak ChaveAlternativa
    {
      get
      {
        return _chaveAlternativa;
      }
      set
      {
        _chaveAlternativa = value;
      }
    }

    public Boolean Ok
    {
      get
      {
        return _ok;
      }
      set
      {
        _ok = value;
      }
    }

    public List<Coluna> Colunas
    {
      get
      {
        return _colunas;
      }
      set
      {
        _colunas = value;
      }
    }

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
      _ok = true;
      _bcoSql = null;
      _acessoSql = null;
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = new Ak ( );
      ProducaoResponsavelComum ( );
    }

    public Classe ( Config bcoSql, AcessoSql acessoSql )
    {
      _ok = true;
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = new Ak ( );
      ProducaoResponsavelComum ( );
    }

    public Classe ( Config bcoSql, AcessoSql acessoSql, Pk chavePrimaria )
    {
      _ok = true;
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      _chavePrimaria = chavePrimaria;
      _chaveAlternativa = new Ak ( );
      ProducaoResponsavelComum ( );
    }

    public Classe ( Config bcoSql, AcessoSql acessoSql, Ak chaveAlternativa )
    {
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = chaveAlternativa;
      ProducaoResponsavelComum ( );
    }

    private void ProducaoResponsavelComum ( )
    {
      _ok = false;
      CriaColunas ( );
      if ( _chavePrimaria.Ok )
      {
        Select ( _chavePrimaria );
      }
      else
      {
        if ( _chaveAlternativa.Ok )
        {
          Select ( _chaveAlternativa );
        }
      }
    }

    private void CriaColunas ( )
    {
      _colunas = new List<Coluna> ( );
      _colunas.Add ( new Coluna ( 0, "serie_producao_responsavel", "Série", typeof ( Int64 ), true, false, false ) );
      _colunas.Add ( new Coluna ( 1, "serie_producao", "Série Produção", typeof ( Int64 ), false, true, false ) );
      _colunas.Add ( new Coluna ( 2, "serie_funcionario", "Série Funcionário", typeof ( Int64 ), false, false, false ) );
    }

    private void SetColumnValue ( String columnName, Object value )
    {
      _colunas.Find ( item => item.ColumnName == columnName ).Value = value;
    }

    private Object GetColumnValue ( String columnName )
    {
      return _colunas.Find ( item => item.ColumnName == columnName ).Value;
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
      _chavePrimaria = new Pk ( NumSerieLmpresp );
      _chaveAlternativa = new Ak ( NumSerieLmp );
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
            _ok = true;
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
      _ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
      if ( _ok )
      {
        NumSerieLmpresp = numSerie;
        _chavePrimaria = new Pk ( NumSerieLmpresp );
        _chaveAlternativa = new Ak ( NumSerieLmp );
        Select ( _chavePrimaria );
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
      _ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    public void Delete ( )
    {
      _ok = false;
      if ( NumSerieLmpresp != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _acessoSql.OpenQuery, NumSerieLmpresp );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, _acessoSql.OpenQuery, NumSerieLmpresp );
        _ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
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
