using System;
using System.Linq.Expressions;
using System.Reflection;
using Panda.Tools.Exception;

namespace Panda.Tools.Extensions
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 根据属性名获取属性值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="name">属性名</param>
        /// <returns>属性的值</returns>
        public static object GetPropertyValue<T>(this T t, string name)
        {
            Type type = t.GetType();
            PropertyInfo p = type.GetProperty(name);
            if (p == null)
            {
                throw new UserException(String.Format("该类型没有名为{0}的属性", name));
            }

            var param_obj = Expression.Parameter(typeof(T));
            var param_val = Expression.Parameter(typeof(object));

            //转成真实类型，防止Dynamic类型转换成object
            var body_obj = Expression.Convert(param_obj, type);

            var body = Expression.Property(body_obj, p);
            var getValue = Expression.Lambda<Func<T, object>>(body, param_obj).Compile();
            return getValue(t);
        }

        /// <summary>
        /// 根据属性名称设置属性的值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="name">属性名</param>
        /// <param name="value">属性的值</param>
        public static void SetPropertyValue<T>(this T t, string name, object value)
        {
            Type type = t.GetType();
            PropertyInfo p = type.GetProperty(name);
            if (p == null)
            {
                throw new UserException(String.Format("该类型没有名为{0}的属性", name));
            }


            var param_obj = Expression.Parameter(type);
            var param_val = Expression.Parameter(typeof(object));
            var body_obj = Expression.Convert(param_obj, type);
            var body_val = Expression.Convert(param_val, p.PropertyType);

            //获取设置属性的值的方法
            var setMethod = p.GetSetMethod(true);

            //如果只是只读,则setMethod==null
            if (setMethod == null) return;
            var body = Expression.Call(param_obj, p.GetSetMethod(), body_val);
            var setValue = Expression.Lambda<Action<T, object>>(body, param_obj, param_val).Compile();
            if (p.PropertyType == typeof(Int32))
            {
                setValue(t, Convert.ToInt32(value));
            }
            else if (p.PropertyType == typeof(Boolean))
            {
                setValue(t, Convert.ToBoolean(value));
            }
            else if (p.PropertyType == typeof(string))
            {
                setValue(t, value.ToString() ?? string.Empty);
            }
            else
            {
                setValue(t, value);
            }
        }
    }
}