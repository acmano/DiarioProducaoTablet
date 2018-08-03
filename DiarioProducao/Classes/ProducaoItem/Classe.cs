using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DiarioProducao.Classes.Comum;
using DiarioProducao.Classes.Comum;
using Lorenzetti.DB;

namespace DiarioProducao.Classes.ProducaoItem
{
  public class Classe
  {
    public class Pk
    {
      private Int64 _numSerieLmpi;
      private readonly Boolean _ok;

      public Int64 NumSerieLmpi
      {
        get
        {
          return _numSerieLmpi;
        }
        set
        {
          _numSerieLmpi = value;
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
        _numSerieLmpi = 0L;
        _ok = false;
      }

      public Pk ( Int64 numSerieLmpi )
      {
        if ( numSerieLmpi != Int64.MinValue && numSerieLmpi != 0L )
        {
          _ok = true;
          _numSerieLmpi = numSerieLmpi;
        }
      }

    }

    public class Ak
    {
      private Int64 _numSerieLmp;
      private Int64 _numSerieLmie;
      private readonly Boolean _ok;

      public Int64 NumSerieLmp
      {
        get
        {
          return _numSerieLmp;
        }
        set
        {
          _numSerieLmp = value;
        }
      }

      public Int64 NumSerieLmie
      {
        get
        {
          return _numSerieLmie;
        }
        set
        {
          _numSerieLmie = value;
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
        _numSerieLmp = 0L;
        _numSerieLmie = 0L;
        _ok = false;
      }

      public Ak ( Int64 numSerieLmp, Int64 numSerieLmie )
      {
        if ( numSerieLmp != 0L && numSerieLmie != 0L )
        {
          _ok = true;
          _numSerieLmp = numSerieLmp;
          _numSerieLmie = numSerieLmie;
        }
      }

    }

    private readonly Config    _bcoSql;
    private readonly AcessoSql _acessoSql;
    private Boolean            _ok;
    private Tabela             _producaoItem;
    private Pk                 _chavePrimaria;
    private Ak                 _chaveAlternativa;
    private List<Coluna>       _colunas;

