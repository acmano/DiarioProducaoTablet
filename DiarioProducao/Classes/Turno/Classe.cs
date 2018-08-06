using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DiarioProducao.Classes.Comum;
using Lorenzetti.DB;

namespace DiarioProducao.Classes.Turno
{
  public class Classe
  {

    public class Pk
    {

      public Int64 NumSerieLmtrn { get; set; }
      public Boolean Ok { get; }

      public Pk ( )
      {
        NumSerieLmtrn = 0L;
        Ok = false;
      }

      public Pk ( Int64 numSerieLmtrn )
      {
        if ( numSerieLmtrn != Int64.MinValue && numSerieLmtrn != 0L )
        {
          Ok = true;
          NumSerieLmtrn = numSerieLmtrn;
        }
      }

    }

    public class Ak
    {

      public String CodEmpresa { get; set; }

      public string CodTurno { get; set; }

      public Boolean Ok { get; }

      public Ak ( )
      {
        CodEmpresa = String.Empty;
        CodTurno = String.Empty;
        Ok = false;
      }

      public Ak ( String codEmpresa, String codTurno )
      {
        if ( !String.IsNullOrEmpty ( codEmpresa ) && !String.IsNullOrEmpty ( codTurno ) )
        {
          Ok = true;
          CodEmpresa = codEmpresa;
          CodTurno = codTurno;
        }
        else
        {
          Ok = false;
          CodEmpresa = String.Empty;
          CodTurno = String.Empty;

        }
      }

    }

    private readonly Config         _bcoSql;
    private readonly String         _openQuery;
    private readonly String         _codEmpresa;

    public Tabela Tabela { get; set; }

    public Pk ChavePrimaria { get; set; }

    public Ak ChaveAlternativa { get; set; }

    public Boolean Ok { get; set; }

    public List<Coluna> Colunas { get; set; }

    public Int64 NumSerieLmtrn
    {
      get
      {
        return GetColumnValueInt64 ( "serie_turno" );
      }
      set
      {
        SetColumnValue ( "serie_turno", value );
      }
    }

    public String CodEmpresa
    {
      get
      {
        return GetColumnValueString ( "codigo_empresa" );
      }
      set
      {
        Empresa = new Empresa.Classe ( _bcoSql, value );
        SetColumnValue ( "codigo_empresa", Empresa.CodEmpresa );
      }
    }

    public String CodTurnoTipo
    {
      get
      {
        return GetColumnValueString ( "codigo_turno_tipo" );
      }
      set
      {
        TurnoTipo = new TurnoTipo.Classe ( _bcoSql, _openQuery, value );
        SetColumnValue ( "codigo_turno_tipo", TurnoTipo.CodTurnoTipo );
      }
    }

    public String CodTurno
    {
      get
      {
        return GetColumnValueString ( "codigo_turno" );
      }
      set
      {
        SetColumnValue ( "codigo_turno", value );
      }
    }

    public String DenTurno
    {
      get
      {
        return GetColumnValueString ( "descricao_turno" );
      }
      set
      {
        SetColumnValue ( "descricao_turno", value );
      }
    }

    public String HorInicio
    {
      get
      {
        return GetColumnValueString ( "hora_inicio" );
      }
      set
      {
        SetColumnValue ( "hora_inicio", value );
      }
    }

    public Int64 QtdMinutos
    {
      get
      {
        return GetColumnValueInt64 ( "duracao" );
      }
      set
      {
        SetColumnValue ( "duracao", value );
      }
    }

    public DateTime DatValidadeIni
    {
      get
      {
        return GetColumnValueDateTime ( "data_validade_inicio" );
      }
      set
      {
        SetColumnValue ( "data_validade_inicio", value );
      }
    }

    public DateTime DatValidadeFim
    {
      get
      {
        return GetColumnValueDateTime ( "data_validade_fim" );
      }
      private set
      {
        SetColumnValue ( "data_validade_fim", value );
      }
    }

    public Empresa.Classe Empresa { get; set; }

    public TurnoTipo.Classe TurnoTipo { get; set; }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_turno
    /// </summary>
    /// <returns>
    /// </returns>
    public Classe ( )
    {
      CriaColunas ( );
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( );
      TurnoComum ( );
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_turno
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
      TurnoComum ( );
    }

