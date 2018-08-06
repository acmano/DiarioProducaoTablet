using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Text;
using Android.Widget;
using Lorenzetti;
using DiarioProducao.Classes.Producao;
using DiarioProducao.Classes.Comum;

namespace DiarioProducao
{
  [
    Activity
    (
      Label = @"Diário de Produção"
    , Theme = "@style/LorenTheme"
    , Icon = "@drawable/DiarioProducao"
    , MainLauncher = true
    )
  ]

  public class MainActivity : Activity // AppCompatActivity
  {
    private String _codEmpresa;
    private EditText _datProducao;
    private EditText _txtDenLinha;
    private EditText _txtDenTurno;
    private TextView _txtProducoesQuantidade;
    private ListView _listViewProducoes;
    private Button _btnPesquisar;
    private Button _btnLimpar;
    private AcessoSql _acessoSql;
    private AcessoSql.AcessoTipo _acessoTipo;
    private Classes.PopUp.Linha.PopUp _popUpLinha;
    private Classes.PopUp.Turno.PopUp _popUpTurno;
    private EditText _editText;
    private DateTime _date;
    private string _txtDataPicker;
    private const int DateDialogId = 0;
    private ProgressDialog _progress;
    private ProgressDialogTask _task;
    private ListaProducao _listaProducao;
    private Intent _intentProducao;
    private eAmbiente _ambiente;

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate( savedInstanceState );
      _codEmpresa = "10";
      SetContentView( Resource.Layout.diario );
      AtualizaVersao();
      InitClasses();
      Campos();
    }

    protected override void OnResume()
    {
      base.OnResume();
      _intentProducao = null;
    }

    private void ListaProducaoClick(object sender, AdapterView.ItemClickEventArgs e)
    {
      var linhaSelecionada = _listaProducao.ListaProducaoAdapter.GetItemAtPosition( e.Position );
      _intentProducao = new Intent( this, typeof( ActProducao ) );
      _intentProducao.AddFlags( ActivityFlags.NewTask );
      _intentProducao.AddFlags( ActivityFlags.NoAnimation );
      SetaParametros( ref _intentProducao, linhaSelecionada );
      StartActivity( _intentProducao );
    }

    private void InitClasses()
    {
      _ambiente   = eAmbiente.Desenvolvimento;
      _acessoTipo = Classes.Comum.AcessoSql.AcessoTipo.Desenvolvimento;
      _acessoSql  = new Classes.Comum.AcessoSql( _acessoTipo );
      _popUpLinha = new Classes.PopUp.Linha.PopUp( this, _acessoSql, _codEmpresa );
      _popUpTurno = new Classes.PopUp.Turno.PopUp( this, _acessoSql, _codEmpresa );
    }

    private void SetaParametros(ref Intent intent, Classes.Producao.Detalhe linhaSelecionada)
    {
      intent.PutExtra( "Ambiente", Convert.ToInt64( _ambiente ) );
      intent.PutExtra( "NumSerieLmp", linhaSelecionada.NumSerieProducao );
      intent.PutExtra( "CodEmpresa", _codEmpresa );
      intent.PutExtra( "NumSerieLmlm", linhaSelecionada.ColunasDetalhe.NumSerieLmlm );
      intent.PutExtra( "NumSerieLmtrn", linhaSelecionada.ColunasDetalhe.NumSerieLmtrn );
      intent.PutExtra( "AcessoTipo", Convert.ToInt64( _acessoTipo ) );
    }

    private void AtualizaVersao()
    {
      //Altera versão do Sistema
      var novaVersao = new AlteraVersao();
      var lAtualizaVersao = false;
      novaVersao.TrocaVersao( this, "", ref lAtualizaVersao );
    }

    private void Campos()
    {
      DefineCampoDatProducao();
      DefineCampoDenLinha();
      DefineCampoDenTurno();
      DefineBotaoPesquisar();
      DefineBotaoLimpar();
      DefineListaProducoes();
      DefineCampoProducoesQtuantidade();
    }

    private void DefineCampoDatProducao()
    {
      _datProducao = FindViewById<EditText>( Resource.Id.txtData );     // Associa um objeto a um elemento do XML
      _datProducao.InputType = InputTypes.Null;                          // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
      _datProducao.Click += delegate
      {
        DataPopUp( _datProducao );
      };
    }

