using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ApprovalVisualiser
{
    public static class MyDataContext 
    {
        public static ObservableCollection<ItemSourceData> GetItemsFromEnum<T>() {
            return Enum.GetValues(typeof(T))
         .Cast<T>()
         .Select(v => new ItemSourceData { Code = Convert.ToInt32(v), Description = v.ToString() })
         .AsObservableCollection();
        }

        public static ObservableCollection<T> AsObservableCollection<T>
    (this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new ObservableCollection<T>(source);
        }
    }


public class ItemSourceData
{
    public int Code { get; set; }
    public string Description { get; set; }
}
}
