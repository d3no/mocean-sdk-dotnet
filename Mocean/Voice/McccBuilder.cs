using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice
{
    public class McccBuilder
    {
        protected List<AbstractMccc> mccc;

        public McccBuilder()
        {
            this.mccc = new List<AbstractMccc>();
        }

        public McccBuilder add(AbstractMccc mccc)
        {
            this.mccc.Add(mccc);
            return this;
        }

        public List<Dictionary<string, object>> build()
        {
            List<Dictionary<string, object>> converted = new List<Dictionary<string, object>>();
            foreach (AbstractMccc mccc in this.mccc)
            {
                converted.Add(mccc.GetRequestData());
            }
            return converted;
        }
    }
}