    /// <summary>
    /// Cria uma instância vazia do objeto lor_man_turno
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração de tipo de acesso ao banco
    /// </param>
    /// <param name="codEmpresa">
    /// Cigo da Empresa
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery, String codEmpresa )
    {
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      CriaColunas ( );
      _codEmpresa = codEmpresa;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( );
      TurnoComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_turno a partir dos campos de sua chave primária
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração de tipo de acesso ao banco
    /// </param>
    /// <param name="numSerieLmtrn">
    /// Número de série do registro
    /// </param>
    /// <param name="codEmpresa">
    /// Código da Empresa
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery, String codEmpresa, Int64 numSerieLmtrn )
    {
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      CriaColunas ( );
      _codEmpresa = codEmpresa;
      ChavePrimaria = new Pk ( numSerieLmtrn );
      ChaveAlternativa = new Ak ( );
      TurnoComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_turno a partir de sua chave primária
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
    /// <param name="codEmpresa">
    /// Código da Empresa
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery, String codEmpresa, Pk chavePrimaria )
    {
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      CriaColunas ( );
      _codEmpresa = codEmpresa;
      ChavePrimaria = chavePrimaria;
      ChaveAlternativa = new Ak ( );
      TurnoComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_turno a partir dos campos de sua chave alternativa
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração de tipo de acesso ao banco
    /// </param>
    /// <param name="codEmpresa">
    /// Código da empresa
    /// </param>
    /// <param name="codTurno">
    /// Código do turno
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery, String codEmpresa, String codTurno )
    {
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      CriaColunas ( );
      _codEmpresa = codEmpresa;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = new Ak ( codEmpresa, codTurno );
      TurnoComum ( );
    }

    /// <summary>
    /// Cria uma instância do objeto lor_man_turno a partir de sua chave alternativa
    /// </summary>
    /// <param name="bcoSql">
    /// Configuração do banco de dados alvo
    /// </param>
    /// <param name="openQuery">
    /// Configuração de tipo de acesso ao banco
    /// </param>
    /// <param name="codEmpresa">
    /// Código da Empresa
    /// </param>
    /// <param name="chaveAlternativa">
    /// Chave alternativa da tabela
    /// </param>
    /// <returns>
    /// </returns>
    public Classe ( Config bcoSql, String openQuery, String codEmpresa, Ak chaveAlternativa )
    {
      _bcoSql = bcoSql;
      _openQuery = openQuery;
      CriaColunas ( );
      _codEmpresa = codEmpresa;
      ChavePrimaria = new Pk ( );
      ChaveAlternativa = chaveAlternativa;
      TurnoComum ( );
    }

