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
      private Int64 _numSerieLmtrntp;
      private readonly Boolean _ok;

      public Int64 NumSerieLmtrntp
      {
        get
        {
          return _numSerieLmtrntp;
        }
        set
        {
          _numSerieLmtrntp = value;
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
        _numSerieLmtrntp = 0L;
        _ok = false;
      }

      public Pk ( Int64 numSerieLmtrntp )
      {
        if ( numSerieLmtrntp != Int64.MinValue && numSerieLmtrntp != 0L )
        {
          _ok = true;
          _numSerieLmtrntp = numSerieLmtrntp;
        }
      }

    }

    public class Ak
    {
      private String _codTurnoTipo;
      private readonly Boolean _ok;

      public String CodTurnoTipo
      {
        get
        {
          return _codTurnoTipo;
        }
        set
        {
          _codTurnoTipo = value;
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
        _codTurnoTipo = String.Empty;
        _ok = false;
      }

      public Ak ( String codTurnoTipo )
      {
        if ( !String.IsNullOrEmpty ( codTurnoTipo ) )
        {
          _ok = true;
          _codTurnoTipo = codTurnoTipo;
        }
      }

    }

    private readonly Config  _bcoSql;
    private readonly String _openQuery;
    private Boolean         _ok;
    private Tabela          _turnoTipo;
    private Pk              _chavePrimaria;
    private Ak              _chaveAlternativa;
    private List<Coluna>    _colunas;

    public Tabela Tabela
    {
      get
      {
        return _turnoTipo;
      }
      set
      {
        _turnoTipo = value;
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
      _ok = true;
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = new Ak ( );
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
      _ok = true;
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = new Ak ( );
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
      _ok = true;
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      _chavePrimaria = new Pk ( numSerieLmtrntp );
      _chaveAlternativa = new Ak ( );
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
      _ok = true;
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      _chavePrimaria = chavePrimaria;
      _chaveAlternativa = new Ak ( );
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
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = new Ak ( codTurnoTipo );
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
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = chaveAlternativa;
      TurnoTipoComum ( );
    }

    private void TurnoTipoComum ( )
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
      _colunas.Add ( new Coluna ( 0, "serie_turno_tipo", "Série", typeof ( Int64 ), true, false, false ) );
      _colunas.Add ( new Coluna ( 1, "codigo_turno_tipo", "Código", typeof ( String ), false, true, true ) );
      _colunas.Add ( new Coluna ( 2, "descricao_turno_tipo", "Descrição", typeof ( String ), false, false, true ) );
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
      _chavePrimaria = new Pk ( NumSerieLmtrntp );
      _chaveAlternativa = new Ak ( CodTurnoTipo );
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
