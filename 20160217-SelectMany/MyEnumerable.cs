using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20160217_SelectMany
{
    public static class MyEnumerable
    {

        #region CASE 1 的 SelectMany() 源碼

        //參數1: this IEnumerable<TSource> source				--> 即 IEnumerable<PetOwner>
        //參數2: Func<TSource, IEnumerable<TResult>> selector	--> 即 Func<PetOwner, IEnumerable<String>>
        //source: PetOwner[] 陣列 (共 3 個元素)
        //selector: petOwner => petOwner.Pets, 其中, petOwner 為 PetOwner 類別的物件實體
        public static IEnumerable<TResult> MySelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            //if (source == null) throw Error.ArgumentNull("source");
            //if (selector == null) throw Error.ArgumentNull("selector");
            return MySelectManyIterator<TSource, TResult>(source, selector);
        }

        static IEnumerable<TResult> MySelectManyIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            //source: PetOwner[] 陣列 (共 3 個元素)
            //element: PetOwner[] 陣列裡的每個元素, 即 PetOwner 的物件實體
            //selector: petOwner => petOwner.Pets, 其中, petOwner 即為 element
            foreach (TSource element in source)
            {
                //執行 selector(element) 之後, 會產出 petOwner.Pets 回傳
                foreach (TResult subElement in selector(element))
                {
                    yield return subElement;
                }
            }
        }
        #endregion

        #region  CASE 2 的 SelectMany() 源碼

        //參數1: this IEnumerable<TSource> source					--> 即 IEnumerable<PetOwner>
        //參數2: Func<TSource, int, IEnumerable<TResult>> selector	--> 即 Func<PetOwner, int, IEnumerable<String>>
        //source: PetOwner[] 陣列 (共 4 個元素)
        //selector: (petOwner, index) => petOwner.Pets.Select(pet => index + pet), 其中, petOwner 為 PetOwner 類別的物件實體, index 為 int
        public static IEnumerable<TResult> MySelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
        {
            //		if (source == null) throw Error.ArgumentNull("source");
            //		if (selector == null) throw Error.ArgumentNull("selector");
            return MySelectManyIterator<TSource, TResult>(source, selector);
        }

        static IEnumerable<TResult> MySelectManyIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
        {
            //source: PetOwner[] 陣列 (共 4 個元素)
            //element: PetOwner[] 陣列裡的每個元素, 即 PetOwner 的物件實體
            //selector: (petOwner, index) => petOwner.Pets.Select(pet => index + pet); 其中, petOwner 即為 element, index 為 int
            //  註: index 代表陣列元素的位置, 由 0 起算
            int index = -1;
            foreach (TSource element in source)
            {
                checked { index++; }
                foreach (TResult subElement in selector(element, index))
                {
                    yield return subElement;
                }
            }
        }
        #endregion

        #region  CASE 3 的 SelectMany() 源碼

        //參數1: this IEnumerable<TSource> source								--> 即 IEnumerable<PetOwner>
        //參數2: Func<TSource, IEnumerable<TCollection>> collectionSelector		--> 即 Func<PetOwner, IEnumerable<String>>
        //參數3: Func<TSource, TCollection, TResult> resultSelector				--> 即 Func<PetOwner, String, IEnumerable<String>>
        //source: PetOwner[] 陣列 (共 4 個元素)
        //collectionSelector: petOwner => petOwner.Pets
        //resultSelector:  (petOwner, petName) => new { petOwner, petName }
        public static IEnumerable<TResult> MySelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            // if (source == null) throw Error.ArgumentNull("source");
            // if (collectionSelector == null) throw Error.ArgumentNull("collectionSelector");
            // if (resultSelector == null) throw Error.ArgumentNull("resultSelector");
            return MySelectManyIterator<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }

        static IEnumerable<TResult> MySelectManyIterator<TSource, TCollection, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            //source: PetOwner[] 陣列 (共 4 個元素)
            //element: PetOwner[] 陣列裡的每個元素, 即 PetOwner 的物件實體
            //collectionSelector: petOwner => petOwner.Pets
            //resultSelector:  (petOwner, petName) => new { petOwner, petName }
            foreach (TSource element in source)
            {
                //把 element 傳入 collectionSelector 執行	
                //petOwner => petOwner.Pets  ---> element => element.Pets
                foreach (TCollection subElement in collectionSelector(element))
                {
                    //把 element, subElement 傳入 resultSelector 執行 
                    //(petOwner, petName) => new { petOwner, petName } 	---> (element, subElement) => new { element, subElement }
                    yield return resultSelector(element, subElement);
                }
            }
        }

        #endregion

    }
}