    public Tabela Tabela
    {
      get
      {
        return _producaoItem;
      }
      set
      {
        _producaoItem = value;
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

    public Int64 NumSerieLmpi
    {
      get
      {
        return GetColumnValueInt64( "serie_producao_item" );
      }
      set
      {
        SetColumnValue ( "serie_producao_item", value );
      }
    }

    public Int64 NumSerieLmp
    {
      get
      {
        return GetColumnValueInt64( "serie_producao" ) ;
      }
      set
      {
        SetColumnValue ( "serie_producao", value );
      }
    }

    public Int64 NumSerieLmie
    {
      get
      {
        return GetColumnValueInt64( "serie_item_empresa" ) ;
      }
      set
      {
        SetColumnValue ( "serie_item_empresa", value );
      }
    }

    public String CodItem
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

    public String DenItem
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

    public DateTime HorInicio
    {
      get
      {
        return GetColumnValueDateTime( "hora_inicio" ) ;
      }
      set
      {
        SetColumnValue ( "hora_inicio", value );
      }
    }

    public DateTime HorFim
    {
      get
      {
        return GetColumnValueDateTime( "hora_fim" ) ;
      }
      set
      {
        SetColumnValue ( "hora_fim", value );
      }
    }

    public Int64 QtdProduzida
    {
      get
      {
        return GetColumnValueInt64( "quantidade_informada" ) ;
      }
      set
      {
        SetColumnValue ( "quantidade_informada", value );
      }
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_producao_item
    /// </summary>
    /// <returns>
    /// </returns>
    public Classe ( )
    {
      _ok = true;
      _bcoSql = null;
      _acessoSql = null;
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_producao_item
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
      _ok = true;
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_item a partir dos campos de sua chave primária
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="acessoSql">
    /// Configuração de tipo de acesso
    /// </param>
    /// <param name="numSerieLmpi">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, AcessoSql acessoSql, Int64 numSerieLmpi )
    {
      _ok = true;
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      _chavePrimaria = new Pk ( numSerieLmpi );
      _chaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_item a partir de sua chave primária
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
      _ok = true;
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      _chavePrimaria = chavePrimaria;
      _chaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_item a partir dos campos de sua chave alternativa
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
    /// <param name="numSerieLmie">
    /// Número de série da linha de montagem
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, AcessoSql acessoSql, Int64 numSerieLmp, Int64 numSerieLmie )
    {
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = new Ak ( numSerieLmp, numSerieLmie );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_item a partir de sua chave alternativa
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
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = chaveAlternativa;
      ClasseComum ( );
    }

    private void ClasseComum ( )
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
      _colunas.Add ( new Coluna ( 0, "serie_producao_item", "Série", typeof ( Int64 ), true, false, false ) );
      _colunas.Add ( new Coluna ( 1, "serie_producao", "Série Produção", typeof ( Int64 ), true, false, false ) );
      _colunas.Add ( new Coluna ( 2, "serie_item_empresa", "Série Item Empresa", typeof ( Int64 ), true, false, false ) );
      _colunas.Add ( new Coluna ( 3, "codigo_item", "Item", typeof ( String ), false, true, false ) );
      _colunas.Add ( new Coluna ( 4, "descricao_item", "Descrição", typeof ( String ), false, false, false ) );
      _colunas.Add ( new Coluna ( 5, "hora_inicio", "Início", typeof ( DateTime ), false, true, true ) );
      _colunas.Add ( new Coluna ( 6, "hora_fim", "Fim", typeof ( DateTime ), false, true, false ) );
      _colunas.Add ( new Coluna ( 7, "quantidade_informada", "Quantidade", typeof ( Int64 ), false, true, false ) );
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
        SetColumnValue ( "serie_producao_item", Convert.ToInt64 ( dataReader [ "serie_producao_item" ] ) );
        SetColumnValue ( "serie_producao", Convert.ToInt64 ( dataReader [ "serie_producao" ] ) );
        SetColumnValue ( "serie_item_empresa", Convert.ToInt64 ( dataReader [ "serie_item_empresa" ] ) );
        SetColumnValue ( "codigo_item", Convert.ToInt64 ( dataReader [ "codigo_item" ].ToString ( ) ) );
        SetColumnValue ( "descricao_item", dataReader [ "descricao_item" ].ToString ( ) );
        SetColumnValue ( "hora_inicio", Convert.ToDateTime ( dataReader [ "hora_inicio" ] ) );
        SetColumnValue ( "hora_fim", Convert.ToDateTime ( dataReader [ "hora_fim" ].ToString ( ) ) );
        SetColumnValue ( "quantidade_informada", Convert.ToInt64 ( dataReader [ "quantidade_informada" ].ToString ( ) ) );
      }
      else
      {
        SetColumnValue ( "serie_producao_item", 0L );
        SetColumnValue ( "serie_producao", 0L );
        SetColumnValue ( "serie_item_empresa", 0L );
        SetColumnValue ( "codigo_item", String.Empty );
        SetColumnValue ( "descricao_item", String.Empty );
        SetColumnValue ( "hora_inicio", DateTime.MinValue );
        SetColumnValue ( "hora_fim", DateTime.MinValue );
        SetColumnValue ( "quantidade_informada", 0L );
      }
      _chavePrimaria = new Pk ( NumSerieLmpi );
      _chaveAlternativa = new Ak ( NumSerieLmp, NumSerieLmie );
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
        sql.AppendFormat ( Sql.QueryRecordPk, _acessoSql.OpenQuery, chavePrimaria.NumSerieLmpi );
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
          , chaveAlternativa.NumSerieLmie
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
        , NumSerieLmie
      );
      var numSerie = 0L;
      _ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
      if ( _ok )
      {
        NumSerieLmpi = numSerie;
        _chavePrimaria = new Pk ( NumSerieLmpi );
        _chaveAlternativa = new Ak ( NumSerieLmp, NumSerieLmie );
        Select ( _chavePrimaria );
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
        , NumSerieLmpi
        , NumSerieLmp
        , NumSerieLmie
      );
      _ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    public void Delete ( )
    {
      _ok = false;
      if ( NumSerieLmpi != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _acessoSql.OpenQuery, NumSerieLmpi );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, NumSerieLmpi );
        _ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
      }
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave alternativa fornecida
    /// </summary>
    /// <param name="numSerieLmp">
    /// Número de série da empresa
    /// </param>
    /// <param name="numSerieLmie">
    /// Número de série do item
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExisteAk ( Int64 numSerieLmp, Int64 numSerieLmie )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExisteAk, _acessoSql.OpenQuery, numSerieLmp, numSerieLmie );
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
      sql.AppendFormat ( Sql.ExisteAk, _acessoSql.OpenQuery, chaveAlternativa.NumSerieLmp, chaveAlternativa.NumSerieLmie );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave primaria fornecida
    /// </summary>
    /// <param name="numSerieLmpi">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExistePk ( Int64 numSerieLmpi )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _acessoSql.OpenQuery, numSerieLmpi );
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
      sql.AppendFormat ( Sql.ExistePk, _acessoSql.OpenQuery, chavePrimaria.NumSerieLmpi );
      return ExisteComum ( sql.ToString ( ) );
    }

    private Boolean ExisteComum ( String sql )
    {
      return Db.Existe ( _bcoSql, sql );
    }

    public Boolean Valido ( )
    {
      var valido = NumSerieLmp != 0L && NumSerieLmp != Int64.MinValue
                 && NumSerieLmie != 0L && NumSerieLmie != Int64.MinValue
                 && NumSerieLmpi != 0L && NumSerieLmpi != Int64.MinValue;
      return valido;
    }
  }
}
