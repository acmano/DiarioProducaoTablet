using System;
using System.Data.SqlClient;
using Lorenzetti.DB;

namespace DiarioProducao.Classes.Comum
{
  public static class Db
  {

    /// <summary>
    /// Insere o registro corrente no banco de dados
    /// </summary>
    /// <param name="bcoSql">todo: describe bcoSql parameter on Insert</param>
    /// <param name="tabName">todo: describe tabName parameter on Insert</param>
    /// <param name="sql">todo: describe sql parameter on Insert</param>
    /// <param name="numSerie">todo: describe numSerie parameter on Insert</param>
    public static Boolean Insert ( Config bcoSql, String tabName, String sql, ref Int64 numSerie )
    {
      Boolean ok;
      using ( var conexao = new Banco ( bcoSql ) )
      {
        conexao.BeginTransaction ( );
        ok = Insert ( conexao, tabName, sql, ref numSerie );
        if ( ok )
        {
          conexao.Commit ( );
        }
        else
        {
          conexao.Rollback ( );
        }
      }
      return ok;
    }

    /// <summary>
    /// Insere o registro corrente no banco de dados
    /// </summary>
    /// <param name="conexao">todo: describe conexao parameter on Insert</param>
    /// <param name="tabName">todo: describe tabName parameter on Insert</param>
    /// <param name="sql">todo: describe sql parameter on Insert</param>
    /// <param name="numSerie">todo: describe numSerie parameter on Insert</param>
    public static Boolean Insert ( Banco conexao, String tabName, String sql, ref Int64 numSerie )
    {
      var ok = false;
      try
      {
        conexao.NonQuery ( sql );
        if ( conexao.LinhasAfetadas > 0 )
        {
          ok = true;
          numSerie = conexao.ObtemIdentity ( tabName );
        }
      }
      catch ( Exception )
      {
        ok = false;
      }
      return ok;
    }

    /// <summary>
    /// Insere o registro corrente no banco de dados
    /// </summary>
    /// <param name="bcoSql">todo: describe bcoSql parameter on Insert</param>
    /// <param name="sql">todo: describe sql parameter on Insert</param>
    public static Boolean Insert ( Config bcoSql, String sql )
    {
      Boolean ok;
      using ( var conexao = new Banco( bcoSql ) )
      {
        conexao.BeginTransaction ( );
        ok = Insert ( conexao, sql );
        if ( ok )
        {
          conexao.Commit ( );
        }
        else
        {
          conexao.Rollback ( );
        }
      }
      return ok;
    }


    /// <summary>
    /// Insere o registro corrente no banco de dados
    /// </summary>
    /// <param name="conexao">todo: describe conexao parameter on Insert</param>
    /// <param name="sql">todo: describe sql parameter on Insert</param>
    public static Boolean Insert ( Banco conexao, String sql )
    {
      var ok = false;
      try
      {
        conexao.NonQuery ( sql );
        if ( conexao.LinhasAfetadas > 0 )
        {
          ok = true;
        }
      }
      catch ( Exception )
      {
        ok = false;
      }
      return ok;
    }

    /// <summary>
    /// Atualiza o registro corrente no banco de dados
    /// </summary>
    /// <param name="bcoSql">todo: describe bcoSql parameter on Update</param>
    /// <param name="sql">todo: describe sql parameter on Update</param>
    public static Boolean Update ( Config bcoSql, String sql )
    {
      Boolean ok;
      using ( var conexao = new Banco ( ( bcoSql ) ) )
      {
        conexao.BeginTransaction ( );
        ok = Update ( conexao, sql );
        if ( ok )
        {
          conexao.Commit ( );
        }
        else
        {
          conexao.Rollback ( );
        }
      }
      return ok;
    }

    /// <summary>
    /// Atualiza o registro corrente no banco de dados
    /// </summary>
    /// <param name="bcoSql">todo: describe bcoSql parameter on Update</param>
    /// <param name="sql">todo: describe sql parameter on Update</param>
    public static Boolean Update ( Banco bcoSql, String sql )
    {
      var ok = false;
      try
      {
        bcoSql.NonQuery ( sql );
        if ( bcoSql.LinhasAfetadas > 0 )
        {
          ok = true;
        }
      }
      catch ( Exception )
      {
        ok = false;
      }
      return ok;
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    /// <param name="bcoSql">todo: describe bcoSql parameter on Delete</param>
    /// <param name="sqlDependencia">todo: describe sqlDependencia parameter on Delete</param>
    /// <param name="sqlDelete">todo: describe sqlDelete parameter on Delete</param>
    public static Boolean Delete ( Config bcoSql, String sqlDependencia, String sqlDelete )
    {
      var ok = false;
      if ( !Existe ( bcoSql, sqlDependencia ) )
      {
        using ( var conexao = new Banco ( ( bcoSql ) ) )
        {
          conexao.BeginTransaction ( );
          ok = Delete ( conexao, sqlDelete );
          if ( ok )
          {
            conexao.Commit ( );
          }
          else
          {
            conexao.Rollback ( );
          }
        }
      }
      return ok;
    }

    /// <summary>
    /// Exclui o registro corrente do banco de dados
    /// </summary>
    /// <param name="bcoSql">todo: describe bcoSql parameter on Delete</param>
    /// <param name="sqlDelete">todo: describe sqlDelete parameter on Delete</param>
    public static Boolean Delete ( Banco bcoSql, String sqlDelete )
    {
      var ok = false;
      try
      {
        bcoSql.NonQuery ( sqlDelete );
        if ( bcoSql.LinhasAfetadas > 0 )
        {
          ok = true;
        }
      }
      catch ( Exception )
      {
        ok = false;
      }
      return ok;
    }

    /// <summary>
    /// Verifica se existe registro no banco de dados com a consulta fornecida
    /// </summary>
    /// <param name="bcoSql">
    /// Msdeuração de acesso ao banco de dados
    /// </param>
    /// <param name="sql">
    /// Comando de pesquisa
    /// </param>
    /// <returns>
    /// true - Existem registros
    /// false - Não existem registros
    /// </returns>
    public static Boolean Existe ( Config bcoSql,  String sql )
    {
      var result = false;
      using ( var banco = new Banco(bcoSql))
      {
        using ( SqlDataReader dR = banco.DataReader ( sql ) )
        {
          if ( dR.Read ( ) )
          {
            result = true;
          }
        }
      }
      return result;
    }

  }
}
