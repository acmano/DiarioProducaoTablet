using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DiarioProducao.Classes.Comum;
using Lorenzetti.DB;

namespace DiarioProducao.Classes.ProducaoItemOcorrencia
{
  public class Classe
  {
    public class Pk
    {

      public Int64 NumSerieLmpioco { get; set; }

      public Boolean Ok { get; }

      public Pk ( )
      {
        NumSerieLmpioco = 0L;
        Ok = false;
      }

      public Pk ( Int64 numSerieLmpioco )
      {
        if ( numSerieLmpioco != Int64.MinValue && numSerieLmpioco != 0L )
        {
          Ok = true;
          NumSerieLmpioco = numSerieLmpioco;
        }
      }

    }

    public class Ak
    {

      public Int64 NumSerieLmpi { get; set; }
      public Int64 NumSerieLmoco { get; set; }
      public Boolean Ok { get; }

      public Ak ( )
      {
        NumSerieLmpi = 0L;
        NumSerieLmoco = 0L;
        Ok = false;
      }

      public Ak ( Int64 numSerieLmpi, Int64 numSerieLmoco )
      {
        if ( numSerieLmpi != 0L && numSerieLmoco != 0L )
        {
          Ok = true;
          NumSerieLmpi = numSerieLmpi;
          NumSerieLmoco = numSerieLmoco;
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

    public Int64 NumSerieLmpioco
    {
      get
      {
        return Convert.ToInt64 ( GetColumnValue ( "serie_producao_item_ocorrencia" ) );
      }
      set
      {
        SetColumnValue ( "serie_producao_item_ocorrencia", value );
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

    public Int64 NumSerieLmoco
    {
      get
      {
        return Convert.ToInt64 ( GetColumnValue ( "serie_ocorrencia" ) );
      }
      set
      {
        SetColumnValue ( "serie_ocorrencia", value );
      }
    }

    public String CodOcorrencia
    {
      get
      {
        return GetColumnValue ( "codigo_ocorrencia" ).ToString ( );
      }
      set
      {
        SetColumnValue ( "codigo_ocorrencia", value );
      }
    }

    public String DenOcorrencia
    {
      get
      {
        return GetColumnValue ( "descricao_ocorrencia" ).ToString ( );
      }
      set
      {
        SetColumnValue ( "descricao_ocorrencia", value );
      }
    }

    /// <summary>
    /// Cria uma inst�ncia vazia do objeto lor_man_producao_item_ocorrencia
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
      ProducaoItemOcorrenciaComum ( );
    }

    /// <summary>
    /// Cria uma inst�ncia vazia do objeto lor_man_producao_item_ocorrencia
    /// </summary>
    /// <param name="bcoSql">
    /// Configura��o do banco de dados alvo
    /// </param>
    /// <param name="acessoSql">
    /// Configura��o de tipo de acesso
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
      ProducaoItemOcorrenciaComum ( );
    }

    /// <summary>
    /// Cria uma inst�ncia do objeto lor_man_producao_item_ocorrencia a partir dos campos de sua chave prim�ria
    /// </summary>
    /// <param name="bcoSql">
    /// Configura��o do banco de dados alvo
    /// </param>
    /// <param name="acessoSql">
    /// Configura��o de tipo de acesso
    /// </param>
    /// <param name="numSerieLmpioco">
    /// N�mero de s�rie do registro
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, AcessoSql acessoSql, Int64 numSerieLmpioco )
    {
      Ok = true;
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      ChavePrimaria = new Pk ( numSerieLmpioco );
      ChaveAlternativa = new Ak ( );
      ProducaoItemOcorrenciaComum ( );
    }

    /// <summary>
    /// Cria uma inst�ncia do objeto lor_man_producao_item_ocorrencia a partir de sua chave prim�ria
    /// </summary>
    /// <param name="bcoSql">
    /// Configura��o do banco de dados alvo
    /// </param>
    /// <param name="acessoSql">
    /// Configura��o de tipo de acesso
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
      ProducaoItemOcorrenciaComum ( );
    }

    /// <summary>
    /// Cria uma inst�ncia do objeto lor_man_producao_item_ocorrencia a partir dos campos de sua chave alternativa
    /// </summary>
    /// <param name="bcoSql">
    /// Configura��o do banco de dados alvo
    /// </param>
    /// <param name="acessoSql">
    /// Configura��o de tipo de acesso
    /// </param>
    /// <param name="numSerieLmpi">
    /// Producao do item
    /// </param>
    /// <param name="numSerieLmoco">
    /// N�mero de s�rie daocorr�ncia
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, AcessoSql acessoSql, Int64 numSerieLmpi, Int64 numSerieLmoco )
    {
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( numSerieLmpi, numSerieLmoco );
      ProducaoItemOcorrenciaComum ( );
    }

    /// <summary>
    /// Cria uma inst�ncia do objeto lor_man_producao_item_ocorrencia a partir de sua chave alternativa
    /// </summary>
    /// <param name="bcoSql">
    /// Configura��o do banco de dados alvo
    /// </param>
    /// <param name="acessoSql">
    /// Configura��o de tipo de acesso
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
      ProducaoItemOcorrenciaComum ( );
    }

