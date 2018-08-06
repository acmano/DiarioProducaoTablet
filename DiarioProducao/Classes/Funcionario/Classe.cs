using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DiarioProducao.Classes.Comum;
using Lorenzetti.DB;

namespace DiarioProducao.Classes.Funcionario
{
  public class Classe
  {

    public class Pk
    {
      private readonly Boolean _ok;

      public Int64 NumSerieLmf { get; set; }

      public Boolean Ok
      {
        get
        {
          return _ok;
        }
      }

      public Pk ( )
      {
        NumSerieLmf = 0L;
        _ok = false;
      }

      public Pk ( Int64 numSerieLmf )
      {
        if ( numSerieLmf != Int64.MinValue && numSerieLmf != 0L )
        {
          _ok = true;
          NumSerieLmf = numSerieLmf;
        }
      }

    }

    public class Ak
    {
      private readonly Boolean _ok;

      public Int64 MatriculaFuncionario { get; set; }

      public Boolean Ok
      {
        get
        {
          return _ok;
        }
      }

      public Ak ( )
      {
        MatriculaFuncionario = 0L;
        _ok = false;
      }

      public Ak ( Int64 matriculaFuncionario )
      {
        if ( matriculaFuncionario != 0L )
        {
          _ok = true;
          MatriculaFuncionario = matriculaFuncionario;
        }
        else
        {
          _ok = false;
          MatriculaFuncionario = 0L;

        }
      }

    }

    private readonly Config         _bcoSql;
    private readonly String         _openQuery;

    public Tabela Tabela { get; set; }

    public Pk ChavePrimaria { get; set; }

    public Ak ChaveAlternativa { get; set; }

    public Boolean Ok { get; set; }

    public List<Coluna> Colunas { get; set; }

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

    public Int64 MatriculaFuncionario
    {
      get
      {
        return GetColumnValueInt64( "matricula_funcionario" );
      }
      set
      {
        SetColumnValue ( "matricula_funcionario", value );
      }
    }

    public String NomeFuncionario
    {
      get
      {
        return GetColumnValueString ( "nome_funcionario" );
      }
      set
      {
        SetColumnValue ( "nome_funcionario", value );
      }
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_funcionario
    /// </summary>
    /// <returns>
    /// </returns>
    public Classe ( )
    {
      CriaColunas ( );
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_funcionario
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
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_funcionario a partir dos campos de sua chave primária
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
      ChavePrimaria = new Pk ( numSerieLmi );
      ChaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_funcionario a partir de sua chave primária
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
      ChavePrimaria = chavePrimaria;
      ChaveAlternativa = new Ak ( );
      ClasseComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_funcionario a partir de sua chave alternativa
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
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = chaveAlternativa;
      ClasseComum ( );
    }

    private void ClasseComum ( )
    {
      Ok = false;
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
      Colunas = new List<Coluna>
      {
        new Coluna( 0, "serie_funcionario", "Série", typeof( Int64 ), true, false, false ),
        new Coluna( 1, "matricula_funcionario", "Código", typeof( Int64 ), false, true, false ),
        new Coluna( 2, "nome_funcionario", "Descrição", typeof( String ), false, false, false )
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
    private String GetColumnValueString ( String columnName )
    {
      //  if ( _colunas.Find(x => x.GetId() == columnName) )
      var c = Colunas.Find ( item => item.ColumnName == columnName );
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
      var c = Colunas.Find ( item => item.ColumnName == columnName );
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
      var c = Colunas.Find ( item => item.ColumnName == columnName );
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
        SetColumnValue ( "serie_funcionario", Convert.ToInt64 ( dataReader [ "serie_funcionario" ] ) );
        SetColumnValue ( "matricula_funcionario", Convert.ToInt64( dataReader [ "matricula_funcionario" ] ) );
        SetColumnValue ( "nome_funcionario", dataReader [ "nome_funcionario" ].ToString ( ) );
      }
      else
      {
        SetColumnValue ( "serie_funcionario", 0L );
        SetColumnValue ( "matricula_funcionario", 0L );
        SetColumnValue ( "nome_funcionario", String.Empty );
      }
      ChavePrimaria = new Pk ( NumSerieLmf );
      ChaveAlternativa = new Ak ( MatriculaFuncionario );
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_funcionario através de sua chave primária
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
        sql.AppendFormat ( Sql.QueryRecordPk, _openQuery, chavePrimaria.NumSerieLmf );
        SelectComum ( sql.ToString ( ) );
      }
      else
      {
        PopulaRecord ( );
      }
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_funcionario através de sua chave Alternativa
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
        sql.AppendFormat ( Sql.QueryRecordAk, _openQuery, chaveAlternativa.MatriculaFuncionario );
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
            Ok = true;
          }
          else
          {
            PopulaRecord ( );
          }
        }
      }
    }

    private Boolean ValidaMatriculaFuncionario ( )
    {
      return MatriculaFuncionario != 0L;
    }

    private Boolean ValidaNomeFuncionario ( )
    {
      return !String.IsNullOrEmpty ( NomeFuncionario );
    }

    private Boolean Valida ( )
    {
      var retorno = ValidaMatriculaFuncionario ( )
                        && ValidaNomeFuncionario ( )
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
          , MatriculaFuncionario
          , NomeFuncionario
        );
        var numSerie = 0L;
        Ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
        if ( Ok )
        {
          NumSerieLmf = numSerie;
          ChavePrimaria = new Pk ( NumSerieLmf );
          ChaveAlternativa = new Ak ( MatriculaFuncionario );
          Select ( ChavePrimaria );
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
        , NumSerieLmf
        , MatriculaFuncionario
        , NomeFuncionario
      );
      Ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    public void Delete ( )
    {
      Ok = false;
      if ( NumSerieLmf != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _openQuery, NumSerieLmf );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, _openQuery, NumSerieLmf );
        Ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
      }
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave alternativa fornecida
    /// </summary>
    /// <param name="matriculaFuncionario">
    /// Matrícula do Funcionário
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExisteAk ( Int64 matriculaFuncionario )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExisteAk, _openQuery, matriculaFuncionario );
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
      sql.AppendFormat ( Sql.ExisteAk, _openQuery, chaveAlternativa.MatriculaFuncionario );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave primaria fornecida
    /// </summary>
    /// <param name="numSerieLmf">
    /// Número de série do registro
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExistePk ( Int64 numSerieLmf )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _openQuery, numSerieLmf );
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
      sql.AppendFormat ( Sql.ExistePk, _openQuery, chavePrimaria.NumSerieLmf );
      return ExisteComum ( sql.ToString ( ) );
    }

    private Boolean ExisteComum ( String sql )
    {
      return Db.Existe ( _bcoSql, sql );
    }
  }
}
