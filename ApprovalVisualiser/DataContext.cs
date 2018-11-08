using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalVisualiser
{
    public static class MyDataContext
    {
        public static IEnumerable<ItemSourceData> GetItemsFromEnum<T>() {
            return Enum.GetValues(typeof(T))
         .Cast<T>()
         .Select(v => new ItemSourceData { Code = Convert.ToInt32(v), Description = v.ToString() })
         .ToList();
        }
    }


public class ItemSourceData
{
    public int Code { get; set; }
    public string Description { get; set; }
}
}
