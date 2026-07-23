using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace MesToSapApi.DataControl
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class StoreProcedureParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreProcedureParameter"/> class.
        /// </summary>
        public StoreProcedureParameter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreProcedureParameter"/> class.
        /// </summary>
        /// <param name="dbType">Type of the db.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public StoreProcedureParameter(DbType dbType, ParameterDirection direction, string parameterName)
        {
            this.DbType = dbType;
            this.Direction = direction;
            this.ParameterName = parameterName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreProcedureParameter"/> class.
        /// </summary>
        /// <param name="dbType">Type of the db.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="size">The size.</param>
        public StoreProcedureParameter(DbType dbType, ParameterDirection direction, string parameterName, int size)
        {
            this.DbType = dbType;
            this.Direction = direction;
            this.ParameterName = parameterName;
            this.Size = size;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreProcedureParameter"/> class.
        /// </summary>
        /// <param name="dbType">Type of the db.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        public StoreProcedureParameter(DbType dbType, ParameterDirection direction, string parameterName, object value)
        {
            this.DbType = dbType;
            this.Direction = direction;
            this.ParameterName = parameterName;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the type of the db.
        /// </summary>
        /// <value>The type of the db.</value>
        public DbType DbType { get; set; }
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>The direction.</value>
        public ParameterDirection Direction { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is nullable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is nullable; otherwise, <c>false</c>.
        /// </value>
        public bool IsNullable { get; set; }
        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        /// <value>The name of the parameter.</value>
        public string ParameterName { get; set; }
        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public int Size { get; set; }
        /// <summary>
        /// Gets or sets the source column.
        /// </summary>
        /// <value>The source column.</value>
        public string SourceColumn { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [source column null mapping].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [source column null mapping]; otherwise, <c>false</c>.
        /// </value>
        public bool SourceColumnNullMapping { get; set; }
        /// <summary>
        /// Gets or sets the source version.
        /// </summary>
        /// <value>The source version.</value>
        public DataRowVersion SourceVersion { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value { get; set; }
    }
}