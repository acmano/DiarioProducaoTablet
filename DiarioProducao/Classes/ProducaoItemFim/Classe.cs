using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DiarioProducao.Classes.Comum;
using Lorenzetti.DB;

namespace DiarioProducao.Classes.ProducaoItemFim
{
  public class Classe
  {

    public class Pk
    {

      public Int64 NumSerieLmpifim { get; set; }
      public Boolean Ok { get; }
      public Pk ( )
      {
        NumSerieLmpifim = 0L;
        Ok = false;
      }

      public Pk ( Int64 numSerieLmpifim )
      {
        if ( numSerieLmpifim != Int64.MinValue && numSerieLmpifim != 0L )
        {
          Ok = true;
          NumSerieLmpifim = numSerieLmpifim;
        }
      }

    }

    public class Ak
    {

      public Int64 NumSerieLmpi { get; set; }

      public Boolean Ok { get; }

      public Ak ( )
      {
        NumSerieLmpi = 0L;
        Ok = false;
      }

      public Ak ( Int64 numSerieLmpi )
      {
        if ( numSerieLmpi != 0L )
        {
          Ok = true;
          NumSerieLmpi = numSerieLmpi;
        }
        else
        {
          Ok = false;
          NumSerieLmpi = 0L;

        }
      }

    }

    private readonly Config         _bcoSql;
    private readonly String         _openQuery;

    public Tabela Tabela { get; set; }
    public Pk ChavePrimaria { get; set; }
    public Ak ChaveAlternativa { get; set; }
    public Boolean Ok { get; set; }
    public List<Coluna> Colunas { get; set; }
    public Int64 NumSerieLmpifim
    {
      get
      {
        return GetColumnValueInt64 ( "serie_producao_item_fim" );
      }
      set
      {
        SetColumnValue ( "serie_producao_item_fim", value );
      }
    }

    public Int64 NumSerieLmpi
    {
      get
      {
        return GetColumnValueInt64 ( "serie_producao_item" );
      }
      set
      {
        SetColumnValue ( "serie_producao_item", value );
      }
    }

    public String HoraFim
    {
      get
      {
        return GetColumnValueString ( "hora_fim" );
      }
      set
      {
        SetColumnValue ( "hora_fim", value );
      }
    }

