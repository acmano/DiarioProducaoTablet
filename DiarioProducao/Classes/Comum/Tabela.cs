using System;

namespace DiarioProducao.Classes.Comum
{
  public class Tabela
  {

    private String _tabName;
    private String _tabDescription;
    private Int64 _linhasAfetadas;
    private Boolean _ok;

    public String TabName
    {
      get
      {
        return _tabName;
      }
      set
      {
        _tabName = value;
      }
    }
    public String TabDescription
    {
      get
      {
        return _tabDescription;
      }
      set
      {
        _tabDescription = value;
      }
    }
    public Int64 LinhasAfetadas
    {
      get
      {
        return _linhasAfetadas;
      }
      set
      {
        _linhasAfetadas = value;
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

    public Tabela ( string tabName, string tabDescription )
    {
      _tabName = tabName;
      _tabDescription = tabDescription;
      _linhasAfetadas = 0;
      _ok = true;
    }

  }
}
