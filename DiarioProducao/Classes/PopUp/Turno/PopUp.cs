using System;
using Android.App;
using Android.Views;
using Android.Widget;
using DiarioProducao.Classes.Comum;

namespace DiarioProducao.Classes.PopUp.Turno
{
  using Android;

  public class PopUp
  {

    private readonly Activity  _activityMestre;
    private readonly AcessoSql _acessoSql;
    private readonly String    _codEmpresa;
    private Adapter            _popUpAdapter;
    private ListView           _listViewDetalhe;
    private AlertDialog        _alertDialog;
    private Boolean            _aberto;
    private ProgressDialog     _progress;
    private ProgressDialogTask _task;


    public bool Aberto
    {
      get { return _aberto; }
    }

    public PopUp ( Activity activityMestre, AcessoSql acessoSql, String codEmpresa )
    {
      _activityMestre = activityMestre;
      _acessoSql = acessoSql;
      _codEmpresa = codEmpresa;
    }
    
    public void Exibe ( EditText descricao )
    {
      _aberto = true;
      var inflater = LayoutInflater.From ( _activityMestre );
//aqui      var vwPopUp = inflater.Inflate ( Resource.Layout.PopUp, null );
//aqui      _listViewDetalhe = vwPopUp.FindViewById<ListView> ( Resource.Id.lvwDetalhe );
      _listViewDetalhe.ItemClick += delegate ( object sender, AdapterView.ItemClickEventArgs e )
      {
        var popUpDetalhe = _popUpAdapter.GetItemAtPosition ( e.Position );
        descricao.Tag = popUpDetalhe.NumSerie;
        descricao.Text = popUpDetalhe.Descricao;
        _aberto = false;
        _alertDialog.Dismiss ( );
      };
      ThreadingProcessar ( );
      var builder = new AlertDialog.Builder ( _activityMestre );
      builder.SetTitle ( Sql.TabDescription );
//aqui      builder.SetView ( vwPopUp );
      builder.SetNegativeButton ( "Fechar", delegate
      {
        _aberto = false;
        _alertDialog.Dismiss ( );
      } );
      builder.SetCancelable ( false );
      _alertDialog = builder.Create ( );
    }

    private void ThreadingProcessar ( )
    {
      _progress = new ProgressDialog ( _activityMestre ) { Indeterminate = false };
      _progress.SetCancelable ( false );
      _progress.SetMessage ( "Aguarde, carregando ..." );
      _progress.Show ( );
      _task = new ProgressDialogTask
      {
        RunInBackgroundMethod = Processar,
        OnPostExecuteMethod = TerminarProcesso
      };
      _task.Execute ( );
    }

    private Boolean Processar ( )
    {
      _popUpAdapter = new Adapter ( _activityMestre, _acessoSql, _codEmpresa );
      return ( true );
    }

    private Boolean TerminarProcesso ( )
    {
      _listViewDetalhe.Adapter = _popUpAdapter;
      _progress.Dismiss ( );
      _alertDialog.Show ( );
      return ( true );
    }

  }
}