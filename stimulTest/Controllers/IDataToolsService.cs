using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace stimulTest.Controllers
{
    public interface IDataToolsService
    {
        string GetQueryStringOfObject(object input);
        void SetNullStringsEmpty(object input);
        int BGRtoRGB(int BGR);
        Expression<Func<TModel, Dictionary<string, object>>> GetDictionarySelectorExpression<TModel>(List<string> Columns);
    }

    public class DataToolsService : IDataToolsService
    {
        /// <summary>
        /// Returns querystring equivalent of input object
        /// <br/>
        /// <strong>
        /// Lists and other complex datatypes are not supported as properties
        /// </strong>
        /// </summary>
        public string GetQueryStringOfObject(object input)
        {
            var properties = from p in input.GetType().GetProperties()
                             where p.GetValue(input, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(input, null).ToString());

            string queryString = String.Join("&", properties.ToArray());

            return queryString;
        }
        /// <summary>
        /// Gets an object and searches for null string properties, and fills them with empty string
        /// </summary>
        /// <param name="input">Object to fill its null strings</param>
        public void SetNullStringsEmpty(object input)
        {
            foreach (var property in input.GetType()
                                            .GetProperties()
                                            .Where(p => p.PropertyType == typeof(string) && p.GetValue(input, null) == null))
            {
                property.SetValue(input, "");
            }
        }
        /// <summary>
        /// Converts color number in BGR format into RGB equivalent
        /// </summary>
        public int BGRtoRGB(int BGR)
        {
            string Hex = BGR.ToString("X6");
            Hex = $"{Hex.Substring(4)}{Hex.Substring(2, 2)}{Hex.Substring(0, 2)}";
            BGR = int.Parse(Hex, System.Globalization.NumberStyles.HexNumber);
            return BGR;
        }


        public Expression<Func<TModel, Dictionary<string, object>>> GetDictionarySelectorExpression<TModel>(List<string> Columns)
        {
            Type modelType = typeof(TModel);

            var itemParam = Expression.Parameter(modelType, "x");

            var addMethod = typeof(Dictionary<string, object>).GetMethod(
                "Add", new[] { typeof(string), typeof(object) });
            var selector = Expression.ListInit(
                    Expression.New(typeof(Dictionary<string, object>)),
                    Columns.Select(field => Expression.ElementInit(addMethod,
                        Expression.Constant(field),
                        Expression.Convert(
                            Expression.PropertyOrField(itemParam, field),
                            typeof(object)
                        )
                    )));
            var lambda = Expression.Lambda<Func<TModel, Dictionary<string, object>>>(
                selector, itemParam);
            return lambda;
        }
    }
}