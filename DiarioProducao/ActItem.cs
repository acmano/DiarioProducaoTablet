using System;
using Android.App;
using Android.OS;
using Android.Text;
using Android.Widget;
using DiarioProducao.Classes.Comum;
using Lorenzetti;
using Lorenzetti.DB;

namespace DiarioProducao
{

  [
    Activity
    (
      Label = "Detalhes da Produção do Item"
      , MainLauncher = false
      , Icon = "@drawable/DiarioProducao"
      , Theme = "@style/LorenTheme"
    )
  ]

  public class ActItem : Activity
  {

    private AcessoSql                                                                _acessoSql;
    private AcessoSql.AcessoTipo                                                     _acessoTipo;
    private eAmbiente                                                                _ambiente;
    private Int64                                                                    _numSerieLmpi;
    private Config                                                                   _bcoSql;

    private EditText                                                                 _codigoItem;
    private EditText                                                                 _descricaoItem;
    private EditText                                                                 _matriculaFuncionario;
    private EditText                                                                 _nomeFuncionario;
    private EditText                                                                 _horaInicio;
    private EditText                                                                 _quantidadeFuncionariosInicio;
    private EditText                                                                 _horaFim;
    private EditText                                                                 _quantidadeFuncionariosTermino;
    private EditText                                                                 _quantidadeInformada;
    private EditText                                                                 _quantidadeApontada;
    private EditText                                                                 _dataApontamento;
    private ListView                                                                 _producaoItemObservacoes;
    private ListView                                                                 _producaoItemOcorrencias;
    private ListView                                                                 _producaoItemFuncionarios;
    private TextView                                                                 _txtProducaoItemObservacoesQuantidade;
    private TextView                                                                 _txtProducaoItemOcorrenciasQuantidade;
    private TextView                                                                 _txtProducaoItemFuncionariosQuantidade;

    private Classes.Item.Classe                                                      _item;
    private Classes.Funcionario.Classe                                               _funcionario;
    private Classes.Producao.Classe                                                  _producao;
    private Classes.ProducaoResponsavel.Classe                                       _producaoResponsavel;
    private Classes.ProducaoItem.Classe                                              _producaoItem;
    private Classes.ProducaoItemInicio.Classe                                        _producaoItemInicio;
    private Classes.ProducaoItemFim.Classe                                           _producaoItemFim;
    private Classes.ProducaoItemQuantidade.Classe                                    _producaoItemQuantidade;
    private Classes.ProducaoItemObservacao.ListaProducaoItemObservacao               _listaProducaoItemObservacao;
    private Classes.ProducaoItemOcorrencia.ListaProducaoItemOcorrencia               _listaProducaoItemOcorrencia;
    private Classes.ProducaoItemFuncionario.ListaProducaoItemFuncionario             _listaProducaoItemFuncionario;
    private ProgressDialog                                                           _progress;
    private ProgressDialogTask                                                       _task;



    protected override void OnCreate ( Bundle bundle )
    {
      base.OnCreate ( bundle );
      SetContentView ( Resource.Layout.item );
      PegaParametros ( );
      InitClasses ( );
      DefineTela ( );
      PreencheCampos ( );
      CarregaListas ( );
    }

    private void PegaParametros ( )
    {
      _ambiente = ( eAmbiente )Intent.GetLongExtra ( "Ambiente", 0L );
      _acessoTipo = ( AcessoSql.AcessoTipo )Intent.GetLongExtra ( "AcessoTipo", 0L );
      _numSerieLmpi = Intent.GetLongExtra ( "NumSerieLmpi", 0L );
    }

    private void InitClasses ( )
    {
      _acessoSql = new AcessoSql ( _acessoTipo );
      _bcoSql = new Config ( _ambiente, eClientBanco.SQLServer, eBanco.sqlloren );
      _producaoItem = new Classes.ProducaoItem.Classe ( _bcoSql, _acessoSql, new Classes.ProducaoItem.Classe.Pk ( _numSerieLmpi ) );
      _item = new Classes.Item.Classe (_bcoSql, _acessoSql.OpenQuery, new Classes.Item.Classe.Ak( _producaoItem.CodItem ));
      _producao = new Classes.Producao.Classe( _bcoSql, _acessoSql, new Classes.Producao.Classe.Pk(_producaoItem.NumSerieLmp) );
      _producaoResponsavel = new Classes.ProducaoResponsavel.Classe( _bcoSql, _acessoSql, new Classes.ProducaoResponsavel.Classe.Ak(_producao.NumSerieLmp));
      _funcionario = new Classes.Funcionario.Classe(_bcoSql, _acessoSql.OpenQuery, new Classes.Funcionario.Classe.Pk(_producaoResponsavel.NumSerieLmf ));
      _producaoItemInicio = new Classes.ProducaoItemInicio.Classe( _bcoSql, _acessoSql.OpenQuery, new Classes.ProducaoItemInicio.Classe.Ak(_numSerieLmpi));
      _producaoItemFim  = new Classes.ProducaoItemFim.Classe ( _bcoSql, _acessoSql.OpenQuery, new Classes.ProducaoItemFim.Classe.Ak ( _numSerieLmpi ) );
      _producaoItemQuantidade = new Classes.ProducaoItemQuantidade.Classe ( _bcoSql, _acessoSql.OpenQuery, new Classes.ProducaoItemQuantidade.Classe.Ak ( _numSerieLmpi ) );
    }

