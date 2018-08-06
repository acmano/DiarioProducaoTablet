using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DiarioProducao.Classes.Comum;
using Lorenzetti.DB;

namespace DiarioProducao.Classes.ProducaoObservacao
{
  public class Classe
  {
    public class Pk
    {
      private readonly Boolean _ok;

      public Int64 NumSerieLmpobs { get; set; }

      public Boolean Ok => _ok;

      public Pk ( )
      {
        NumSerieLmpobs = 0L;
        _ok = false;
      }

      public Pk ( Int64 numSerieLmpobs )
      {
        if ( numSerieLmpobs != Int64.MinValue && numSerieLmpobs != 0L )
        {
          _ok = true;
          NumSerieLmpobs = numSerieLmpobs;
        }
      }

    }

    public class Ak
    {

      public Int64 NumSerieLmp { get; set; }

      public DateTime DatObservacao { get; set; }

      public Boolean Ok { get; }

      public Ak ( )
      {
        NumSerieLmp = 0L;
        DatObservacao = DateTime.MinValue;
        Ok = false;
      }

      public Ak ( Int64 numSerieLmp, DateTime datObservacao )
      {
        if ( numSerieLmp != 0L && datObservacao != DateTime.MinValue )
        {
          Ok = true;
          NumSerieLmp = numSerieLmp;
          DatObservacao = datObservacao;
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

    public Int64 NumSerieLmpobs
    {
      get
      {
        return Convert.ToInt64 ( GetColumnValue ( "serie_producao_observacao" ) );
      }
      set
      {
        SetColumnValue ( "serie_producao_observacao", value );
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
      get { return Convert.ToInt64(GetColumnValue("serie_funcionario")); }
      set { SetColumnValue("serie_funcionario", value); }
    }

    public DateTime DatObservacao
    {
      get
      {
        return Convert.ToDateTime ( GetColumnValue ( "data_observacao" ) );
      }
      set
      {
        SetColumnValue ( "data_observacao", value );
      }
    }

    public String TxtObservacao
    {
      get { return GetColumnValue("texto_observacao").ToString(); }
      set { SetColumnValue( "texto_observacao", value); }
    }



    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_producao_observacao
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
      ProducaoObservacaoComum ( );
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_producao_observacao
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
      ProducaoObservacaoComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_observacao a partir dos campos de sua chave primária
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="acessoSql">
    /// Configuração de tipo de acesso
    /// </param>
    /// <param name="numSerieLmpobs">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, AcessoSql acessoSql, Int64 numSerieLmpobs )
    {
      Ok = true;
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      ChavePrimaria = new Pk ( numSerieLmpobs );
      ChaveAlternativa = new Ak ( );
      ProducaoObservacaoComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_observacao a partir de sua chave primária
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
      ProducaoObservacaoComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_observacao a partir dos campos de sua chave alternativa
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="acessoSql">
    /// Configuração de tipo de acesso
    /// </param>
    /// <param name="numSerieLmp">
    /// Producao
    /// </param>
    /// <param name="datObservacao">
    /// Número de série da linha de montagem
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, AcessoSql acessoSql, Int64 numSerieLmp, DateTime datObservacao )
    {
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( numSerieLmp, datObservacao );
      ProducaoObservacaoComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_observacao a partir de sua chave alternativa
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
      ProducaoObservacaoComum ( );
    }

    private void ProducaoObservacaoComum ( )
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
        new Coluna( 0, "serie_producao_observacao", "Série", typeof( Int64 ), true, false, false ),
        new Coluna( 1, "serie_producao", "Série Produção", typeof( Int64 ), false, true, false ),
        new Coluna( 2, "serie_funcionario", "Série Funcionário", typeof( Int64 ), false, false, false ),
        new Coluna( 3, "data_observacao", "Data", typeof( DateTime ), false, true, false ),
        new Coluna( 4, "texto_observacao", "Observacão", typeof( String ), false, false, false )
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
        SetColumnValue ( "serie_producao_observacao", Convert.ToInt64 ( dataReader [ "serie_producao_observacao" ] ) );
        SetColumnValue ( "serie_producao", Convert.ToInt64 ( dataReader [ "serie_producao" ] ) );
        SetColumnValue ( "serie_funcionario", Convert.ToInt64 ( dataReader [ "serie_funcionario" ] ) );
        SetColumnValue ( "data_observacao", Convert.ToDateTime ( dataReader [ "data_observacao" ] ) );
        SetColumnValue ( "texto_observacao", dataReader [ "texto_observacao" ].ToString ( ) );
      }
      else
      {
        SetColumnValue ( "serie_producao_observacao", 0L );
        SetColumnValue ( "serie_producao", 0L );
        SetColumnValue ( "serie_funcionario", 0L );
        SetColumnValue ( "data_observacao", DateTime.MinValue );
        SetColumnValue ( "texto_observacao", String.Empty );
      }
      ChavePrimaria = new Pk ( NumSerieLmpobs );
      ChaveAlternativa = new Ak ( NumSerieLmp, DatObservacao );
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_producao através de sua chave primária
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
        sql.AppendFormat ( Sql.QueryRecordPk, _acessoSql.OpenQuery, chavePrimaria.NumSerieLmpobs );
        SelectComum ( sql.ToString ( ) );
      }
      else
      {
        PopulaRecord ( );
      }
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_producao através de sua chave Alternativa
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
          , chaveAlternativa.NumSerieLmp
          , chaveAlternativa.DatObservacao
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
        , NumSerieLmp
        , NumSerieLmf
        , DatObservacao
        , TxtObservacao
      );
      var numSerie = 0L;
      Ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
      if ( Ok )
      {
        NumSerieLmpobs = numSerie;
        ChavePrimaria = new Pk ( NumSerieLmpobs );
        ChaveAlternativa = new Ak ( NumSerieLmp, DatObservacao );
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
        , NumSerieLmpobs
        , NumSerieLmp
        , NumSerieLmf
        , DatObservacao
        , TxtObservacao
      );
      Ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    public void Delete ( )
    {
      Ok = false;
      if ( NumSerieLmpobs != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _acessoSql.OpenQuery, NumSerieLmpobs );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, NumSerieLmpobs );
        Ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
      }
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave alternativa fornecida
    /// </summary>
    /// <param name="numSerieLmp">
    /// Número de série da empresa
    /// </param>
    /// <param name="datObservacao">
    /// Número de série do item
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExisteAk ( Int64 numSerieLmp, DateTime datObservacao )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExisteAk, _acessoSql.OpenQuery, numSerieLmp, datObservacao );
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
      sql.AppendFormat ( Sql.ExisteAk, _acessoSql.OpenQuery, chaveAlternativa.NumSerieLmp, chaveAlternativa.DatObservacao );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave primaria fornecida
    /// </summary>
    /// <param name="numSerieLmpobs">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExistePk ( Int64 numSerieLmpobs )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _acessoSql.OpenQuery, numSerieLmpobs );
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
      sql.AppendFormat ( Sql.ExistePk, _acessoSql.OpenQuery, chavePrimaria.NumSerieLmpobs );
      return ExisteComum ( sql.ToString ( ) );
    }

    private Boolean ExisteComum ( String sql )
    {
      return Db.Existe ( _bcoSql, sql );
    }

    public Boolean Valido ( )
    {
      var valido = NumSerieLmpobs != 0L && NumSerieLmpobs != Int64.MinValue
                    && NumSerieLmp != 0L && NumSerieLmp != Int64.MinValue
                    && NumSerieLmf != 0L && NumSerieLmf != Int64.MinValue
                    && DatObservacao != DateTime.MinValue
                    && !String.IsNullOrEmpty(TxtObservacao);
      return valido;
    }
  }
}
