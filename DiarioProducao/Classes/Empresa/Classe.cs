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
      private readonly Boolean _ok;

      public Int64 NumSerieLme { get; set; }

      public Boolean Ok
      {
        get
        {
          return _ok;
        }
      }

      public Pk ( )
      {
        NumSerieLme = 0L;
        _ok = false;
      }

      public Pk ( Int64 numSerieLme )
      {
        if ( numSerieLme != Int64.MinValue && numSerieLme != 0L )
        {
          _ok = true;
          NumSerieLme = numSerieLme;
        }
      }

    }

    public class Ak
    {
      private readonly Boolean _ok;

      public String CodEmpresa { get; set; }

      public Boolean Ok
      {
        get
        {
          return _ok;
        }
      }

      public Ak ( )
      {
        CodEmpresa = String.Empty;
        _ok = false;
      }

      public Ak ( String codEmpresa )
      {
        if ( !String.IsNullOrEmpty ( codEmpresa ) )
        {
          _ok = true;
          CodEmpresa = codEmpresa;
        }
      }

    }

    private readonly Config       _bcoSql;
    private readonly String       _openQuery;

    public Tabela Tabela { get; set; }
    public Pk ChavePrimaria { get; set; }
    public Ak ChaveAlternativa { get; set; }

    public Boolean Ok { get; set; }

    public List<Coluna> Colunas { get; set; }

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
      Ok = true;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( );
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
      Ok = true;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( );
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
      Ok = true;
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      ChavePrimaria = new Pk ( numSerieLme );
      ChaveAlternativa = new Ak ( );
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
      Ok = true;
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      ChavePrimaria = chavePrimaria;
      ChaveAlternativa = new Ak ( );
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
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( codEmpresa );
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
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = chaveAlternativa;
      EmpresaComum ( );
    }

    private void EmpresaComum ( )
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

    /// <summary>
    /// Define as propriedadas das colunas da tabela
    /// </summary>
    private void CriaColunas ( )
    {
      Colunas = new List<Coluna>
      {
        new Coluna( 0, "serie_empresa", "Série", typeof( Int64 ), true, false, false ),
        new Coluna( 1, "codigo_empresa", "Código", typeof( String ), false, true, true ),
        new Coluna( 2, "descricao_empresa", "Descrição", typeof( String ), false, false, true ),
        new Coluna( 3, "descricao_empresa_reduzida", "Descrição Reduzida", typeof( String ), false, false, true )
      };
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
      Colunas.Find ( item => item.ColumnName == columnName ).Value = value;
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
      return Colunas.Find ( item => item.ColumnName == columnName ).Value;
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
      ChavePrimaria = new Pk ( NumSerieLme );
      ChaveAlternativa = new Ak ( CodEmpresa );
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
            Ok = true;
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
      Ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
      if ( Ok )
      {
        NumSerieLme = numSerie;
        ChavePrimaria = new Pk ( NumSerieLme );
        ChaveAlternativa = new Ak ( CodEmpresa );
        Select ( ChavePrimaria );
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
      Ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    public void Delete ( )
    {
      Ok = false;
      if ( NumSerieLme != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _openQuery, NumSerieLme );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, _openQuery, NumSerieLme );
        Ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
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
