using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DiarioProducao.Classes.Comum;
using Lorenzetti.DB;

namespace DiarioProducao.Classes.LinhaMontagem
{
  public class Classe
  {
    public class Pk
    {
      private Int64 _numSerieLmlm;
      private readonly Boolean _ok;

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

      public Boolean Ok
      {
        get
        {
          return _ok;
        }
      }

      public Pk ( )
      {
        _numSerieLmlm = 0L;
        _ok = false;
      }

      public Pk ( Int64 numSerieLmlm )
      {
        if ( numSerieLmlm != Int64.MinValue && numSerieLmlm != 0L )
        {
          _ok = true;
          _numSerieLmlm = numSerieLmlm;
        }
      }

    }

    public class Ak
    {
      private String _codEmpresa;
      private String _codLinhaMontagem;
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

      public String CodLinhaMontagem
      {
        get
        {
          return _codLinhaMontagem;
        }
        set
        {
          _codLinhaMontagem = value;
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
        _codLinhaMontagem = String.Empty;
        _ok = false;
      }

      public Ak ( String codEmpresa, String codLinhaMontagem )
      {
        if ( !String.IsNullOrEmpty ( codEmpresa ) && !String.IsNullOrEmpty ( codLinhaMontagem ) )
        {
          _ok = true;
          _codEmpresa = codEmpresa;
          _codLinhaMontagem = codLinhaMontagem;
        }
      }

    }

    private readonly Config _bcoSql;
    private readonly String _openQuery;
    private Boolean         _ok;
    private Tabela          _linhaMontagem;
    private Pk              _chavePrimaria;
    private Ak              _chaveAlternativa;
    private List<Coluna>    _colunas;

