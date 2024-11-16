using Combination.BLL.Interfaces;
using Combination.BLL.Services;
using System.Numerics;

namespace Combination.BLL
{
    public class CombinationBL : ICombinationBL
    {
        private readonly CombinationService _combinationService;

        public CombinationBL(CombinationService combinationService)
        {
            _combinationService = combinationService;
        }

        public long CalcTotalCombinations(int n)
        {
            _combinationService.SetN(n);

            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            _combinationService.SetTotalCombinations(result);

            return result;
        }

        public List<int[]> GetCombinationsByPage(int pageIndex, int pageSize, int n, long total) 
        {
            var start = pageIndex * pageSize; 
            var end = start + pageSize <= total? start + pageSize : total;
            //int endIndex = (int)Math.Min(start + pageSize, total);

            var result = new List<int[]>();
            
            for (var i = start; i < end; i++) 
            { 
                result.Add(GetNextCombination(i + 1,n, total)); 
            } 
            return result; 
        }
        public int[] GetNextCombination(long index,int n,long factor)
        {
                
            var elements = new List<int>();
            for (int i = 1; i <= n; i++)
                elements.Add(i);

            var result = new int[n];
            index--;

            for (var i = 0; i < n; i++)
            {
                factor /= (n - i);
                var choice = (int)(index / factor);
                result[i] = elements[choice];
                elements.RemoveAt(choice);
                index %= factor;
            }
            return result;
        }

    }

}