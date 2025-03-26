using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asm.As.Oib.FactoryCalendar.Proxy.Business.Types;

namespace FcSmartProxyDemo
{
    //public interface IAppointmentTypeMapper
    //{
    //    bool TryParse(string name, out AppointmentType appointmentType);
    //    IEnumerable<string> GetNames();
    //    string GetName(AppointmentType appointmentType);
    //}
    //public class AppointmentTypeMapper: IAppointmentTypeMapper
    //{
    //    private Dictionary<string, AppointmentType> _substitutions = new Dictionary<string, AppointmentType>();
    //    private List<AppointmentType> _forbiddenTypes = new List<AppointmentType>();
    //    private bool _allowObsolete = false;

    //    public void AddSubstitution(string name, AppointmentType appointmentType)
    //    {
    //        _substitutions.Add(name, appointmentType);
    //    }
    //    public void Forbid(AppointmentType appointmentType)
    //    {
    //        if(!_forbiddenTypes.Contains(appointmentType))
    //            _forbiddenTypes.Add(appointmentType);
    //    }

    //    public void AllowObsolete()
    //    {
    //        _allowObsolete = true;
    //    }
    //    public void ForbidObsolete()
    //    {
    //        _allowObsolete = false;
    //    }
    //    public bool TryParse(string name, out AppointmentType appointmentType)
    //    {
    //        AppointmentType resultType;
    //        bool result = false;
    //        if (string.IsNullOrWhiteSpace(name))
    //        {
    //            resultType = AppointmentType.Undefined;
    //            result = true;
    //        }
    //        else if (_substitutions.ContainsKey(name))
    //        {
    //            resultType = _substitutions[name];
    //            result = true;
    //        }
    //        else if (Enum.TryParse(name, true, out resultType))
    //        {
    //            result = true;
    //        }

    //        appointmentType = resultType;
    //        return result && !_forbiddenTypes.Contains(resultType);
    //    }

    //    public string GetName(AppointmentType appointmentType)
    //    {
    //        if (!_allowObsolete && IsObsolete(appointmentType))
    //            return null;
    //        if (_forbiddenTypes.Contains(appointmentType))
    //            return null;
    //        return _substitutions.ContainsValue(appointmentType)
    //            ? _substitutions.First(x => x.Value == appointmentType).Key
    //            : appointmentType.ToString();
    //    }
    //    public IEnumerable<string> GetNames()
    //    {
    //        List<string> allowedNames = new List<string>();
    //        foreach (AppointmentType appointmentType in Enum.GetValues(typeof(AppointmentType)))
    //        {
    //            string name = GetName(appointmentType);
    //            if(name != null) allowedNames.Add(name);
    //        }
    //        return allowedNames;
    //    }
    //    public static bool IsObsolete(Enum value)
    //    {
    //        var fi = value.GetType().GetField(value.ToString());
    //        var attributes = (ObsoleteAttribute[])fi.GetCustomAttributes(typeof(ObsoleteAttribute), false);
    //        return attributes.Length > 0;
    //    }
    //}
    public interface IEnumMapper<T> where T: struct
    {
        bool TryParse(string name, out T value);
        IEnumerable<string> GetNames();
        string GetNameOrNull(T value);
    }
    public class EnumMapper<TEnum> : IEnumMapper<TEnum> where TEnum : struct
    {
        private Dictionary<string, TEnum> _substitutions = new Dictionary<string, TEnum>();
        private List<TEnum> _forbiddenValues = new List<TEnum>();
        private bool _allowObsolete = false;

        public void AddSubstitution(string name, TEnum value)
        {
            _substitutions.Add(name, value);
        }
        public void Forbid(TEnum value)
        {
            if (!_forbiddenValues.Contains(value))
                _forbiddenValues.Add(value);
        }

        public void AllowObsolete()
        {
            _allowObsolete = true;
        }
        public void ForbidObsolete()
        {
            _allowObsolete = false;
        }
        public bool TryParse(string name, out TEnum value)
        {
            TEnum resultValue;
            bool result = false;
            if (string.IsNullOrWhiteSpace(name))
            {
                resultValue = default(TEnum);
                result = true;
            }
            else if (_substitutions.ContainsKey(name))
            {
                resultValue = _substitutions[name];
                result = true;
            }
            else if (Enum.TryParse(name, true, out resultValue))
            {
                result = true;
            }

            value = resultValue;
            return result && !_forbiddenValues.Contains(resultValue);
        }

        public string GetNameOrNull(TEnum value)
        {
            if (!IsAllowedValue(value))
                return null;
            return _substitutions.ContainsValue(value)
                ? _substitutions.First(x => value.Equals(x.Value)).Key
                : value.ToString();
        }
        public IEnumerable<string> GetNames()
        {
            List<string> allowedNames = new List<string>();
            foreach (TEnum value in Enum.GetValues(typeof(AppointmentType)))
            {
                string name = GetNameOrNull(value);
                if (name != null) allowedNames.Add(name);
            }
            return allowedNames;
        }

        public bool IsAllowedValue(TEnum value)
        {
            if (!_allowObsolete && IsObsolete(value))
                return false;
            if (_forbiddenValues.Contains(value))
                return false;
            return true;
        }
        public static bool IsObsolete(TEnum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (ObsoleteAttribute[])fi.GetCustomAttributes(typeof(ObsoleteAttribute), false);
            return attributes.Length > 0;
        }
    }
}
