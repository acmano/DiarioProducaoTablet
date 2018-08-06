using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Android.App;
using Android.Views;
using Android.Widget;
using DiarioProducao.Classes.Comum;

namespace DiarioProducao.Classes.ProducaoOcorrencia
{

    public class Detalhe
    {

        public class Colunas
        {

            public Int64 SerieProducaoOcorrencia { get; set; }
            public Int64 SerieProducao { get; set; }
            public Int64 SerieOcorrencia { get; set; }
            public Int64 SerieFuncionario { get; set; }
            public String CodigoOcorrencia { get; set; }
            public String DescricaoOcorrencia { get; set; }
            public Int64 MatriculaFuncionario { get; set; }
            public String NomeFuncionario { get; set; }
            public DateTime DataOcorrencia { get; set; }

            public Colunas()
            {
                SerieProducaoOcorrencia = 0L;
                SerieProducao = 0L;
                SerieOcorrencia = 0L;
                SerieFuncionario = 0L;
                CodigoOcorrencia = String.Empty;
                DescricaoOcorrencia = String.Empty;
                MatriculaFuncionario = 0L;
                NomeFuncionario = String.Empty;
                DataOcorrencia = DateTime.MinValue;
            }

            public Colunas(Int64 serieProducaoOcorrencia, Int64 serieProducao, Int64 serieOcorrencia, Int64 serieFuncionario, String codigoOcorrencia, String descricaoOcorrencia, Int64 matriculaFuncionario, String nomeFuncionario, DateTime dataOcorrencia)
            {
                SerieProducaoOcorrencia = serieProducaoOcorrencia;
                SerieProducao = serieProducao;
                SerieOcorrencia = serieOcorrencia;
                SerieFuncionario = serieFuncionario;
                CodigoOcorrencia = codigoOcorrencia;
                DescricaoOcorrencia = descricaoOcorrencia;
                MatriculaFuncionario = matriculaFuncionario;
                NomeFuncionario = nomeFuncionario;
                DataOcorrencia = dataOcorrencia;
            }

            public Colunas(SqlDataReader reader)
            {
                SerieProducaoOcorrencia = Convert.ToInt64(reader["serie_producao_ocorrencia"]);
                SerieProducao = Convert.ToInt64(reader["serie_producao"]);
                SerieOcorrencia = Convert.ToInt64(reader["serie_ocorrencia"]);
                SerieFuncionario = Convert.ToInt64(reader["serie_funcionario"]);
                CodigoOcorrencia = reader["codigo_ocorrencia"].ToString();
                DescricaoOcorrencia = reader["descricao_ocorrencia"].ToString();
                MatriculaFuncionario = Convert.ToInt64(reader["matricula_funcionario"]);
                NomeFuncionario = reader["nome_funcionario"].ToString();
                DataOcorrencia = Convert.ToDateTime(reader["data_Ocorrencia"]);
            }

        }

        public Int64 SerieProducao { get; set; }

        public Colunas ColunasDetalhe { get; set; }


        private void DetalheVazio()
        {
            SerieProducao = 0L;
            ColunasDetalhe = new Colunas();
        }

        public Detalhe()
        {
            DetalheVazio();
        }

        public Detalhe(Int64 serieProducaoOcorrencia, Int64 serieProducao, Int64 serieocorrencia, Int64 serieFuncionario, String codigoOcorrencia, String descricaoOcorrencia, Int64 matriculaFuncionario, String nomeFuncionario, DateTime dataOcorrencia)
        {
            SerieProducao = serieProducao;
            ColunasDetalhe = new Colunas(serieProducaoOcorrencia, serieProducao, serieocorrencia, serieFuncionario, codigoOcorrencia, descricaoOcorrencia, matriculaFuncionario, nomeFuncionario, dataOcorrencia);
        }

        public Detalhe(SqlDataReader reader)
        {
            try
            {
                SerieProducao = Convert.ToInt64((reader["serie_producao"]));
                ColunasDetalhe = new Colunas(reader);
            }
            catch (Exception)
            {
                DetalheVazio();
            }
        }

    }

    public class AdapterProducaoOcorrencia : BaseAdapter
    {
        private readonly Activity _activityMestre;
        private readonly List<Detalhe> _detalhes;

        public AdapterProducaoOcorrencia(Activity activityMestre, AcessoSql acessoSql, String filtro)
        {
            _activityMestre = activityMestre;
            var query = new StringBuilder();
            query.AppendFormat(Sql.QueryRecordAll, acessoSql.OpenQuery, filtro);
            var banco = new Msde(acessoSql);
            banco.Open();
            var reader = banco.DataReader(query.ToString());
            _detalhes = new List<Detalhe>();
            while (reader.Read())
            {
                var detalhe = new Detalhe(reader);
                _detalhes.Add(detalhe);
            }
            if (_detalhes.Count <= 0)
            {
                _detalhes.Add(new Detalhe
                {
                    ColunasDetalhe =
          {
            DescricaoOcorrencia = "Sem dados para exibir"
          }
                });
            }
            reader.Close();
            reader.Dispose();
            banco.Close();
        }

        public override int Count
        {
            get
            {
                return _detalhes.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var detalhe = _detalhes[position];
            var view = convertView;
            if (convertView == null)
            {
                view = (_activityMestre.LayoutInflater.Inflate(Resource.Layout.producaoocorrencia, parent, false)) as LinearLayout;
            }
            if (view != null)
            {
        if (view.FindViewById( Resource.Id.txtDataOcorrencia ) is TextView dataOcorrencia)
        {
          dataOcorrencia.SetText( detalhe.ColunasDetalhe.DataOcorrencia.ToString( "G" ).Trim(), TextView.BufferType.Normal );
        }
        if (view.FindViewById( Resource.Id.txtCodigoOcorrencia ) is TextView codigoOcorrencia)
        {
          codigoOcorrencia.SetText( detalhe.ColunasDetalhe.CodigoOcorrencia.Trim(), TextView.BufferType.Normal );
        }
        if (view.FindViewById( Resource.Id.txtDescricaoOcorrencia ) is TextView descricaoOcorrencia)
        {
          descricaoOcorrencia.SetText( detalhe.ColunasDetalhe.DescricaoOcorrencia.Trim(), TextView.BufferType.Normal );
        }
      }
            return view;
        }

        public Detalhe GetItemAtPosition(int position)
        {
            return _detalhes[position];
        }
    }
}