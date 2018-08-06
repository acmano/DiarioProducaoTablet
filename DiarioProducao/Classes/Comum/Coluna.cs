using System;

namespace DiarioProducao.Classes.Comum
{
  public class Coluna
  {

    public Int32 Order { get; set; }
    public String ColumnName { get; set; }
    public String ColumnDescription { get; set; }
    public Type ColumnType { get; set; }
    public Boolean IsPk { get; set; }
    public Boolean IsAk { get; set; }
    public Boolean IsVisible { get; set; }
    public Object Value { get; set; }

    public Coluna ( int order, string columnName, string columnDescription, Type columnType, bool isPk, bool isAk, bool isVisible )
    {
      Order = order;
      ColumnName = columnName;
      ColumnDescription = columnDescription;
      ColumnType = columnType;
      IsPk = isPk;
      IsAk = isAk;
      IsVisible = isVisible;
    }

  }
}
