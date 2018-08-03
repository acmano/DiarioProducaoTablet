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
      private Int64 _numSerieLmpiqtd;
      private readonly Boolean _ok;

      public Int64 NumSerieLmpiqtd
      {
        get
        {
          return _numSerieLmpiqtd;
        }
        set
        {
          _numSerieLmpiqtd = value;
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
        _numSerieLmpiqtd = 0L;
        _ok = false;
      }

      public Pk ( Int64 numSerieLmpiqtd )
      {
        if ( numSerieLmpiqtd != Int64.MinValue && numSerieLmpiqtd != 0L )
        {
          _ok = true;
          _numSerieLmpiqtd = numSerieLmpiqtd;
        }
      }

    }

    public class Ak
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

      public Ak ( )
      {
        _numSerieLmpi = 0L;
        _ok = false;
      }

      public Ak ( Int64 numSerieLmpi )
      {
        if ( numSerieLmpi != 0L )
        {
          _ok = true;
          _numSerieLmpi = numSerieLmpi;
        }
        else
        {
          _ok = false;
          _numSerieLmpi = 0L;

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
    /// Cria uma instância vazia do objeto lor_man_producao_item_quantidade
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
    /// Cria uma instância vazia do objeto lor_man_producao_item_quantidade
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
    /// Cria uma instância do objeto lor_man_producao_item_quantidade a partir dos campos de sua chave primária
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração de tipo de acesso ao banco
    /// </param>
    /// <param name="numSerieLmpiqtd">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery, Int64 numSerieLmpiqtd )
    {
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      CriaColunas ( );
      _chavePrimaria = new Pk ( numSerieLmpiqtd );
      _chaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_item_quantidade a partir de sua chave primária
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
    /// Cria uma instância do objeto lor_man_producao_item_quantidade a partir de sua chave alternativa
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
      _colunas.Add ( new Coluna ( 0, "serie_producao_item_quantidade", "Série", typeof ( Int64 ), true, false, false ) );
      _colunas.Add ( new Coluna ( 1, "serie_producao_item", "Série Produção Item", typeof ( Int64 ), false, true, false ) );
      _colunas.Add ( new Coluna ( 2, "serie_funcionario", "Série Funcionário", typeof ( Int64 ), false, false, false ) );
      _colunas.Add ( new Coluna ( 3, "quantidade_informada", "Qtd.Informada", typeof ( Int64 ), false, false, true ) );
      _colunas.Add ( new Coluna ( 4, "quantidade_apontada", "Qtd.Apontada", typeof ( Int64 ), false, false, true ) );
      _colunas.Add ( new Coluna ( 5, "data_apontamento", "Data Apontamento", typeof ( DateTime ), false, false, true ) );
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
      _chavePrimaria = new Pk ( NumSerieLmpiqtd );
      _chaveAlternativa = new Ak ( NumSerieLmpi );
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_producao_item_quantidade através de sua chave primária
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
        sql.AppendFormat ( Sql.QueryRecordPk, _openQuery, chavePrimaria.NumSerieLmpiqtd );
        SelectComum ( sql.ToString ( ) );
      }
      else
      {
        PopulaRecord ( );
      }
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_producao_item_quantidade através de sua chave Alternativa
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
            _ok = true;
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
        _ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
        if ( _ok )
        {
          NumSerieLmpiqtd = numSerie;
          _chavePrimaria = new Pk ( NumSerieLmpiqtd );
          _chaveAlternativa = new Ak ( NumSerieLmpi );
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
        , NumSerieLmpiqtd
        , NumSerieLmpi
        , NumSerieLmf
        , QuantidadeInformada
        , QuantidadeApontada
        , DataApontamento
      );
      _ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    public void Delete ( )
    {
      _ok = false;
      if ( NumSerieLmpiqtd != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _openQuery, NumSerieLmpiqtd );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, _openQuery, NumSerieLmpiqtd );
        _ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
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
    /// <param name="numSerieLmpiqtd">
    /// Número de série do registro de empresa
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExistePk ( Int64 numSerieLmpiqtd )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _openQuery, numSerieLmpiqtd );
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
      sql.AppendFormat ( Sql.ExistePk, _openQuery, chavePrimaria.NumSerieLmpiqtd );
      return ExisteComum ( sql.ToString ( ) );
    }

    private Boolean ExisteComum ( String sql )
    {
      return Db.Existe ( _bcoSql, sql );
    }
  }
}
