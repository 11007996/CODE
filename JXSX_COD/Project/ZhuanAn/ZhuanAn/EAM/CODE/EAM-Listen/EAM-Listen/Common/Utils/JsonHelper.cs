using Newtonsoft.Json.Linq;
using System.Linq;

namespace EAM.Listen.Common.Utils
{
    public class JsonHelper
    {
        public static bool AreJsonStructuresEqual(JToken token1, JToken token2)
        {
            // 类型必须相同
            if (token1.Type != token2.Type)
                return false;

            switch (token1.Type)
            {
                case JTokenType.Object:
                    return AreObjectsStructurallyEqual((JObject)token1, (JObject)token2);

                case JTokenType.Array:
                    return AreArraysStructurallyEqual((JArray)token1, (JArray)token2);

                default: return false;
            };
        }

        private static bool AreObjectsStructurallyEqual(JObject obj1, JObject obj2)
        {
            var props1 = obj1.Properties().Select(p => p.Name).OrderBy(x => x).ToList();
            var props2 = obj2.Properties().Select(p => p.Name).OrderBy(x => x).ToList();

            if (!props1.SequenceEqual(props2))
                return false;

            // 递归比较每个属性的子结构
            foreach (var prop in props1)
            {
                if (!AreJsonStructuresEqual(obj1[prop], obj2[prop]))
                    return false;
            }

            return true;
        }

        private static bool AreArraysStructurallyEqual(JArray arr1, JArray arr2)
        {
            // 数组结构相同：只要求元素类型结构一致（不要求长度相同？看需求）
            // 常见做法：比较第一个元素的结构（假设数组是同质的）
            // 或者：要求所有对应位置的元素结构一致（需长度相同）

            // 方案 A：只要数组内元素结构一致（同质数组），不要求长度
            if (arr1.Count == 0 && arr2.Count == 0) return true;
            if (arr1.Count == 0 || arr2.Count == 0) return false; // 一个空一个非空 → 结构不同

            // 假设数组是同质的：用第一个元素代表整个数组结构
            return AreJsonStructuresEqual(arr1[0], arr2[0]);

            // 方案 B（更严格）：要求长度相同 + 每个位置结构相同
            /*
            if (arr1.Count != arr2.Count) return false;
            for (int i = 0; i < arr1.Count; i++)
            {
                if (!AreJsonStructuresEqual(arr1[i], arr2[i]))
                    return false;
            }
            return true;
            */
        }
    }
}