    private void TurnoComum ( )
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
        if ( Ok )
        {
          Empresa = new Empresa.Classe ( _bcoSql, _openQuery, CodEmpresa );
          if ( Empresa.Ok )
          {
            TurnoTipo = new TurnoTipo.Classe ( _bcoSql, _openQuery, CodTurnoTipo );
          }
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
        new Coluna( 0, "serie_turno", "Série", typeof( Int64 ), true, false, false ),
        new Coluna( 1, "codigo_empresa", nameof( Classes.Empresa ), typeof( String ), false, true, false ),
        new Coluna( 2, "codigo_turno_tipo", "Tipo Turno", typeof( String ), false, true, true ),
        new Coluna( 3, "codigo_turno", nameof( Turno ), typeof( String ), false, true, true ),
        new Coluna( 4, "descricao_turno", "Descrição", typeof( String ), false, false, true ),
        new Coluna( 5, "hora_inicio", "Hora inicial", typeof( String ), false, false, true ),
        new Coluna( 6, "duracao", "Duração", typeof( Int64 ), false, false, true ),
        new Coluna( 7, "data_validade_inicio", "Validade Inicial", typeof( DateTime ), false, false, true ),
        new Coluna( 8, "data_validade_fim", "Validade Final", typeof( DateTime ), false, false, true )
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
        SetColumnValue ( "serie_turno", Convert.ToInt64 ( dataReader [ "serie_turno" ] ) );
        SetColumnValue ( "codigo_empresa", dataReader [ "codigo_empresa" ].ToString ( ) );
        SetColumnValue ( "codigo_turno_tipo", dataReader [ "codigo_turno_tipo" ].ToString ( ) );
        SetColumnValue ( "codigo_turno", dataReader [ "codigo_turno" ].ToString ( ) );
        SetColumnValue ( "descricao_turno", dataReader [ "descricao_turno" ].ToString ( ) );
        SetColumnValue ( "hora_inicio", dataReader [ "hora_inicio" ].ToString ( ) );
        SetColumnValue ( "duracao", Convert.ToInt64 ( dataReader [ "duracao" ] ) );
        SetColumnValue ( "data_validade_inicio", Convert.ToDateTime ( dataReader [ "data_validade_inicio" ] ) );
        if ( dataReader [ "data_validade_fim" ].ToString ( ) != String.Empty )
        {
          SetColumnValue ( "data_validade_fim", Convert.ToDateTime ( dataReader [ "data_validade_fim" ] ) );

        }
        else
        {
          SetColumnValue ( "data_validade_fim", null );
        }
        Empresa = new Empresa.Classe ( _bcoSql, _openQuery, CodEmpresa );
        TurnoTipo = new TurnoTipo.Classe ( _bcoSql, _openQuery, CodTurnoTipo );
      }
      else
      {
        SetColumnValue ( "serie_turno", 0L );
        SetColumnValue ( "codigo_empresa", String.Empty );
        SetColumnValue ( "codigo_turno_tipo", String.Empty );
        SetColumnValue ( "codigo_turno", String.Empty );
        SetColumnValue ( "descricao_turno", String.Empty );
        SetColumnValue ( "hora_inicio", DateTime.MinValue );
        SetColumnValue ( "duracao", 0L );
        SetColumnValue ( "data_validade_inicio", DateTime.Today );
        SetColumnValue ( "data_validade_fim", null );
        Empresa = new Empresa.Classe ( _bcoSql, _openQuery );
        TurnoTipo = new TurnoTipo.Classe ( _bcoSql, _openQuery );
      }
      ChavePrimaria = new Pk ( NumSerieLmtrn );
      ChaveAlternativa = new Ak ( CodEmpresa, CodTurno );
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_turno através de sua chave primária
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
        sql.AppendFormat ( Sql.QueryRecordPk, _openQuery, chavePrimaria.NumSerieLmtrn );
        SelectComum ( sql.ToString ( ) );
      }
      else
      {
        PopulaRecord ( );
      }
    }

    /// <summary>
    /// Faz a leitura no banco de dados de um registro da tabela lor_man_turno através de sua chave Alternativa
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
        sql.AppendFormat ( Sql.QueryRecordAk, _openQuery, chaveAlternativa.CodEmpresa, chaveAlternativa.CodTurno );
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

    private Boolean ValidaCodEmpresa ( )
    {
      var retorno = false;
      if ( !String.IsNullOrEmpty ( CodEmpresa ) && !String.IsNullOrWhiteSpace ( CodEmpresa ) )
      {
        Empresa = new Empresa.Classe ( _bcoSql, _openQuery, CodEmpresa );
        if ( Empresa.Ok )
        {
          retorno = true;
        }
      }
      return retorno;
    }

    private Boolean ValidaCodTurnoTipo ( )
    {
      var retorno = false;
      if ( !String.IsNullOrEmpty ( CodTurnoTipo ) && !String.IsNullOrWhiteSpace ( CodTurnoTipo ) )
      {
        TurnoTipo = new TurnoTipo.Classe ( _bcoSql, _openQuery, CodTurnoTipo );
        if ( TurnoTipo.Ok )
        {
          retorno = true;
        }
      }
      return retorno;
    }

    private Boolean ValidaCodTurno ( )
    {
      var retorno = false;
      if ( !String.IsNullOrEmpty ( CodTurno ) && !String.IsNullOrWhiteSpace ( CodTurno ) )
      {
        if ( !ExisteAk ( CodEmpresa, CodTurno ) )
        {
          retorno = true;
        }
      }
      return retorno;
    }

    private Boolean ValidaDenTurno ( )
    {
      var retorno = !String.IsNullOrEmpty ( DenTurno ) && !String.IsNullOrWhiteSpace ( DenTurno );
      return retorno;
    }

    private Boolean ValidaHorInicio ( )
    {
      var retorno = !String.IsNullOrEmpty ( HorInicio ) && !String.IsNullOrWhiteSpace ( HorInicio );
      return retorno;
    }

    private Boolean ValidaQtdMinutos ( )
    {
      var retorno = QtdMinutos != Int64.MinValue && QtdMinutos != 0L;
      return retorno;
    }

    private Boolean Valida ( )
    {
      var retorno = ValidaCodEmpresa ( );
      retorno = retorno & ValidaCodTurnoTipo ( );
      retorno = retorno & ValidaCodTurno ( );
      retorno = retorno & ValidaDenTurno ( );
      retorno = retorno & ValidaHorInicio ( );
      retorno = retorno & ValidaQtdMinutos ( );
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
          , CodEmpresa
          , CodTurnoTipo
          , CodTurno
          , DenTurno
          , HorInicio
          , QtdMinutos
          , DatValidadeIni == DateTime.MinValue ? "" : DatValidadeIni.ToString ( "MM/dd/yyyy" )
          , DatValidadeFim == DateTime.MinValue ? "" : DatValidadeFim.ToString ( "MM/dd/yyyy" )
        );
        var numSerie = 0L;
        Ok = Db.Insert ( _bcoSql, Sql.TableName, sql.ToString ( ), ref numSerie );
        if ( Ok )
        {
          NumSerieLmtrn = numSerie;
          ChavePrimaria = new Pk ( NumSerieLmtrn );
          ChaveAlternativa = new Ak ( CodEmpresa, CodTurno );
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
        , NumSerieLmtrn
        , DenTurno
        , HorInicio
        , QtdMinutos
        , DatValidadeIni == DateTime.MinValue ? "" : DatValidadeIni.ToString ( "MM/dd/yyyy" )
        , DatValidadeFim == DateTime.MinValue ? "" : DatValidadeFim.ToString ( "MM/dd/yyyy" )
      );
      Ok = Db.Update ( _bcoSql, sql.ToString ( ) );
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    public void Delete ( )
    {
      Ok = false;
      if ( NumSerieLmtrn != 0L )
      {
        var sqlDependencia = new StringBuilder ( );
        sqlDependencia.Clear ( );
        sqlDependencia.AppendFormat ( Sql.ExisteDependencias, _openQuery, NumSerieLmtrn );
        var sqlDelete = new StringBuilder ( );
        sqlDelete.Clear ( );
        sqlDelete.AppendFormat ( Sql.DeleteRecord, _openQuery, NumSerieLmtrn );
        Ok = Db.Delete ( _bcoSql, sqlDependencia.ToString ( ), sqlDelete.ToString ( ) );
      }
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave alternativa fornecida
    /// </summary>
    /// <param name="codEmpresa">
    /// Código da empresa
    /// </param>
    /// <param name="codTurno">
    /// Código do turno
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExisteAk ( String codEmpresa, String codTurno )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExisteAk, _openQuery, codEmpresa, codTurno );
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
      sql.AppendFormat ( Sql.ExisteAk, _openQuery, chaveAlternativa.CodEmpresa );
      return ExisteComum ( sql.ToString ( ) );
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com os campos da chave primaria fornecida
    /// </summary>
    /// <param name="numSerieLmtrn">
    /// Número de série do registro de empresa
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existe registro
    /// </returns>
    public Boolean ExistePk ( Int64 numSerieLmtrn )
    {
      var sql = new StringBuilder ( );
      sql.Clear ( );
      sql.AppendFormat ( Sql.ExistePk, _openQuery, numSerieLmtrn );
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
      sql.AppendFormat ( Sql.ExistePk, _openQuery, chavePrimaria.NumSerieLmtrn );
      return ExisteComum ( sql.ToString ( ) );
    }

    private Boolean ExisteComum ( String sql )
    {
      return Db.Existe ( _bcoSql, sql );
    }
  }
}
