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
      private readonly Boolean _ok;

      public Int64 NumSerieLmlm { get; set; }

      public Boolean Ok
      {
        get
        {
          return _ok;
        }
      }

      public Pk ( )
      {
        NumSerieLmlm = 0L;
        _ok = false;
      }

      public Pk ( Int64 numSerieLmlm )
      {
        if ( numSerieLmlm != Int64.MinValue && numSerieLmlm != 0L )
        {
          _ok = true;
          NumSerieLmlm = numSerieLmlm;
        }
      }

    }

    public class Ak
    {
      private readonly Boolean _ok;

      public String CodEmpresa { get; set; }

      public String CodLinhaMontagem { get; set; }

      public Boolean Ok => _ok;

      public Ak ( )
      {
        CodEmpresa = String.Empty;
        CodLinhaMontagem = String.Empty;
        _ok = false;
      }

      public Ak ( String codEmpresa, String codLinhaMontagem )
      {
        if ( !String.IsNullOrEmpty ( codEmpresa ) && !String.IsNullOrEmpty ( codLinhaMontagem ) )
        {
          _ok = true;
          CodEmpresa = codEmpresa;
          CodLinhaMontagem = codLinhaMontagem;
        }
      }

    }

    private readonly Config _bcoSql;
    private readonly String _openQuery;

    public Tabela Tabela { get; set; }
    public Pk ChavePrimaria { get; set; }
    public Ak ChaveAlternativa { get; set; }
    public Boolean Ok { get; set; }
    public List<Coluna> Colunas { get; set; }
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
      Ok = true;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( );
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
      Ok = true;
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( );
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
      Ok = true;
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      ChavePrimaria = new Pk ( numSerieLmlm );
      ChaveAlternativa = new Ak ( );
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
      Ok = true;
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      ChavePrimaria = chavePrimaria;
      ChaveAlternativa = new Ak ( );
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
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( codEmpresa, codLinhaMontagem );
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
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = chaveAlternativa;
      LinhaMontagemComum ( );
    }

    private void LinhaMontagemComum ( )
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
        new Coluna( 0, "serie_linha_montagem", "Série", typeof( Int64 ), true, false, false ),
        new Coluna( 1, "codigo_empresa", nameof( Empresa ), typeof( String ), false, true, false ),
        new Coluna( 2, "codigo_linha_montagem", "Código", typeof( String ), false, true, true ),
        new Coluna( 3, "descricao_linha_montagem", "Descrição", typeof( String ), false, false, true )
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
      ChavePrimaria = new Pk ( NumSerieLmlm );
      ChaveAlternativa = new Ak ( CodEmpresa, CodLinhaMontagem );
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
        , CodLinhaMontagem
        , DenLinhaMontagem
      );
      var numSerie = 0L;
      Ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
      if ( Ok )
      {
        NumSerieLmlm = numSerie;
        ChavePrimaria = new Pk ( NumSerieLmlm );
        ChaveAlternativa = new Ak ( CodEmpresa, CodLinhaMontagem );
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
        , NumSerieLmlm
        , DenLinhaMontagem
      );
      Ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    public void Delete ( )
    {
      Ok = false;
      if ( NumSerieLmlm != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _openQuery, NumSerieLmlm );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, _openQuery, NumSerieLmlm );
        Ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
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
