using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Text;
using Android.Widget;
using DiarioProducao.Classes.ProducaoItem;
using DiarioProducao.Classes.ProducaoObservacao;
using DiarioProducao.Classes.ProducaoOcorrencia;
using DiarioProducao.Classes.Comum;
using Lorenzetti;
using Lorenzetti.DB;

namespace DiarioProducao
{
  using Android;

  [
    Activity
    (
      Label = "Itens da Produção"
      , MainLauncher = false
      , Icon = "@drawable/DiarioProducao"
      , Theme = "@style/LorenTheme"
    )
  ]

  public class ActProducao : Activity
  {

    private AcessoSql                                _acessoSql;
    private AcessoSql.AcessoTipo                     _acessoTipo;
    private eAmbiente                                _ambiente;
    private String                                   _codEmpresa;
    private Int64                                    _numSerieLmp;
    private ListView                                 _listViewProducaoItem;
    private ListView                                 _listViewProducaoObservacao;
    private ListView                                 _listViewProducaoOcorrencia;
    private EditText                                 _datProducao;
    private EditText                                 _txtDenLinha;
    private EditText                                 _txtDenTurno;
    private TextView                                 _txtProducaoItensQuantidade;
    private TextView                                 _txtProducaoOcorrenciasQuantidade;
    private TextView                                 _txtProducaoObservacoesQuantidade;
    private Intent                                   _intent;
    private Int64                                    _numSerieLmlm;
    private Int64                                    _numSerieLmtrn;
    private Config                                   _bcoSql;
    private Classes.Empresa.Classe                   _empresa;
    private Classes.Producao.Classe                  _producao;
    private Classes.LinhaMontagem.Classe             _linhaMontagem;
    private Classes.Turno.Classe                     _turno;
    private ProgressDialog                           _progress;
    private ProgressDialogTask                       _task;
    private ListaProducaoItem                        _listaProducaoItem;
    private ListaProducaoObservacao                  _listaProducaoObservacao;
    private ListaProducaoOcorrencia                  _listaProducaoOcorrencia;

    protected override void OnCreate ( Bundle bundle )
    {
      base.OnCreate ( bundle );
      SetContentView ( Resource.Layout.producao );
      PegaParametros ( );
      InitClasses ( );
      Campos ( );
      CarregaListas ( );
    }

    protected override void OnResume ( )
    {
      base.OnResume ( );
      _intent = null;
    }

    private void PegaParametros ( )
    {
      _ambiente = ( eAmbiente )Intent.GetLongExtra ( "Ambiente", 0L );
      _acessoTipo = ( AcessoSql.AcessoTipo )Intent.GetLongExtra ( "AcessoTipo", 0L );
      _codEmpresa = Intent.GetStringExtra ( "CodEmpresa" );
      _numSerieLmp = Intent.GetLongExtra ( "NumSerieLmp", 0L );
      _numSerieLmlm = Intent.GetLongExtra ( "NumSerieLmlm", 0L );
      _numSerieLmtrn = Intent.GetLongExtra ( "NumSerieLmtrn", 0L );
    }

    private void InitClasses ( )
    {
      _acessoSql = new AcessoSql ( _acessoTipo );
      _bcoSql = new Config ( _ambiente, eClientBanco.SQLServer, eBanco.sqlloren );
      _empresa = new Classes.Empresa.Classe ( _bcoSql, _acessoSql.OpenQuery, new Classes.Empresa.Classe.Ak ( _codEmpresa ) );
      _turno = new Classes.Turno.Classe ( _bcoSql, _acessoSql.OpenQuery, _empresa.CodEmpresa, new Classes.Turno.Classe.Pk ( _numSerieLmtrn ) );
      _linhaMontagem = new Classes.LinhaMontagem.Classe ( _bcoSql, _acessoSql.OpenQuery, new Classes.LinhaMontagem.Classe.Pk ( _numSerieLmlm ) );
      _producao = new Classes.Producao.Classe ( _bcoSql, _acessoSql, new Classes.Producao.Classe.Pk ( _numSerieLmp ) );
    }

    private void Campos ( )
    {
      DefineCampoDatProducao ( );
      DefineCampoDenLinha ( );
      DefineCampoDenTurno ( );
      DefineCampoProducaoItensQuantidade();
      DefineListViewItens ( );
      DefineCampoProducaoObservacoesQuantidade();
      DefineListViewObservacoes ( );
      DefineCampoProducaoOcorrenciasQuantidade();
      DefineListViewOcorrencias ( );
    }

    private void DefineCampoDatProducao ( )
    {
      _datProducao = FindViewById<EditText> ( Resource.Id.txtData );     // Associa um objeto a um elemento do XML
      _datProducao.InputType = InputTypes.Null;                          // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
      _datProducao.Text = _producao.DatInicio.ToString ( "d" );
    }

    private void DefineCampoDenLinha ( )
    {
      //aqui_txtDenLinha = FindViewById<EditText> ( Resource.Id.txtDenLinha ); // Associa um objeto a um elemento do XML
      _txtDenLinha.InputType = InputTypes.Null;                          // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
      _txtDenLinha.Text = _linhaMontagem.DenLinhaMontagem;
    }

    private void DefineCampoDenTurno ( )
    {
      _txtDenTurno = FindViewById<EditText> ( Resource.Id.txtDenTurno ); // Associa um objeto a um elemento do XML
      _txtDenTurno.InputType = InputTypes.Null;                          // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
      _txtDenTurno.Text = _turno.DenTurno;
    }

