namespace BestRestaraunt.Objects
{
  public class Cuisine
  {
    private int _id;
    private string _name;

    public Cuisine(string CuisineName, int Id = 0)
    {
      _name = CuisineName;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetId(int newId)
    {
      _id = newId;
    }

    public int GetId()
    {
      return _id;
    }
  }
}
