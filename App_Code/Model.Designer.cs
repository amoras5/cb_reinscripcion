﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace mySQLTemp
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class mySQL : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new mySQL object using the connection string found in the 'mySQL' section of the application configuration file.
        /// </summary>
        public mySQL() : base("name=mySQL", "mySQL")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new mySQL object.
        /// </summary>
        public mySQL(string connectionString) : base(connectionString, "mySQL")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new mySQL object.
        /// </summary>
        public mySQL(EntityConnection connection) : base(connection, "mySQL")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region Function Imports
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectResult<Nullable<global::System.Int32>> spEXEMateriasReprobadas()
        {
            return base.ExecuteFunction<Nullable<global::System.Int32>>("spEXEMateriasReprobadas");
        }

        #endregion

    }

    #endregion

    
}
