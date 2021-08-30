using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace MyLibrary.Model
{
    public class DynamicModel : DynamicObject
    {
        public string EmpName { get; set; }
        public string TotalHour { get; set; }
        public string TotalWork { get; set; }

        Dictionary<string, object> Properties = new Dictionary<string, object>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!Properties.Keys.Contains(binder.Name))
            {
                //在此可以做一些小动作
                //if (binder.Name == "Col")
                //　　Properties.Add(binder.Name + (Properties.Count), value.ToString());
                //else
                //　　Properties.Add(binder.Name, value.ToString());

                Properties.Add(binder.Name, value.ToString());
            }
            return true;
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return Properties.TryGetValue(binder.Name, out result);
        }
    }
}
