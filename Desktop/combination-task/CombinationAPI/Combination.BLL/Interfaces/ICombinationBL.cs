using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combination.BLL.Interfaces
{
    public interface ICombinationBL
    {
        long CalcTotalCombinations(int n);
        int[] GetNextCombination(long index, int n, long factor);
        List<int[]> GetCombinationsByPage(int pageIndex, int pageSize, int n, long total);

    }
}