    private void DefineCampoDenLinha()
    {
      _txtDenLinha = FindViewById<EditText>( Resource.Id.txtDenLinha ); // Associa um objeto a um elemento do XML
      _txtDenLinha.InputType = InputTypes.Null;                          // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
      _txtDenLinha.Click += LinhaPopUp;
    }

    private void DefineCampoDenTurno()
    {
      _txtDenTurno = FindViewById<EditText>( Resource.Id.txtDenTurno ); // Associa um objeto a um elemento do XML
      _txtDenTurno.InputType = InputTypes.Null;                          // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
      _txtDenTurno.Click += TurnoPopUp;
    }

    private void DefineCampoProducoesQtuantidade()
    {
      _txtProducoesQuantidade = FindViewById<TextView>( Resource.Id.txtProducoesQuantidade ); // Associa um objeto a um elemento do XML
      _txtProducoesQuantidade.InputType = InputTypes.Null;                          // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineBotaoPesquisar()
    {
      _btnPesquisar = FindViewById<Button>( Resource.Id.btnPesquisar ); // Associa um objeto a um elemento do XML
      _btnPesquisar.Click += ThreadingProcessar;
    }

    private void DefineBotaoLimpar()
    {
      _btnLimpar = FindViewById<Button>( Resource.Id.btnLimpar ); // Associa um objeto a um elemento do XML
      _btnLimpar.Click += LimpaCampos;
    }

    private void DefineListaProducoes()
    {
      _listViewProducoes = FindViewById<ListView>( Resource.Id.listProducoes ); // Associa um objeto a um elemento do XML
      _listViewProducoes.ItemClick += ListaProducaoClick;
    }

    private void LimpaCampos(Object sender, EventArgs e)
    {
      _datProducao.Text = String.Empty;
      _txtDenLinha.Text = String.Empty;
      _txtDenTurno.Text = String.Empty;
      _listViewProducoes.Adapter = null;
      _txtProducoesQuantidade.Text = "Produções (0)";

    }

    private void LinhaPopUp(Object sender, EventArgs e)
    {
      if (!_popUpLinha.Aberto)
      {
        _popUpLinha.Exibe( _txtDenLinha );
        //_txtCodLinha.Tag contém o número de série do registro
      }
    }

    private void TurnoPopUp(Object sender, EventArgs e)
    {
      if (!_popUpTurno.Aberto)
      {
        _popUpTurno.Exibe( _txtDenTurno );
        //_txtCodTurno.Tag contém o número de série do registro
      }
    }

    private void DataPopUp(EditText editText)
    {
      _date = String.IsNullOrEmpty( editText.Text ) ? DateTime.Today : Convert.ToDateTime( editText.Text );
      _editText = editText;
      _txtDataPicker = String.Empty;
      ShowDialog( DateDialogId );
    }

    private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
    {
      _txtDataPicker = e.Date.ToString( "dd/MM/yyyy" );
      _editText.Text = _txtDataPicker;
    }

    protected override Dialog OnCreateDialog(int id)
    {
      switch (id)
      {
        case DateDialogId:
          return new DatePickerDialog( this, OnDateSet, _date.Year, _date.Month, _date.Day );
        default:
          break;
      }
      return null;
    }

    private void ThreadingProcessar(Object sender, EventArgs e)
    {
      ValidaData();
      _progress = new ProgressDialog( this )
      {
        Indeterminate = false
      };
      _progress.SetCancelable( false );
      _progress.SetMessage( "Aguarde, carregando ..." );
      _progress.Show();
      _task = new ProgressDialogTask
      {
        RunInBackgroundMethod = Processar
      ,
        OnPostExecuteMethod = TerminarProcesso
      };
      _task.Execute();
    }

    private void ValidaData()
    {
      if (String.IsNullOrEmpty( _datProducao.Text ))
      {
        _datProducao.Text = DateTime.Today.ToString( "d" );
      }
    }

    private Boolean Processar()
    {
      _listaProducao = new Classes.Producao.ListaProducao( this, _acessoSql, _codEmpresa, _datProducao.Text, Convert.ToInt64( _txtDenLinha.Tag ), Convert.ToInt64( _txtDenTurno.Tag ) );
      return ( true );
    }

    private Boolean TerminarProcesso()
    {
      _listViewProducoes.Adapter = _listaProducao.ListaProducaoAdapter;
      _progress.Dismiss();
      _txtProducoesQuantidade.Text = "Produções (" + _listaProducao.ListaProducaoAdapter.Count + ")";
      return ( true );
    }

  }

}

