using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DiarioProducao.Classes.Comum;
using Lorenzetti.DB;

namespace DiarioProducao.Classes.ProducaoItemQuantidade
{
  public class Classe
  {

    public class Pk
    {

      public Int64 NumSerieLmpiqtd { get; set; }

      public Boolean Ok { get; }

      public Pk ( )
      {
        NumSerieLmpiqtd = 0L;
        Ok = false;
      }

      public Pk ( Int64 numSerieLmpiqtd )
      {
        if ( numSerieLmpiqtd != Int64.MinValue && numSerieLmpiqtd != 0L )
        {
          Ok = true;
          NumSerieLmpiqtd = numSerieLmpiqtd;
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

    public Int64 NumSerieLmpiqtd
    {
      get
      {
        return GetColumnValueInt64 ( "serie_producao_item_quantidade" );
      }
      set
      {
        SetColumnValue ( "serie_producao_item_quantidade", value );
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

    public Int64 NumSerieLmf
    {
      get
      {
        return GetColumnValueInt64 ( "serie_funcionario" );
      }
      set
      {
        SetColumnValue ( "serie_funcionario", value );
      }
    }

    public Int64 QuantidadeInformada
    {
      get
      {
        return GetColumnValueInt64 ( "quantidade_informada" );
      }
      set
      {
        SetColumnValue ( "quantidade_informada", value );
      }
    }

    public Int64 QuantidadeApontada
    {
      get
      {
        return GetColumnValueInt64 ( "quantidade_apontada" );
      }
      set
      {
        SetColumnValue ( "quantidade_apontada", value );
      }
    }

    public DateTime DataApontamento
    {
      get
      {
        return GetColumnValueDateTime( "data_apontamento" );
      }
      set
      {
        SetColumnValue ( "data_apontamento", value );
      }
    }

    /// <summary>
    /// Cria uma inst�ncia vazia do objeto lor_man_producao_item_quantidade
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
    /// Cria uma inst�ncia vazia do objeto lor_man_producao_item_quantidade
    /// </summary>
    /// <param name="bcoSql">
    /// Configura��o do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configura��o de tipo de acesso ao banco
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
    /// Cria uma inst�ncia do objeto lor_man_producao_item_quantidade a partir dos campos de sua chave prim�ria
    /// </summary>
    /// <param name="bcoSql">
    /// Configura��o do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configura��o de tipo de acesso ao banco
    /// </param>
    /// <param name="numSerieLmpiqtd">
    /// N�mero de s�rie do registro
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery, Int64 numSerieLmpiqtd )
    {
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      CriaColunas ( );
      ChavePrimaria = new Pk ( numSerieLmpiqtd );
      ChaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma inst�ncia do objeto lor_man_producao_item_quantidade a partir de sua chave prim�ria
    /// </summary>
    /// <param name="bcoSql">
    /// Configura��o do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configura��o de tipo de acesso ao banco
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
    /// Cria uma inst�ncia do objeto lor_man_producao_item_quantidade a partir de sua chave alternativa
    /// </summary>
    /// <param name="bcoSql">
    /// Configura��o do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configura��o de tipo de acesso ao banco
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
        new Coluna( 0, "serie_producao_item_quantidade", "S�rie", typeof( Int64 ), true, false, false ),
        new Coluna( 1, "serie_producao_item", "S�rie Produ��o Item", typeof( Int64 ), false, true, false ),
        new Coluna( 2, "serie_funcionario", "S�rie Funcion�rio", typeof( Int64 ), false, false, false ),
        new Coluna( 3, "quantidade_informada", "Qtd.Informada", typeof( Int64 ), false, false, true ),
        new Coluna( 4, "quantidade_apontada", "Qtd.Apontada", typeof( Int64 ), false, false, true ),
        new Coluna( 5, "data_apontamento", "Data Apontamento", typeof( DateTime ), false, false, true )
      };
    }

    /// <summary>
    /// Atribui um valor a uma coluna
    /// </summary>
    /// <param name="columnName">
    /// Nome da coluna
    /// </param>
    /// <param name="value">
    /// Valor a ser atribu�do
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
    /// Carrega as vari�veis da classe empresa atrav�s de um dataReader
    /// </summary>
    /// <param name="dataReader">
    /// dataReader contendo o registro lido do banco de dados
    /// </param>
    /// <returns>
    /// N�o h� retorno
    /// </returns>
    private void PopulaRecord ( SqlDataReader dataReader = null )
    {
      if ( dataReader != null )
      {
        SetColumnValue ( "serie_producao_item_quantidade", Convert.ToInt64 ( dataReader [ "serie_producao_item_quantidade" ] ) );
        SetColumnValue ( "serie_producao_item", Convert.ToInt64 ( dataReader [ "serie_producao_item" ] ) );
        SetColumnValue ( "serie_funcionario", Convert.ToInt64 ( dataReader [ "serie_funcionario" ] ) );
        SetColumnValue ( "quantidade_informada", Convert.ToInt64 ( dataReader [ "quantidade_informada" ] ) );
        SetColumnValue ( "quantidade_apontada", Convert.ToInt64 ( dataReader [ "quantidade_apontada" ] ) );
        SetColumnValue ( "data_apontamento", Convert.ToDateTime( dataReader [ "data_apontamento" ] ) );
      }
      else
      {
        SetColumnValue ( "serie_producao_item_quantidade", 0L );
        SetColumnValue ( "serie_producao_item", 0L );
        SetColumnValue ( "serie_funcionario", 0L );
        SetColumnValue ( "quantidade_informada", 0L );
        SetColumnValue ( "quantidade_apontada", 0L );
        SetColumnValue ( "data_apontamento", DateTime.MinValue );
      }
      ChavePrimaria = new Pk ( NumSerieLmpiqtd );
      ChaveAlternativa = new Ak ( NumSerieLmpi );
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_producao_item_quantidade atrav�s de sua chave prim�ria
    /// </summary>
    /// <param name="chavePrimaria">
    /// Chave prim�ria da tabela
    /// </param>
    /// <returns>
    /// N�o h� retorno
    /// </returns>
    private void Select ( Pk chavePrimaria )
    {
      if ( chavePrimaria.Ok )
      {
        var sql = new StringBuilder ( );
        sql.AppendFormat ( Sql.QueryRecordPk, _openQuery, chavePrimaria.NumSerieLmpiqtd );
        SelectComum ( sql.ToString ( ) );
      }
      else
      {
        PopulaRecord ( );
      }
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_producao_item_quantidade atrav�s de sua chave Alternativa
    /// </summary>
    /// <param name="chaveAlternativa">
    /// Chave alternativa da tabela
    /// </param>
    /// <returns>
    /// N�o h� retorno
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

    private Boolean ValidaSerieFuncionario ( )
    {
      return NumSerieLmf != 0L && NumSerieLmf != Int64.MinValue;
    }

    private Boolean ValidaQuantidadeInformada ( )
    {
      return QuantidadeInformada != 0L && QuantidadeInformada != Int64.MinValue;
    }

    private Boolean ValidaQuantidadeApontada ( )
    {
      return QuantidadeApontada != 0L && QuantidadeApontada != Int64.MinValue;
    }

    private Boolean ValidaDataApontamento ( )
    {
      return DataApontamento != DateTime.MinValue;
    }

    private Boolean Valida ( )
    {
      var retorno = ValidaSerieProducaoItem()
                        && ValidaSerieFuncionario()
                        && ValidaQuantidadeApontada()
                        && ValidaQuantidadeInformada()
                        && ValidaDataApontamento()
        ;
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
          , NumSerieLmf
          , QuantidadeInformada
          , QuantidadeApontada
          , DataApontamento
        );
        var numSerie = 0L;
        Ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
        if ( Ok )
        {
          NumSerieLmpiqtd = numSerie;
          ChavePrimaria = new Pk ( NumSerieLmpiqtd );
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
        , NumSerieLmpiqtd
        , NumSerieLmpi
        , NumSerieLmf
        , QuantidadeInformada
        , QuantidadeApontada
        , DataApontamento
      );
      Ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    public void Delete ( )
    {
      Ok = false;
      if ( NumSerieLmpiqtd != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _openQuery, NumSerieLmpiqtd );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, _openQuery, NumSerieLmpiqtd );
        Ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
      }
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave alternativa fornecida
    /// </summary>
    /// <param name="numSerieLmpi">
    /// S�rie da produ��o do item
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - N�o existe registro
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
    /// false - N�o existe registro
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
    /// <param name="numSerieLmpiqtd">
    /// N�mero de s�rie do registro de empresa
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - N�o existe registro
    /// </returns>
    public Boolean ExistePk ( Int64 numSerieLmpiqtd )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _openQuery, numSerieLmpiqtd );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com a chave prim�ria fornecida
    /// </summary>
    /// <param name="chavePrimaria">
    /// Chave primaria da empresa
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - N�o existe registro
    /// </returns>
    public Boolean ExistePk ( Pk chavePrimaria )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _openQuery, chavePrimaria.NumSerieLmpiqtd );
      return ExisteComum ( sql.ToString ( ) );
    }

    private Boolean ExisteComum ( String sql )
    {
      return Db.Existe ( _bcoSql, sql );
    }
  }
}
