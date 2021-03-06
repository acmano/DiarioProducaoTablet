﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiarioProducao.Classes.ProducaoItemQuantidade {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Sql {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Sql() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DiarioProducao.Classes.ProducaoItemQuantidade.Sql", typeof(Sql).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE
        ///  FROM OPENQUERY
        ///  (
        ///    {0}
        ///  , &apos;
        ///      SELECT  lor_man_producao_item_quantidade.num_serie_lmpiqtd
        ///        FROM  lor_man_producao_item_quantidade
        ///        WHERE lor_man_producao_item_quantidade.num_serie_lmpiqtd = {1}
        ///    &apos;
        ///  ).
        /// </summary>
        internal static string DeleteRecord {
            get {
                return ResourceManager.GetString("DeleteRecord", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT  tabela.existe
        ///  FROM  OPENQUERY
        ///  (
        ///    {0}
        ///  , &apos;
        ///      SELECT  DISTINCT 1 AS existe
        ///        FROM  lor_man_producao_item_quantidade
        ///        WHERE lor_man_producao_item_quantidade.num_serie_lmpi = {1}
        ///    &apos;
        ///  ) AS tabela.
        /// </summary>
        internal static string ExisteAk {
            get {
                return ResourceManager.GetString("ExisteAk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT  tabela.existe
        ///  FROM  OPENQUERY
        ///  (
        ///    {0}
        ///  , &apos;
        ///      SELECT  DISTINCT 1 AS existe
        ///        FROM  lor_man_producao_item_quantidade
        ///        WHERE lor_man_producao_item_quantidade.num_serie_lmpiqtd = {1}
        ///        AND   1 = 0
        ///  &apos;
        ///  ) AS tabela.
        /// </summary>
        internal static string ExisteDependencias {
            get {
                return ResourceManager.GetString("ExisteDependencias", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT  tabela.existe
        ///  FROM  OPENQUERY
        ///  (
        ///    {0}
        ///  , &apos;
        ///      SELECT  DISTINCT 1 AS existe
        ///        FROM  lor_man_producao_item_quantidade
        ///        WHERE lor_man_producao_item_quantidade.num_serie_lmpiqtd = {1}
        ///  &apos;
        ///  ) AS tabela.
        /// </summary>
        internal static string ExistePk {
            get {
                return ResourceManager.GetString("ExistePk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT
        ///  INTO OPENQUERY
        ///  (
        ///    {0}
        ///  , &apos;
        ///      SELECT  lor_man_producao_item_quantidade.num_serie_lmpi
        ///           ,  lor_man_producao_item_quantidade.num_serie_lmpf
        ///           ,  lor_man_producao_item_quantidade.qtd_informada
        ///           ,  lor_man_producao_item_quantidade.qtd_apontada
        ///           ,  lor_man_producao_item_quantidade.dat_quantidade
        ///        FROM  lor_man_producao_item_quantidade
        ///    &apos;
        ///  )
        ///  VALUES
        ///  (
        ///    {1}
        ///  , {2}
        ///  , {3}
        ///  , {4}
        ///  , &quot;{5}&quot;
        ///  ).
        /// </summary>
        internal static string InsertRecord {
            get {
                return ResourceManager.GetString("InsertRecord", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT  tabela.serie_producao_item_quantidade
        ///     ,  tabela.serie_producao_item
        ///     ,  tabela.serie_funcionario
        ///     ,  tabela.quantidade_informada
        ///     ,  tabela.quantidade_apontada
        ///     ,  tabela.data_apontamento
        ///  FROM  OPENQUERY
        ///  (
        ///    {0}
        ///  , &apos;
        ///      SELECT  lor_man_producao_item_quantidade.num_serie_lmpiqtd  AS serie_producao_item_quantidade
        ///           ,  lor_man_producao_item_quantidade.num_serie_lmpi     AS serie_producao_item
        ///           ,  lor_man_producao_item_quantidade.num_serie_l [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string QueryRecordAk {
            get {
                return ResourceManager.GetString("QueryRecordAk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT  tabela.serie_producao_item_quantidade
        ///     ,  tabela.serie_producao_item
        ///     ,  tabela.serie_funcionario
        ///     ,  tabela.quantidade_informada
        ///     ,  tabela.quantidade_apontada
        ///     ,  tabela.data_apontamento
        ///  FROM  OPENQUERY
        ///  (
        ///    {0}
        ///  , &apos;
        ///      SELECT  lor_man_producao_item_quantidade.num_serie_lmpiqtd  AS serie_producao_item_quantidade
        ///           ,  lor_man_producao_item_quantidade.num_serie_lmpi     AS serie_producao_item
        ///           ,  lor_man_producao_item_quantidade.num_serie_l [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string QueryRecordAll {
            get {
                return ResourceManager.GetString("QueryRecordAll", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT  tabela.serie_producao_item_quantidade
        ///     ,  tabela.serie_producao_item
        ///     ,  tabela.serie_funcionario
        ///     ,  tabela.quantidade_informada
        ///     ,  tabela.quantidade_apontada
        ///     ,  tabela.data_apontamento
        ///  FROM  OPENQUERY
        ///  (
        ///    {0}
        ///  , &apos;
        ///      SELECT  lor_man_producao_item_quantidade.num_serie_lmpiqtd  AS serie_producao_item_quantidade
        ///           ,  lor_man_producao_item_quantidade.num_serie_lmpi     AS serie_producao_item
        ///           ,  lor_man_producao_item_quantidade.num_serie_l [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string QueryRecordPk {
            get {
                return ResourceManager.GetString("QueryRecordPk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Quantidades da Produção do item.
        /// </summary>
        internal static string TableDescription {
            get {
                return ResourceManager.GetString("TableDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to lor_man_producao_item_quantidade.
        /// </summary>
        internal static string TableName {
            get {
                return ResourceManager.GetString("TableName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE  OPENQUERY
        ///  (
        ///    {0}
        ///  , &apos;
        ///      SELECT  lor_man_producao_item_quantidade.num_serie_lmpi     AS serie_producao_item
        ///           ,  lor_man_producao_item_quantidade.num_serie_lmpf     AS serie_producao_funcionario
        ///           ,  lor_man_producao_item_quantidade.qtd_informada      AS quantidade_informada
        ///           ,  lor_man_producao_item_quantidade.qtd_apontada       AS quantidade_apontada
        ///           ,  lor_man_producao_item_quantidade.dat_quantidade     AS data_apontamento
        ///        FROM  lor [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string UpdateRecord {
            get {
                return ResourceManager.GetString("UpdateRecord", resourceCulture);
            }
        }
    }
}
