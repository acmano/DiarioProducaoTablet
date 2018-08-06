using System;

namespace DiarioProducao.Classes.Comum
{
  public class AcessoSql
  {

    private readonly String _openQuery;
    private readonly String _server;
    private readonly String _userName;
    private readonly String _password;

    public enum AcessoTipo
    {
      Desenvolvimento
    , Producao
    }

    public string OpenQuery
    {
      get { return _openQuery; }
    }

    public string Server
    {
      get { return _server; }
    }

    public string UserName
    {
      get { return _userName; }
    }

    public string Password
    {
      get { return _password; }
    }

    public AcessoSql(AcessoTipo acessoTipo)
    {
      switch (acessoTipo)
      {
        case AcessoTipo.Desenvolvimento:
          _openQuery = SqlDef.BancoSQLDesenv.OpenQuery;
          _server = SqlDef.BancoSQLDesenv.Server;
          _userName = SqlDef.BancoSQLDesenv.Username;
          _password = SqlDef.BancoSQLDesenv.Password;
          break;
        case AcessoTipo.Producao:
          _openQuery = SqlDef.BancoSQLProd.OpenQuery;
          _server = SqlDef.BancoSQLProd.Server;
          _userName = SqlDef.BancoSQLProd.Username;
          _password = SqlDef.BancoSQLProd.Password;
          break;
        default:
          break;
      }
    }
  }
}