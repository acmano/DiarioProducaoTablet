using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DiarioProducao.Classes.Comum;
using Lorenzetti.DB;

namespace DiarioProducao.Classes.Producao
{
  public class Classe
  {
    public class Pk
    {

      public Int64 NumSerieLmp { get; set; }

      public Boolean Ok { get; }

      public Pk ( )
      {
        NumSerieLmp = 0L;
        Ok = false;
      }

      public Pk ( Int64 numSerieLmp )
      {
        if ( numSerieLmp != Int64.MinValue && numSerieLmp != 0L )
        {
          Ok = true;
          NumSerieLmp = numSerieLmp;
        }
      }

    }

    public class Ak
    {

      public Int64 NumSerieLme { get; set; }

      public DateTime DatInicio { get; set; }

      public Int64 NumSerieLmtrn { get; set; }

      public Int64 NumSerieLmlm { get; set; }

      public Boolean Ok { get; }

      public Ak ( )
      {
        NumSerieLme = 0L;
        DatInicio = DateTime.MinValue;
        NumSerieLmtrn = 0L;
        NumSerieLmlm = 0L;
        Ok = false;
      }

      public Ak ( Int64 numSerieLme, DateTime datInicio, Int64 numSerieLmtrn, Int64 numSerieLmlm )
      {
        if ( numSerieLme != 0L && datInicio != DateTime.MinValue && numSerieLmtrn != 0L && numSerieLmlm != 0L )
        {
          Ok = true;
          NumSerieLme = numSerieLme;
          DatInicio = datInicio;
          NumSerieLmtrn = numSerieLmtrn;
          NumSerieLmlm = numSerieLmlm;
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

    public DateTime DatInicio
    {
      get
      {
        return Convert.ToDateTime ( GetColumnValue ( "data_producao" ) );
      }
      set
      {
        SetColumnValue ( "data_producao", value );
      }
    }

    public Int64 NumSerieLmtrn
    {
      get
      {
        return Convert.ToInt64 ( GetColumnValue ( "serie_turno" ) );
      }
      set
      {
        SetColumnValue ( "serie_turno", value );
      }
    }

    public String CodTurno
    {
      get
      {
        return GetColumnValue ( "codigo_turno" ).ToString ( );
      }
      set
      {
        SetColumnValue ( "codigo_turno", value );
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

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_producao
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
      ProducaoComum ( );
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_producao
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
      ProducaoComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao a partir dos campos de sua chave primária
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="numSerieLmp">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, AcessoSql acessoSql, Int64 numSerieLmp )
    {
      Ok = true;
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      ChavePrimaria = new Pk ( numSerieLmp );
      ChaveAlternativa = new Ak ( );
      ProducaoComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao a partir de sua chave primária
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
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
      ProducaoComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao a partir dos campos de sua chave alternativa
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="numSerieLme">
    /// Empresa da Producao
    /// </param>
    /// <param name="datInicio">
    /// Data da producao
    /// </param>
    /// <param name="numSerieLmtrn">
    /// Número de série do turno
    /// </param>
    /// <param name="numSerielmlm">
    /// Número de série da linha de montagem
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, AcessoSql acessoSql, Int64 numSerieLme, DateTime datInicio, Int64 numSerieLmtrn, Int64 numSerielmlm )
    {
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      ChavePrimaria = new Pk();
      ChaveAlternativa = new Ak ( numSerieLme, datInicio, numSerieLmtrn, numSerielmlm );
      ProducaoComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao a partir de sua chave alternativa
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
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
      ChavePrimaria = new Pk();
      ChaveAlternativa = chaveAlternativa;
      ProducaoComum ( );
    }

    private void ProducaoComum ( )
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
        new Coluna( 0, "serie_producao", "Série", typeof( Int64 ), true, false, false ),
        new Coluna( 1, "serie_empresa", "Série Empresa", typeof( Int64 ), false, true, false ),
        new Coluna( 2, "codigo_empresa", nameof( Empresa ), typeof( String ), false, false, false ),
        new Coluna( 3, "data_producao", "Data", typeof( DateTime ), false, true, true ),
        new Coluna( 4, "serie_turno", "Série Turno", typeof( Int64 ), false, true, false ),
        new Coluna( 5, "codigo_turno", nameof( Turno ), typeof( String ), false, false, true ),
        new Coluna( 6, "serie_linha_montagem", "Série Linha", typeof( Int64 ), false, true, false ),
        new Coluna( 7, "codigo_linha_montagem", "Linha", typeof( String ), false, false, true )
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
        SetColumnValue ( "serie_producao", Convert.ToInt64 ( dataReader [ "serie_producao" ] ) );
        SetColumnValue ( "serie_empresa", Convert.ToInt64 ( dataReader [ "serie_empresa" ].ToString ( ) ) );
        SetColumnValue ( "codigo_empresa", dataReader [ "codigo_empresa" ].ToString ( ) );
        SetColumnValue ( "data_producao", Convert.ToDateTime ( dataReader [ "data_producao" ] ) );
        SetColumnValue ( "serie_turno", Convert.ToInt64 ( dataReader [ "serie_turno" ].ToString ( ) ) );
        SetColumnValue ( "codigo_turno", dataReader [ "codigo_turno" ].ToString ( ) );
        SetColumnValue ( "serie_linha_montagem", Convert.ToInt64 ( dataReader [ "serie_linha_montagem" ].ToString ( ) ) );
        SetColumnValue ( "codigo_linha_montagem", dataReader [ "codigo_linha_montagem" ].ToString ( ) );
      }
      else
      {
        SetColumnValue ( "serie_producao", 0L );
        SetColumnValue ( "serie_empresa", 0L );
        SetColumnValue ( "codigo_empresa", String.Empty );
        SetColumnValue ( "data_producao", DateTime.MinValue );
        SetColumnValue ( "serie_turno", 0L );
        SetColumnValue ( "codigo_turno", String.Empty );
        SetColumnValue ( "serie_linha_montagem", 0L );
        SetColumnValue ( "codigo_linha_montagem", String.Empty );
      }
      ChavePrimaria = new Pk ( NumSerieLmp );
      ChaveAlternativa = new Ak ( NumSerieLme, DatInicio, NumSerieLmtrn, NumSerieLmlm );
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
        sql.AppendFormat ( Sql.QueryRecordPk, _acessoSql.OpenQuery, chavePrimaria.NumSerieLmp );
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
        , chaveAlternativa.NumSerieLme
        , chaveAlternativa.DatInicio
        , chaveAlternativa.NumSerieLmtrn
        , chaveAlternativa.NumSerieLmlm
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
      , NumSerieLme
      , DatInicio.ToString ( "MM/dd/yyyy" )
      , NumSerieLmtrn
      , NumSerieLmlm
      );
      var numSerie = 0L;
      Ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
      if ( Ok )
      {
        NumSerieLmp = numSerie;
        ChavePrimaria = new Pk ( NumSerieLmp );
        ChaveAlternativa = new Ak ( NumSerieLme, DatInicio, NumSerieLmtrn, NumSerieLmlm );
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
      , NumSerieLmp
      , NumSerieLme
      , DatInicio.ToString ( "MM/dd/yyyy" )
      , NumSerieLmtrn
      , NumSerieLmlm
      );
      Ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    public void Delete ( )
    {
      Ok = false;
      if ( NumSerieLmp != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _acessoSql.OpenQuery, NumSerieLmp );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, NumSerieLmp );
        Ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
      }
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave alternativa fornecida
    /// </summary>
    /// <param name="numSerieLme">
    /// Número de série da empresa
    /// </param>
    /// <param name="datInicio">
    /// Data de início da produção
    /// </param>
    /// <param name="numSerieLmtrn">
    /// Número de série do turno
    /// </param>
    /// <param name="numSerieLmlm">
    /// Número de série da linha de montagem
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExisteAk ( Int64 numSerieLme, DateTime datInicio, Int64 numSerieLmtrn, Int64 numSerieLmlm )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExisteAk, _acessoSql.OpenQuery, numSerieLme, datInicio, numSerieLmtrn, numSerieLmlm );
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
      sql.AppendFormat ( Sql.ExisteAk, _acessoSql.OpenQuery, chaveAlternativa.NumSerieLme, chaveAlternativa.DatInicio, chaveAlternativa.NumSerieLmtrn, chaveAlternativa.NumSerieLmlm );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave primaria fornecida
    /// </summary>
    /// <param name="numSerieLmp">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExistePk ( Int64 numSerieLmp )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _acessoSql.OpenQuery, numSerieLmp );
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
      sql.AppendFormat ( Sql.ExistePk, _acessoSql.OpenQuery, chavePrimaria.NumSerieLmp );
      return ExisteComum ( sql.ToString ( ) );
    }

    private Boolean ExisteComum ( String sql )
    {
      return Db.Existe ( _bcoSql, sql );
    }

    public Boolean Valido ( )
    {
      var valido = DatInicio != DateTime.MinValue
        && NumSerieLme != 0 && NumSerieLme != Int64.MinValue
        && NumSerieLmtrn != 0 && NumSerieLmtrn != Int64.MinValue
        && NumSerieLmlm != 0 && NumSerieLmlm != Int64.MinValue;
      return valido;
    }
  }
}
