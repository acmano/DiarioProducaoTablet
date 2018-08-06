using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DiarioProducao.Classes.Comum;
using Lorenzetti.DB;

namespace DiarioProducao.Classes.ProducaoOcorrencia
{
  public class Classe
  {
    public class Pk
    {
      private readonly Boolean _ok;

      public Int64 NumSerieLmpoco { get; set; }

      public Boolean Ok => _ok;

      public Pk ( )
      {
        NumSerieLmpoco = 0L;
        _ok = false;
      }

      public Pk ( Int64 numSerieLmpoco )
      {
        if ( numSerieLmpoco != Int64.MinValue && numSerieLmpoco != 0L )
        {
          _ok = true;
          NumSerieLmpoco = numSerieLmpoco;
        }
      }

    }

    public class Ak
    {

      public Int64 NumSerieLmp { get; set; }

      public DateTime DatOcorrencia { get; set; }

      public Boolean Ok { get; }

      public Ak ( )
      {
        NumSerieLmp = 0L;
        DatOcorrencia = DateTime.MinValue;
        Ok = false;
      }

      public Ak ( Int64 numSerieLmp, DateTime datOcorrencia )
      {
        if ( numSerieLmp != 0L && datOcorrencia != DateTime.MinValue )
        {
          Ok = true;
          NumSerieLmp = numSerieLmp;
          DatOcorrencia = datOcorrencia;
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

    public Int64 NumSerieLmpoco
    {
      get
      {
        return Convert.ToInt64 ( GetColumnValue ( "serie_producao_ocorrencia" ) );
      }
      set
      {
        SetColumnValue ( "serie_producao_ocorrencia", value );
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

    public Int64 NumSerieLmf
    {
      get
      {
        return Convert.ToInt64 ( GetColumnValue ( "serie_funcionario" ) );
      }
      set
      {
        SetColumnValue ( "serie_funcionario", value );
      }
    }

    public String CodOcorrencia
    {
      get { return GetColumnValue("codigo_ocorrencia").ToString(); }
      set { SetColumnValue("codigo_ocorrencia", value);}
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

    public Int64 NumMatricula
    {
      get
      {
        return Convert.ToInt64 ( GetColumnValue ( "matricula_funcionario" ) );
      }
      set
      {
        SetColumnValue ( "matricula_funcionario", value );
      }
    }

    public String NomFuncionario
    {
      get
      {
        return GetColumnValue ( "nome_funcionario" ).ToString ( );
      }
      set
      {
        SetColumnValue ( "nome_funcionario", value );
      }
    }


    public DateTime DatOcorrencia
    {
      get
      {
        return Convert.ToDateTime ( GetColumnValue ( "data_ocorrencia" ) );
      }
      set
      {
        SetColumnValue ( "data_ocorrencia", value );
      }
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_producao_ocorrencia
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
      ProducaoOcorrenciaComum ( );
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_producao_ocorrencia
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
      ProducaoOcorrenciaComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_ocorrencia a partir dos campos de sua chave primária
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="acessoSql">
    /// Configuração de tipo de acesso
    /// </param>
    /// <param name="numSerieLmpoco">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, AcessoSql acessoSql, Int64 numSerieLmpoco )
    {
      Ok = true;
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      ChavePrimaria = new Pk ( numSerieLmpoco );
      ChaveAlternativa = new Ak ( );
      ProducaoOcorrenciaComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_ocorrencia a partir de sua chave primária
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
      ProducaoOcorrenciaComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_ocorrencia a partir dos campos de sua chave alternativa
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
    /// <param name="datOcorrencia">
    /// Número de série da linha de montagem
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, AcessoSql acessoSql, Int64 numSerieLmp, DateTime datOcorrencia )
    {
      _bcoSql = bcoSql;
      _acessoSql = acessoSql;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( numSerieLmp, datOcorrencia );
      ProducaoOcorrenciaComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_producao_ocorrencia a partir de sua chave alternativa
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
      ProducaoOcorrenciaComum ( );
    }

    private void ProducaoOcorrenciaComum ( )
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
        new Coluna( 0, "serie_producao_ocorrencia", "Série", typeof( Int64 ), true, false, false ),
        new Coluna( 1, "serie_producao", "Série Produção", typeof( Int64 ), false, true, false ),
        new Coluna( 2, "serie_ocorrencia", "Série Ocorrência", typeof( Int64 ), false, false, false ),
        new Coluna( 3, "serie_funcionario", "Série Funcionário", typeof( Int64 ), false, false, false ),
        new Coluna( 4, "codigo_ocorrencia", "Ocorrência", typeof( String ), false, false, false ),
        new Coluna( 5, "descricao_ocorrencia", "Descrição", typeof( String ), false, false, false ),
        new Coluna( 6, "matricula_funcionario", "Matrícula", typeof( Int64 ), false, false, false ),
        new Coluna( 7, "nome_funcionario", "Nome", typeof( String ), false, false, false ),
        new Coluna( 8, "data_ocorrencia", "Data", typeof( DateTime ), false, true, false )
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
        SetColumnValue ( "serie_producao_ocorrencia", Convert.ToInt64 ( dataReader [ "serie_producao_ocorrencia" ] ) );
        SetColumnValue ( "serie_producao", Convert.ToInt64 ( dataReader [ "serie_producao" ] ) );
        SetColumnValue ( "serie_ocorrencia", Convert.ToInt64 ( dataReader [ "serie_ocorrencia" ] ) );
        SetColumnValue ( "serie_funcionario", Convert.ToInt64 ( dataReader [ "serie_funcionario" ] ) );
        SetColumnValue ( "codigo_ocorrencia", dataReader [ "codigo_ocorrencia" ].ToString ( ) );
        SetColumnValue ( "descricao_ocorrencia", dataReader [ "descricao_ocorrencia" ].ToString ( ) );
        SetColumnValue ( "matricula_funcionario", Convert.ToInt64 ( dataReader [ "matricula_funcionario" ] ) );
        SetColumnValue ( "nome_funcionario", dataReader [ "nome_funcionario" ].ToString ( ) );
        SetColumnValue ( "data_ocorrencia", Convert.ToDateTime ( dataReader [ "data_ocorrencia" ] ) );
      }
      else
      {
        SetColumnValue ( "serie_producao_ocorrencia", 0L );
        SetColumnValue ( "serie_producao", 0L );
        SetColumnValue ( "serie_ocorrencia", 0L );
        SetColumnValue ( "serie_funcionario", 0L );
        SetColumnValue ( "codigo_ocorrencia", String.Empty );
        SetColumnValue ( "descricao_ocorrencia", String.Empty );
        SetColumnValue ( "matricula_funcionario", 0L );
        SetColumnValue ( "nome_funcionario", String.Empty );
        SetColumnValue ( "data_ocorrencia", DateTime.MinValue );
      }
      ChavePrimaria = new Pk ( NumSerieLmpoco );
      ChaveAlternativa = new Ak ( NumSerieLmp, DatOcorrencia );
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
        sql.AppendFormat ( Sql.QueryRecordPk, _acessoSql.OpenQuery, chavePrimaria.NumSerieLmpoco );
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
          , chaveAlternativa.DatOcorrencia
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
        , NumSerieLmoco
        , NumSerieLmf
        , DatOcorrencia
      );
      var numSerie = 0L;
      Ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
      if ( Ok )
      {
        NumSerieLmpoco = numSerie;
        ChavePrimaria = new Pk ( NumSerieLmpoco );
        ChaveAlternativa = new Ak ( NumSerieLmp, DatOcorrencia );
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
        , NumSerieLmpoco
        , NumSerieLmp
        , NumSerieLmoco
        , NumSerieLmf
        , DatOcorrencia
      );
      Ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    public void Delete ( )
    {
      Ok = false;
      if ( NumSerieLmpoco != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _acessoSql.OpenQuery, NumSerieLmpoco );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, _acessoSql.OpenQuery, NumSerieLmpoco );
        Ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
      }
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave alternativa fornecida
    /// </summary>
    /// <param name="numSerieLmp">
    /// Número de série da empresa
    /// </param>
    /// <param name="datOcorrencia">
    /// Número de série do item
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExisteAk ( Int64 numSerieLmp, DateTime datOcorrencia )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExisteAk, _acessoSql.OpenQuery, numSerieLmp, datOcorrencia );
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
      sql.AppendFormat ( Sql.ExisteAk, _acessoSql.OpenQuery, chaveAlternativa.NumSerieLmp, chaveAlternativa.DatOcorrencia );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave primaria fornecida
    /// </summary>
    /// <param name="numSerieLmpoco">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExistePk ( Int64 numSerieLmpoco )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _acessoSql.OpenQuery, numSerieLmpoco );
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
      sql.AppendFormat ( Sql.ExistePk, _acessoSql.OpenQuery, chavePrimaria.NumSerieLmpoco );
      return ExisteComum ( sql.ToString ( ) );
    }

    private Boolean ExisteComum ( String sql )
    {
      return Db.Existe ( _bcoSql, sql );
    }

    public Boolean Valido ( )
    {
      var valido = NumSerieLmpoco != 0L && NumSerieLmpoco != Int64.MinValue
                    && NumSerieLmp != 0L && NumSerieLmp != Int64.MinValue
                    && NumSerieLmoco != 0L && NumSerieLmoco != Int64.MinValue
                    && NumSerieLmf != 0L && NumSerieLmf != Int64.MinValue
                    && DatOcorrencia != DateTime.MinValue;
      return valido;
    }
  }
}
