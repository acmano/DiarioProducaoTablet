using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DiarioProducao.Classes.Comum;
using Lorenzetti.DB;

namespace DiarioProducao.Classes.Item
{
  public class Classe
  {

    public class Pk
    {
      private Int64 _numSerieLmi;
      private readonly Boolean _ok;

      public Int64 NumSerieLmi
      {
        get
        {
          return _numSerieLmi;
        }
        set
        {
          _numSerieLmi = value;
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
        _numSerieLmi = 0L;
        _ok = false;
      }

      public Pk ( Int64 numSerieLmi )
      {
        if ( numSerieLmi != Int64.MinValue && numSerieLmi != 0L )
        {
          _ok = true;
          _numSerieLmi = numSerieLmi;
        }
      }

    }

    public class Ak
    {
      private String _codigoItem;
      private readonly Boolean _ok;

      public String CodigoItem
      {
        get
        {
          return _codigoItem;
        }
        set
        {
          _codigoItem = value;
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
        _codigoItem = String.Empty;
        _ok = false;
      }

      public Ak ( String codigoItem )
      {
        if ( !String.IsNullOrEmpty( codigoItem ) )
        {
          _ok = true;
          _codigoItem = codigoItem;
        }
        else
        {
          _ok = false;
          _codigoItem = String.Empty;

        }
      }

    }

    private readonly Config         _bcoSql;
    private readonly String         _openQuery;
    private Boolean                 _ok;
    private Tabela                  _turno;
    private Pk                      _chavePrimaria;
    private Ak                      _chaveAlternativa;
    private List<Coluna>            _colunas;

    public Tabela Tabela
    {
      get
      {
        return _turno;
      }
      set
      {
        _turno = value;
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

    public Int64 NumSerieLmi
    {
      get
      {
        return GetColumnValueInt64 ( "serie_item" );
      }
      set
      {
        SetColumnValue ( "serie_item", value );
      }
    }

    public String CodigoItem
    {
      get
      {
        return GetColumnValueString( "codigo_item" );
      }
      set
      {
        SetColumnValue ( "codigo_item", value );
      }
    }

    public String DescricaoItem
    {
      get
      {
        return GetColumnValueString( "descricao_item" );
      }
      set
      {
        SetColumnValue ( "descricao_item", value );
      }
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_item
    /// </summary>
    /// <returns>
    /// </returns>
    public Classe ( )
    {
      CriaColunas ( );
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_item
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
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_item a partir dos campos de sua chave primária
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração de tipo de acesso ao banco
    /// </param>
    /// <param name="numSerieLmi">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery, Int64 numSerieLmi )
    {
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      CriaColunas ( );
      _chavePrimaria = new Pk ( numSerieLmi );
      _chaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_item a partir de sua chave primária
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
      _chavePrimaria = chavePrimaria;
      _chaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_item a partir de sua chave alternativa
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
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = chaveAlternativa;
      ClasseComum ( );
    }

    private void ClasseComum ( )
    {
      _ok = false;
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
      _colunas = new List<Coluna> ( );
      _colunas.Add ( new Coluna ( 0, "serie_item", "Série", typeof ( Int64 ), true, false, false ) );
      _colunas.Add ( new Coluna ( 1, "codigo_item", "Código", typeof ( String ), false, true, false ) );
      _colunas.Add ( new Coluna ( 2, "descricao_item", "Descrição", typeof ( String ), false, false, false ) );
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
    private String GetColumnValueString ( String columnName )
    {
      //  if ( _colunas.Find(x => x.GetId() == columnName) )
      var c = _colunas.Find ( item => item.ColumnName == columnName );
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
      var c = _colunas.Find ( item => item.ColumnName == columnName );
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
      var c = _colunas.Find ( item => item.ColumnName == columnName );
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
        SetColumnValue ( "serie_item", Convert.ToInt64 ( dataReader [ "serie_item" ] ) );
        SetColumnValue ( "codigo_item", dataReader [ "codigo_item" ].ToString() );
        SetColumnValue ( "descricao_item", dataReader [ "descricao_item" ].ToString() );
      }
      else
      {
        SetColumnValue ( "serie_item", 0L );
        SetColumnValue ( "codigo_item", String.Empty );
        SetColumnValue ( "descricao_item", String.Empty );
      }
      _chavePrimaria = new Pk ( NumSerieLmi );
      _chaveAlternativa = new Ak ( CodigoItem );
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_item através de sua chave primária
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
        sql.AppendFormat ( Sql.QueryRecordPk, _openQuery, chavePrimaria.NumSerieLmi );
        SelectComum ( sql.ToString ( ) );
      }
      else
      {
        PopulaRecord ( );
      }
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_item através de sua chave Alternativa
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
        sql.AppendFormat ( Sql.QueryRecordAk, _openQuery, chaveAlternativa.CodigoItem );
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
            _ok = true;
          }
          else
          {
            PopulaRecord ( );
          }
        }
      }
    }

    private Boolean ValidaCodigoItem ( )
    {
      return !String.IsNullOrEmpty(CodigoItem);
    }

    private Boolean ValidaDescricaoItem ( )
    {
      return !String.IsNullOrEmpty(DescricaoItem);
    }

    private Boolean Valida ( )
    {
      var retorno = ValidaCodigoItem ( )
                        && ValidaDescricaoItem ( )
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
          , CodigoItem
          , DescricaoItem
        );
        var numSerie = 0L;
        _ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
        if ( _ok )
        {
          NumSerieLmi = numSerie;
          _chavePrimaria = new Pk ( NumSerieLmi );
          _chaveAlternativa = new Ak ( CodigoItem );
          Select ( _chavePrimaria );
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
        , NumSerieLmi
        , CodigoItem
        , DescricaoItem
      );
      _ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    public void Delete ( )
    {
      _ok = false;
      if ( NumSerieLmi != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _openQuery, NumSerieLmi );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, _openQuery, NumSerieLmi );
        _ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
      }
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave alternativa fornecida
    /// </summary>
    /// <param name="codigoItem">
    /// Código do Item
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExisteAk ( String codigoItem )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExisteAk, _openQuery, codigoItem );
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
      sql.AppendFormat ( Sql.ExisteAk, _openQuery, chaveAlternativa.CodigoItem );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave primaria fornecida
    /// </summary>
    /// <param name="numSerieLmi">
    /// Número de série do registro de empresa
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExistePk ( Int64 numSerieLmi )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _openQuery, numSerieLmi );
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
      sql.AppendFormat ( Sql.ExistePk, _openQuery, chavePrimaria.NumSerieLmi );
      return ExisteComum ( sql.ToString ( ) );
    }

    private Boolean ExisteComum ( String sql )
    {
      return Db.Existe ( _bcoSql, sql );
    }
  }
}