    private void DefineCampoProducaoItensQuantidade ( )
    {
      _txtProducaoItensQuantidade = FindViewById<TextView> ( Resource.Id.txtProducaoItensQuantidade );               // Associa um objeto a um elemento do XML
      _txtProducaoItensQuantidade.InputType = InputTypes.Null;                                                                 // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineListViewItens ( )
    {
      _listViewProducaoItem = FindViewById<ListView> ( Resource.Id.listItens ); // Associa um objeto a um elemento do XML
      _listViewProducaoItem.ItemClick += ListaProducaoItemClick;
    }

    private void DefineCampoProducaoObservacoesQuantidade ( )
    {
      _txtProducaoObservacoesQuantidade = FindViewById<TextView> ( Resource.Id.txtProducaoObservacoesQuantidade );               // Associa um objeto a um elemento do XML
      _txtProducaoObservacoesQuantidade.InputType = InputTypes.Null;                                                                 // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineListViewObservacoes ( )
    {
      _listViewProducaoObservacao = FindViewById<ListView> ( Resource.Id.listObservacoes );
    }

    private void DefineCampoProducaoOcorrenciasQuantidade ( )
    {
      _txtProducaoOcorrenciasQuantidade = FindViewById<TextView> ( Resource.Id.txtProducaoOcorrenciasQuantidade );               // Associa um objeto a um elemento do XML
      _txtProducaoOcorrenciasQuantidade.InputType = InputTypes.Null;                                                                 // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineListViewOcorrencias ( )
    {
      _listViewProducaoOcorrencia = FindViewById<ListView> ( Resource.Id.listOcorrencias );
    }

    private void SetaParametros ( ref Intent intent, Classes.ProducaoItem.Detalhe linhaSelecionada )
    {
      intent.PutExtra ( "Ambiente", Convert.ToInt64 ( _ambiente ) );
      intent.PutExtra ( "AcessoTipo", Convert.ToInt64 ( _acessoTipo ) );
      intent.PutExtra ( "NumSerieLmpi", linhaSelecionada.ColunasDetalhe.SerieProducaoItem );
    }

    private void ListaProducaoItemClick ( object sender, AdapterView.ItemClickEventArgs e )
    {
      var linhaSelecionada = _listaProducaoItem.ListaProducaoItemAdapter.GetItemAtPosition ( e.Position );
      _intent = new Intent ( this, typeof ( ActItem ) );
      _intent.AddFlags ( ActivityFlags.NewTask );
      _intent.AddFlags ( ActivityFlags.NoAnimation );
      SetaParametros ( ref _intent, linhaSelecionada );
      StartActivity ( _intent );
    }

    private void CarregaListas ( )
    {
      _progress = new ProgressDialog ( this )
      {
        Indeterminate = false
      };
      _progress.SetCancelable ( false );
      _progress.SetMessage ( "Aguarde, carregando ..." );
      _progress.Show ( );
      _task = new ProgressDialogTask
      {
        RunInBackgroundMethod = IniciaProcessamento
      , OnPostExecuteMethod = TerminaProcessamento
      };
      _task.Execute ( );
    }

    private Boolean IniciaProcessamento ( )
    {
      CarregaListaProcaoItem ( );
      CarregaListaProducaoObservacao ( );
      CarregaListaProducaoOcorrencia ( );
      return ( true );
    }

    private void CarregaListaProcaoItem ( )
    {
      _listaProducaoItem = new ListaProducaoItem ( this, _acessoSql, _producao.NumSerieLmp );
    }

    private void CarregaListaProducaoObservacao ( )
    {
      _listaProducaoObservacao = new ListaProducaoObservacao ( this, _acessoSql, _producao.NumSerieLmp );
    }

    private void CarregaListaProducaoOcorrencia ( )
    {
      _listaProducaoOcorrencia = new ListaProducaoOcorrencia ( this, _acessoSql, _producao.NumSerieLmp );
    }

    private Boolean TerminaProcessamento ( )
    {
      TerminaProcessamentoProducaoItem ( );
      TerminaProcessamentoProducaoObservacao ( );
      TerminaProcessamentoProducaoOcorrencia ( );
      _txtProducaoItensQuantidade.Text = "Itens da Produção (" + _listaProducaoItem.ListaProducaoItemAdapter.Count + ")";
      _txtProducaoObservacoesQuantidade.Text = "Observações Gerais (" + _listaProducaoObservacao.ListaProducaoObservacaoAdapter.Count + ")";
      _txtProducaoOcorrenciasQuantidade.Text = "Ocorrências Gerais (" + _listaProducaoOcorrencia.ListaProducaoOcorrenciaAdapter.Count + ")";
      _progress.Dismiss ( );
      return ( true );
    }

    private void TerminaProcessamentoProducaoItem ( )
    {
      _listViewProducaoItem.Adapter = _listaProducaoItem.ListaProducaoItemAdapter;
    }

    private void TerminaProcessamentoProducaoObservacao ( )
    {
      _listViewProducaoObservacao.Adapter = _listaProducaoObservacao.ListaProducaoObservacaoAdapter;
    }

    private void TerminaProcessamentoProducaoOcorrencia ( )
    {
      _listViewProducaoOcorrencia.Adapter = _listaProducaoOcorrencia.ListaProducaoOcorrenciaAdapter;
    }

  }
}