    public Int64 QuantidadeFuncionarios
    {
      get
      {
        return GetColumnValueInt64 ( "quantidade_funcionarios" );
      }
      set
      {
        SetColumnValue ( "quantidade_funcionarios", value );
      }
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_producao_item_fim
    /// </summary>
    /// <returns>
    /// </returns>
    public Classe ( )
    {
      CriaColunas ( );
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_producao_item_fim
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
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      CriaColunas ( );
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_item_fim a partir dos campos de sua chave primária
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração de tipo de acesso ao banco
    /// </param>
    /// <param name="numSerieLmpifim">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery, Int64 numSerieLmpifim )
    {
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      CriaColunas ( );
      ChavePrimaria = new Pk ( numSerieLmpifim );
      ChaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_item_fim a partir de sua chave primária
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
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      CriaColunas ( );
      ChavePrimaria = chavePrimaria;
      ChaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_item_fim a partir de sua chave alternativa
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
      CriaColunas ( );
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = chaveAlternativa;
      ClasseComum ( );
    }

    private void ClasseComum ( )
    {
      Ok = false;
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
        else
        {
          PopulaRecord ( );
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
        new Coluna( 0, "serie_producao_item_fim", "Série", typeof( Int64 ), true, false, false ),
        new Coluna( 1, "serie_producao_item", "Série Produção Item", typeof( Int64 ), false, true, false ),
        new Coluna( 2, "hora_fim", "Hora final", typeof( String ), false, false, true ),
        new Coluna( 3, "quantidade_funcionarios", "Funcionários", typeof( Int64 ), false, false, true )
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
    private String GetColumnValueString ( String columnName )
    {
      //  if ( _colunas.Find(x => x.GetId() == columnName) )
      var c = Colunas.Find ( item => item.ColumnName == columnName );
      return c.Value.ToString ( );
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
    private DateTime GetColumnValueDateTime ( String columnName )
    {
      //  if ( _colunas.Find(x => x.GetId() == columnName) )
      var c = Colunas.Find ( item => item.ColumnName == columnName );
      return Convert.ToDateTime ( c.Value );
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
    private Int64 GetColumnValueInt64 ( String columnName )
    {
      //  if ( _colunas.Find(x => x.GetId() == columnName) )
      var c = Colunas.Find ( item => item.ColumnName == columnName );
      return Convert.ToInt64 ( c.Value );
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
        SetColumnValue ( "serie_producao_item_fim", Convert.ToInt64 ( dataReader [ "serie_producao_item_fim" ] ) );
        SetColumnValue ( "serie_producao_item", Convert.ToInt64 ( dataReader [ "serie_producao_item" ] ) );
        SetColumnValue ( "hora_fim", dataReader [ "hora_fim" ].ToString ( ) );
        SetColumnValue ( "quantidade_funcionarios", Convert.ToInt64 ( dataReader [ "quantidade_funcionarios" ] ) );
      }
      else
      {
        SetColumnValue ( "serie_producao_item_fim", 0L );
        SetColumnValue ( "serie_producao_item", 0L );
        SetColumnValue ( "hora_fim", DateTime.MinValue );
        SetColumnValue ( "quantidade_funcionarios", 0L );
      }
      ChavePrimaria = new Pk ( NumSerieLmpifim );
      ChaveAlternativa = new Ak ( NumSerieLmpi );
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_producao_item_fim através de sua chave primária
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
        sql.AppendFormat ( Sql.QueryRecordPk, _openQuery, chavePrimaria.NumSerieLmpifim );
        SelectComum ( sql.ToString ( ) );
      }
      else
      {
        PopulaRecord ( );
      }
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_producao_item_fim através de sua chave Alternativa
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
        sql.AppendFormat ( Sql.QueryRecordAk, _openQuery, chaveAlternativa.NumSerieLmpi );
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

    private Boolean ValidaSerieProducaoItem ( )
    {
      return NumSerieLmpi != 0L && NumSerieLmpi != Int64.MinValue;
    }

    private Boolean ValidaHoraFim ( )
    {
      return !String.IsNullOrEmpty ( HoraFim ) && !String.IsNullOrWhiteSpace ( HoraFim );
    }

    private Boolean ValidaQuantidadeFuncionarios ( )
    {
      return QuantidadeFuncionarios != Int64.MinValue && QuantidadeFuncionarios != 0L;
    }

    private Boolean Valida ( )
    {
      var retorno = ValidaSerieProducaoItem ( );
      retorno = retorno & ValidaHoraFim ( );
      retorno = retorno & ValidaQuantidadeFuncionarios ( );
      return retorno;
    }

    /// <summary>
    /// Insere o registro corrente no banco de dados
    /// </summary>
    public void Insert ( )
    {
      if ( Valida ( ) )
      {
        var sql = new StringBuilder ( );
        sql.Clear ( );
        sql.AppendFormat
        (
          Sql.InsertRecord
          , _openQuery
          , NumSerieLmpi
          , HoraFim
          , QuantidadeFuncionarios
        );
        var numSerie = 0L;
        Ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
        if ( Ok )
        {
          NumSerieLmpifim = numSerie;
          ChavePrimaria = new Pk ( NumSerieLmpifim );
          ChaveAlternativa = new Ak ( NumSerieLmpi );
          Select ( ChavePrimaria );
        }
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
        , NumSerieLmpifim
        , NumSerieLmpi
        , HoraFim
        , QuantidadeFuncionarios
      );
      Ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    public void Delete ( )
    {
      Ok = false;
      if ( NumSerieLmpifim != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _openQuery, NumSerieLmpifim );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, _openQuery, NumSerieLmpifim );
        Ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
      }
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave alternativa fornecida
    /// </summary>
    /// <param name="numSerieLmpi">
    /// Série da produção do item
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExisteAk ( String numSerieLmpi )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExisteAk, _openQuery, numSerieLmpi );
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
      sql.AppendFormat ( Sql.ExisteAk, _openQuery, chaveAlternativa.NumSerieLmpi );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave primaria fornecida
    /// </summary>
    /// <param name="numSerieLmpifim">
    /// Número de série do registro de empresa
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExistePk ( Int64 numSerieLmpifim )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _openQuery, numSerieLmpifim );
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
      sql.AppendFormat ( Sql.ExistePk, _openQuery, chavePrimaria.NumSerieLmpifim );
      return ExisteComum ( sql.ToString ( ) );
    }

    private Boolean ExisteComum ( String sql )
    {
      return Db.Existe ( _bcoSql, sql );
    }
  }
}