    private void ProducaoItemOcorrenciaComum ( )
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
        new Coluna( 0, "serie_producao_item_ocorrencia", "S�rie", typeof( Int64 ), true, false, false ),
        new Coluna( 1, "serie_producao_item", "S�rie Produ��o", typeof( Int64 ), false, true, false ),
        new Coluna( 2, "serie_ocorrencia", "S�rie Ocorr�ncia", typeof( Int64 ), false, true, false ),
        new Coluna( 4, "codigo_ocorrencia", "Ocorr�ncia", typeof( String ), false, false, false ),
        new Coluna( 5, "descricao_ocorrencia", "Descri��o", typeof( String ), false, false, false )
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
    private Object GetColumnValue ( String columnName )
    {
      return Colunas.Find ( item => item.ColumnName == columnName ).Value;
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
        SetColumnValue ( "serie_producao_item_ocorrencia", Convert.ToInt64 ( dataReader [ "serie_producao_item_ocorrencia" ] ) );
        SetColumnValue ( "serie_producao_item", Convert.ToInt64 ( dataReader [ "serie_producao_item" ] ) );
        SetColumnValue ( "serie_ocorrencia", Convert.ToInt64 ( dataReader [ "serie_ocorrencia" ] ) );
        SetColumnValue ( "codigo_ocorrencia", dataReader [ "codigo_ocorrencia" ].ToString ( ) );
        SetColumnValue ( "descricao_ocorrencia", dataReader [ "descricao_ocorrencia" ].ToString ( ) );
      }
      else
      {
        SetColumnValue ( "serie_producao_item_ocorrencia", 0L );
        SetColumnValue ( "serie_producao_item", 0L );
        SetColumnValue ( "serie_ocorrencia", 0L );
        SetColumnValue ( "codigo_ocorrencia", String.Empty );
        SetColumnValue ( "descricao_ocorrencia", String.Empty );
      }
      ChavePrimaria = new Pk ( NumSerieLmpioco );
      ChaveAlternativa = new Ak ( NumSerieLmpi, NumSerieLmoco );
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_producao atrav�s de sua chave prim�ria
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
        sql.AppendFormat ( Sql.QueryRecordPk, _acessoSql.OpenQuery, chavePrimaria.NumSerieLmpioco );
        SelectComum ( sql.ToString ( ) );
      }
      else
      {
        PopulaRecord ( );
      }
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_producao_item_ocorrencia atrav�s de sua chave Alternativa
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
        sql.AppendFormat
        (
          Sql.QueryRecordAk
          , _acessoSql.OpenQuery
          , chaveAlternativa.NumSerieLmpi
          , chaveAlternativa.NumSerieLmoco
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
        , NumSerieLmoco
      );
      var numSerie = 0L;
      Ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
      if ( Ok )
      {
        NumSerieLmpioco = numSerie;
        ChavePrimaria = new Pk ( NumSerieLmpioco );
        ChaveAlternativa = new Ak ( NumSerieLmpi, NumSerieLmoco );
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
        , NumSerieLmpioco
        , NumSerieLmpi
        , NumSerieLmoco
      );
      Ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    public void Delete ( )
    {
      Ok = false;
      if ( NumSerieLmpioco != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _acessoSql.OpenQuery, NumSerieLmpioco );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, _acessoSql.OpenQuery, NumSerieLmpioco );
        Ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
      }
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave alternativa fornecida
    /// </summary>
    /// <param name="numSerieLmpi">
    /// N�mero de s�rie da produ��o do item
    /// </param>
    /// <param name="numSerieLmoco">
    /// N�mero de s�rie da ocorr�ncia
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - N�o existe registro
    /// </returns>
    public Boolean ExisteAk ( Int64 numSerieLmpi, Int64 numSerieLmoco )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExisteAk, _acessoSql.OpenQuery, numSerieLmpi, numSerieLmoco );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com a chave alternativa fornecida
    /// </summary>
    /// <param name="chaveAlternativa">
    /// Chave alternativa
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - N�o existe registro
    /// </returns>
    public Boolean ExisteAk ( Ak chaveAlternativa )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExisteAk, _acessoSql.OpenQuery, chaveAlternativa.NumSerieLmpi, chaveAlternativa.NumSerieLmoco );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave primaria fornecida
    /// </summary>
    /// <param name="numSerieLmpioco">
    /// N�mero de s�rie do registro
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - N�o existe registro
    /// </returns>
    public Boolean ExistePk ( Int64 numSerieLmpioco )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _acessoSql.OpenQuery, numSerieLmpioco );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com a chave prim�ria fornecida
    /// </summary>
    /// <param name="chavePrimaria">
    /// Chave primaria da tabela
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - N�o existe registro
    /// </returns>
    public Boolean ExistePk ( Pk chavePrimaria )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _acessoSql.OpenQuery, chavePrimaria.NumSerieLmpioco );
      return ExisteComum ( sql.ToString ( ) );
    }

    private Boolean ExisteComum ( String sql )
    {
      return Db.Existe ( _bcoSql, sql );
    }

    public Boolean Valido ( )
    {
      var valido = NumSerieLmpioco != 0L && NumSerieLmpioco != Int64.MinValue
                    && NumSerieLmpi != 0L && NumSerieLmpi != Int64.MinValue
                    && NumSerieLmoco != 0L && NumSerieLmoco != Int64.MinValue
                    ;
      return valido;
    }
  }
}
