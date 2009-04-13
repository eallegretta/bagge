using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities.Exceptions;
using Bagge.Seti.BusinessLogic.Properties;
using Bagge.Seti.DesignByContract;

namespace Bagge.Seti.BusinessLogic
{
	public class ProductManager : AuditableGenericManager<Product, int>, IProductManager
	{
		public ProductManager(IProductDao dao)
			: base(dao)
		{
		}


		public Product[] FindAllByName(string productName, int maxRecords)
		{
			Check.Require(!string.IsNullOrEmpty(productName));

			FilterPropertyValue filter = new FilterPropertyValue();
			filter.Property = "Name";
			filter.Type = FilterPropertyValueType.Like;
			filter.Value = productName;

			return Dao.SlicedFindAllByPropertiesOrdered(
				0,
				maxRecords,
				new List<FilterPropertyValue> { filter },
				"Name");
		}
		
		public Product[] FindAllByName(string productName)
		{
			Check.Require(!string.IsNullOrEmpty(productName));

			FilterPropertyValue filter = new FilterPropertyValue();
			filter.Property = "Name";
			filter.Type = FilterPropertyValueType.Like;
			filter.Value = productName;

			return Dao.FindAllByPropertiesOrdered(
				new List<FilterPropertyValue> { filter }, 
				"Name");
		}

		public Product GetByName(string name)
		{
			Check.Require(!string.IsNullOrEmpty(name));

			var products = Dao.FindAllByProperty("Name", name);
			if (products.Length > 0)
				throw new BusinessRuleException(Resources.MultipleNamesErrorMessage);

			if (products.Length == 1)
				return products[0];

			throw new ObjectNotFoundException(Resources.InstanceNotFound);
		}
	}
}
