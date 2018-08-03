using System;

namespace DiarioProducao.Classes.Comum
{
  public class Coluna
  {
    private Int32 _order;
    private String _columnName;
    private String _columnDescription;
    private Type _columnType;
    private Boolean _isPk;
    private Boolean _isAk;
    private Boolean _isVisible;
    private Object _value;

    public Int32 Order
    {
      get
      {
        return _order;
      }
      set
      {
        _order = value;
      }
    }
    public String ColumnName
    {
      get
      {
        return _columnName;
      }
      set
      {
        _columnName = value;
      }
    }
    public String ColumnDescription
    {
      get
      {
        return _columnDescription;
      }
      set
      {
        _columnDescription = value;
      }
    }
    public Type ColumnType
    {
      get
      {
        return _columnType;
      }
      set
      {
        _columnType = value;
      }
    }
    public Boolean IsPk
    {
      get
      {
        return _isPk;
      }
      set
      {
        _isPk = value;
      }
    }
    public Boolean IsAk
    {
      get
      {
        return _isAk;
      }
      set
      {
        _isAk = value;
      }
    }
    public Boolean IsVisible
    {
      get
      {
        return _isVisible;
      }
      set
      {
        _isVisible = value;
      }
    }
    public Object Value
    {
      get
      {
        return _value;
      }
      set
      {
        _value = value;
      }
    }

    public Coluna ( int order, string columnName, string columnDescription, Type columnType, bool isPk, bool isAk, bool isVisible )
    {
      _order = order;
      _columnName = columnName;
      _columnDescription = columnDescription;
      _columnType = columnType;
      _isPk = isPk;
      _isAk = isAk;
      _isVisible = isVisible;
    }

  }
}
