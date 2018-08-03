using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Lorenzetti.DB;
using DiarioProducao.Classes.Comum;

namespace DiarioProducao.Classes.Empresa
{
  public class Classe
  {
    public class Pk
    {
      private Int64 _numSerieLme;
      private readonly Boolean _ok;

      public Int64 NumSerieLme
      {
        get
        {
          return _numSerieLme;
        }
        set
        {
          _numSerieLme = value;
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
        _numSerieLme = 0L;
        _ok = false;
      }

      public Pk ( Int64 numSerieLme )
      {
        if ( numSerieLme != Int64.MinValue && numSerieLme != 0L )
        {
          _ok = true;
          _numSerieLme = numSerieLme;
        }
      }

    }

    public class Ak
    {
      private String _codEmpresa;
      private readonly Boolean _ok;

      public String CodEmpresa
      {
        get
        {
          return _codEmpresa;
        }
        set
        {
          _codEmpresa = value;
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
        _codEmpresa = String.Empty;
        _ok = false;
      }

      public Ak ( String codEmpresa )
      {
        if ( !String.IsNullOrEmpty ( codEmpresa ) )
        {
          _ok = true;
          _codEmpresa = codEmpresa;
        }
      }

    }

    private readonly Config       _bcoSql;
    private readonly String       _openQuery;
    private Boolean               _ok;
    private Tabela                _empresa;
    private Pk                    _chavePrimaria;
    private Ak                    _chaveAlternativa;
    private List<Coluna>          _colunas;

