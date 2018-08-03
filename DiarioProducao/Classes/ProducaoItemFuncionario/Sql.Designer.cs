﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiarioProducao.Classes.ProducaoItemFuncionario {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DiarioProducao.Classes.ProducaoItemFuncionario.Sql", typeof(Sql).Assembly);
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
        ///      SELECT  lor_man_producao_item_funcionario.num_serie_lmpif
        ///        FROM  lor_man_producao_item_funcionario
        ///        WHERE lor_man_producao_item_funcionario.num_serie_lmpif = {1}
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
        ///        FROM  lor_man_producao_item_funcionario
        ///        WHERE lor_man_producao_item_funcionario.num_serie_lmpi  = {1}
        ///        AND   lor_man_producao_item_funcionario.num_serie_lmf   = {2}
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
        ///        FROM  lor_man_producao_item_funcionario
        ///        WHERE lor_man_producao_item_funcionario.num_serie_lmpif = {1}
        ///        AND   1 = 0
        ///    &apos;
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
        ///        FROM  lor_man_producao_item_funcionario
        ///        WHERE lor_man_producao_item_funcionario.num_serie_lmpif = {1}
        ///    &apos;
        ///  ) AS tabela.
        /// </summary>
        internal static string ExistePk {
            get {
                return ResourceManager.GetString("ExistePk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AND lor_man_producao_item_funcionario.num_serie_lmpi = {0}.
        /// </summary>
        internal static string FiltroProducaoItem {
            get {
                return ResourceManager.GetString("FiltroProducaoItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT
        ///  INTO OPENQUERY
        ///  (
        ///    {0}
        ///  , &apos;
        ///      SELECT  lor_man_producao_item_funcionario.num_serie_lmpi
        ///           ,  lor_man_producao_item_funcionario.num_serie_lmf
        ///        FROM  lor_man_producao_item_funcionario
        ///    &apos;
        ///  )
        ///  VALUES
        ///  (
        ///    {1}
        ///  , {2}
        ///  ).
        /// </summary>
        internal static string InsertRecord {
            get {
                return ResourceManager.GetString("InsertRecord", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT  tabela.serie_producao_item_funcionario
        ///     ,  tabela.serie_producao_item
        ///     ,  tabela.serie_funcionario
        ///     ,  tabela.matricula_funcionario
        ///     ,  tabela.nome_funcionario
        ///  FROM  OPENQUERY
        ///  (
        ///    {0}
        ///  , &apos;
        ///      SELECT  lor_man_producao_item_funcionario.num_serie_lmpif AS serie_producao_item_funcionario
        ///           ,  lor_man_producao_item_funcionario.num_serie_lmpi  AS serie_producao_item
        ///           ,  lor_man_producao_item_funcionario.num_serie_lmf   AS serie_funcionario
        ///          [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string QueryRecordAk {
            get {
                return ResourceManager.GetString("QueryRecordAk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT  tabela.serie_producao_item_funcionario
        ///     ,  tabela.serie_producao_item
        ///     ,  tabela.serie_funcionario
        ///     ,  tabela.matricula_funcionario
        ///     ,  tabela.nome_funcionario
        ///  FROM  OPENQUERY
        ///  (
        ///    {0}
        ///  , &apos;
        ///      SELECT  lor_man_producao_item_funcionario.num_serie_lmpif AS serie_producao_item_funcionario
        ///           ,  lor_man_producao_item_funcionario.num_serie_lmpi  AS serie_producao_item
        ///           ,  lor_man_producao_item_funcionario.num_serie_lmf   AS serie_funcionario
        ///          [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string QueryRecordAll {
            get {
                return ResourceManager.GetString("QueryRecordAll", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT  tabela.serie_producao_item_funcionario
        ///     ,  tabela.serie_producao_item
        ///     ,  tabela.serie_funcionario
        ///     ,  tabela.matricula_funcionario
        ///     ,  tabela.nome_funcionario
        ///  FROM  OPENQUERY
        ///  (
        ///    {0}
        ///  , &apos;
        ///      SELECT  lor_man_producao_item_funcionario.num_serie_lmpif AS serie_producao_item_funcionario
        ///           ,  lor_man_producao_item_funcionario.num_serie_lmpi  AS serie_producao_item
        ///           ,  lor_man_producao_item_funcionario.num_serie_lmf   AS serie_funcionario
        ///          [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string QueryRecordPk {
            get {
                return ResourceManager.GetString("QueryRecordPk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Funcionários da Produção do Item.
        /// </summary>
        internal static string TableDescription {
            get {
                return ResourceManager.GetString("TableDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to lor_man_producao_item_funcionario.
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
        ///      SELECT  lor_man_producao_ocorrencia.num_serie_lmpi  AS serie_producao_item
        ///           ,  lor_man_producao_ocorrencia.num_serie_lmf   AS serie_funcionario
        ///        FROM  lor_man_producao_ocorrencia
        ///        WHERE lor_man_producao_ocorrencia.num_serie_lmpoco = {1}
        ///    &apos;
        ///  ) 
        ///  SET  serie_producao_item  = {2}
        ///    ,  serie_funcionario    = {3}.
        /// </summary>
        internal static string UpdateRecord {
            get {
                return ResourceManager.GetString("UpdateRecord", resourceCulture);
            }
        }
    }
}
