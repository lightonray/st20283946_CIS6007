using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDS2
{
    class ToolInventory
    {
        private const int TOTAL_ITEMS = 100000;
        private const int NUM_TOOL_TYPES = 100;

        public List<Tool> GenerateRandomInventory()
        {
            Random random = new Random();
            List<Tool> inventory = new List<Tool>();

            for (int i = 0; i < TOTAL_ITEMS; i++)
            {
                int randomType = random.Next(1, NUM_TOOL_TYPES + 1);

                //Creates a unique random barcode for each tool
                string randomBarcode = Guid.NewGuid().ToString().Substring(0, 8);

                inventory.Add(new Tool { Type = randomType, Barcode = randomBarcode });
            }

            return inventory;
        }
    }
}
