using System;

namespace DiarioProducao.Classes.Comum
{
  public class Tabela
  {

    public String TabName { get; set; }
    public String TabDescription { get; set; }
    public Int64 LinhasAfetadas { get; set; }
    public Boolean Ok { get; set; }

    public Tabela ( string tabName, string tabDescription )
    {
      TabName = tabName;
      TabDescription = tabDescription;
      LinhasAfetadas = 0;
      Ok = true;
    }

  }
}