    private void DefineTela ( )
    {
      DefineCampoCodigoItem ( );
      DefineCampoDescricaoItem ( );
      DefineCampoMatriculaFuncionario ( );
      DefineCampoNomeFuncionario ( );
      DefineCampoHoraInicio ( );
      DefineCampoQuantidadeFuncionariosInicio ( );
      DefineCampoHoraTermino ( );
      DefineCampoQuantidadeFuncionariosTermino ( );
      DefineCampoQuantidadeInformada ( );
      DefineCampoQuantidadeApontada ( );
      DefineCampoDataApontamento ( );
      DefineCampoProducaoItemObservacoesQuantidade();
      DefineListaObservacoes ( );
      DefineCampoProducaoItemOcorrenciasQuantidade ( );
      DefineListaOcorrencias ( );
      DefineCampoproducaoItemFuncionariosQuantidade ( );
      DefineListaFuncionarios ( );
    }

    private void DefineCampoCodigoItem ( )
    {
      _codigoItem = FindViewById<EditText> ( Resource.Id.txtCodigoItem );                                           // Associa um objeto a um elemento do XML
      _codigoItem.InputType = InputTypes.Null;                                                                      // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineCampoDescricaoItem ( )
    {
      _descricaoItem = FindViewById<EditText> ( Resource.Id.txtDescricaoItem );                                     // Associa um objeto a um elemento do XML
      _descricaoItem.InputType = InputTypes.Null;                                                                   // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineCampoMatriculaFuncionario ( )
    {
      _matriculaFuncionario = FindViewById<EditText> ( Resource.Id.txtMatriculaFuncionario );                       // Associa um objeto a um elemento do XML
      _matriculaFuncionario.InputType = InputTypes.Null;                                                            // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineCampoNomeFuncionario ( )
    {
      _nomeFuncionario = FindViewById<EditText> ( Resource.Id.txtNomeFuncionario );                                 // Associa um objeto a um elemento do XML
      _nomeFuncionario.InputType = InputTypes.Null;                                                                 // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineCampoHoraInicio ( )
    {
      _horaInicio = FindViewById<EditText> ( Resource.Id.txtHoraInicio );                                           // Associa um objeto a um elemento do XML
      _horaInicio.InputType = InputTypes.Null;                                                                      // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineCampoQuantidadeFuncionariosInicio ( )
    {
      _quantidadeFuncionariosInicio = FindViewById<EditText> ( Resource.Id.txtQuantidadeFuncionariosInicio );       // Associa um objeto a um elemento do XML
      _quantidadeFuncionariosInicio.InputType = InputTypes.Null;                                                    // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineCampoHoraTermino ( )
    {
      _horaFim = FindViewById<EditText> ( Resource.Id.txtHoraFim );                                                 // Associa um objeto a um elemento do XML
      _horaFim.InputType = InputTypes.Null;                                                                         // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineCampoQuantidadeFuncionariosTermino ( )
    {
      _quantidadeFuncionariosTermino = FindViewById<EditText> ( Resource.Id.txtQuantidadeFuncionariosFim );     // Associa um objeto a um elemento do XML
      _quantidadeFuncionariosTermino.InputType = InputTypes.Null;                                                   // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineCampoQuantidadeInformada ( )
    {
      _quantidadeInformada = FindViewById<EditText> ( Resource.Id.txtQuantidadeInformada );                         // Associa um objeto a um elemento do XML
      _quantidadeInformada.InputType = InputTypes.Null;                                                             // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineCampoQuantidadeApontada ( )
    {
      _quantidadeApontada = FindViewById<EditText> ( Resource.Id.txtQuantidadeApontada );                           // Associa um objeto a um elemento do XML
      _quantidadeApontada.InputType = InputTypes.Null;                                                              // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineCampoDataApontamento ( )
    {
      _dataApontamento = FindViewById<EditText> ( Resource.Id.txtDataApontamento );                                 // Associa um objeto a um elemento do XML
      _dataApontamento.InputType = InputTypes.Null;                                                                 // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineCampoProducaoItemObservacoesQuantidade()
    {
      _txtProducaoItemObservacoesQuantidade = FindViewById<TextView> ( Resource.Id.txtProducaoItemObservacoesQuantidade );               // Associa um objeto a um elemento do XML
      _txtProducaoItemObservacoesQuantidade.InputType = InputTypes.Null;                                                                 // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineCampoProducaoItemOcorrenciasQuantidade()
    {
      _txtProducaoItemOcorrenciasQuantidade = FindViewById<TextView> ( Resource.Id.txtProducaoItemOcorrenciasQuantidade );               // Associa um objeto a um elemento do XML
      _txtProducaoItemOcorrenciasQuantidade.InputType = InputTypes.Null;                                                                 // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineCampoproducaoItemFuncionariosQuantidade()
    {
      _txtProducaoItemFuncionariosQuantidade = FindViewById<TextView> ( Resource.Id.txtProducaoItemFuncionariosQuantidade );               // Associa um objeto a um elemento do XML
      _txtProducaoItemFuncionariosQuantidade.InputType = InputTypes.Null;                                                                 // impedir de usar o teclado mesmo clicando no campo. InputType determina o tipo de teclado a ser utilzado no campo
    }

    private void DefineListaObservacoes ( )
    {
      _producaoItemObservacoes = FindViewById<ListView> ( Resource.Id.listaObservacoes );                                  // Associa um objeto a um elemento do XML
    }

    private void DefineListaOcorrencias ( )
    {
      _producaoItemOcorrencias = FindViewById<ListView> ( Resource.Id.listaOcorrencias );                                  // Associa um objeto a um elemento do XML
    }

    private void DefineListaFuncionarios ( )
    {
      _producaoItemFuncionarios = FindViewById<ListView> ( Resource.Id.listaFuncionarios );                                // Associa um objeto a um elemento do XML
    }

    private void PreencheCampos ( )
    {
      _codigoItem.Text = _item.CodigoItem;
      _descricaoItem.Text = _item.DescricaoItem;
      _matriculaFuncionario.Text = _funcionario.MatriculaFuncionario.ToString();
      _nomeFuncionario.Text = _funcionario.NomeFuncionario;
      _horaInicio.Text = _producaoItemInicio.HoraInicio;
      _quantidadeFuncionariosInicio.Text = _producaoItemInicio.QuantidadeFuncionarios.ToString ( );
      _horaFim.Text = _producaoItemFim.HoraFim;
      _quantidadeFuncionariosTermino.Text = _producaoItemFim.QuantidadeFuncionarios.ToString ( );
      _quantidadeInformada.Text = _producaoItemQuantidade.QuantidadeInformada.ToString ( );
      _quantidadeApontada.Text = _producaoItemQuantidade.QuantidadeApontada.ToString ( );
      _dataApontamento.Text = _producaoItemQuantidade.DataApontamento.ToString ( "g" );
    }

    private void CarregaListas()
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
      CarregaListaObservacao ( );
      CarregaListaOcorrencia ( );
      CarregaListaFuncionario ( );
      return ( true );
    }

    private void CarregaListaObservacao ( )
    {
      _listaProducaoItemObservacao = new Classes.ProducaoItemObservacao.ListaProducaoItemObservacao( this, _acessoSql, _numSerieLmpi );
    }

    private void CarregaListaOcorrencia ( )
    {
      _listaProducaoItemOcorrencia = new Classes.ProducaoItemOcorrencia.ListaProducaoItemOcorrencia ( this, _acessoSql, _numSerieLmpi );
    }

    private void CarregaListaFuncionario ( )
    {
      _listaProducaoItemFuncionario = new Classes.ProducaoItemFuncionario.ListaProducaoItemFuncionario( this, _acessoSql, _numSerieLmpi );
    }

    private Boolean TerminaProcessamento ( )
    {
      TerminaProcessamentoProducaoItemObservacao ( );
      TerminaProcessamentoProducaoItemOcorrencia ( );
      TerminaProcessamentoProducaoItemFuncionario ( );
      _progress.Dismiss ( );
      _txtProducaoItemObservacoesQuantidade.Text = "Observações (" + _listaProducaoItemObservacao.ListaProducaoItemObservacaoAdapter.Count + ")";
      _txtProducaoItemOcorrenciasQuantidade.Text = "Ocorrências (" + _listaProducaoItemOcorrencia.ListaProducaoItemOcorrenciaAdapter.Count + ")";
      _txtProducaoItemFuncionariosQuantidade.Text = "Funcionários (" + _listaProducaoItemFuncionario.ListaProducaoItemFuncionarioAdapter.Count + ")";
      return ( true );
    }

    private void TerminaProcessamentoProducaoItemObservacao ( )
    {
      _producaoItemObservacoes.Adapter = _listaProducaoItemObservacao.ListaProducaoItemObservacaoAdapter;
    }

    private void TerminaProcessamentoProducaoItemOcorrencia ( )
    {
      _producaoItemOcorrencias.Adapter = _listaProducaoItemOcorrencia.ListaProducaoItemOcorrenciaAdapter;
    }

    private void TerminaProcessamentoProducaoItemFuncionario ( )
    {
      _producaoItemFuncionarios.Adapter = _listaProducaoItemFuncionario.ListaProducaoItemFuncionarioAdapter;
    }
  }
}