using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Tcc.Apoio;

namespace Tcc
{
    public abstract class Modelo : GenClass
    {
        public void Update(Modelo prModelo)
        {  
            PropertyInfo[] propertiesPAR = prModelo.GetType().GetProperties();
            PropertyInfo propertyTHIS;

            foreach (PropertyInfo propertyPAR in propertiesPAR)
            {
                if (propertyPAR.GetCustomAttributes(typeof(KeyAttribute), true).Length == 0)
                {
                    propertyTHIS = GetType().GetProperty(propertyPAR.Name);
                    if (propertyTHIS != null && propertyTHIS.CanWrite && propertyPAR.PropertyType.Name == propertyTHIS.PropertyType.Name)
                        propertyTHIS.SetValue(this, propertyPAR.GetValue(prModelo, null), null);
                }
            }
        }
    }
}