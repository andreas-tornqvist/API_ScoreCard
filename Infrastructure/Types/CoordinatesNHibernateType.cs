using NHibernate.UserTypes;
using System;
using System.Linq;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using System.Data.Common;
using System.Data;
using GeoAPI.Geometries;
using Common.HelperClasses;

namespace Infrastructure.Types
{
    public class CoordinatesNHibernateType : IUserVersionType
    {
        SqlType[] _sqlTypes;
        public CoordinatesNHibernateType()
        {
            _sqlTypes = new[] { SqlTypeFactory.GetSqlType(DbType.String, 0, 0) };
        }
        public SqlType[] SqlTypes => _sqlTypes;

        public Type ReturnedType => typeof(Coordinate);

        public bool IsMutable => false;

        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public int Compare(object x, object y)
        {
            return 0;
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Disassemble(object value)
        {
            return value;
        }

        public new bool Equals(object x, object y)
        {
            return x?.Equals(y) ?? false;
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public object Next(object current, ISessionImplementor session)
        {
            return null;
        }

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            return StringToCoordinates(rs, names);
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var param = (IDataParameter)cmd.Parameters[index];
            param.DbType = _sqlTypes[0].DbType;
            var coordinates = (Coordinates)value;
            if (coordinates != null)
            {
                param.Value = $"{coordinates.Latitude} {coordinates.Longitude}";
            }
            else
                param.Value = "";
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public object Seed(ISessionImplementor session)
        {
            return null;
        }

        private object StringToCoordinates(DbDataReader rs, string[] names)
        {
            if (names != null && names.Length > 0 && rs[names[0]] as string != null)
            {
                var coordinates = ((string)rs[names[0]]).Split();
                if (coordinates.Count() != 2) return null;
                if (!float.TryParse(coordinates[0], out float latitude) || !float.TryParse(coordinates[1], out float longitude))
                    return null;
                var coordinate = new Coordinates(latitude, longitude);
                return coordinate;
            }
            return null;
        }
    }
}
