using Entities.Models;
using Services.Concreate;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DataShaping<T> : IDateShaping<T> where T : class
    {

        public PropertyInfo[] Properties { get; set; }

        public DataShaping(PropertyInfo[] properties)
        {
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        public IEnumerable<ShaperEntity> ShapeData(IEnumerable<T> entities, string fieldString)
        {
            var requiredFeilds = GetRequiredProperties(fieldString);
             return FetchData(entities, requiredFeilds);
        }

        public ShaperEntity ShapeData(T entity, string fieldString)
        {
            var requiredProperties = GetRequiredProperties(fieldString);
            return FetchDataForEntity(entity, requiredProperties);  
        }
        // secilan alanlarini almak istedik
        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldString)
        {
            // burda esa properties lari list olarak tanim yapmis 
            var requiredFields = new List<PropertyInfo>();
            if (!string.IsNullOrWhiteSpace(fieldString)) 
            {
               // surda su on split yani bolmek icin calisyor   
                var fields = fieldString.Split(',',StringSplitOptions.RemoveEmptyEntries);

                foreach (var field in fields)
                {
                    var property = Properties.FirstOrDefault(pi => pi.Name.Equals(field.Trim(),StringComparison.InvariantCultureIgnoreCase));   
                    if(property is null)
                    { continue; }
                    requiredFields.Add(property);
                }
                
            }
            else
            {
                // eger burda veri sekilandirma yapmiyorsak direk else inar ve list olarak bize donar  
                requiredFields = Properties.ToList();
            }

            return requiredFields;



        }

        private ShaperEntity FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requeiredProperties)
        {
            var shapedObject = new ShaperEntity();

            foreach (var property in requeiredProperties)
            {
                var objectPeoperty = property.GetValue(entity);

                shapedObject.Entity.TryAdd(property.Name, objectPeoperty);
            }

            // su on burda biz id ve type aliyoruz 
            var objectProperty = entity.GetType().GetProperty("Id");
            shapedObject.Id = (int)objectProperty.GetValue(entity);
                // sonra esa id icina atiyoruz                 
            return shapedObject;
        }     

        private IEnumerable<ShaperEntity> FetchData(IEnumerable<T> entities , IEnumerable<PropertyInfo> requeiredProperties)
        {
             var shapedData = new List<ShaperEntity>();

            foreach (var entity in entities)
            {
                var shapedObject = FetchDataForEntity(entity, requeiredProperties);
                shapedData.Add(shapedObject);   
            }
            return shapedData;
        }     

    }
    
}
