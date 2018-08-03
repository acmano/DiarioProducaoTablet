using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DiarioProducao.Classes.Comum;
using DiarioProducao.Classes.Comum;
using Lorenzetti.DB;

namespace DiarioProducao.Classes.ProducaoItemOcorrencia
{
  public class Classe
  {
    public class Pk
    {
      private Int64 _numSerieLmpioco;
      private readonly Boolean _ok;

      public Int64 NumSerieLmpioco
      {
        get
        {
          return _numSerieLmpioco;
        }
        set
        {
          _numSerieLmpioco = value;
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
        _numSerieLmpioco = 0L;
        _ok = false;
      }

      public Pk ( Int64 numSerieLmpioco )
      {
        if ( numSerieLmpioco != Int64.MinValue && numSerieLmpioco != 0L )
        {
          _ok = true;
          _numSerieLmpioco = numSerieLmpioco;
        }
      }

    }

    public class Ak
    {
      private Int64 _numSerieLmpi;
      private Int64 _numSerieLmoco;
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

      public Int64 NumSerieLmoco
      {
        get
        {
          return _numSerieLmoco;
        }
        set
        {
          _numSerieLmoco = value;
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
        _numSerieLmoco = 0L;
        _ok = false;
      }

      public Ak ( Int64 numSerieLmpi, Int64 numSerieLmoco )
      {
        if ( numSerieLmpi != 0L && numSerieLmoco != 0L )
        {
          _ok = true;
          _numSerieLmpi = numSerieLmpi;
          _numSerieLmoco = numSerieLmoco;
        }
      }

    }

    private readonly Config    _bcoSql;
    private readonly AcessoSql _acessoSql;
    private Boolean            _ok;
    private Tabela             _producaoItemOcorrencia;
    private Pk                 _chavePrimaria;
    private Ak                 _chaveAlternativa;
    private List<Coluna>       _colunas;

    public Tabela Tabela
    {
      get
      {
        return _producaoItemOcorrencia;
      }
      set
      {
        _producaoItemOcorrencia = value;
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
    /// Cria uma instância vazia do objeto lor_man_producao_item_ocorrencia
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
      ProducaoItemOcorrenciaComum ( );
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_producao_item_ocorrencia
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
      ProducaoItemOcorrenciaComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_item_ocorrencia a partir dos campos de sua chave primária
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="acessoSql">
    /// Configuração de tipo de acesso
    /// </param>
    /// <param name="numSerieLmpioco">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, AcessoSql acessoSql, Int64 numSerieLmpioco )
    {
      _ok = true;
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      _chavePrimaria = new Pk ( numSerieLmpioco );
      _chaveAlternativa = new Ak ( );
      ProducaoItemOcorrenciaComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_item_ocorrencia a partir de sua chave primária
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
      ProducaoItemOcorrenciaComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_item_ocorrencia a partir dos campos de sua chave alternativa
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="acessoSql">
    /// Configuração de tipo de acesso
    /// </param>
    /// <param name="numSerieLmpi">
    /// Producao do item
    /// </param>
    /// <param name="numSerieLmoco">
    /// Número de série daocorrência
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, AcessoSql acessoSql, Int64 numSerieLmpi, Int64 numSerieLmoco )
    {
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      _chavePrimaria = new Pk ( );
      _chaveAlternativa = new Ak ( numSerieLmpi, numSerieLmoco );
      ProducaoItemOcorrenciaComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_item_ocorrencia a partir de sua chave alternativa
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
      ProducaoItemOcorrenciaComum ( );
    }

    private void ProducaoItemOcorrenciaComum ( )
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
      _colunas.Add ( new Coluna ( 0, "serie_producao_item_ocorrencia", "Série", typeof ( Int64 ), true, false, false ) );
      _colunas.Add ( new Coluna ( 1, "serie_producao_item", "Série Produção", typeof ( Int64 ), false, true, false ) );
      _colunas.Add ( new Coluna ( 2, "serie_ocorrencia", "Série Ocorrência", typeof ( Int64 ), false, true, false ) );
      _colunas.Add ( new Coluna ( 4, "codigo_ocorrencia", "Ocorrência", typeof ( String ), false, false, false ) );
      _colunas.Add ( new Coluna ( 5, "descricao_ocorrencia", "Descrição", typeof ( String ), false, false, false ) );
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
      _chavePrimaria = new Pk ( NumSerieLmpioco );
      _chaveAlternativa = new Ak ( NumSerieLmpi, NumSerieLmoco );
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
        sql.AppendFormat ( Sql.QueryRecordPk, _acessoSql.OpenQuery, chavePrimaria.NumSerieLmpioco );
        SelectComum ( sql.ToString ( ) );
      }
      else
      {
        PopulaRecord ( );
      }
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_producao_item_ocorrencia através de sua chave Alternativa
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
        , NumSerieLmpi
        , NumSerieLmoco
      );
      var numSerie = 0L;
      _ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
      if ( _ok )
      {
        NumSerieLmpioco = numSerie;
        _chavePrimaria = new Pk ( NumSerieLmpioco );
        _chaveAlternativa = new Ak ( NumSerieLmpi, NumSerieLmoco );
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
        , NumSerieLmpioco
        , NumSerieLmpi
        , NumSerieLmoco
      );
      _ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    public void Delete ( )
    {
      _ok = false;
      if ( NumSerieLmpioco != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _acessoSql.OpenQuery, NumSerieLmpioco );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, _acessoSql.OpenQuery, NumSerieLmpioco );
        _ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
      }
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave alternativa fornecida
    /// </summary>
    /// <param name="numSerieLmpi">
    /// Número de série da produção do item
    /// </param>
    /// <param name="numSerieLmoco">
    /// Número de série da ocorrência
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
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
    /// false - Não existe registro
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
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExistePk ( Int64 numSerieLmpioco )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _acessoSql.OpenQuery, numSerieLmpioco );
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