    public Tabela Tabela
    {
      get
      {
        return _linhaMontagem;
      }
      set
      {
        _linhaMontagem = value;
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

    public Int64 NumSerieLmlm
    {
      get
      {
        return Convert.ToInt64 ( GetColumnValue ( "serie_linha_montagem" ) );
      }
      set
      {
        SetColumnValue ( "serie_linha_montagem", value );
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

    public String CodLinhaMontagem
    {
      get
      {
        return GetColumnValue ( "codigo_linha_montagem" ).ToString ( );
      }
      set
      {
        SetColumnValue ( "codigo_linha_montagem", value );
      }
    }

    public String DenLinhaMontagem
    {
      get
      {
        return GetColumnValue ( "descricao_linha_montagem" ).ToString ( );
      }
      set
      {
        SetColumnValue ( "descricao_linha_montagem", value );
      }
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_linha_montagem
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
    /// Cria uma instância vazia do objeto lor_man_linha_montagem
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração de tipo de acesso ao banco
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery )
    {
      _ok = true;
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = new Ak ( );
      CriaColunas ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_linha_montagem a partir dos campos de sua chave primária
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração de tip ode acesso ao banco
    /// </param>
    /// <param name="numSerieLmlm">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery, Int64 numSerieLmlm )
    {
      _ok = true;
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      _chavePrimaria = new Pk ( numSerieLmlm );
      _chaveAlternativa = new Ak ( );
      LinhaMontagemComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_linha_montagem a partir de sua chave primária
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração de tipo de acesso ao banco
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
      LinhaMontagemComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_linha_montagem a partir dos campos de sua chave alternativa
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração de tipo de acesso ao banco
    /// </param>
    /// <param name="codEmpresa">
    /// Código da empresa
    /// </param>
    /// <param name="codLinhaMontagem">
    /// Código da linha de montagem
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery, String codEmpresa, String codLinhaMontagem )
    {
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = new Ak ( codEmpresa, codLinhaMontagem );
      LinhaMontagemComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_linha_montagem a partir de sua chave alternativa
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
      LinhaMontagemComum ( );
    }

    private void LinhaMontagemComum ( )
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
      _colunas.Add ( new Coluna ( 0, "serie_linha_montagem", "Série", typeof ( Int64 ), true, false, false ) );
      _colunas.Add ( new Coluna ( 1, "codigo_empresa", "Empresa", typeof ( String ), false, true, false ) );
      _colunas.Add ( new Coluna ( 2, "codigo_linha_montagem", "Código", typeof ( String ), false, true, true ) );
      _colunas.Add ( new Coluna ( 3, "descricao_linha_montagem", "Descrição", typeof ( String ), false, false, true ) );
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
        SetColumnValue ( "serie_linha_montagem", Convert.ToInt64 ( dataReader [ "serie_linha_montagem" ] ) );
        SetColumnValue ( "codigo_empresa", dataReader [ "codigo_empresa" ].ToString ( ) );
        SetColumnValue ( "codigo_linha_montagem", dataReader [ "codigo_linha_montagem" ].ToString ( ) );
        SetColumnValue ( "descricao_linha_montagem", dataReader [ "descricao_linha_montagem" ].ToString ( ) );
      }
      else
      {
        SetColumnValue ( "serie_linha_montagem", 0L );
        SetColumnValue ( "codigo_empresa", String.Empty );
        SetColumnValue ( "codigo_linha_montagem", String.Empty );
        SetColumnValue ( "descricao_linha_montagem", String.Empty );
      }
      _chavePrimaria = new Pk ( NumSerieLmlm );
      _chaveAlternativa = new Ak ( CodEmpresa, CodLinhaMontagem );
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_linha_montagem através de sua chave primária
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
        sql.AppendFormat ( Sql.QueryRecordPk, _openQuery, chavePrimaria.NumSerieLmlm );
        SelectComum ( sql.ToString ( ) );
      }
      else
      {
        PopulaRecord ( );
      }
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_linha_montagem através de sua chave Alternativa
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
        sql.AppendFormat ( Sql.QueryRecordAk, _openQuery, chaveAlternativa.CodEmpresa, chaveAlternativa.CodLinhaMontagem );
        SelectComum ( sql.ToString ( ) );
      }
      else
      {
        PopulaRecord ( );
      }
    }

    private void SelectComum ( String sql )
    {
      using ( var conexao = new Banco ( _bcoSql ) )
      {
        using ( SqlDataReader dR = conexao.DataReader ( sql ) )
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
        , CodLinhaMontagem
        , DenLinhaMontagem
      );
      var numSerie = 0L;
      _ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
      if ( _ok )
      {
        NumSerieLmlm = numSerie;
        _chavePrimaria = new Pk ( NumSerieLmlm );
        _chaveAlternativa = new Ak ( CodEmpresa, CodLinhaMontagem );
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
        , NumSerieLmlm
        , DenLinhaMontagem
      );
      _ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    public void Delete ( )
    {
      _ok = false;
      if ( NumSerieLmlm != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _openQuery, NumSerieLmlm );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, _openQuery, NumSerieLmlm );
        _ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
      }
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave alternativa fornecida
    /// </summary>
    /// <param name="codEmpresa">
    /// Código da empresa
    /// </param>
    /// <param name="codLinhaMontagem">
    /// Código da Linha de Montagem
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExisteAk ( String codEmpresa, String codLinhaMontagem )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExisteAk, codEmpresa, codLinhaMontagem );
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
      sql.AppendFormat ( Sql.ExisteAk, _openQuery,  chaveAlternativa.CodEmpresa );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave primaria fornecida
    /// </summary>
    /// <param name="numSerieLmlm">
    /// Número de série do registro de empresa
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExistePk ( Int64 numSerieLmlm )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _openQuery, numSerieLmlm );
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
      sql.AppendFormat ( Sql.ExistePk, _openQuery, chavePrimaria.NumSerieLmlm );
      return ExisteComum ( sql.ToString ( ) );
    }

    private Boolean ExisteComum ( String sql )
    {
      return Db.Existe ( _bcoSql, sql );
    }
  }
}
