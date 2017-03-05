using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace BLL
{
    /// <summary>
    /// 实体类之间的转换
    /// </summary>
    public class EntityConvert
    {
        /// <summary>
        /// 同一个类的属性赋值
        /// </summary>
        /// <param name="s">来源类</param>
        /// <param name="t">目标类</param>
        /// <returns></returns>
        public static T ConvertClass<S,T>(S s, T t)
        {
            //获取来源类的所有属性,目标类的所有属性
            PropertyInfo[] sPI = typeof(S).GetProperties();
            //PropertyInfo[] tPI = typeof(T).GetProperties();
            Type type = typeof(T);
            PropertyInfo propertyInfo;
            //循环遍历,相同属性赋值
            foreach (PropertyInfo spi in sPI)
            {
                //判断来源类某个属性是否为数组或泛型
                if (spi.PropertyType.IsArray || spi.PropertyType.IsGenericType)
                    continue;
                //判断属性是否为自定义类类型（字符串也是类类型，但它是密封的）
                if (spi.PropertyType.IsClass && !spi.PropertyType.IsSealed)
                    continue;

                propertyInfo = type.GetProperty(spi.Name);
                if (spi.PropertyType.IsEnum)
                {
                    object obj = spi.GetValue(s, null);
                    //tpi.SetValue(t,Enum.ToObject(typeof(T).GetProperty(spi.Name).PropertyType,obj),null);
                    propertyInfo.SetValue(t, Enum.ToObject(propertyInfo.PropertyType, obj), null);
                    continue;
                }
                //判断属性是否可写
                if (!spi.CanWrite)
                    continue;
                //相同属性名称,获取来源实体对象该属性的值赋值为目标实体对象的该属性
                //tpi.SetValue(t, spi.GetValue(s, null), null);
                propertyInfo.SetValue(t, spi.GetValue(s, null), null);
            }
            return t;
        }

    }
}
