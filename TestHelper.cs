using System;
using System.Reflection;
using System.Linq;

namespace UnitTests
{
	public static class TestHelper
	{
		public static Random Rnd
		{
			get
			{
				return new Random((int)DateTime.UtcNow.Ticks);
			}
		}

		public static string RndString()
		{
			return Guid.NewGuid().ToString().Replace("-", "");
		}
		
		public static int RndInt()
		{
			return Rnd.Next(0, 1000);
		}
		
		public static int RndInt(int min, int max)
		{
			return Rnd.Next(min, max);
		}
		
		public static bool RndBool()
		{
			return Rnd.Next(0, 100) % 2 == 0;
		}
		
		public static T Fill<T>(T obj, params string[] excludedProperties)
		{
			var properties = obj.GetType().GetProperties();
			foreach (PropertyInfo property in properties)
			{
				if (!property.CanWrite 
					|| (excludedProperties != null && excludedProperties.Contains(property.Name))
					|| property.Name == typeof(T).Name + "Id")
				{
					continue;
				}
				
				if (property.PropertyType.IsEnum)
				{
					var values = Enum.GetValues(property.PropertyType);
					
					property.SetValue(obj, values.GetValue(Rnd.Next(0, values.Length)), null);
					
					continue;
				}
				
				var typeName = GetTypeName(property.PropertyType);
				switch (typeName)
				{
					case "int32":
					case "int64":
						property.SetValue(obj, RndInt(), null);
						break;
					case "int32?":
						if (RndBool())
							property.SetValue(obj, RndInt() as int?, null);
						break;
					case "int64?":
						if (RndBool())
							property.SetValue(obj, (long?)RndInt(), null);
						break;
					case "double?":
						if (RndBool())
							property.SetValue(obj, Rnd.NextDouble() * RndInt(), null);
						break;
					case "double":
						property.SetValue(obj, Rnd.NextDouble() * RndInt(), null);
						break;
					case "string":
						property.SetValue(obj, RndString(), null);
						break;
					case "datetime":
						property.SetValue(obj, DateTime.UtcNow.AddMilliseconds(RndInt()), null);
						break;
				}
			}
			
			return obj;
		}
		
		public static T RndEnum<T>(T currentValue)
		{
			var values = Enum.GetValues(typeof(T));
			T val;
			do
			{
				val = (T)values.GetValue(Rnd.Next(0, values.Length));
			}
			while(Enum.Equals (val, currentValue));
			
			return val;
		}
				
		static string GetTypeName(Type type)
		{
			switch (type.Name.ToLower())
			{
				case "nullable`1":
					return string.Format("{0}?", type.GetGenericArguments()[0].Name.ToLower());
				default:
					return type.Name.ToLower();
			}
		}
	}
}