    public Tabela Tabela
    {
      get
      {
        return _empresa;
      }
      set
      {
        _empresa = value;
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

    public Int64 NumSerieLme
    {
      get
      {
        return Convert.ToInt64 ( GetColumnValue ( "serie_empresa" ) );
      }
      set
      {
        SetColumnValue ( "serie_empresa", value );
      }
    }

    public String CodEmpresa
    {
      get
      {
        return GetColumnValue ( "codigo_empresa" ).ToString ( );
      }
      set
      {
        SetColumnValue ( "codigo_empresa", value );
      }
    }

    public String DenEmpresa
    {
      get
      {
        return GetColumnValue ( "descricao_empresa" ).ToString ( );
      }
      set
      {
        SetColumnValue ( "descricao_empresa", value );
      }
    }

    public String DenEmpresaReduz
    {
      get
      {
        return GetColumnValue ( "descricao_empresa_reduzida" ).ToString ( );
      }
      set
      {
        SetColumnValue ( "descricao_empresa_reduzida", value );
      }
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_empresa
    /// </summary>
    /// <returns>
    /// </returns>
    public Classe ( )
    {
      _ok = true;
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = new Ak ( );
      CriaColunas ( );
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_empresa
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração do tipo de acesso ao banco
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery )
    {
      _ok = true;
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = new Ak ( );
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      CriaColunas ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_empresa a partir dos campos de sua chave primária
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração do tipo de acesso ao banco
    /// </param>
    /// <param name="numSerieLme">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery, Int64 numSerieLme )
    {
      _ok = true;
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      _chavePrimaria = new Pk ( numSerieLme );
      _chaveAlternativa = new Ak ( );
      EmpresaComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_empresa a partir de sua chave primária
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração do tipo de acesso ao banco
    /// </param>
    /// <param name="chavePrimaria">
    /// Chave primaria da tabela
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery, Pk chavePrimaria )
    {
      _ok = true;
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      _chavePrimaria = chavePrimaria;
      _chaveAlternativa = new Ak ( );
      EmpresaComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_empresa a partir dos campos de sua chave alternativa
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração de tipo de aceeso ao banco
    /// </param>
    /// <param name="codEmpresa">
    /// Código da empresa
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery, String codEmpresa )
    {
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = new Ak ( codEmpresa );
      EmpresaComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_empresa a partir de sua chave alternativa
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração de tipo de acesso ao banco
    /// </param>
    /// <param name="chaveAlternativa">
    /// Chave alternativa da tabela
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery, Ak chaveAlternativa )
    {
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = chaveAlternativa;
      EmpresaComum ( );
    }

    private void EmpresaComum ( )
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

    /// <summary>
    /// Define as propriedadas das colunas da tabela
    /// </summary>
    private void CriaColunas ( )
    {
      _colunas = new List<Coluna> ( );
      _colunas.Add ( new Coluna ( 0, "serie_empresa", "Série", typeof ( Int64 ), true, false, false ) );
      _colunas.Add ( new Coluna ( 1, "codigo_empresa", "Código", typeof ( String ), false, true, true ) );
      _colunas.Add ( new Coluna ( 2, "descricao_empresa", "Descrição", typeof ( String ), false, false, true ) );
      _colunas.Add ( new Coluna ( 3, "descricao_empresa_reduzida", "Descrição Reduzida", typeof ( String ), false, false, true ) );
    }

    /// <summary>
    /// Atribui um valor a uma coluna
    /// </summary>
    /// <param name="columnName">
    /// Nome da coluna
    /// </param>
    /// <param name="value">
    /// Valor a ser atribuído
    /// </param>
    /// <returns>
    /// </returns>
    private void SetColumnValue ( String columnName, Object value )
    {
      _colunas.Find ( item => item.ColumnName == columnName ).Value = value;
    }

    /// <summary>
    /// Obtem o valor de uma coluna
    /// </summary>
    /// <param name="columnName">
    /// Nome da coluna
    /// </param>
    /// <returns>
    /// Valor sendo armazenado pela coluna
    /// </returns>
    private Object GetColumnValue ( String columnName )
    {
      return _colunas.Find ( item => item.ColumnName == columnName ).Value;
    }

    /// <summary>
    /// Carrega as variáveis da classe empresa através de um dataReader
    /// </summary>
    /// <param name="dataReader">
    /// dataReader contendo o registro lido do banco de dados
    /// </param>
    /// <returns>
    /// Não há retorno
    /// </returns>
    private void PopulaRecord ( SqlDataReader dataReader = null )
    {
      if ( dataReader != null )
      {
        SetColumnValue ( "serie_empresa", Convert.ToInt64 ( dataReader [ "serie_empresa" ] ) );
        SetColumnValue ( "codigo_empresa", dataReader [ "codigo_empresa" ].ToString ( ) );
        SetColumnValue ( "descricao_empresa", dataReader [ "descricao_empresa" ].ToString ( ) );
        SetColumnValue ( "descricao_empresa_reduzida", dataReader [ "descricao_empresa_reduzida" ].ToString ( ) );
      }
      else
      {
        SetColumnValue ( "serie_empresa", 0L );
        SetColumnValue ( "codigo_empresa", String.Empty );
        SetColumnValue ( "descricao_empresa", String.Empty );
        SetColumnValue ( "descricao_empresa_reduzida", String.Empty );
      }
      _chavePrimaria = new Pk ( NumSerieLme );
      _chaveAlternativa = new Ak ( CodEmpresa );
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_empresa através de sua chave primária
    /// </summary>
    /// <param name="chavePrimaria">
    /// Chave primária da tabela
    /// </param>
    /// <returns>
    /// Não há retorno
    /// </returns>
    private void Select ( Pk chavePrimaria )
    {
      if ( chavePrimaria.Ok )
      {
        var sql = new StringBuilder ( );
        sql.AppendFormat ( Sql.QueryRecordPk, _openQuery, chavePrimaria.NumSerieLme );
        SelectComum ( sql.ToString ( ) );
      }
      else
      {
        PopulaRecord ( );
      }
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_empresa através de sua chave Alternativa
    /// </summary>
    /// <param name="chaveAlternativa">
    /// Chave alternativa da tabela
    /// </param>
    /// <returns>
    /// Não há retorno
    /// </returns>
    private void Select ( Ak chaveAlternativa )
    {
      if ( chaveAlternativa.Ok )
      {
        var sql = new StringBuilder ( );
        sql.AppendFormat ( Sql.QueryRecordAk, _openQuery, chaveAlternativa.CodEmpresa );
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

    /// <summary>
    /// Insere o registro corrente no banco de dados
    /// </summary>
    public void Insert ( )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat
      (
        Sql.InsertRecord
        , _openQuery
        , CodEmpresa
        , DenEmpresa
        , DenEmpresaReduz
      );
      var numSerie = 0L;
      _ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
      if ( _ok )
      {
        NumSerieLme = numSerie;
        _chavePrimaria = new Pk ( NumSerieLme );
        _chaveAlternativa = new Ak ( CodEmpresa );
        Select ( _chavePrimaria );
      }
    }


    /// <summary>
    /// Atualiza o registro corrente no banco de dados
    /// </summary>
    public void Update ( )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat
      (
        Sql.UpdateRecord
        , _openQuery
        , NumSerieLme
        , DenEmpresa
        , DenEmpresaReduz
      );
      _ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    public void Delete ( )
    {
      _ok = false;
      if ( NumSerieLme != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _openQuery, NumSerieLme );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, _openQuery, NumSerieLme );
        _ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
      }
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave alternativa fornecida
    /// </summary>
    /// <param name="codigo">
    /// Código da empresa
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExisteAk ( String codigo )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExisteAk, _openQuery, codigo );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com a chave alternativa fornecida
    /// </summary>
    /// <param name="chaveAlternativa">
    /// Chave alternativa da empresa
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExisteAk ( Ak chaveAlternativa )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExisteAk, _openQuery, chaveAlternativa.CodEmpresa );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave primaria fornecida
    /// </summary>
    /// <param name="numSerieLme">
    /// Número de série do registro de empresa
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExistePk ( Int64 numSerieLme )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _openQuery, numSerieLme );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com a chave primária fornecida
    /// </summary>
    /// <param name="chavePrimaria">
    /// Chave primaria da empresa
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExistePk ( Pk chavePrimaria )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _openQuery, chavePrimaria.NumSerieLme );
      return ExisteComum ( sql.ToString ( ) );
    }

    private Boolean ExisteComum ( String sql )
    {
      return Db.Existe ( _bcoSql, sql );
    }
  }
}
