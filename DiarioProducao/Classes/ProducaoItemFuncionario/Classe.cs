using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DiarioProducao.Classes.Comum;
using Lorenzetti.DB;

namespace DiarioProducao.Classes.ProducaoItemFuncionario
{
  public class Classe
  {
    public class Pk
    {

      public Int64 NumSerieLmpif { get; set; }

      public Boolean Ok { get; }

      public Pk ( )
      {
        NumSerieLmpif = 0L;
        Ok = false;
      }

      public Pk ( Int64 numSerieLmpif )
      {
        if ( numSerieLmpif != Int64.MinValue && numSerieLmpif != 0L )
        {
          Ok = true;
          NumSerieLmpif = numSerieLmpif;
        }
      }

    }

    public class Ak
    {

      public Int64 NumSerieLmpi { get; set; }

      public Int64 NumSerieLmf { get; set; }

      public Boolean Ok { get; }

      public Ak ( )
      {
        NumSerieLmpi = 0L;
        NumSerieLmf = 0L;
        Ok = false;
      }

      public Ak ( Int64 numSerieLmpi, Int64 numSerieLmf )
      {
        if ( numSerieLmpi != 0L && numSerieLmf != 0L )
        {
          Ok = true;
          NumSerieLmpi = numSerieLmpi;
          NumSerieLmf = numSerieLmf;
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

    public Int64 NumSerieLmpif
    {
      get
      {
        return Convert.ToInt64 ( GetColumnValue ( "serie_producao_item_funcionario" ) );
      }
      set
      {
        SetColumnValue ( "serie_producao_item_funcionario", value );
      }
    }

    public Int64 NumSerieLmpi
    {
      get
      {
        return Convert.ToInt64 ( GetColumnValue ( "serie_producao_item" ) );
      }
      set
      {
        SetColumnValue ( "serie_producao_item", value );
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

    public Int64 NumMatricula
    {
      get
      {
        return Convert.ToInt64 ( GetColumnValue ( "matricula_funcionario" ) );
      }
      set
      {
        SetColumnValue ( "matricula_funcionario", value );
      }
    }

    public String NomFuncionario
    {
      get
      {
        return GetColumnValue ( "nome_funcionario" ).ToString ( );
      }
      set
      {
        SetColumnValue ( "nome_funcionario", value );
      }
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_producao_item_funcionario
    /// </summary>
    /// <returns>
    /// </returns>
    public Classe ( )
    {
      Ok = true;
      _bcoSql = null;
      _acessoSql = null;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( );
      ProducaoItemFuncionarioComum ( );
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_producao_item_funcionario
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="acessoSql">
    /// Configuração de tipo de acesso
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, AcessoSql acessoSql )
    {
      Ok = true;
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( );
      ProducaoItemFuncionarioComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_item_funcionario a partir dos campos de sua chave primária
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="acessoSql">
    /// Configuração de tipo de acesso
    /// </param>
    /// <param name="numSerieLmpif">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, AcessoSql acessoSql, Int64 numSerieLmpif )
    {
      Ok = true;
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      ChavePrimaria = new Pk ( numSerieLmpif );
      ChaveAlternativa = new Ak ( );
      ProducaoItemFuncionarioComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_item_funcionario a partir de sua chave primária
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="acessoSql">
    /// Configuração de tipo de acesso
    /// </param>
    /// <param name="chavePrimaria">
    /// Chave primaria da tabela
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, AcessoSql acessoSql, Pk chavePrimaria )
    {
      Ok = true;
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      ChavePrimaria = chavePrimaria;
      ChaveAlternativa = new Ak ( );
      ProducaoItemFuncionarioComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_item_funcionario a partir dos campos de sua chave alternativa
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="acessoSql">
    /// Configuração de tipo de acesso
    /// </param>
    /// <param name="numSerieLmpi">
    /// Producao do Item
    /// </param>
    /// <param name="numSerieLmf">
    /// Número de série do funcionário
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, AcessoSql acessoSql, Int64 numSerieLmpi, Int64 numSerieLmf )
    {
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( numSerieLmpi, numSerieLmf );
      ProducaoItemFuncionarioComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_item_funcionario a partir de sua chave alternativa
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="acessoSql">
    /// Configuração de tipo de acesso
    /// </param>
    /// <param name="chaveAlternativa">
    /// Chave alternativa da tabela
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, AcessoSql acessoSql, Ak chaveAlternativa )
    {
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = chaveAlternativa;
      ProducaoItemFuncionarioComum ( );
    }

    private void ProducaoItemFuncionarioComum ( )
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
        new Coluna( 0, "serie_producao_item_funcionario", "Série", typeof( Int64 ), true, false, false ),
        new Coluna( 1, "serie_producao_item", "Série Produção Item", typeof( Int64 ), false, true, false ),
        new Coluna( 2, "serie_funcionario", "Série Funcionário", typeof( Int64 ), false, true, false ),
        new Coluna( 3, "matricula_funcionario", "Matrícula", typeof( Int64 ), false, false, false ),
        new Coluna( 4, "nome_funcionario", "Nome", typeof( String ), false, false, false )
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
        SetColumnValue ( "serie_producao_item_funcionario", Convert.ToInt64 ( dataReader [ "serie_producao_item_funcionario" ] ) );
        SetColumnValue ( "serie_producao_item", Convert.ToInt64 ( dataReader [ "serie_producao_item" ] ) );
        SetColumnValue ( "serie_funcionario", Convert.ToInt64 ( dataReader [ "serie_funcionario" ] ) );
        SetColumnValue ( "matricula_funcionario", Convert.ToInt64 ( dataReader [ "matricula_funcionario" ] ) );
        SetColumnValue ( "nome_funcionario", dataReader [ "nome_funcionario" ].ToString ( ) );
      }
      else
      {
        SetColumnValue ( "serie_producao_item_funcionario", 0L );
        SetColumnValue ( "serie_producao_item", 0L );
        SetColumnValue ( "serie_funcionario", 0L );
        SetColumnValue ( "matricula_funcionario", 0L );
        SetColumnValue ( "nome_funcionario", String.Empty );
      }
      ChavePrimaria = new Pk ( NumSerieLmpif );
      ChaveAlternativa = new Ak ( NumSerieLmpi, NumSerieLmf );
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_producao_item_funcionario através de sua chave primária
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
        sql.AppendFormat ( Sql.QueryRecordPk, _acessoSql.OpenQuery, chavePrimaria.NumSerieLmpif );
        SelectComum ( sql.ToString ( ) );
      }
      else
      {
        PopulaRecord ( );
      }
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_producao_item_funcionario através de sua chave Alternativa
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
        sql.AppendFormat
        (
          Sql.QueryRecordAk
          , _acessoSql.OpenQuery
          , chaveAlternativa.NumSerieLmpi
          , chaveAlternativa.NumSerieLmf
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
        , _acessoSql.OpenQuery
        , NumSerieLmpi
        , NumSerieLmf
      );
      var numSerie = 0L;
      Ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
      if ( Ok )
      {
        NumSerieLmpif = numSerie;
        ChavePrimaria = new Pk ( NumSerieLmpif );
        ChaveAlternativa = new Ak ( NumSerieLmpi, NumSerieLmf );
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
        , _acessoSql.OpenQuery
        , NumSerieLmpif
        , NumSerieLmpi
        , NumSerieLmf
      );
      Ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    public void Delete ( )
    {
      Ok = false;
      if ( NumSerieLmpif != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _acessoSql.OpenQuery, NumSerieLmpif );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, _acessoSql.OpenQuery, NumSerieLmpif );
        Ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
      }
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave alternativa fornecida
    /// </summary>
    /// <param name="numSerieLmpi">
    /// Número de série da producao do item
    /// </param>
    /// <param name="numSerieLmf">
    /// Número de série do funcionário
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExisteAk ( Int64 numSerieLmpi, DateTime numSerieLmf )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExisteAk, _acessoSql.OpenQuery, numSerieLmpi, numSerieLmf );
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
      sql.AppendFormat ( Sql.ExisteAk, _acessoSql.OpenQuery, chaveAlternativa.NumSerieLmpi, chaveAlternativa.NumSerieLmf );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave primaria fornecida
    /// </summary>
    /// <param name="numSerieLmpif">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExistePk ( Int64 numSerieLmpif )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _acessoSql.OpenQuery, numSerieLmpif );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com a chave primária fornecida
    /// </summary>
    /// <param name="chavePrimaria">
    /// Chave primaria da tabela
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExistePk ( Pk chavePrimaria )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _acessoSql.OpenQuery, chavePrimaria.NumSerieLmpif );
      return ExisteComum ( sql.ToString ( ) );
    }

    private Boolean ExisteComum ( String sql )
    {
      return Db.Existe ( _bcoSql, sql );
    }

    public Boolean Valido ( )
    {
      var valido = NumSerieLmpif != 0L && NumSerieLmpif != Int64.MinValue
                    && NumSerieLmpi != 0L && NumSerieLmpi != Int64.MinValue
                    && NumSerieLmf != 0L && NumSerieLmf != Int64.MinValue
        ;
      return valido;
    }
  }
}
