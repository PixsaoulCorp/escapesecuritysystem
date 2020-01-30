// * Copyright [2017] [thibaud BOURDEAU]
// *
// * Licensed under the Apache License, Version 2.0 (the "License");
// * you may not use this file except in compliance with the License.
// * You may obtain a copy of the License at
// *
// *     http://www.apache.org/licenses/LICENSE-2.0
// *
// * Unless required by applicable law or agreed to in writing, software
// * distributed under the License is distributed on an "AS IS" BASIS,
// * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// * See the License for the specific language governing permissions and
// * limitations under the License.
using UnityEngine;

namespace Pixsaoul.Tools
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Delete children
        /// </summary>
        /// <param name="transform"></param>
        public static void DeleteChildren(this Transform trans)
        {
            if (trans != null)
            {
                int childs = trans.childCount;
                for (int i = childs - 1; i >= 0; i--)
                {
                    GameObject.Destroy(trans.GetChild(i).gameObject);
                }
            }
        }
    }
}