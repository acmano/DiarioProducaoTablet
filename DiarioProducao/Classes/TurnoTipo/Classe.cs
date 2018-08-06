using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DiarioProducao.Classes.Comum;
using Lorenzetti.DB;

namespace DiarioProducao.Classes.TurnoTipo
{
  public class Classe
  {
    public class Pk
    {

      public Int64 NumSerieLmtrntp { get; set; }

      public Boolean Ok { get; }

      public Pk ( )
      {
        NumSerieLmtrntp = 0L;
        Ok = false;
      }

      public Pk ( Int64 numSerieLmtrntp )
      {
        if ( numSerieLmtrntp != Int64.MinValue && numSerieLmtrntp != 0L )
        {
          Ok = true;
          NumSerieLmtrntp = numSerieLmtrntp;
        }
      }

    }

    public class Ak
    {
      private readonly Boolean _ok;

      public String CodTurnoTipo { get; set; }

      public Boolean Ok
      {
        get
        {
          return _ok;
        }
      }

      public Ak ( )
      {
        CodTurnoTipo = String.Empty;
        _ok = false;
      }

      public Ak ( String codTurnoTipo )
      {
        if ( !String.IsNullOrEmpty ( codTurnoTipo ) )
        {
          _ok = true;
          CodTurnoTipo = codTurnoTipo;
        }
      }

    }

    private readonly Config  _bcoSql;
    private readonly String _openQuery;

    public Tabela Tabela { get; set; }

    public Pk ChavePrimaria { get; set; }

    public Ak ChaveAlternativa { get; set; }

    public Boolean Ok { get; set; }

    public List<Coluna> Colunas { get; set; }

    public Int64 NumSerieLmtrntp
    {
      get
      {
        return Convert.ToInt64 ( GetColumnValue ( "serie_turno_tipo" ) );
      }
      set
      {
        SetColumnValue ( "serie_turno_tipo", value );
      }
    }

    public String CodTurnoTipo
    {
      get
      {
        return GetColumnValue ( "codigo_turno_tipo" ).ToString ( );
      }
      set
      {
        SetColumnValue ( "codigo_turno_tipo", value );
      }
    }

    public String DenTurnoTipo
    {
      get
      {
        return GetColumnValue ( "descricao_turno_tipo" ).ToString ( );
      }
      set
      {
        SetColumnValue ( "descricao_turno_tipo", value );
      }
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_turno_tipo
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
    /// Cria uma instância vazia do objeto lor_man_turno_tipo
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
    /// Cria uma instância do objeto lor_man_turno_tipo a partir dos campos de sua chave primária
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração de tipo de acesso ao Banco
    /// </param>
    /// <param name="numSerieLmtrntp">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery, Int64 numSerieLmtrntp )
    {
      Ok = true;
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      ChavePrimaria = new Pk ( numSerieLmtrntp );
      ChaveAlternativa = new Ak ( );
      TurnoTipoComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_turno_tipo a partir de sua chave primária
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
      TurnoTipoComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_turno_tipo a partir dos campos de sua chave alternativa
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração de tipo de acesso ao banco
    /// </param>
    /// <param name="codTurnoTipo">
    /// Código da empresa
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery, String codTurnoTipo )
    {
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( codTurnoTipo );
      TurnoTipoComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_turno_tipo a partir de sua chave alternativa
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
      TurnoTipoComum ( );
    }

    private void TurnoTipoComum ( )
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
        new Coluna( 0, "serie_turno_tipo", "Série", typeof( Int64 ), true, false, false ),
        new Coluna( 1, "codigo_turno_tipo", "Código", typeof( String ), false, true, true ),
        new Coluna( 2, "descricao_turno_tipo", "Descrição", typeof( String ), false, false, true )
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
        SetColumnValue ( "serie_turno_tipo", Convert.ToInt64 ( dataReader [ "serie_turno_tipo" ] ) );
        SetColumnValue ( "codigo_turno_tipo", dataReader [ "codigo_turno_tipo" ].ToString ( ) );
        SetColumnValue ( "descricao_turno_tipo", dataReader [ "descricao_turno_tipo" ].ToString ( ) );
      }
      else
      {
        SetColumnValue ( "serie_turno_tipo", 0L );
        SetColumnValue ( "codigo_turno_tipo", String.Empty );
        SetColumnValue ( "descricao_turno_tipo", String.Empty );
      }
      ChavePrimaria = new Pk ( NumSerieLmtrntp );
      ChaveAlternativa = new Ak ( CodTurnoTipo );
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_turno_tipo através de sua chave primária
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
        sql.AppendFormat ( Sql.QueryRecordPk, _openQuery, chavePrimaria.NumSerieLmtrntp );
        SelectComum ( sql.ToString ( ) );
      }
      else
      {
        PopulaRecord ( );
      }
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_turno_tipo através de sua chave Alternativa
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
        sql.AppendFormat ( Sql.QueryRecordAk, _openQuery, chaveAlternativa.CodTurnoTipo );
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
        using ( SqlDataReader dR = dbSql.DataReader( sql ) )
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
    /// Verifica se existe registro no banco de dados com os campos da chave alternativa fornecida
    /// </summary>
    /// <param name="codTurnoTipo">
    /// Código do Tipo de Turno
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExisteAk ( String codTurnoTipo )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExisteAk, _openQuery, codTurnoTipo );
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
      sql.AppendFormat ( Sql.ExisteAk, _openQuery, chaveAlternativa.CodTurnoTipo );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave primaria fornecida
    /// </summary>
    /// <param name="numSerieLmtrntp">
    /// Número de série do registro de empresa
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExistePk ( Int64 numSerieLmtrntp )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _openQuery, numSerieLmtrntp );
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
      sql.AppendFormat ( Sql.ExistePk, _openQuery, chavePrimaria.NumSerieLmtrntp );
      return ExisteComum ( sql.ToString ( ) );
    }

    private Boolean ExisteComum ( String sql )
    {
      return Db.Existe ( _bcoSql, sql );
    }
  }
}
