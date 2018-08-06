using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Views;
using Android.Widget;
using DiarioProducao.Classes.Comum;

namespace DiarioProducao.Classes.PopUp.Linha
{
    public class Detalhe
    {
        public Int64 NumSerie { get; set; }
        public String Codigo { get; set; }
        public String Descricao { get; set; }
    }

    public class Adapter : BaseAdapter
    {
        private readonly Activity _activityMestre;
        private readonly List<Detalhe> _detalhes;

        public Adapter(Activity activityMestre, AcessoSql acessoSql, String codEmpresa)
        {
            _activityMestre = activityMestre;
            var query = new StringBuilder();
            query.AppendFormat(Sql.PopUp, acessoSql.OpenQuery, codEmpresa);
            var banco = new Msde(acessoSql);
            banco.Open();
            var reader = banco.DataReader(query.ToString());
            _detalhes = new List<Detalhe>();
            while (reader.Read())
            {
                var detalhe = new Detalhe
                {
                    NumSerie = 0L
                ,
                    Codigo = String.Empty
                ,
                    Descricao = String.Empty
                };
                try
                {
                    detalhe.NumSerie = Convert.ToInt64((reader["serie"]));
                    detalhe.Codigo = reader["codigo"].ToString();
                    detalhe.Descricao = reader["descricao"].ToString();
                }
                catch (Exception)
                {
                    detalhe.NumSerie = 0L;
                    detalhe.Codigo = "";
                    detalhe.Descricao = "";
                }
                _detalhes.Add(detalhe);
            }
            if (_detalhes.Count <= 0)
            {
                var detalhe = new Detalhe
                {
                    NumSerie = 0L
                ,
                    Codigo = ""
                ,
                    Descricao = @"* Nenhum registro encontrado..."
                };
                _detalhes.Add(detalhe);
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
            var item = _detalhes[position];
            var view = convertView;
            if (convertView == null)
            {
                view = (_activityMestre.LayoutInflater.Inflate(Resource.Layout.popupdetalhe, parent, false)) as LinearLayout;
            }
            if (view != null)
            {
        if (view.FindViewById( Resource.Id.txtCodigoItem ) is TextView codigo)
        {
          codigo.SetText( item.Codigo.Trim(), TextView.BufferType.Normal );
        }
        if (view.FindViewById( Resource.Id.txtDescricaoItem ) is TextView descricao)
        {
          descricao.SetText( item.Descricao.Trim(), TextView.BufferType.Normal );
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