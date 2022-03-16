using System;

namespace AlbelliOrderPortal.Helpers
{
    public class BinWidthCompuation
    {
        public static double ComputeBinWidth(ProductType name, int quantity)
        {
           if (!Enum.IsDefined(typeof(ProductType), name))
            {
                throw new NotImplementedException();
            }

            double requiredBinWidth = 0;

            switch (name)
            {
                case ProductType.PhotoBook:
                    requiredBinWidth = 19 * quantity;
                    break;
                case ProductType.Calendar:
                    requiredBinWidth = 10 * quantity;
                    break;
                case ProductType.Canvas:
                    requiredBinWidth = 16 * quantity;
                    break;
                case ProductType.Cards:
                    requiredBinWidth = 4.7 * quantity;
                    break;
                case ProductType.Mug:
                    requiredBinWidth = 94 * Math.Ceiling((Double)quantity/4);
                    break;
                default:
                    break;
            }
            return Math.Round(requiredBinWidth, 2);
        }
    }
}
