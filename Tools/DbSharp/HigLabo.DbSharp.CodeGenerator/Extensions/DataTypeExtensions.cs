using HigLabo.Core;
using HigLabo.DbSharp.MetaData;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HigLabo.DbSharp
{
    public static class DataTypeExtensions
    {
        public static IDbDataParameter CreateParameter(this DataType dataType)
        {
            var name = dataType.Name;

            switch (dataType.DbType.DatabaseServer)
            {
                case DatabaseServer.SqlServer:
                    {
                        var p = new System.Data.SqlClient.SqlParameter(name, dataType.DbType.SqlServerDbType.Value);
                        if (dataType is SqlInputParameter dType)
                        {
                            p.Direction = dType.ParameterDirection;
                        }
                        if (p.Direction != ParameterDirection.Output)
                        {
                            p.Value = GetParameterValue(dataType, dataType.DbType.SqlServerDbType.Value);
                        }
                        return p;
                    }
                case DatabaseServer.Oracle:
                    {
                        var p = new Oracle.ManagedDataAccess.Client.OracleParameter(name, dataType.DbType.OracleServerDbType.Value);
                        if (dataType is SqlInputParameter dType)
                        {
                            p.Direction = dType.ParameterDirection;
                        }
                        if (p.Direction != ParameterDirection.Output)
                        {
                            p.Value = dataType.GetParameterValue(dataType.DbType.OracleServerDbType.Value);
                        }
                        return p;
                    }
                case DatabaseServer.MySql:
                    {
                        var p = new MySql.Data.MySqlClient.MySqlParameter(name, dataType.DbType.MySqlServerDbType.Value.GetMySqlDbType());
                        if (dataType is SqlInputParameter dType)
                        {
                            p.Direction = dType.ParameterDirection;
                        }
                        if (p.Direction != ParameterDirection.Output)
                        {
                            p.Value = dataType.GetParameterValue(dataType.DbType.MySqlServerDbType.Value);
                        }
                        return p;
                    }
                case DatabaseServer.PostgreSql:
                    {
                        var p = new Npgsql.NpgsqlParameter(name, dataType.DbType.PostgreSqlServerDbType.Value);
                        if (dataType is SqlInputParameter dType)
                        {
                            p.Direction = dType.ParameterDirection;
                        }
                        if (p.Direction != ParameterDirection.Output)
                        {
                            p.Value = dataType.GetParameterValue(dataType.DbType.PostgreSqlServerDbType.Value);
                        }
                        return p;
                    }
                default: throw new InvalidOperationException();
            }
        }
        private static Object GetParameterValue(this DataType dataType, SqlServer2012DbType sqlDbType)
        {
            switch (sqlDbType)
            {
                case SqlServer2012DbType.BigInt:
                    return 1;

                case SqlServer2012DbType.Binary:
                case SqlServer2012DbType.Image:
                case SqlServer2012DbType.Timestamp:
                case SqlServer2012DbType.VarBinary:
                    return new Byte[0];

                case SqlServer2012DbType.Bit:
                    return true;

                case SqlServer2012DbType.Char:
                case SqlServer2012DbType.NChar:
                case SqlServer2012DbType.NText:
                case SqlServer2012DbType.NVarChar:
                case SqlServer2012DbType.Text:
                case SqlServer2012DbType.VarChar:
                    return "a";
                case SqlServer2012DbType.Xml:
                    return "<xml></xml>";

                case SqlServer2012DbType.DateTime:
                case SqlServer2012DbType.SmallDateTime:
                case SqlServer2012DbType.Date:
                case SqlServer2012DbType.DateTime2:
                    return new DateTime(2000, 1, 1);

                case SqlServer2012DbType.Time:
                    return new TimeSpan(2, 0, 0);

                case SqlServer2012DbType.Decimal:
                case SqlServer2012DbType.Money:
                case SqlServer2012DbType.SmallMoney:
                    return 1;

                case SqlServer2012DbType.Float:
                    return 1;

                case SqlServer2012DbType.Int:
                    return 1;

                case SqlServer2012DbType.Real:
                    return 1;

                case SqlServer2012DbType.UniqueIdentifier:
                    return Guid.NewGuid();

                case SqlServer2012DbType.SmallInt:
                    return 1;

                case SqlServer2012DbType.TinyInt:
                    return 1;

                case SqlServer2012DbType.Variant:
                case SqlServer2012DbType.Udt:
                    return new Object();

                case SqlServer2012DbType.Structured:
                    return new DataTable();

                case SqlServer2012DbType.DateTimeOffset:
                    return new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.FromHours(9));

                default: throw new ArgumentException();
            }
        }
        private static Object GetParameterValue(this DataType dataType, MySqlDbType sqlDbType)
        {
            switch (sqlDbType)
            {
                case MySqlDbType.Binary:
                case MySqlDbType.Blob:
                case MySqlDbType.TinyBlob:
                case MySqlDbType.MediumBlob:
                case MySqlDbType.LongBlob:
                case MySqlDbType.VarBinary:
                    return new Byte[0];
                case MySqlDbType.Date:
                case MySqlDbType.Newdate:
                case MySqlDbType.DateTime:
                    return new DateTime(2000, 1, 1);
                case MySqlDbType.Enum:
                    return "a";
                case MySqlDbType.Geometry:
                    return new MySqlGeometry(40.735031, -73.768387);
                case MySqlDbType.Guid:
                    return Guid.NewGuid();
                case MySqlDbType.Bit:
                case MySqlDbType.Double:
                case MySqlDbType.Float:
                case MySqlDbType.Byte:
                case MySqlDbType.Int16:
                case MySqlDbType.Int24:
                case MySqlDbType.Int32:
                case MySqlDbType.Int64:
                case MySqlDbType.UByte:
                case MySqlDbType.UInt16:
                case MySqlDbType.UInt24:
                case MySqlDbType.UInt32:
                case MySqlDbType.UInt64:
                case MySqlDbType.Decimal:
                case MySqlDbType.NewDecimal:
                    return 1;
                case MySqlDbType.JSON:
                    return "{ \"Name\" : \"Name1\" }";
                case MySqlDbType.Set:
                    return "a";
                case MySqlDbType.Time:
                    return new TimeSpan(2, 0, 0);
                case MySqlDbType.Timestamp:
                    return null;
                case MySqlDbType.LongText:
                case MySqlDbType.MediumText:
                case MySqlDbType.String:
                case MySqlDbType.Text:
                case MySqlDbType.TinyText:
                case MySqlDbType.VarChar:
                case MySqlDbType.VarString:
                    return "a";
                case MySqlDbType.Year:
                    return 2000;
                default: throw new InvalidOperationException();
            }
        }
        private static Object GetParameterValue(this DataType dataType, NpgsqlDbType sqlDbType)
        {
            switch (sqlDbType)
            {
                case NpgsqlDbType.Array: return new Byte[0];
                case NpgsqlDbType.Bigint:
                    return 1;
                case NpgsqlDbType.Boolean:
                    return true;
                case NpgsqlDbType.Box:
                    return new NpgsqlTypes.NpgsqlBox();
                case NpgsqlDbType.Bytea:
                    return new Byte[0];
                case NpgsqlDbType.Circle:
                    return new NpgsqlTypes.NpgsqlCircle();
                case NpgsqlDbType.Char:
                    return "a";
                case NpgsqlDbType.Date:
                    return new DateTime(2000, 1, 1);
                case NpgsqlDbType.Double:
                case NpgsqlDbType.Integer:
                    return 1;
                case NpgsqlDbType.Line:
                    return new NpgsqlTypes.NpgsqlLine();
                case NpgsqlDbType.LSeg:
                    return new NpgsqlTypes.NpgsqlLSeg();
                case NpgsqlDbType.Money:
                case NpgsqlDbType.Numeric:
                    return 1.0m;
                case NpgsqlDbType.Path:
                    return new NpgsqlTypes.NpgsqlPath();
                case NpgsqlDbType.Point:
                    return new NpgsqlTypes.NpgsqlPoint();
                case NpgsqlDbType.Polygon:
                    return new NpgsqlTypes.NpgsqlPolygon();
                case NpgsqlDbType.Real:
                    return 1;
                case NpgsqlDbType.Smallint:
                    return 1;
                case NpgsqlDbType.Text:
                    return "a";
                case NpgsqlDbType.Time:
                case NpgsqlDbType.Timestamp:
                    return new DateTime(2000, 1, 1);
                case NpgsqlDbType.Varchar:
                    return "a";
                case NpgsqlDbType.Refcursor:
                    throw new NotSupportedException();
                case NpgsqlDbType.Inet:
                    return ValueTuple.Create(new System.Net.IPAddress(new byte[] { 127, 0, 0, 1 }, 0));
                case NpgsqlDbType.Bit:
                    return 1;
                case NpgsqlDbType.TimestampTZ:
                    return new DateTime(2000, 1, 1);
                case NpgsqlDbType.Uuid:
                    return new Guid();
                case NpgsqlDbType.Xml:
                    return "<xml></xml>";
                case NpgsqlDbType.Oidvector:
                    return new uint[0];
                case NpgsqlDbType.Interval:
                    return "1";
                case NpgsqlDbType.TimeTZ:
                    return new DateTimeOffset(new DateTime(2000, 1, 1), TimeSpan.FromHours(0));
                case NpgsqlDbType.Name:
                    return "a";
                case NpgsqlDbType.Abstime:
                    throw new NotSupportedException();
                case NpgsqlDbType.MacAddr:
                    return new System.Net.NetworkInformation.PhysicalAddress(new Byte[] { 127, 0, 0, 1 });
                case NpgsqlDbType.Json:
                case NpgsqlDbType.Jsonb:
                    return "{ \"Name\" : \"MyName\" }";
                case NpgsqlDbType.Hstore:
                    return new Dictionary<String, String>();
                case NpgsqlDbType.InternalChar:
                    return 'a';
                case NpgsqlDbType.Varbit:
                    return true;
                case NpgsqlDbType.Unknown:
                    throw new NotSupportedException();
                case NpgsqlDbType.Oid:
                    return 1;
                case NpgsqlDbType.Xid:
                    return 1;
                case NpgsqlDbType.Cid:
                    return 1;
                case NpgsqlDbType.Cidr:
                    return ValueTuple.Create(new System.Net.IPAddress(new byte[] { 127, 0, 0, 1 }, 0));
                case NpgsqlDbType.TsVector:
                    return NpgsqlTypes.NpgsqlTsVector.Parse("a");
                case NpgsqlDbType.TsQuery:
                    return NpgsqlTypes.NpgsqlTsQuery.Parse("a");
                case NpgsqlDbType.Regtype:
                    return new NotSupportedException();
                case NpgsqlDbType.Geometry:
                    throw new NotSupportedException();
                case NpgsqlDbType.Citext:
                    return "a";
                case NpgsqlDbType.Int2Vector:
                case NpgsqlDbType.Tid:
                    throw new NotSupportedException();
                case NpgsqlDbType.MacAddr8:
                    return new PhysicalAddress(new byte[] { 127, 0, 0, 1 });
                case NpgsqlDbType.Geography:
                case NpgsqlDbType.Regconfig:
                case NpgsqlDbType.Range:
                    throw new NotSupportedException();
                default: throw new InvalidOperationException();
            }
        }
        private static Object GetParameterValue(this DataType dataType, OracleDbType sqlDbType)
        {
            switch (sqlDbType)
            {
                case OracleDbType.BFile:
                case OracleDbType.Blob:
                    return new Byte[0];
                case OracleDbType.Byte:
                    return 1;
                case OracleDbType.Char:
                    return "a";
                case OracleDbType.Clob:
                    return 1;
                case OracleDbType.Date:
                    return new DateTime(2000, 1, 1);
                case OracleDbType.Decimal:
                    return 1.0m;
                case OracleDbType.Double:
                    return 1;
                case OracleDbType.Long:
                    return 1;
                case OracleDbType.LongRaw:
                    return new Byte[0];
                case OracleDbType.Int16:
                case OracleDbType.Int32:
                case OracleDbType.Int64:
                    return 1;
                case OracleDbType.IntervalDS:
                    return TimeSpan.FromHours(1);
                case OracleDbType.IntervalYM:
                    return 1;
                case OracleDbType.NClob:
                    return 1;
                case OracleDbType.NChar:
                case OracleDbType.NVarchar2:
                    return "a";
                case OracleDbType.Raw:
                    return new Byte[0];
                case OracleDbType.RefCursor:
                    throw new NotSupportedException();
                case OracleDbType.Single:
                    return 1;
                case OracleDbType.TimeStamp:
                case OracleDbType.TimeStampLTZ:
                case OracleDbType.TimeStampTZ:
                    return new DateTime(2000, 1, 1);
                case OracleDbType.Varchar2:
                    return "a";
                case OracleDbType.XmlType:
                    return "<xml></xml>";
                case OracleDbType.BinaryDouble:
                case OracleDbType.BinaryFloat:
                    return 1;
                case OracleDbType.Boolean:
                    return true;
                default:throw new InvalidOperationException();
            }
        }
    }

    public static class MySqlDbTypeExtensions
    {
        public static MySql.Data.MySqlClient.MySqlDbType GetMySqlDbType(this HigLabo.DbSharp.MetaData.MySqlDbType type)
        {
            return type.ToStringFromEnum().ToEnum<MySql.Data.MySqlClient.MySqlDbType>().Value;
        }
    }
    public static class NpgsqlDbTypeExtensions
    {
        public static NpgsqlTypes.NpgsqlDbType GetMySqlDbType(this HigLabo.DbSharp.MetaData.NpgsqlDbType type)
        {
            return type.ToStringFromEnum().ToEnum<NpgsqlTypes.NpgsqlDbType>().Value;
        }
    }
    public static class OracleDbTypeExtensions
    {
        public static Oracle.ManagedDataAccess.Client.OracleDbType GetMySqlDbType(this HigLabo.DbSharp.MetaData.OracleDbType type)
        {
            return type.ToStringFromEnum().ToEnum<Oracle.ManagedDataAccess.Client.OracleDbType>().Value;
        }
    }
}
