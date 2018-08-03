using Android.App;

namespace DiarioProducao.Classes.Comum
{
  internal class Comum
  {

    public void MessageBox(Activity context, string pMsg)
    {
      var builder = new AlertDialog.Builder(context);
      builder.SetMessage(pMsg);
      builder.SetTitle("");
      builder.SetNegativeButton("OK", delegate { });
      builder.Show();
    }
  }
}